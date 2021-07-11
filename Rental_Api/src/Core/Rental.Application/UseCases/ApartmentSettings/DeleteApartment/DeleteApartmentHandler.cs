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

namespace Rental.Application.UseCases.ApartmentSettings.DeleteApartment
{
    public class DeleteApartmentHandler: IRequestHandler<DeleteApartmentCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;

        public DeleteApartmentHandler(IUserRepository userRepository, IApartmentRepository apartmentRepository)
        {
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
        }

        public async Task<Result> Handle(DeleteApartmentCommand request,
            CancellationToken cancellationToken)
            => await _apartmentRepository.GetById(request.Id)
                .Ensure(a => a != null && (a.User.UserName == request.UserName || request.Role == "ADMIN"), "apartment not found or you dont have permissions")
                .Bind(async a => await _apartmentRepository.Delete(a));

    }
}