using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using Rental.Domain.Dtos;
using Rental.Domain.ResultMessages;
using MediatR;

namespace Rental.Application.UseCases.AuthSettings.Authenticate
{
    public class AuthenticateCommand: IRequest<Result<AuthModelDto, ResultError>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
