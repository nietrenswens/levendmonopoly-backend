using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IUserSessionService
    {
        Task<Session?> CreateSessionAsync(User user);
        Task<Session?> GetSessionAsync(string token);
        Task<bool> DisableSessionAsync(string token);
        Task<IEnumerable<Session>> GetSessionsAsync(Guid userId);
        Task UpdateSessionsAsync(IEnumerable<Session> sessions);
    }
}