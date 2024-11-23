using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Records;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Services
{
    public class StartcodeService : IStartcodeService
    {
        private readonly List<string> startCodes = new () {
            "HOI123",
            "DOEI456",
            "START01",
            "GEEFGELD7",
            "WINNER9"
        };

        private readonly DataContext _context;

        public StartcodeService(DataContext context)
        {
            _context = context;
        }

        public bool PullStartcode(Guid teamId, string code)
        {
            if (!startCodes.Contains(code))
            {
                return false;
            }
            if (_context.StartcodePull.Any(s => s.Startcode == code && s.TeamId == teamId))
            {
                return false;
            }
            _context.StartcodePull.Add(new StartcodePull { TeamId = teamId, Startcode = code });
            _context.SaveChanges();
            return true;
        }
    }
}
