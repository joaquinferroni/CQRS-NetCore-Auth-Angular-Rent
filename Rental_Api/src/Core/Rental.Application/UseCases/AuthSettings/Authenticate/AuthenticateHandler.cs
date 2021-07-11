using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Rental.Domain.Dtos;
using Rental.Domain.Infrastructure;
using Rental.Domain.ResultMessages;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rental.Domain.Interfaces;

namespace Rental.Application.UseCases.AuthSettings.Authenticate
{
    public class AuthenticateHandler: IRequestHandler<AuthenticateCommand, Result<AuthModelDto, ResultError>>
    {
        private readonly AuthOptions _authConfig;
        private readonly IUserRepository _userRepository;

        public AuthenticateHandler(IOptions<AuthOptions> authConfig, IUserRepository userRepository)
        {
            _authConfig = authConfig.Value;
            _userRepository = userRepository;
        }

        public async Task<Result<AuthModelDto, ResultError>> Handle(AuthenticateCommand request,
            CancellationToken cancellationToken)
            =>  _userRepository.GetByUserName(request.UserName)
                .Ensure(u => u != null && VerifyPasswordHash(request.Password, u.PasswordHash, u.PasswordSalt), "Not Valid username or password")
                .Bind(u => Result.Success(new AuthModelDto()
                {
                    Token = CreateToken(u.UserName, u.Role)
                }))
                .MapError(e => new ResultError(e));
               

        private string CreateToken(string username, string role)
        {
            var key = Encoding.ASCII.GetBytes(_authConfig.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(_authConfig.ExpireHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}