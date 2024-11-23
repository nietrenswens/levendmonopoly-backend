using LevendMonopoly.Api.Records;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IStartcodeService
    {
        Task<bool> PullStartcode(Guid teamId, string code);
    }
}
