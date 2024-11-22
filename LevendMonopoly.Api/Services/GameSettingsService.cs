using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Services
{
    public class GameSettingsService : IGameSettingService
    {
        private readonly DataContext _context;

        public GameSettingsService(DataContext context)
        {
            _context = context;
        }

        public GameSettings GetGameSettings()
        {
            var data = _context.GameSettings.Single();
            return _context.GameSettings.Single();
        }

        public void UpdateGameSettings(GameSettings gameSettings)
        {
            _context.GameSettings.Update(gameSettings);
            _context.SaveChanges();
        }
    }
}
