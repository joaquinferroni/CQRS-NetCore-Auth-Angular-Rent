using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using Rental.Domain.Dtos;
using Rental.Domain.ResultMessages;
using MediatR;
using Rental.Domain.Entities;
using Rental.Domain.Enums;
using System.Text.Json.Serialization;
using Rental.Domain.Dto;

namespace Rental.Application.UseCases.ApartmentSettings.FindUserApartment
{
    public class GetUserApartmentQuery : IRequest<Result<IEnumerable<ApartmentDto>, ResultError>>
    {
        [JsonIgnore]
        public string UserName { get; set; }
        public ApartmentStatusEnum? Status { get; set; }
    }
}
