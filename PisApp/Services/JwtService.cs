using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService(string key, string issuer)
{
    public string GenerateToken(int userId, bool isUserVIP, int expiryMinutes = 1440)
    {
        var claims = new List<Claim> {
            new Claim("userId", userId.ToString()),
            new Claim("isUserVIP", isUserVIP.ToString())
        };

        var issuerKey   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds       = new SigningCredentials(issuerKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer   : issuer,
            audience : issuer,
            claims   : claims,
            expires  : DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var issuerKey    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer   = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer      = issuer,
                ValidAudience    = issuer,
                IssuerSigningKey = issuerKey,
                ClockSkew        = TimeSpan.Zero // Prevent clock skew issues
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            if (validatedToken is JwtSecurityToken jwtToken &&
                jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase) 
                is not false)
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

        ArgumentNullException.ThrowIfNull(userIdClaim, nameof(userIdClaim));

        var userId = ConvertUserIdToInt(userIdClaim);

        return userId;
    }

    public bool GetUserVIPStatus(HttpContext httpContext)
    {
        var userVIPClaim = httpContext.User.FindFirst("isUserVIP")?.Value;

        ArgumentNullException.ThrowIfNull(userVIPClaim, nameof(userVIPClaim));
        
        var isUserVIP = ConvertUserVIPStatusToBool(userVIPClaim);

        return isUserVIP;
    }

    private bool ConvertUserVIPStatusToBool(string userVIPClaim)
    {
        if (bool.TryParse(userVIPClaim, out bool isVIP) is false)
        {
            throw new ArgumentException("Invalid isVIP format");
        }

        return isVIP;
    }
        
    private int ConvertUserIdToInt(string userIdClaim)
    {
        if (int.TryParse(userIdClaim, out int userId) is false)
        {
            throw new ArgumentException("Invalid userId format");
        }   

        return userId;
    }
}