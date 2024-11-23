using LevendMonopoly.Api.Filters;
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
        private ITransactionService _transactionService;
        private IStartcodeService _startcodeService;

        public TeamController(ITeamService teamService, IBuildingService buildingService, IUserService userService, IChanceCardService chanceCardService, ITransactionService transactionService, IStartcodeService startcodeService)
        {
            _teamService = teamService;
            _buildingService = buildingService;
            _userService = userService;
            _chanceCardService = chanceCardService;
            _transactionService = transactionService;
            _startcodeService = startcodeService;
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

        [HttpPost("{teamId}/chance")]
        [Authorize(Policy = "UserOnly")]
        [TypeFilter(typeof(PauseFilter))]

        public async Task<ActionResult<ChanceCard>> Chance(Guid teamId)
        {
            var chanceCard = _chanceCardService.PullChanceCard();
            var team = await _teamService.GetTeamAsync(teamId);
            if (team == null) return NotFound();
            var lastpull = _chanceCardService.LastPull(teamId);
            var timeSinceLastPull = DateTime.Now.ToUniversalTime() - lastpull;
            if (timeSinceLastPull < TimeSpan.FromMinutes(3))
                return BadRequest("Je mag maar 1 keer per 3 minuten een kanskaart trekken.");

            await _chanceCardService.AddPull(teamId, DateTime.Now);
            team.Balance += chanceCard.Result;
            await _teamService.UpdateTeamAsync(team);

            _transactionService.AddTransaction(new Transaction
            {
                Amount = Math.Abs(chanceCard.Result),
                Sender = chanceCard.Result > 0 ? null : teamId,
                Receiver = chanceCard.Result > 0 ? teamId : null,
                DateTime = DateTime.UtcNow,
                Message = chanceCard.Prompt,
            });

            return Ok(chanceCard);
        }

        [HttpPost("startcode")]
        public ActionResult PullChanceCard(StartcodePullCommand command)
        {
            var teamId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (teamId == null) return Unauthorized();
            var id = Guid.Parse(teamId);
            if (!_startcodeService.PullStartcode(id, command.Code))
                return BadRequest("De startcode is onjuist.");
            return NoContent();
        }
    }

    public record StartcodePullCommand
    {
        public required string Code { get; init; }
    }

    public record CreateTeamCommand
    {
        public required string Name { get; init; }
        public required string Password { get; init; }
    }
}
