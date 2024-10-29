using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace LevendMonopoly.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameSettingsController : ControllerBase
    {
        private readonly IGameSettingService _service;

        public GameSettingsController(IGameSettingService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<GameSettings> Get()
        {
            return _service.GetGameSettings(); 
        }

        [HttpPut]
        public ActionResult Put(GameSettings gameSettings)
        {
            _service.UpdateGameSettings(gameSettings);
            return Ok();
        }
    }
}
