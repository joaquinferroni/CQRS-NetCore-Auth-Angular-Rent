using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Rental.Domain.Dtos;
using Rental.Domain.ResultMessages;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Rental.Application.UseCases.UserSettings.CreateUser;
using Rental.Domain.Interfaces;
using Rental.Domain.Entities;
using Rental.Domain.Enums;
using Rental.Application.UseCases.ApartmentSettings.FindUserApartment;
using System.Collections.Generic;
using Rental.Domain.Dto;

namespace Rental.Application.UseCases.ApartmentSettings.FindApartment
{
    public class GetAllApartmentHandler: IRequestHandler<GetAllApartmentQuery, Result<IEnumerable<ApartmentDto>, ResultError>>
    {
        private readonly IApartmentRepository _apartmentRepository;

        public GetAllApartmentHandler(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<Result<IEnumerable<ApartmentDto>, ResultError>> Handle(GetAllApartmentQuery request,
            CancellationToken cancellationToken)
            => _apartmentRepository.GetByFilter(request.Status,request.SizeFrom, request.SizeTo, request.PriceFrom, request.PriceTo
                , request.RoomsFrom,request.RoomsTo)
                .MapError(e => new ResultError(e));
    }
}