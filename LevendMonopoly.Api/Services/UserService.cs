using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Utils;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;
        private ITeamService _teamService;

        public UserService(DataContext context, ITeamService teamService)
        {
            _context = context;
            _teamService = teamService;
        }

        public async Task<Result> CreateUserIfNotExistsAsync(string name, string password, Guid roleId)
        {
            if (await _teamService.GetTeamByNameAsync(name) != null || GetUser(u => u.Name == name) != null)
                return Result.Failure("Gebruiker bestaat al");
            await _context.Users.AddAsync(User.CreateNewUser(name, password, roleId));
            return Result.Success();
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

        public Task<User?> GetUserAsync(Guid id)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public User? GetUser(Func<User, bool> predicate)
        {
            var user = _context.Users.Include(u => u.Role).Where(predicate).FirstOrDefault();
            return user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}