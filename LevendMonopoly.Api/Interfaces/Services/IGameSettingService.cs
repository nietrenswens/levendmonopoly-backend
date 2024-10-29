using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IGameSettingService
    {
        GameSettings GetGameSettings();
        void UpdateGameSettings(GameSettings gameSettings);
    }
}
