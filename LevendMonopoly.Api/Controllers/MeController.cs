using LevendMonopoly.Api.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevendMonopoly.Api.Controllers
{
    [Authorize(Policy = "TeamOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class MeController : Controller
    {
        private readonly IBuildingService _buildingService;
        private readonly ITeamService _teamService;

        public MeController(IBuildingService buildingService, ITeamService teamService)
        {
            _buildingService = buildingService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<MeResult>> Get()
        {
            // Get user id
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
            var Team = await _teamService.GetTeamAsync(userId);
            if (Team == null)
            {
                return NotFound();
            }
            var buildings = (await _buildingService.GetBuildings(userId));
            int worth = buildings.Sum(building => building.Price) + Team.Balance;
            return Ok(new MeResult()
            {
                Name = Team.Name,
                Balance = Team.Balance,
                Worth = worth,
                NumberOfBuildings = buildings.Count(),
                Position = 1
            });
        }
    }

    public record MeResult
    {
        public required string Name { get; init; }
        public required int Balance { get; init; }
        public required int Worth { get; init; }
        public required int NumberOfBuildings { get; init; }
        public required int Position { get; init; }
    }
}