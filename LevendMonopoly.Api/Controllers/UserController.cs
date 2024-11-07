using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace LevendMonopoly.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;

        public UserController(IUserService userService, ITeamService teamService)
        {
            _userService = userService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _userService.GetUsersAsync();
            return users;
        }

        [HttpPost]
        [Authorize(Policy = "UserOnly")]
        public async Task<ActionResult> Post(PostCommand command)
        {
            if (await _teamService.GetTeamByNameAsync(command.Name) != null || _userService.GetUser(u => u.Name == command.Name) != null)
                return BadRequest("Gebruiker bestaat al");

            var user = Models.User.CreateNewUser(command.Name, command.Password, command.RoleId);
            await _userService.CreateUserAsync(user);

            return NoContent();
        }
        
    }

    public class PostCommand
    {
        public required string Name { get; set; } = null!;
        public required string Password { get; set; }
        public required Guid RoleId { get; set; }

    }
}