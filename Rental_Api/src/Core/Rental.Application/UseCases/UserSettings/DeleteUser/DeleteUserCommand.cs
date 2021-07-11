using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using Rental.Domain.Dtos;
using Rental.Domain.ResultMessages;
using MediatR;
using Rental.Domain.Entities;

namespace Rental.Application.UseCases.UserSettings.DeleteUser
{
    public class DeleteUserCommand: IRequest<Result>
    {
        public string UserName { get; set; }
    }
}
