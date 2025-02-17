using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

public class JwtService
{
    private readonly string _key;

    private readonly string _issuer;

    public JwtService(string key, string issuer)
    {
        _key = key;
        _issuer = issuer;
    }

    public string GenerateToken(int userId, bool isUserVIP, int expiryMinutes = 1440)
    {
        var claims = new List<Claim> {
                new Claim("userId", userId.ToString()),
                new Claim("isUserVIP", isUserVIP.ToString())
        };

        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer   : _issuer,
            audience : _issuer,
            claims   : claims,
            expires  : DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var key          = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer   = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer      = _issuer,
                ValidAudience    = _issuer,
                IssuerSigningKey = key,
                ClockSkew        = TimeSpan.Zero // Prevent clock skew issues
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            if (validatedToken is JwtSecurityToken jwtToken &&
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token algorithm.");
            }

            return principal;
        }
        catch
        {
            return null;
        }
    }

    public int GetUserId(HttpContext httpContext)
    {
        var userId = httpContext.User.FindFirst("userId")?.Value;

        if (!int.TryParse(userId, out int clientId))
            throw new ArgumentException("Invalid userId format");

        return clientId;
    }
}