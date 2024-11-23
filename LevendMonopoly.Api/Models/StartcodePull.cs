using LevendMonopoly.Api.Interfaces;

namespace LevendMonopoly.Api.Models
{
    public class StartcodePull : IHasID
    {
        public Guid Id { get; set; }
        public string Startcode { get; set; } = null!;
        public Guid TeamId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
