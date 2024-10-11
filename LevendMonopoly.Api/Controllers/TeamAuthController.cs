using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LevendMonopoly.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamAuthController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ITeamSessionService _sessionService;
        private readonly Interfaces.Services.ILogger _logger;

        public TeamAuthController(ITeamService teamService, Interfaces.Services.ILogger logger, ITeamSessionService sessionService)
        {
            _teamService = teamService;
            _logger = logger;
            _sessionService = sessionService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TeamSession>> Login(TeamLoginCommand command)
        { 
            var potentialTeam = await _teamService.GetTeamByNameAsync(command.Name);
            if (potentialTeam == null)
            {
                return Unauthorized();
            }

            var teambodySalt = Convert.FromBase64String(potentialTeam.PasswordSalt);
            var inputPassword = Cryptography.HashPassword(command.Password, teambodySalt);

            if (inputPassword != potentialTeam.PasswordHash)
            {
                return Unauthorized();
            }

            var session = await _sessionService.CreateSessionAsync(potentialTeam);
            return Ok(session);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout(TeamLogoutCommand command)
        {
            var session = await _sessionService.GetSessionAsync(command.Token);
            if (session == null) return BadRequest();
            await _sessionService.DisableSessionAsync(command.Token);
            return NoContent();
        }
    }

   

    public record TeamLoginCommand
    {
        public required string Name { get; init; }
        public required string Password { get; init; }
    }

    public class TeamLogoutCommand
    {
        public required string Token { get; init; }
    }
}