using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LevendMonopoly.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private ITeamService _teamService;
        private IUserService _userService;

        public AuthService(DataContext context, ITeamService teamService, IUserService userService)
        {
            _context = context;
            _teamService = teamService;
            _userService = userService;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            IIdentityEntity? entity = await _teamService.GetTeamByNameAsync(username);
            if (entity == null)
                entity = _userService.GetUser(user => user.Name == username);
            if (entity == null)
                return "";

            if (!validateCredentials(entity, password))
                return "";

            return createToken(entity);
        }

        private bool validateCredentials(IIdentityEntity entity, string password)
        {
            var givenPasswordHash = Cryptography.HashPassword(password, Convert.FromBase64String(entity.PasswordSalt));
            return givenPasswordHash == entity.PasswordHash;
        }

        private string createToken(IIdentityEntity entity)
        {
            var handler = new JwtSecurityTokenHandler();
            var privateKey = Encoding.UTF8.GetBytes("DfeFGH67fgG#yySSD#$%^dew^&fu(ughbyu$%");
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256);

            ClaimsIdentity claims;
            if (entity is Team teamEntity)
            {
                claims = generateClaims(teamEntity);
            }
            else if (entity is User userEntity)
            {
                claims = generateClaims(userEntity);
            }
            else
            {
                throw new Exception("Authentication subject must either be a team or a user.");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddDays(2),
                Subject = claims
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private ClaimsIdentity generateClaims(Team team)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("id", team.Id.ToString()));
            ci.AddClaim(new Claim("type", "team"));
            ci.AddClaim(new Claim("IsTeam", "true"));
            ci.AddClaim(new Claim("name", team.Name.ToString()));

            return ci;
        }

        private ClaimsIdentity generateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("id", user.Id.ToString()));
            ci.AddClaim(new Claim("type", "user"));
            ci.AddClaim(new Claim("name", user.Name.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));
            ci.AddClaim(new Claim("IsUser","true"));

            return ci;
        }
    }
}
