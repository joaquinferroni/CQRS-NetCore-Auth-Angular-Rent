using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Rental.Domain.Entities;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.ApplicationContext;

namespace Rental.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
           _context = context;
        }

        public Result<User> GetById(int id)
        {
            
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
                _context.Entry(user).State = EntityState.Detached;
            return user;
        }

        public Result<User> GetByUserName(string userName) => _context.Users.AsNoTracking().FirstOrDefault(x => x.UserName == userName);

        public Result<IEnumerable<User>> GetAll() => _context.Users.AsNoTracking().ToList();

        public async Task<Result<User>> Create(User u)
        {
            await _context.Users.AddAsync(u);
            await _context.SaveChangesAsync();
            return u;
        }
        public async Task<Result<User>> Update(User u)
        {
            _context.Users.Update(u);
            await _context.SaveChangesAsync();
            return u;
        }
        public async Task<Result> Delete(User u)
        {
            _context.Users.Remove(u);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
