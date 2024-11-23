using LevendMonopoly.Api.Records;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IStartcodeService
    {
        bool PullStartcode(Guid teamId, string code);
    }
}
