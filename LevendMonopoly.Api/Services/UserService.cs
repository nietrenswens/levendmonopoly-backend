using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces;
using LevendMonopoly.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<User> GetUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public User? GetUser(Func<User, bool> predicate)
        {
            var user = _context.Users.Where(predicate).FirstOrDefault();
            return user;
        }

        public Task<List<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(User user, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}