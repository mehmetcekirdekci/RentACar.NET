using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants.Messages;
using System.Linq;
using Core.Utilities.Helpers;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileHelper _fileHelper;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }

        
        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfCountofCarImageCorrect(carImage.CarId));
            _carImageDal.Add(carImage);
            return new SuccessResult(CarImageMessages.CarImageAdded);
        }

        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(CarImageMessages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetImageByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(i => i.CarId == carId).ToList());
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult(CarImageMessages.CarImageUpdated);
        }

        private IResult CheckIfCountofCarImageCorrect(int carId)
        {
            var result = _carImageDal.GetAll(i => i.CarId == carId);

            if (result.Count >= 5)
            {
                return new ErrorResult(CarImageMessages.CountofCarImageError);
            }
            return new SuccessResult();
        }

    }
}
