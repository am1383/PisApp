using PisApp.API.Interfaces.UnitOfWork;
using System.Security.Claims;

public class VipMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUnitOfWork _unitOfWork;

    public VipMiddleware(
        RequestDelegate next, 
        IUnitOfWork unitOfWork)
    {
        _next = next;
        _unitOfWork = unitOfWork;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid User ID");
            return;
        }

        var expirationTime = await _unitOfWork.Users.VIPChecker(userId);

        if (expirationTime == DateTime.MinValue)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("User Is Not VIP");
            return;
        }

        await _next(context);
    }
}
