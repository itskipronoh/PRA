using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.Models.DTOs;
using PerformanceManagement.Services;
using System.Security.Claims;

namespace PerformanceManagement.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AppraisalsController : ControllerBase
{
    private readonly IAppraisalService _appraisalService;

    public AppraisalsController(IAppraisalService appraisalService)
    {
        _appraisalService = appraisalService;
    }

    [Authorize(Roles = "HR,Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateAppraisal([FromBody] CreateAppraisalDto createAppraisalDto)
    {
        var result = await _appraisalService.CreateAppraisalAsync(createAppraisalDto);
        
        if (!result.Success)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetAppraisalById), new { id = result.Data?.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppraisalById(int id)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
        
        var result = await _appraisalService.GetAppraisalByIdAsync(id, currentUserId, currentUserRole);
        
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAppraisals(
        [FromQuery] int? employeeId,
        [FromQuery] int? managerId,
        [FromQuery] string? status,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
        
        var result = await _appraisalService.GetAppraisalsAsync(
            employeeId, managerId, status, startDate, endDate, page, limit, currentUserId, currentUserRole);
        
        return Ok(result);
    }

    [Authorize(Roles = "HR,Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppraisal(int id, [FromBody] UpdateAppraisalDto updateAppraisalDto)
    {
        var result = await _appraisalService.UpdateAppraisalAsync(id, updateAppraisalDto);
        
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetAppraisalsByEmployeeId(int employeeId, [FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
        
        var result = await _appraisalService.GetAppraisalsAsync(
            employeeId, null, null, null, null, page, limit, currentUserId, currentUserRole);
        
        return Ok(result);
    }

    [HttpGet("manager/{managerId}")]
    public async Task<IActionResult> GetAppraisalsByManagerId(int managerId, [FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
        
        var result = await _appraisalService.GetAppraisalsAsync(
            null, managerId, null, null, null, page, limit, currentUserId, currentUserRole);
        
        return Ok(result);
    }
}
