using LevendMonopoly.Api.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LevendMonopoly.Api.Controllers.Admin
{
    [ApiController]
    [Authorize(Policy = "UserOnly")]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IBuildingService _buildingService;
        private readonly ITeamService _teamService;
        private readonly IChanceCardService _chanceCardService;
        private readonly ITransactionService _transactionService;

        public AdminController(IBuildingService buildingService, ITeamService teamService, IChanceCardService chanceCardService, ITransactionService transactionService)
        {
            _buildingService = buildingService;
            _teamService = teamService;
            _chanceCardService = chanceCardService;
            _transactionService = transactionService;
        }

        [HttpPost("resetgame")]
        public async Task<ActionResult> ResetGame()
        {
            await _buildingService.ResetAllBuildings();
            await _teamService.ResetAllTeams();
            _chanceCardService.ResetPulls();
            _transactionService.ResetTransactions();

            return NoContent();
        }
    }
}
