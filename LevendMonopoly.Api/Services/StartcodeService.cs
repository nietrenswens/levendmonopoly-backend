using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Records;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
        private readonly ITeamService _teamService;
        private readonly ITransactionService _transactionService;

        public StartcodeService(DataContext context, ITeamService teamservice, ITransactionService transactionService)
        {
            _context = context;
            _teamService = teamservice;
            _transactionService = transactionService;
        }

        public async Task<bool> PullStartcode(Guid teamId, string code)
        {
            var team = await _teamService.GetTeamAsync(teamId);
            if (team == null) return false;
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
            team.Balance += 400;

            await _teamService.UpdateTeamAsync(team);
            _transactionService.AddTransaction(new Transaction() {
                Amount = 400,
                Sender = null,
                Receiver = teamId,
                DateTime = DateTime.UtcNow,
                Message = $"Startcode {code} ingewisseld"
            });
            return true;
        }
    }
}
