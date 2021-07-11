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

namespace Rental.Application.UseCases.ApartmentSettings.FindUserApartment
{
    public class GetUserApartmentHandler: IRequestHandler<GetUserApartmentQuery, Result<IEnumerable<ApartmentDto>, ResultError>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;

        public GetUserApartmentHandler(IUserRepository userRepository, IApartmentRepository apartmentRepository)
        {
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
        }

        public async Task<Result<IEnumerable<ApartmentDto>, ResultError>> Handle(GetUserApartmentQuery request,
            CancellationToken cancellationToken)
            => _userRepository.GetByUserName(request.UserName)
                .Ensure(u => u != null, "user not found")
                .Bind(u => _apartmentRepository.GetByUserId(u.Id,request.Status))
                .MapError(e => new ResultError(e));
    }
}