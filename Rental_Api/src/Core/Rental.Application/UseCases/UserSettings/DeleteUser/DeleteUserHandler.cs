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
using Rental.Domain.Interfaces;
using Rental.Domain.Entities;

namespace Rental.Application.UseCases.UserSettings.DeleteUser
{
    public class DeleteUserHandler: IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
            => await _userRepository.GetByUserName(request.UserName)
                .Ensure(u => u != null, "There is no a user with that username")
                .Bind(async u => await _userRepository.Delete(u));


    }
}