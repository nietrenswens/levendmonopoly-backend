using LevendMonopoly.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LevendMonopoly.Api.Filters
{
    public class PauseFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IGameSettingService? gameSettingService = context.HttpContext.RequestServices.GetService(typeof(IGameSettingService)) as IGameSettingService;
            if (gameSettingService == null)
            {
                context.Result = new StatusCodeResult(500);
                return;
            }
            if (gameSettingService.GetGameSettings().Paused)
            {
                context.Result = new BadRequestObjectResult("Het spel is gepauzeerd");
                return;
            }
        }
    }
}
