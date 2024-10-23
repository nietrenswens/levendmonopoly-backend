namespace LevendMonopoly.Api.Interfaces
{
    public interface IIdentityEntity
    {
        Guid Id { get; set; }
        String Name { get; set; }
        String PasswordHash { get; set; }
        String PasswordSalt { get; set; }
    }
}
