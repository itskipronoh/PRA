using PerformanceManagement.Models.DTOs;

namespace PerformanceManagement.Services;

public interface IAuthService
{
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto);
    Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    Task<ApiResponse<object>> LogoutAsync(int userId);
    Task<ApiResponse<object>> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
}
