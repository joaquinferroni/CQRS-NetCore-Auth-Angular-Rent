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

namespace Rental.Application.UseCases.UserSettings.CreateUser
{
    public class CreateUserHandler: IRequestHandler<CreateUserCommand, Result<User, ResultError>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User, ResultError>> Handle(CreateUserCommand request,
            CancellationToken cancellationToken)
            => await  _userRepository.GetByUserName(request.UserName)
                .Ensure(u => u == null, "There is a user with that username")
                .Bind(_ => CreatePasswordHash(request.Password))
                .Bind(pass => User.Create(0, request.UserName, request.Name, pass.passwordHash,pass.passwordSalt, request.Role))
                .Bind(async u => await _userRepository.Create(u))
                .MapError(e => new ResultError(e));


        private Result<(byte[] passwordHash, byte[] passwordSalt)> CreatePasswordHash(string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            if (string.IsNullOrWhiteSpace(password)) 
                return Result.Failure<(byte[] passwordHash, byte[] passwordSalt)>("Password cannot be empty or whitespace only string.");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return (passwordHash, passwordSalt);
        }
    }
}