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
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly LevendMonopoly.Api.Interfaces.ILogger _logger;

        public AuthController(IUserService userService, LevendMonopoly.Api.Interfaces.ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }
        
        [HttpPost("register")]
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
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserPostBody userbody)
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
            // TODO: Create Session and send token
            return Ok();
        }
    }
}