using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _issuer,
                ValidAudience = _issuer,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero // Prevent clock skew issues
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

    public string GetUserId(HttpRequest request)
    {
        var token = ExtractTokenFromRequest(request);

        return GetUserIdFromToken(token);
    }

    public List<string> ExtractRolesFromToken(HttpRequest request)
    {
        var token = ExtractTokenFromRequest(request);

        return ExtractRolesFromToken(token);
    }

    public List<string> ExtractRolesFromToken(string token)
    {
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

        return jwtToken.Claims
                       .Where(c => c.Type == ClaimTypes.Role)
                       .Select(c => c.Value)
                       .ToList();
    }

    private string GetUserIdFromToken(string token)
    {
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

        return jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value 
               ?? throw new InvalidOperationException("User ID claim not found.");
    }

    private string ExtractTokenFromRequest(HttpRequest request)
    {
        if (request?.Headers == null || !request.Headers.ContainsKey("Authorization"))
            throw new InvalidOperationException("Authorization header is missing.");

        var authHeader = request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Authorization header is invalid.");

        return authHeader["Bearer ".Length..].Trim();
    }
}