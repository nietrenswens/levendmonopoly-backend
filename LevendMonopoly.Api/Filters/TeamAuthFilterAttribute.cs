using LevendMonopoly.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LevendMonopoly.Api.Filters
{
    public class TeamAuthFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next)
        {
            var context = actionContext.HttpContext;
            var sessionService = (IUserSessionService)context.RequestServices.GetService(typeof(ITeamSessionService))!;
            if (sessionService == null)
            {
                context.Response.StatusCode = 500;
                return;
            }
            var token = context.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = 401;
                return;
            }
            var session = await sessionService.GetSessionAsync(token!);
            if (session == null || !session.IsActive)
            {
                context.Response.StatusCode = 401;
                return;
            }
            await next();
        }
    }
}