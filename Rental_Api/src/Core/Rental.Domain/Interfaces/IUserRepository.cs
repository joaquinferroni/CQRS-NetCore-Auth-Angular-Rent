using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Rental.Domain.Entities;

namespace Rental.Domain.Interfaces
{
    public interface IUserRepository
    {
        Result<User> GetById(int id);
        Result<User> GetByUserName(string userName);
        Result<IEnumerable<User>> GetAll();
        Task<Result<User>> Create(User u);
        Task<Result<User>> Update(User u);
        Task<Result> Delete(User u);
    }
}