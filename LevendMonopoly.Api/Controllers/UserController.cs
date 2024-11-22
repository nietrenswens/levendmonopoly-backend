using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Utils;
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
            var result = await _userService.CreateUserIfNotExistsAsync(command.Name, command.Password, command.RoleId);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "UserOnly")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _userService.GetUserAsync(id) == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UserOnly")]
        public async Task<ActionResult> Put([FromBody] PutCommand command, Guid id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null) return NotFound();
            var oldPasswordHash = Cryptography.HashPassword(command.OldPassword, Convert.FromBase64String(user.PasswordSalt));
            if (oldPasswordHash != user.PasswordHash) return Forbid();

            if (command.NewPassword != null)
            {
                user.ChangePassword(command.NewPassword);
            }
            user.Name = command.UserName;

            await _userService.UpdateUserAsync(user);

            return NoContent();
        }

    }

    public class PutCommand
    {
        public required string UserName { get; init; }
        public required string OldPassword { get; init; }
        public string? NewPassword { get; init; }
    }

    public class PostCommand
    {
        public required string Name { get; set; } = null!;
        public required string Password { get; set; }
        public required Guid RoleId { get; set; }

    }
}