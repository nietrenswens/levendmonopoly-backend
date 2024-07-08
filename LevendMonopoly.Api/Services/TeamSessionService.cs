using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces;
using LevendMonopoly.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Services
{
    public class TeamSessionService : ITeamSessionService
    {
        private readonly DataContext _context;

        public TeamSessionService(DataContext context)
        {
            _context = context;
        }

        public async Task<TeamSession?> CreateSessionAsync(Team team)
        {
            TeamSession session = new TeamSession()
            {
                Id = Guid.NewGuid(),
                Team = team,
                TeamId = team.Id,
                Token = Guid.NewGuid().ToString(),
            };
            await _context.TeamSessions.AddAsync(session);
            return await _context.SaveChangesAsync() > 0 ? session : null;
        }

        public async Task<bool> DisableSessionAsync(string token)
        {
            var session = await GetSessionAsync(token);
            if (session == null)
            {
                return false;
            }
            session.IsValid = false;
            return true;
        }

        public async Task<TeamSession?> GetSessionAsync(string token)
        {
            var session = await _context.TeamSessions.FirstOrDefaultAsync(session => session.Token == token);
            return session;
        }
    }
}
