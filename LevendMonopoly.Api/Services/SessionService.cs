using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces;
using LevendMonopoly.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Services
{
    public class SessionService : ISessionService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public SessionService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Session?> CreateSessionAsync(User user)
        {
            int sessionduration = _configuration.GetValue<int>("Sessions:ExpirationTimeInMinutes");
            if (sessionduration == 0)
            {
                return null;
            }
            Session session = new Session
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
                IsActive = true,
                ExpirationDate = DateTime.Now.AddMinutes(sessionduration)
            };
            await _context.Sessions.AddAsync(session);
            return await _context.SaveChangesAsync() > 0 ? session : null;
        }

        public async Task<bool> DisableSessionAsync(string token)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Token == token);
            if (session == null)
            {
                return false;
            }
            session.IsActive = false;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Session?> GetSessionAsync(string token)
        {
            return await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
        }
    }
}