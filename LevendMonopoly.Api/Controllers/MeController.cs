﻿using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevendMonopoly.Api.Controllers
{
    [Authorize(Policy = "TeamOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class MeController : Controller
    {
        private readonly IBuildingService _buildingService;
        private readonly ITeamService _teamService;

        public MeController(IBuildingService buildingService, ITeamService teamService)
        {
            _buildingService = buildingService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<MeResult>> Get()
        {
            // Get user id
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
            var team = await _teamService.GetTeamAsync(userId);
            var teams = (await _teamService.GetAllTeamsAsync()).OrderByDescending(t => t.Worth);
            if (team == null)
            {
                return NotFound();
            }
            var buildings = (await _buildingService.GetBuildingsAsync(userId));

            int position = 1;
            for (int i = 0; i < teams.Count(); i++)
            {
                if (teams.ElementAt(i).Id == team.Id)
                {
                    position = i + 1;
                    break;
                }
            }
            int worth = buildings.Sum(building => building.Price) + team.Balance;
            return Ok(new MeResult()
            {
                Id = team.Id,
                Name = team.Name,
                Balance = team.Balance,
                Worth = worth,
                NumberOfBuildings = buildings.Count(),
                NumberOfBuildingsWithoutTax = buildings.Where(b => !b.Tax).Count(),
                Position = position
            });
        }
    }

    public record MeResult
    {
        public required string Name { get; init; }
        public required Guid Id { get; init; }
        public required int Balance { get; init; }
        public required int Worth { get; init; }
        public required int NumberOfBuildings { get; init; }
        public required int NumberOfBuildingsWithoutTax { get; init; }
        public required int Position { get; init; }
    }
}