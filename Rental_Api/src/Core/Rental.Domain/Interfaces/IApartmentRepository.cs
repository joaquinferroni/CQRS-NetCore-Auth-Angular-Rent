using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Rental.Domain.Dto;
using Rental.Domain.Entities;
using Rental.Domain.Enums;

namespace Rental.Domain.Interfaces
{
    public interface IApartmentRepository
    {
        Result<Apartment> GetById(int id);
        Result<IEnumerable<ApartmentDto>> GetByUserId(int userId, ApartmentStatusEnum? status);
        Result<IEnumerable<ApartmentDto>> GetAll(ApartmentStatusEnum? status);

        Result<IEnumerable<ApartmentDto>> GetByFilter(ApartmentStatusEnum? status, int? sizeFrom, int? sizeTo,
            int? priceFrom, int? priceTo, int? roomsFrom, int? roomsTo);
        Task<Result<Apartment>> Create(Apartment a);
        Task<Result<Apartment>> Update(Apartment a);
        Task<Result> Delete(Apartment a);
    }
}