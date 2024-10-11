using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IUserService
    {
        public Task<bool> CreateUserAsync(User user);
        public Task<bool> DeleteUserAsync(Guid id);
        public Task<bool> UpdateUserAsync(User user, Guid id);
        public Task<User> GetUserAsync(Guid id);
        public User? GetUser(Func<User, bool> predicate);
        public Task<List<User>> GetUsersAsync();

    }
}