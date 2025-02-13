using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Text;

public class JwtService
{
    private readonly string _key;

    private readonly string _issuer;

    public JwtService(string key, string issuer)
    {
        _key = key;
        _issuer = issuer;
    }

    public string GenerateToken(int userId, int expiryMinutes = 1440)
    {
        var claims = new List<Claim> {
                new Claim("userId", userId.ToString())
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

    public int GetUserId(HttpRequest request)
    {
        var token = ExtractTokenFromRequest(request);

        return GetUserIdFromToken(token);
    }

    private int GetUserIdFromToken(string token)
    {
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var userId   = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value
                            ?? throw new InvalidOperationException("User ID claim not found.");

        if (!int.TryParse(userId, out int clientId))
            throw new ArgumentException("Invalid userId format");

        return clientId;
    }

    private string ExtractTokenFromRequest(HttpRequest request)
    {
        var authHeader = request?.Headers["Authorization"].FirstOrDefault();
        
        if (AuthenticationHeaderValue.TryParse(authHeader, out var headerValue) && headerValue.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
        {
            return headerValue.Parameter;
        }

        throw new InvalidOperationException("Invalid or missing Authorization header.");
    }
}