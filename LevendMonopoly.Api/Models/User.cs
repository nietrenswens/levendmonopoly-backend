using LevendMonopoly.Api.Interfaces;
using LevendMonopoly.Api.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace LevendMonopoly.Api.Models
{
    public class User : IIdentityEntity, IHasID
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;

        public Guid RoleId { get; set; }

        public User() : base()
        {
        }

        public void ChangePassword(string password)
        {
            byte[] salt = Convert.FromBase64String(PasswordSalt);
            PasswordHash = Cryptography.HashPassword(password, salt);
        }

        override public string ToString()
        {
            return $"{Id} {Name} {Role.Name} {Role.Id} {RoleId}";
        }

        public static User CreateNewUser(string name, string password, Guid roleId)
        {
            var salt = Cryptography.GenerateSalt();
            return new User()
            {
                Id = Guid.NewGuid(),
                Name = name,
                PasswordHash = Cryptography.HashPassword(password, salt),
                PasswordSalt = Convert.ToBase64String(salt),
                RoleId = roleId
            };
        }
    }
}