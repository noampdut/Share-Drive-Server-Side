﻿using trempApplication.Properties.Models;

namespace trempApplication.Properties.Interfaces
{
    public interface ICar
    {
        Task<(bool IsSuccess, List<Car> Car, string ErrorMessage)> GetAllCars();
        Task<(bool IsSuccess,Car Car, string ErrorMessage)> GetCarById(Guid id);
        Task<(bool IsSuccess, string ErrorMessage)> AddCar(Car car);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateCar(Car car, Guid id);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteCar(Guid id);
        Task<(bool IsSuccess, List<Car> cars, string ErrorMessage)> GetCarsByOwner(Guid owner);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteCarsByOwner(Guid ownerId);
    }
}
