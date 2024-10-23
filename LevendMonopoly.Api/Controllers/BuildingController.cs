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

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet]
        public async Task<IEnumerable<Building>> Get()
        {
            // Get user id
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
            return await _buildingService.GetBuildings(userId);
        }
    }
}
