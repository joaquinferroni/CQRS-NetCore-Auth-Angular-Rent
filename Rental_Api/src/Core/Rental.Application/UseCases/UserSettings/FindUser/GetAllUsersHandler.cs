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

namespace Rental.Application.UseCases.UserSettings.FindUser
{
    public class GetAllUserHandler: IRequestHandler<GetAllUsersQuery, Result<IEnumerable<User>, ResultError>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<IEnumerable<User>, ResultError>> Handle(GetAllUsersQuery request,
            CancellationToken cancellationToken)
            => _userRepository.GetAll()
                .MapError(e => new ResultError(e));
    }
}