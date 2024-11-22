using LevendMonopoly.Api.Interfaces;

namespace LevendMonopoly.Api.Models
{
    public class TeamSession : IHasID
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string Token { get; set; } = null!;
        public Team Team { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
        public bool IsValid { get; set; }
    }
}
