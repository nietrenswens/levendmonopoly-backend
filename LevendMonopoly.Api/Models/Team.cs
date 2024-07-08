namespace LevendMonopoly.Api.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public int Balance { get; set; }
        public List<Building> Buildings { get; set; } = null!;
    }
}