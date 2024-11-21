using LevendMonopoly.Api.Records;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IChanceCardService
    {
        ChanceCard PullChanceCard();
        DateTime LastPull(Guid teamId);
        Task AddPull(Guid teamId, DateTime now);
        void ResetPulls();
    }
}
