using LevendMonopoly.Api.Interfaces;

namespace LevendMonopoly.Api.Models
{
    public class Log : IHasID
    {
        public Guid Id { get; set; }
        public bool Suspicious { get; set; } = false;
        public string Message { get; set; } = null!;
        public string Details { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}