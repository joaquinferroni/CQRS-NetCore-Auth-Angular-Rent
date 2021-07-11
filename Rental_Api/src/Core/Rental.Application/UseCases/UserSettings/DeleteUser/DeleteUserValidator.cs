using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Rental.Domain.Dtos;
using Rental.Domain.ResultMessages;

namespace Rental.Application.UseCases.UserSettings.DeleteUser
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Not valid UserName");
        }
    }
}