using LevendMonopoly.Api.Interfaces;

namespace LevendMonopoly.Api.Models
{
    public class GameSettings
    {
        public int Id { get; set; }
        public decimal TaxRate { get; set; }
        public bool Paused { get; set; }
    }
}
