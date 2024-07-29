using LevendMonopoly.Api.InputValidation;
using LevendMonopoly.Api.Interfaces;
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
        private readonly Interfaces.ILogger _logger;

        public TeamAuthController(ITeamService teamService, Interfaces.ILogger logger, ITeamSessionService sessionService)
        {
            _teamService = teamService;
            _logger = logger;
            _sessionService = sessionService;
        }

        public async Task<ActionResult<TeamSession>> Login(TeamPostBody teamBody)
        {
            if (!TeamValidation.IsValidTeam(teamBody))
            {
                return Unauthorized();
            }

            var potentialTeam = await _teamService.GetTeamByNameAsync(teamBody.Name);
            if (potentialTeam == null)
            {
                return Unauthorized();
            }

            var teambodySalt = Convert.FromBase64String(potentialTeam.PasswordSalt);
            var inputPassword = Cryptography.HashPassword(teamBody.Password, teambodySalt);

            if (inputPassword != potentialTeam.PasswordHash)
            {
                return Unauthorized();
            }

            var session = await _sessionService.CreateSessionAsync(potentialTeam);
            return Ok(session);
        }
    }

    public class TeamPostBody
    {
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}