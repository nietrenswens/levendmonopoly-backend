using LevendMonopoly.Api.DTOs;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevendMonopoly.Api.Controllers
{
    [Authorize(Policy = "TeamOnly")]
    [Route("api/building")]
    [ApiController]
    public class BuildingController : Controller
    {
        private readonly IBuildingService _buildingService;
        private readonly ITeamService _teamService;
        private readonly IGameSettingService _gameSettingService;

        public BuildingController(IBuildingService buildingService, ITeamService teamService, IGameSettingService gameSettingService)
        {
            _buildingService = buildingService;
            _gameSettingService = gameSettingService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IEnumerable<TeamBuilding>> Get()
        {
            // Get user id
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
            var buildings = await _buildingService.GetBuildingsAsync(userId);
            var teamBuildings = new List<TeamBuilding>();
            foreach (var building in buildings)
            {
                teamBuildings.Add(new TeamBuilding {
                    Id = building.Id,
                    Name = building.Name,
                    Price = building.Price,
                    Tax = building.Tax,
                    Image = building.Image
                });
            }
            return teamBuildings;
        }

        [HttpGet("{buildingId}")]
        public async Task<ActionResult<TeamBuilding>> Get(string buildingId)
        {
            if(!Guid.TryParse(buildingId, out Guid id))
            {
                return NotFound();
            }

            var building = await _buildingService.GetBuildingAsync(id);
            if (building == null) return NotFound();

            return Ok(new TeamBuilding
            {
                Id = building.Id,
                Name = building.Name,
                Price = building.Price,
                Image = building.Image
            });
        }

        [HttpPost("buy")]
        public async Task<ActionResult<BuyResult>> Buy(BuyCommand command)
        {
            var teamId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
            var team = await _teamService.GetTeamAsync(teamId);

            var building = await _buildingService.GetBuildingAsync(command.BuildingId);
            if (building == null) return NotFound();
            if (team == null) return NotFound();

            if (team.Balance < building.Price || (command.Tax && team.Balance < building.Price * (1 + _gameSettingService.GetGameSettings().TaxRate)))
            {

                return Ok(new BuyResult()
                {
                    Success = false,
                    Reason = "Je hebt niet genoeg geld om het gebouw te komen!"
                });
            }

            if (building.OwnerId == team.Id)
            {
                return Ok(new BuyResult()
                {
                    Success = false,
                    Reason = "Jij hebt dit gebouw al in bezit!"
                });
            }

            if (building.OwnerId != null)
            {
                var otherTeam = await _teamService.GetTeamAsync((Guid)building.OwnerId);
                if (otherTeam == null)
                    return await buy(building, team, command);

                otherTeam.Balance += building.Price;
                team.Balance -= building.Price;
                await _teamService.UpdateTeamAsync(otherTeam);
                await _teamService.UpdateTeamAsync(team);
                return Ok(new BuyResult()
                {
                    Success = false,
                    Reason = $"Helaas, dit gebouw is al gekocht door {otherTeam.Name}. Je hebt hen {building.Price} betaald."
                });
            }

            return await buy(building, team, command);
        }

        private async Task<ActionResult<BuyResult>> buy(Building building, Team team, BuyCommand command)
        {
            building.OwnerId = team.Id;
            building.Tax = command.Tax;
            var cost = building.Price;
            if (command.Tax)
                cost += (int)(building.Price * (1 + _gameSettingService.GetGameSettings().TaxRate));
            team.Balance -= cost;

            await _buildingService.UpdateBuildingAsync(building);
            await _teamService.UpdateTeamAsync(team);

            return Ok(new BuyResult()
            {
                Success = true,
                Reason = $"Je hebt {building.Name} gekocht voor {cost} euro!"
            });
        }
    }

    public record BuyCommand
    {
        public required Guid BuildingId { get; init; }
        public required bool Tax { get; init; }
    }

    public record BuyResult
    {
        public required bool Success { get; init; }
        public required string Reason { get; init; }
    }
}
