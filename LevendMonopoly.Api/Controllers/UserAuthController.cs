using System.Security.Cryptography;
using LevendMonopoly.Api.InputValidation;
using LevendMonopoly.Api.Interfaces;
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
        private readonly Interfaces.ILogger _logger;


        public UserAuthController(IUserService userService, Interfaces.ILogger logger, IUserSessionService sessionService)
        {
            _userService = userService;
            _logger = logger;
            _sessionService = sessionService;
        }
        
        [HttpPost("register")]
        // TODO: Block this method behind authentication
        public async Task<ActionResult> Register(UserPostBody userbody)
        {
            if (!UserValidation.IsValidUser(userbody))
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "Server-side user input validation failed while registering a new user.",
                    Suspicious = true,
                    Details = $"Name: {userbody.Name}, Email: {userbody.Email}."
                });
                return BadRequest();
            }

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
        public async Task<ActionResult<Session>> Login(UserPostBody userbody)
        {
            if (!UserValidation.IsValidUser(userbody))
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "Server-side user input validation failed while logging in a user.",
                    Suspicious = true,
                    Details = $"Name: {userbody.Name}, Email: {userbody.Email}."
                });
                return BadRequest();
            }

            User? user = _userService.GetUser(user => user.Email == userbody.Email);
            if (user == null)
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "Failed to find a user in the database while logging in.",
                    Suspicious = false,
                    Details = $"Email: {userbody.Email}."
                });
                return NotFound();
            }

            var userbodySalt = Convert.FromBase64String(user.Salt);
            var passwordHash = Cryptography.HashPassword(userbody.Password, userbodySalt);
            if (!passwordHash.Equals(user.PasswordHash))
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "Failed to verify the password of a user while logging in.",
                    Suspicious = false,
                    Details = $"Email: {userbody.Email}."
                });
                return Unauthorized();
            }

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
            return Ok(session);
        }

        [HttpGet("check")]
        public async Task<ActionResult> CheckSession(string token)
        {
            if (token == null)
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "No token was provided while checking a session.",
                    Suspicious = true,
                    Details = "No details."
                });
                return BadRequest();
            }

            Session? session = await _sessionService.GetSessionAsync(token);
            if (session == null)
            {
                await _logger.LogAsync(new Log()
                {
                    Message = "Failed to find a session in the database while checking a session.",
                    Suspicious = false,
                    Details = $"Token: {token}."
                });
                return NotFound();
            }

            if (session.ExpirationDate < DateTime.Now)
            {
                return Unauthorized();
            }

            if (!session.IsActive)
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}