using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectTaskManagement.Application.DTOs.Auth;
using ProjectTaskManagement.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectTaskManagement.Infrastructure.Identity;

public class AuthService(UserManager<AppUser> userManager, IOptions<JwtOptions> jwtOptions) : IAuthService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null) throw new InvalidOperationException("Email is already registered.");

        var user = new AppUser
        {
            FullName = request.FullName.Trim(),
            UserName = request.Email.Trim(),
            Email = request.Email.Trim()
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) throw new InvalidOperationException(string.Join(" | ", result.Errors.Select(e => e.Description)));

        return GenerateToken(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null) throw new UnauthorizedAccessException("Invalid email or password.");

        var validPassword = await userManager.CheckPasswordAsync(user, request.Password);
        if (!validPassword) throw new UnauthorizedAccessException("Invalid email or password.");

        return GenerateToken(user);
    }

    private AuthResponse GenerateToken(AppUser user)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Name, user.FullName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new AuthResponse(user.Id, user.Email ?? string.Empty, new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
}
