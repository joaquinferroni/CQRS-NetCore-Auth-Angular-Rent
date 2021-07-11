using System;
using Rental.Domain.Entities;
using Rental.Domain.Enums;
using Xunit;

namespace Rental.Domain.Test
{
    public class ApartmentShould
    {
        [InlineData(0, "", "", -1, 0, 0,0,99,181,ApartmentStatusEnum.AVAILABLE,0)]
        [InlineData(1, "test", "test", 0, 1, 1, 1, 10, 10, ApartmentStatusEnum.AVAILABLE, 0)]
        [InlineData(1, "test", "test", -1, 1, 1, 1, 10, 10, ApartmentStatusEnum.AVAILABLE, 1)]
        [InlineData(1, "test", "test", 1, 0, 1, 1, 10, 10, ApartmentStatusEnum.AVAILABLE, 1)]
        [InlineData(1, "test", "test", 1, 1, 0, 1, 10, 10, ApartmentStatusEnum.AVAILABLE, 1)]
        [InlineData(1, "test", "test", 1, 1, 1, 0, 10, 10, ApartmentStatusEnum.AVAILABLE, 1)]
        [InlineData(1, "test", "test", 1, 1, 1, 1, 190, 10, ApartmentStatusEnum.AVAILABLE, 1)]
        [InlineData(1, "test", "test", 1, 1, 1, 1, 0, 199, ApartmentStatusEnum.AVAILABLE, 1)]
        [InlineData(1, "test", "", 1, 1, 1, 1, 0, 0, ApartmentStatusEnum.AVAILABLE, 1)]
        [InlineData(1, "", "test", 1, 1, 1, 1, 0, 0, ApartmentStatusEnum.AVAILABLE, 1)]
        [Theory]
        public void Not_Be_Created_Because_Of_Not_Valid_Fields(int id, string name, string description, int floor, int size, decimal price, int rooms, double latitude, 
            double longitude, ApartmentStatusEnum status,  int userId)
        {
            var apartmentResult = Apartment.Create(id, name, description, floor, size, price, rooms, latitude, longitude, status, DateTime.Now, userId);
            Assert.True(apartmentResult.IsFailure);
            Assert.Contains("not valid", apartmentResult.Error);
        }

        [InlineData(0, "test", "test", 1, 1, 1, 1, 0, 100, ApartmentStatusEnum.AVAILABLE, 1)]
        [InlineData(1, "test", "test", 0, 1, 1, 1, 0, 100, ApartmentStatusEnum.RENTED, 1)]
        [Theory]
        public void Be_Created_Because_Of_Valid_Values(int id, string name, string description, int floor, int size, decimal price, int rooms, double latitude,
            double longitude, ApartmentStatusEnum status, int userId)
        {
            var apartmentResult = Apartment.Create(id, name, description, floor, size, price, rooms, latitude, longitude, status, DateTime.Now, userId);
            Assert.True(apartmentResult.IsSuccess);
            Assert.IsType<Apartment>(apartmentResult.Value);
        }
    }
}