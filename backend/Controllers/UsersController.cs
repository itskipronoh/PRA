using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models.DTOs;
using PerformanceManagement.Services;
using System.Security.Claims;

namespace PerformanceManagement.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var result = await _userService.GetCurrentUserAsync(userId);
        
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
        
        var result = await _userService.GetUserByIdAsync(id, currentUserId, currentUserRole);
        
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [Authorize(Roles = "HR,Admin")]
    [HttpGet]
    public async Task<IActionResult> GetUsers(
        [FromQuery] string? department,
        [FromQuery] string? role,
        [FromQuery] bool? isActive,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10)
    {
        var result = await _userService.GetUsersAsync(department, role, isActive, page, limit);
        
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
        
        var result = await _userService.UpdateUserAsync(id, updateUserDto, currentUserId, currentUserRole);
        
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{id}/direct-reports")]
    public async Task<IActionResult> GetDirectReports(int id)
    {
        var result = await _userService.GetDirectReportsAsync(id);
        
        return Ok(result);
    }
}
