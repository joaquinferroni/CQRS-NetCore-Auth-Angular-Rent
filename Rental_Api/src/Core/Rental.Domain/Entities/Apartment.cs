using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using Rental.Domain.Enums;

namespace Rental.Domain.Entities
{
    public class Apartment
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Floor { get; private set; }
        public int Size { get; private set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; private set; }
        public int Rooms { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public DateTime Created_At { get; private set; }
        public ApartmentStatusEnum Status { get; private set; }
        public int UserID { get; private set; }
        public virtual User User { get; private set; }

        private Apartment()
        {
        }

        private Apartment(int id, string name, string description, int floor, int size, decimal price, int rooms, double latitude, double longitude, ApartmentStatusEnum status, DateTime createdAt, int userId)
        {
            Id = id;
            Name = name;
            Description = description;
            Floor = floor;
            Size = size;
            Price = price;
            Rooms = rooms;
            Latitude = latitude;
            Longitude = longitude;
            Created_At = createdAt;
            Status = status;
            UserID = userId;
        }

        public static Result<Apartment> Create(int id, string name, string description, int floor, int size, decimal price, int rooms, double latitude, double longitude, ApartmentStatusEnum status, DateTime createdAt, int userId)
            => ValidateFields(name, description, floor, size, price, rooms, latitude, longitude, createdAt, userId)
                .Map(() => new Apartment(id, name, description, floor, size, price, rooms, latitude, longitude, status, createdAt, userId));

        private static Result ValidateFields(string name, string description, int floor, int size, decimal price, int rooms, double latitude, double longitude, DateTime createdAt,int userId)
            => Result.FailureIf(string.IsNullOrEmpty(description) , "Description not valid")
                .Bind(() => Result.FailureIf(string.IsNullOrWhiteSpace(name), "Name not valid"))
                .Bind(() => Result.FailureIf(floor < 0, "Floor not valid"))
                .Bind(() => Result.FailureIf(size <=0, "Size not valid"))
                .Bind(() => Result.FailureIf(price <= 0, "Price not valid"))
                .Bind(() => Result.FailureIf(rooms <= 0, "Rooms not valid"))
                .Bind(() => Result.FailureIf(latitude < -90 || latitude > 90, "Lat not valid"))
                .Bind(() => Result.FailureIf(longitude < -180 || longitude > 180, "Long not valid"))
                .Bind(() => Result.FailureIf(userId <= 0, "User not valid"))
                .Bind(() => Result.FailureIf(createdAt > DateTime.Now, "Creation date not valid"));
    }
}
