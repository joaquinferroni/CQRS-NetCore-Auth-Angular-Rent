using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Rental.Domain.Dtos;
using Rental.Domain.ResultMessages;

namespace Rental.Application.UseCases.UserSettings.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Not valid Name");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Not valid Password");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Not valid UserName");
            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(x => x.ToUpper() == "ADMIN"|| x.ToUpper() == "REALTOR" || x.ToUpper() == "USER")
                .WithMessage("Not valid Role");
        }
    }
}