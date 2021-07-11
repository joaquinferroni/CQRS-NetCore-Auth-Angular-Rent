using FluentValidation;
using Rental.Application.UseCases.UserSettings.CreateUser;

namespace Rental.Application.UseCases.ApartmentSettings.DeleteApartment
{
    public class DeleteApartmentValidator : AbstractValidator<DeleteApartmentCommand>
    {
        public DeleteApartmentValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Not valid Id");

        }
    }
}