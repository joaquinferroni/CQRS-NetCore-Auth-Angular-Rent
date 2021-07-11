using CSharpFunctionalExtensions;
using Rental.Domain.ResultMessages;
using MediatR;
using Rental.Domain.Entities;
using Rental.Domain.Enums;
using System.Text.Json.Serialization;

namespace Rental.Application.UseCases.ApartmentSettings.UpdateApartment
{
    public class UpdateApartmentCommand : IRequest<Result<Apartment, ResultError>>
    {
        public string UserName { get; set; }
        [JsonIgnore]
        public string Role { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Floor { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ApartmentStatusEnum Status { get; set; }
    }
}
