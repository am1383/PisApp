using Microsoft.AspNetCore.Mvc.Filters;
using PisApp.API.Exceptions;

public class VipAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var jwtService = context.HttpContext.RequestServices.GetRequiredService<JwtService>();
        var isUserVIP  = jwtService.GetUserVIPStatus(context.HttpContext);

        if (isUserVIP is false)
        {
            throw new NotVIPException();
        }
    }
}