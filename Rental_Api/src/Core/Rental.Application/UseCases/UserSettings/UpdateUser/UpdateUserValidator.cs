using FluentValidation;
using Rental.Application.UseCases.UserSettings.CreateUser;

namespace Rental.Application.UseCases.UserSettings.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Not valid Id");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Not valid Name");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Not valid UserName");
            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(x => x.ToUpper() == "ADMIN" || x.ToUpper() == "REALTOR" || x.ToUpper() == "USER")
                .WithMessage("Not valid Role");

        }
    }
}