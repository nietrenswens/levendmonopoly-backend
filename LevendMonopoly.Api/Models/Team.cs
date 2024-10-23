using LevendMonopoly.Api.Interfaces;
using LevendMonopoly.Api.Utils;

namespace LevendMonopoly.Api.Models
{
    public class Team : IIdentityEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public int Balance { get; set; }
        public List<Building> Buildings { get; set; } = null!;

        public static Team CreateNewTeam(string name, string password)
        {
            var salt = Cryptography.GenerateSalt();
            return new Team()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Balance = 5000,
                PasswordHash = Cryptography.HashPassword(password, salt),
                PasswordSalt = Convert.ToBase64String(salt)
            };
        }
    }
}