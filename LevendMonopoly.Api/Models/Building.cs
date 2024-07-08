namespace LevendMonopoly.Api.Models
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public Team? Owner { get; set; }
        public Guid? OwnerId { get; set; }
        
    }
}