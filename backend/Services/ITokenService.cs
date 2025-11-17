using PerformanceManagement.Models.Entities;

namespace PerformanceManagement.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string refreshToken, User user);
}
