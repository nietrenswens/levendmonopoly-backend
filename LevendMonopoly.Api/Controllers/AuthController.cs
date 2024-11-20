using LevendMonopoly.Api.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;

namespace LevendMonopoly.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResult>> Login(LoginCommand command)
        {
            var token = await _authService.LoginAsync(command.Name, command.Password);
            if (String.IsNullOrWhiteSpace(token))
                return Unauthorized();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var jwtSecToken = jsonToken as JwtSecurityToken;
            return Ok(new LoginResult() {
                Token = token,
                Type = jwtSecToken!.Claims.First(claim => claim.Type == "type").Value,
                Name = jwtSecToken!.Claims.First(claim => claim.Type == "name").Value,
                Id = Guid.Parse(jwtSecToken!.Claims.First(claim => claim.Type == "id").Value)
            });
        }

        [HttpGet("check")]
        [Authorize]
        public ActionResult IsAuth()
        {
            return Ok(new {Auth = true});
        }
    }

    public record LoginCommand
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
    }

    public record LoginResult
    {
        public required string Token { get; set; }
        public required string Type { get; set; }
        public required string Name { get; set; }
        public required Guid Id { get; set; }
    }
}
