using CSharpFunctionalExtensions;
using Rental.Domain.ResultMessages;
using MediatR;
using Rental.Domain.Entities;
using Rental.Domain.Enums;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Rental.Application.UseCases.ApartmentSettings.DeleteApartment
{
    public class DeleteApartmentCommand : IRequest<Result>
    {
        public int Id { get; set; }
        [JsonIgnore]
        [BindNever]
        public string Role { get; set; }
        [JsonIgnore]
        [BindNever]
        public string UserName { get; set; }
    }
}
