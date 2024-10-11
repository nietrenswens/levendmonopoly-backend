using System.Security.Cryptography;
using LevendMonopoly.Api.DTOs;
using LevendMonopoly.Api.Filters;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Records;
using LevendMonopoly.Api.Utils;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

namespace LevendMonopoly.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _sessionService;
        private readonly Interfaces.Services.ILogger _logger;


        public UserAuthController(IUserService userService, Interfaces.Services.ILogger logger, IUserSessionService sessionService)
        {
            _userService = userService;
            _logger = logger;
            _sessionService = sessionService;
        }

        [HttpPost("register")]
        [AuthFilter]
        public async Task<ActionResult> Register(UserPostBody userbody)
        {
            // Generate password hash and salt
            byte[] passwordSalt = Cryptography.GenerateSalt();
            string passwordHash = Cryptography.HashPassword(userbody.Password, passwordSalt);

            User user = new User()
            {
                Name = userbody.Name,
                Email = userbody.Email,
                PasswordHash = passwordHash,
                Salt = Convert.ToBase64String(passwordSalt)
            };
            if (!await _userService.CreateUserAsync(user))
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "Failed to create a new user in the database.",
                    Suspicious = false,
                    Details = $"Name: {user.Name}, Email: {user.Email}."
                });
                return StatusCode(500);
            }

            await _logger.LogAsync(new Log()
            {
                Message = $"User {user.Name} has registered.",
                Suspicious = false,
                Details = ""
            });
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<SessionDTO>> Login(UserLoginCommand command)
        {
            User? user = _userService.GetUser(user => user.Email == command.Email);
            if (user == null)
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "Failed to find a user in the database while logging in.",
                    Suspicious = false,
                    Details = $"Email: {command.Email}."
                });
                return NotFound();
            }

            var userbodySalt = Convert.FromBase64String(user.Salt);
            var passwordHash = Cryptography.HashPassword(command.Password, userbodySalt);
            if (!passwordHash.Equals(user.PasswordHash))
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "Failed to verify the password of a user while logging in.",
                    Suspicious = false,
                    Details = $"Email: {command.Email}."
                });
                return Unauthorized();
            }

            var oldSessions = await _sessionService.GetSessionsAsync(user.Id);
            foreach (var oldSession in oldSessions)
            {
                oldSession.IsActive = false;
            }
            await _sessionService.UpdateSessionsAsync(oldSessions);

            Session? session = await _sessionService.CreateSessionAsync(user);
            if (session == null)
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "Failed to create a session for a user while logging in.",
                    Suspicious = false,
                    Details = $"Email: {user.Email}."
                });
                return StatusCode(500);
            }

            await _logger.LogAsync(new Log()
            {
                Message = $"User {user.Name} has logged in.",
                Suspicious = false,
                Details = ""
            });
            return Ok(new SessionDTO(session));
        }

        [HttpPost("check")]
        public async Task<ActionResult> CheckSession(TokenCommand command)
        {
            Session? session = await _sessionService.GetSessionAsync(command.Token);
            if (session == null)
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "Failed to find a session in the database while checking a session.",
                    Suspicious = false,
                    Details = $"Token: {command.Token}."
                });
                return NotFound();
            }

            if (session.ExpirationDate.ToUniversalTime() < DateTime.Now.ToUniversalTime())
            {
                return Unauthorized();
            }

            if (!session.IsActive)
            {
                return Unauthorized();
            }

            return NoContent();
        }
    }

    public record UserLoginCommand()
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }

    public record TokenCommand
    {
        public required string Token { get; init; }
    }
}