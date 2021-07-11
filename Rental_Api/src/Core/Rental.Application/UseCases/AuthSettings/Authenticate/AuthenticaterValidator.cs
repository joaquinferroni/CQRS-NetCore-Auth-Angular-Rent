using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Rental.Application.UseCases.AuthSettings.Authenticate;
using Rental.Domain.Dtos;
using Rental.Domain.ResultMessages;

namespace Rental.Application.UseCases.AuthSettings.Authenticate
{
    public class AuthenticateValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Not valid Username");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Not valid Password");
        }
    }
}