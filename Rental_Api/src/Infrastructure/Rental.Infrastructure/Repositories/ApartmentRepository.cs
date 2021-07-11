using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Rental.Domain.Entities;
using Rental.Domain.Enums;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using Rental.Domain.Dto;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Rental.Infrastructure.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public ApartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Result<Apartment> GetById(int id)
        {

            var apartment = _context.Apartments
                .Include(a => a.User)
                .FirstOrDefault(a => a.Id == id);

            if (apartment != null)
                _context.Entry(apartment).State = EntityState.Detached;
            return apartment;
        }

        public Result<IEnumerable<ApartmentDto>> GetByUserId(int userId, ApartmentStatusEnum? status)
            => _context.Apartments
                .Include(a => a.User)
                .Where(x =>
                    x.UserID == userId
                    && (status == null || x.Status == status)
                )
                .Select(a => new ApartmentDto()
                {
                    Id = a.Id,
                    UserName = a.User.UserName,
                    Created_At = a.Created_At,
                    Description = a.Description,
                    Floor = a.Floor,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    Name = a.Name,
                    Price = a.Price,
                    Rooms = a.Rooms,
                    Size = a.Size,
                    Status = a.Status
                })
                .OrderByDescending(a => a.Created_At)
                .ToList();

        public Result<IEnumerable<ApartmentDto>> GetAll(ApartmentStatusEnum? status)
            => _context.Apartments
                .Include(a => a.User)
                .Where(x =>
                    (status == null || x.Status == status)
                )
                .Select(a => new ApartmentDto()
                {
                    Id = a.Id,
                    UserName = a.User.UserName,
                    Created_At = a.Created_At,
                    Description = a.Description,
                    Floor = a.Floor,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    Name = a.Name,
                    Price = a.Price,
                    Rooms = a.Rooms,
                    Size = a.Size,
                    Status = a.Status
                })
                .OrderByDescending(a => a.Created_At)
                .ToList();

        public Result<IEnumerable<ApartmentDto>> GetByFilter(ApartmentStatusEnum? status, int? sizeFrom, int? sizeTo,
        int? priceFrom, int? priceTo, int? roomsFrom, int? roomsTo)
            => _context.Apartments
                .Include(a => a.User)
                .Where(x =>
                    (status == null || x.Status == status)
                    && (sizeFrom == null || x.Size >= sizeFrom)
                    && (sizeTo == null || x.Size <= sizeTo)
                    && (priceFrom == null || x.Price >= priceFrom)
                    && (priceTo == null || x.Price <= priceTo)
                    && (roomsFrom == null || x.Rooms >= roomsFrom)
                    && (roomsTo == null || x.Rooms <= roomsTo)
                ).Select(a => new ApartmentDto()
                {
                    Id = a.Id,
                    UserName = a.User.UserName,
                    Created_At = a.Created_At,
                    Description = a.Description,
                    Floor = a.Floor,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    Name = a.Name,
                    Price = a.Price,
                    Rooms = a.Rooms,
                    Size = a.Size,
                    Status = a.Status
                })
                .OrderByDescending(a => a.Created_At)
                .ToList();



        public async Task<Result<Apartment>> Create(Apartment a)
        {
            await _context.Apartments.AddAsync(a);
            await _context.SaveChangesAsync();
            return a;
        }

        public async Task<Result<Apartment>> Update(Apartment a)
        {
            _context.Apartments.Update(a);
            await _context.SaveChangesAsync();
            return a;
        }

        public async Task<Result> Delete(Apartment a)
        {
            _context.Apartments.Remove(a);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}