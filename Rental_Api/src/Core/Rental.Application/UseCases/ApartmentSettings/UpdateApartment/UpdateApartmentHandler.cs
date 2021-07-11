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

namespace Rental.Application.UseCases.ApartmentSettings.UpdateApartment
{
    public class UpdateApartmentHandler: IRequestHandler<UpdateApartmentCommand, Result<Apartment, ResultError>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;

        public UpdateApartmentHandler(IUserRepository userRepository, IApartmentRepository apartmentRepository)
        {
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
        }

        public async Task<Result<Apartment, ResultError>> Handle(UpdateApartmentCommand request,
            CancellationToken cancellationToken)
            => await _apartmentRepository.GetById(request.Id)
                .Ensure(a => a != null && (a.User.UserName == request.UserName || request.Role == "ADMIN"), "apartment not found or you dont have permissions" )
                .Bind(a => UpdateApartmentModel(a.Id,request.Name,request.Description,request.Floor,request.Size,request.Price,
                    request.Rooms,request.Latitude,request.Longitude,request.Status,a.Created_At,a.UserID))
                .Bind(async ap=> await _apartmentRepository.Update(ap))
                .MapError(e => new ResultError(e));

        private Result<Apartment> UpdateApartmentModel(int id,string name, string description, int floor, int size,
            decimal price, int rooms, double latitude, double longitude, ApartmentStatusEnum status, DateTime createdAt,
            int userId)
            => Apartment.Create(id, name, description, floor, size,
                price, rooms, latitude, longitude, status, createdAt,
                userId);
    }
}