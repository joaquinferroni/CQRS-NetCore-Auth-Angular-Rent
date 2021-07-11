using CSharpFunctionalExtensions;
using Rental.Domain.ResultMessages;
using MediatR;
using Rental.Domain.Entities;
using Rental.Domain.Enums;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Rental.Application.UseCases.UserSettings.UpdateUser
{
    public class UpdateUserCommand : IRequest<Result<User, ResultError>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public string Role_Updated_By { get; set; }
        [JsonIgnore]
        public string UserName_Updated_By { get; set; }
    }
}
