using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService(string _key, string _issuer)
{
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
                throw new SecurityTokenException("Invalid token algorithm");
            }

            return principal;
        }
        catch
        {
            throw new SecurityTokenException("Token is not valid");
        }
    }

    public int GetUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.FindFirst("userId")?.Value;

        if (userIdClaim is null)
        {
            throw new ArgumentNullException("userId not found");
        }

        var userId = ConvertUserIdToInt(userIdClaim);

        return userId;
    }

    public bool GetUserVIPStatus(HttpContext httpContext)
    {
        var userVIPClaim = httpContext.User.FindFirst("isUserVIP")?.Value;

        if (userVIPClaim is null)
        {
            throw new ArgumentNullException("isVip not found");
        }

        var isUserVIP = ConvertUserVIPStatusToBool(userVIPClaim);

        return isUserVIP;
    }

    private bool ConvertUserVIPStatusToBool(string userVIPClaim)
    {
        if (!bool.TryParse(userVIPClaim, out bool isVIP))
        {
            throw new ArgumentException("Invalid isVIP format");
        }

        return isVIP;
    }
        
    private int ConvertUserIdToInt(string userIdClaim)
    {
        if (!int.TryParse(userIdClaim, out int userId))
        {
            throw new ArgumentException("Invalid userId format");
        }   

        return userId;
    }
}