namespace LevendMonopoly.Api.Models
{
    public class ChanceCardPull
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
