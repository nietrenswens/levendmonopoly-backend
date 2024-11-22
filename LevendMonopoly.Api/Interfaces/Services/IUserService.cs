using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Utils;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IUserService
    {
        public Task<Result> CreateUserIfNotExistsAsync(string name, string password, Guid roleId);
        public Task<bool> DeleteUserAsync(Guid id);
        public Task UpdateUserAsync(User user);
        public Task<User?> GetUserAsync(Guid id);
        public User? GetUser(Func<User, bool> predicate);
        public Task<List<User>> GetUsersAsync();

    }
}