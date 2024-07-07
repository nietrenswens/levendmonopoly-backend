using System.ComponentModel.DataAnnotations.Schema;

namespace LevendMonopoly.Api.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;

        public Guid RoleId { get; set; }

        public User() : base()
        {
        }

        override public string ToString()
        {
            return $"{Id} {Name} {Email} {Role.Name} {Role.Id} {RoleId}";
        }
    }
}