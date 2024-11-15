using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Records;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Services
{
    public class ChanceCardService : IChanceCardService
    {
        private readonly List<ChanceCard> _chanceCards = [
            new ChanceCard { Prompt = "Je hebt Guido geholpen met teamleider worden. Je krijgt 300 euro.", Result = 300 },
            new ChanceCard { Prompt = "Je hebt een Roddel tegen Maxim verteld. Ze geeft je 200 euro.", Result = 200 },
            new ChanceCard { Prompt = "Je hebt Rens onderbroken tijdens zijn speluitleg. Je verliest 200 euro.", Result = -200 },
            new ChanceCard { Prompt = "Je hebt gewonnen met het wedspel. Je krijgt 800 euro.", Result = 800 },
            new ChanceCard { Prompt = "Je hebt geschept in de boter in plaats van schrapen. Je verliest 400 euro.", Result = -400 },
            new ChanceCard { Prompt = "Je hebt de meeste speculaaspoppen verkocht. Je ontvangt 400 euro.", Result = 400 },
            new ChanceCard { Prompt = "Je hebt corvee geskipt. Je verliest 1200 euro.", Result = -1200 },
            new ChanceCard { Prompt = "Je hebt het thema gewonnen op zomerkamp. Je ontvangt 1200 euro.", Result = 1200 },
            new ChanceCard { Prompt = "Je hebt gescholden (zonder appelkoek). Je verliest 400 euro.", Result = -400 },
            new ChanceCard { Prompt = "Je hebt Sjoerd geholpen met het opknappen van de Colemans. Je ontvangt 400 euro.", Result = 400 },
            ];

        private readonly DataContext _context;

        public ChanceCardService(DataContext context)
        {
            _context = context;
        }

        public ChanceCard PullChanceCard()
        {
            return _chanceCards[new Random().Next(0, _chanceCards.Count)];
        }

        public DateTime LastPull(Guid teamId)
        {
            var chancePulls = _context.ChanceCardPulls.Where(c => c.TeamId == teamId).ToList();
            return chancePulls.OrderByDescending(c => c.DateTime).FirstOrDefault()?.DateTime ?? DateTime.MinValue;
        }

        public async Task AddPull(Guid teamId, DateTime now)
        {
            await _context.ChanceCardPulls.AddAsync(new ChanceCardPull { TeamId = teamId, DateTime = now.ToUniversalTime() });
            await _context.SaveChangesAsync();
        }
    }
}
