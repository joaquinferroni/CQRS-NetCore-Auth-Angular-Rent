using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Rental.Domain.ResultMessages;
using MediatR;
using Rental.Domain.Interfaces;
using Rental.Domain.Entities;

namespace Rental.Application.UseCases.UserSettings.UpdateUser
{
    public class UpdateUserHandler: IRequestHandler<UpdateUserCommand, Result<User, ResultError>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRepository _UserRepository;

        public UpdateUserHandler(IUserRepository userRepository, IUserRepository UserRepository)
        {
            _userRepository = userRepository;
            _UserRepository = UserRepository;
        }

        public async Task<Result<User, ResultError>> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
            => await _UserRepository.GetById(request.Id)
                .Ensure(u => u != null && (u.Id == request.Id || request.Role_Updated_By == "ADMIN"), "User not found or you dont have permissions" )
                .Bind(u => UpdateUserModel(u.Id,request.Name,u.PasswordHash,u.PasswordSalt,request.UserName,request.Role))
                .Bind(async ap=> await _UserRepository.Update(ap))
                .MapError(e => new ResultError(e));

        private Result<User> UpdateUserModel(int id, string name, byte[] passwordHash, byte[] passwordSalt, string userName,string role)
            => User.Create(id, userName,name,passwordHash,passwordSalt,role );

        
    }
}