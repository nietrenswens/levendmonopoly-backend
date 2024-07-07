using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Services
{
    public class Logger : ILogger
    {
        private readonly DataContext _context;

        public Logger(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> LogAsync(Log log)
        {
            await _context.Logs.AddAsync(log);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Log>> GetLogsAsync()
        {
            return await _context.Logs.ToListAsync();
        }

        public async Task<Log?> GetLogAsync(Guid id)
        {
            return await _context.Logs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteLogAsync(Guid id)
        {
            var log = await _context.Logs.FirstOrDefaultAsync(x => x.Id == id);
            if (log == null) return false;
            _context.Logs.Remove(log);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateLogAsync(Log log, Guid id)
        {
            var oldLog = await _context.Logs.FirstOrDefaultAsync(x => x.Id == id);
            if (oldLog == null) return false;
            oldLog.Message = log.Message;
            oldLog.Suspicious = log.Suspicious;
            oldLog.Details = log.Details;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}