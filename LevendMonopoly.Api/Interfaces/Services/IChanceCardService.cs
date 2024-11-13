using LevendMonopoly.Api.Records;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IChanceCardService
    {
        ChanceCard PullChanceCard();
        DateTime LastPull(Guid teamId);
    }
}
