using LevendMonopoly.Api.Interfaces;

namespace LevendMonopoly.Api.Models
{
    public class Role : IHasID
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public Role() : base()
        {
        }
    }
}