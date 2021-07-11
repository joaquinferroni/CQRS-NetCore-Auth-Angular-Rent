using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Rental.Domain.ResultMessages;
using MediatR;
using Rental.Domain.Entities;
using Rental.Domain.Enums;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rental.Domain.Dto;

namespace Rental.Application.UseCases.ApartmentSettings.FindApartment
{
    public class GetAllApartmentQuery : IRequest<Result<IEnumerable<ApartmentDto>, ResultError>>
    {
        public int? SizeFrom { get; set; }
        public int? SizeTo { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public int? RoomsFrom { get; set; }
        public int? RoomsTo { get; set; }
        [BindNever] 
        public ApartmentStatusEnum? Status { get; set; } = ApartmentStatusEnum.AVAILABLE;
    }
}
