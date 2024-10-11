using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface ILogger
    {
        Task<bool> LogAsync(Log log);
        Task<List<Log>> GetLogsAsync();
        Task<Log?> GetLogAsync(Guid id);
        Task<bool> DeleteLogAsync(Guid id);
        Task<bool> UpdateLogAsync(Log log, Guid id);
    }
}