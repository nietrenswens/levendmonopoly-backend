using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Records;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevendMonopoly.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController: ControllerBase
    {
        private ITeamService _teamService;
        private IBuildingService _buildingService;
        private IUserService _userService;
        private IChanceCardService _chanceCardService;

        public TeamController(ITeamService teamService, IBuildingService buildingService, IUserService userService, IChanceCardService chanceCardService)
        {
            _teamService = teamService;
            _buildingService = buildingService;
            _userService = userService;
            _chanceCardService = chanceCardService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams.OrderByDescending(t => t.Worth));
        }

        [HttpDelete("{teamId}")]
        [Authorize(Policy = "UserOnly")]
        public async Task<ActionResult> DeleteTeam(Guid teamId)
        {
            if (await _teamService.GetTeamAsync(teamId) == null)
                return NotFound();
            await _buildingService.ResetBuildingsState(teamId);
            await _teamService.DeleteTeamAsync(teamId);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Policy = "UserOnly")]
        public async Task<ActionResult> CreateTeam(CreateTeamCommand command)
        {
            if (await _teamService.GetTeamByNameAsync(command.Name) != null)
                return BadRequest("Er bestaat al een team met deze naam");
            if (_userService.GetUser(u => u.Name == command.Name) != null)
                return BadRequest("Er bestaat al een team met deze naam");
            await _teamService.CreateTeamAsync(Team.CreateNewTeam(command.Name, command.Password));
            return NoContent();
        }

        [HttpPost("chance")]
        [Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<ChanceCard>> Chance([FromBody] Guid TeamId)
        {
            var chanceCard = _chanceCardService.PullChanceCard();
            var teamId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
            var team = await _teamService.GetTeamAsync(teamId);
            if (team == null) return NotFound();

            var timeSinceLastPull = DateTime.Now - _chanceCardService.LastPull(teamId);
            if (timeSinceLastPull < TimeSpan.FromMinutes(3))
                return BadRequest("Je mag maar 1 keer per 3 minuten een kanskaart trekken.");

            team.Balance += chanceCard.Result;
            await _teamService.UpdateTeamAsync(team);
            return Ok(chanceCard);
        }
    }

    public record CreateTeamCommand
    {
        public required string Name { get; init; }
        public required string Password { get; init; }
    }
}
