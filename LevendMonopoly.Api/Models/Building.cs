namespace LevendMonopoly.Api.Models
{
    public class Building
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public Team? Owner { get; set; }
        public Guid? OwnerId { get; set; }
        public bool Tax { get; set; } = false;
        public string? Image { get; set; }

        public void ResetToUnsoldState()
        {
            Owner = null;
            OwnerId = null;
            Tax = false;
        }
        
    }
}