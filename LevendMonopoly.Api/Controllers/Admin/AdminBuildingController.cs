﻿using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Migrations;
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
        private readonly ITeamService _teamService;

        public AdminBuildingController(IBuildingService buildingService, ITeamService teamService)
        {
            _buildingService = buildingService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            return Ok(await _buildingService.GetBuildingsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(Guid id)
        {
            return Ok(await _buildingService.GetBuildingAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> PostBuilding(Building building)
        {
            await _buildingService.CreateBuildingAsync(building);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBuilding(DeleteCommand command)
        {
            if (!(await _buildingService.GetBuildingsAsync()).Any(b => b.Id == command.Id))
                return NotFound();
            await _buildingService.DeleteBuildingAsync(command.Id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> PutBuilding(BuildingUpdateCommand command)
        {
            var existingBuilding = await _buildingService.GetBuildingAsync(command.Id);
            if (existingBuilding == null)
            {
                return NotFound();
            }

            existingBuilding.Name = command.Name;
            existingBuilding.Price = command.Price;
            existingBuilding.Image = command.Image;

            await _buildingService.UpdateBuildingAsync(existingBuilding);
            return Ok();
        }

        [HttpPost("confiscate")]
        public async Task<ActionResult> Confiscate(ConfiscateCommand command)
        {
            var existingBuilding = await _buildingService.GetBuildingAsync(command.Id);
            if (existingBuilding == null || existingBuilding.OwnerId == null)
            {
                return NotFound();
            }
            var team = await _teamService.GetTeamAsync((Guid)existingBuilding.OwnerId);
            if (team == null)
                return NotFound();

            if (existingBuilding.Tax)
                return BadRequest();

            existingBuilding.OwnerId = null;
            existingBuilding.Tax = false;
            await _buildingService.UpdateBuildingAsync(existingBuilding);
            return Ok();
        }
    }

    public record DeleteCommand
    {
        public required Guid Id { get; init; }
    }

    public record ConfiscateCommand
    {
        public required Guid Id { get; init; }
    }

    public record BuildingUpdateCommand
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required int Price { get; init; }
        public string? Image { get; init; }
    }
}
