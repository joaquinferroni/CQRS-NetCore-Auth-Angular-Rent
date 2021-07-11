using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Rental.Domain.ResultMessages;
using MediatR;
using Rental.Domain.Entities;
using Rental.Domain.Enums;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Rental.Application.UseCases.UserSettings.FindUser
{
    public class GetAllUsersQuery : IRequest<Result<IEnumerable<User>, ResultError>>
    {
    }
}
