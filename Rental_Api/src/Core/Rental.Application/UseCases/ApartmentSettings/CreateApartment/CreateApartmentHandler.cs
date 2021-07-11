using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Rental.Domain.Dtos;
using Rental.Domain.ResultMessages;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Rental.Application.UseCases.UserSettings.CreateUser;
using Rental.Domain.Interfaces;
using Rental.Domain.Entities;
using Rental.Domain.Enums;

namespace Rental.Application.UseCases.ApartmentSettings.CreateApartment
{
    public class CreateApartmentHandler: IRequestHandler<CreateApartmentCommand, Result<Apartment, ResultError>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;

        public CreateApartmentHandler(IUserRepository userRepository, IApartmentRepository apartmentRepository)
        {
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
        }

        public async Task<Result<Apartment, ResultError>> Handle(CreateApartmentCommand request,
            CancellationToken cancellationToken)
            => await _userRepository.GetByUserName(request.UserName)
                .Ensure(u => u != null, "user not found")
                .Bind(u => CreateApartmentModel(request.Name,request.Description,request.Floor,request.Size,request.Price,
                    request.Rooms,request.Latitude,request.Longitude,request.Status,DateTime.Now, u.Id))
                .Bind(async ap=> await _apartmentRepository.Create(ap))
                .MapError(e => new ResultError(e));

        private Result<Apartment> CreateApartmentModel(string name, string description, int floor, int size,
            decimal price, int rooms, double latitude, double longitude, ApartmentStatusEnum status, DateTime createdAt,
            int userId)
            => Apartment.Create(0, name, description, floor, size,
                price, rooms, latitude, longitude, status, createdAt,
                userId);
    }
}