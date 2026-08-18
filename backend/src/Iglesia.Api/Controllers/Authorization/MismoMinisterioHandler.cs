using Microsoft.AspNetCore.Authorization;

namespace Iglesia.Api.Authorization;

public class MismoMinisterioRequirement : IAuthorizationRequirement { }

public class MismoMinisterioHandler : AuthorizationHandler<MismoMinisterioRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MismoMinisterioHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        MismoMinisterioRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return Task.CompletedTask;

       
        var ministerioClaim = context.User.FindFirst("ministerioId")?.Value;

      
        if (string.IsNullOrEmpty(ministerioClaim))
        {
           
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }

        
        var routeData = httpContext.GetRouteData();
        if (routeData.Values.TryGetValue("ministerioId", out var routeMinisterioId) ||
            routeData.Values.TryGetValue("id", out routeMinisterioId))
        {
            if (ministerioClaim == routeMinisterioId?.ToString())
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}