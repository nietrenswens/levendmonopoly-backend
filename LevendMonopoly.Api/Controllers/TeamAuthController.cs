using LevendMonopoly.Api.Interfaces;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Records;
using Microsoft.AspNetCore.Mvc;

namespace LevendMonopoly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamAuthController : ControllerBase
    {
        private readonly ITeamSessionService _sessionService;
        private readonly ITeamService _service;

        public TeamAuthController(ITeamSessionService sessionService, ITeamService service)
        {
            _sessionService = sessionService;
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<TeamSession>> Login(UserLoginBody userLoginBody)
        {
            return Ok();
        }
    }
}
