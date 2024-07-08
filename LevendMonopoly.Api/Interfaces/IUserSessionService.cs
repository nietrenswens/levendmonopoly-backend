using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces
{
    public interface IUserSessionService
    {
        Task<Session?> CreateSessionAsync(User user);
        Task<Session?> GetSessionAsync(string token);
        Task<bool> DisableSessionAsync(string token);
    }
}