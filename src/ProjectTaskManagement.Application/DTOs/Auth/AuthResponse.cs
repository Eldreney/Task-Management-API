namespace ProjectTaskManagement.Application.DTOs.Auth;

public record AuthResponse(string UserId, string Email, string Token, DateTime ExpiresAt);
