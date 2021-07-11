using FluentValidation;
using Rental.Application.UseCases.UserSettings.CreateUser;

namespace Rental.Application.UseCases.ApartmentSettings.UpdateApartment
{
    public class UpdateApartmentValidator : AbstractValidator<UpdateApartmentCommand>
    {
        public UpdateApartmentValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Not valid Id");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Not valid Name");
            RuleFor(x => x.Rooms).GreaterThan(0).WithMessage("Not valid Rooms");
            RuleFor(x => x.Floor).GreaterThan(-1).WithMessage("Not valid Floor");
            RuleFor(x => x.Latitude).InclusiveBetween(-90,90).WithMessage("Not valid Lat");
            RuleFor(x => x.Longitude).InclusiveBetween(-180,180).WithMessage("Not valid Long");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Not valid Price");
            RuleFor(x => x.Size).GreaterThan(0).WithMessage("Not valid Size");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Not valid Description");
            RuleFor(x => x.Status == Domain.Enums.ApartmentStatusEnum.AVAILABLE || x.Status == Domain.Enums.ApartmentStatusEnum.RENTED);

        }
    }
}