namespace LevendMonopoly.Api.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public Role() : base()
        {
        }
    }
}