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

        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(Guid id)
        {
            return Ok(await _buildingService.GetBuilding(id));
        }

        [HttpPost]
        public async Task<ActionResult> PostBuilding(Building building)
        {
            await _buildingService.CreateBuilding(building);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBuilding(DeleteCommand command)
        {
            if (!(await _buildingService.GetBuildings()).Any(b => b.Id == command.Id))
                return NotFound();
            await _buildingService.DeleteBuilding(command.Id);
            return Ok();
        }

    }

    public record DeleteCommand
    {
        public required Guid Id { get; init; }
    }
}
