using ProjectTaskManagement.Application.DTOs.Auth;

namespace ProjectTaskManagement.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
