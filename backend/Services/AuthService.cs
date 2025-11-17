using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Data;
using PerformanceManagement.Models.DTOs;

namespace PerformanceManagement.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext context, ITokenService tokenService, IConfiguration configuration)
    {
        _context = context;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return new ApiResponse<LoginResponseDto>
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        if (!user.IsActive)
        {
            return new ApiResponse<LoginResponseDto>
            {
                Success = false,
                Message = "User account is inactive"
            };
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var jwtSettings = _configuration.GetSection("JwtSettings");
        var refreshTokenExpiryDays = int.Parse(jwtSettings["RefreshTokenExpiryDays"] ?? "7");

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);
        await _context.SaveChangesAsync();

        var userDto = new UserDto
        {
            Id = user.Id,
            EmployeeId = user.EmployeeId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            Department = user.Department,
            JobTitle = user.JobTitle,
            ManagerId = user.ManagerId,
            IsActive = user.IsActive,
            HireDate = user.HireDate
        };

        return new ApiResponse<LoginResponseDto>
        {
            Success = true,
            Message = "Login successful",
            Data = new LoginResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                User = userDto
            }
        };
    }

    public async Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenDto.RefreshToken);

        if (user == null)
        {
            return new ApiResponse<LoginResponseDto>
            {
                Success = false,
                Message = "Invalid refresh token"
            };
        }

        if (!_tokenService.ValidateRefreshToken(refreshTokenDto.RefreshToken, user))
        {
            return new ApiResponse<LoginResponseDto>
            {
                Success = false,
                Message = "Refresh token has expired"
            };
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        var jwtSettings = _configuration.GetSection("JwtSettings");
        var refreshTokenExpiryDays = int.Parse(jwtSettings["RefreshTokenExpiryDays"] ?? "7");

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);
        await _context.SaveChangesAsync();

        var userDto = new UserDto
        {
            Id = user.Id,
            EmployeeId = user.EmployeeId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            Department = user.Department,
            JobTitle = user.JobTitle,
            ManagerId = user.ManagerId,
            IsActive = user.IsActive,
            HireDate = user.HireDate
        };

        return new ApiResponse<LoginResponseDto>
        {
            Success = true,
            Message = "Token refreshed successfully",
            Data = new LoginResponseDto
            {
                Token = accessToken,
                RefreshToken = newRefreshToken,
                User = userDto
            }
        };
    }

    public async Task<ApiResponse<object>> LogoutAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return new ApiResponse<object>
            {
                Success = false,
                Message = "User not found"
            };
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _context.SaveChangesAsync();

        return new ApiResponse<object>
        {
            Success = true,
            Message = "Logout successful"
        };
    }

    public async Task<ApiResponse<object>> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return new ApiResponse<object>
            {
                Success = false,
                Message = "User not found"
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, user.PasswordHash))
        {
            return new ApiResponse<object>
            {
                Success = false,
                Message = "Current password is incorrect"
            };
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return new ApiResponse<object>
        {
            Success = true,
            Message = "Password changed successfully"
        };
    }
}
