using Microsoft.AspNetCore.Mvc.Filters;
using PisApp.API.Exceptions;

public class VipAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userVIPClaim = context.HttpContext.User.FindFirst("isUserVIP")?.Value;

        if (!bool.TryParse(userVIPClaim, out bool isVIP) || !isVIP)
        {
            throw new NotVIPException();
        }
    }
}
