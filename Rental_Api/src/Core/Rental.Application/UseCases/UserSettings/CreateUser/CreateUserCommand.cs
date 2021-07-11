using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using Rental.Domain.Dtos;
using Rental.Domain.ResultMessages;
using MediatR;
using Rental.Domain.Entities;

namespace Rental.Application.UseCases.UserSettings.CreateUser
{
    public class CreateUserCommand: IRequest<Result<User, ResultError>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
