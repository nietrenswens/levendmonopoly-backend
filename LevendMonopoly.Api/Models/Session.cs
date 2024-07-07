using System.ComponentModel.DataAnnotations.Schema;

namespace LevendMonopoly.Api.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string Token { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}