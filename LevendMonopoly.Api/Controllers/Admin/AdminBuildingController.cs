using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevendMonopoly.Api.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/building")]
    [Authorize(Policy = "UserOnly")]
    public class AdminBuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public AdminBuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            return Ok(await _buildingService.GetBuildings());
        }

        [HttpPost]
        public async Task<ActionResult> PostBuilding(Building building)
        {
            await _buildingService.CreateBuilding(building);
            return Ok();
        }
    }
}
