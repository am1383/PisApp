using Microsoft.AspNetCore.Mvc.Filters;
using PisApp.API.Exceptions;

public class VipAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userVIPStatus = context.HttpContext.User.FindFirst("isUserVIP")?.Value;

        if (userVIPStatus == "False")
        {
            throw new NotVIPException();
        }
    }
}