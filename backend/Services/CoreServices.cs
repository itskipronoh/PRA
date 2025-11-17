using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Data;
using PerformanceManagement.Models.DTOs;
using PerformanceManagement.Models.Enums;
using PerformanceManagement.Models.Entities;
using PerformanceManagement.Repositories;

namespace PerformanceManagement.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<UserDto>> GetCurrentUserAsync(int userId)
    {
        var user = await _userRepository.GetWithManagerAsync(userId);
        if (user == null)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "User not found"
            };
        }

        return new ApiResponse<UserDto>
        {
            Success = true,
            Data = MapToDto(user)
        };
    }

    public async Task<ApiResponse<UserDto>> GetUserByIdAsync(int id, int currentUserId, string currentUserRole)
    {
        var user = await _userRepository.GetWithManagerAsync(id);
        if (user == null)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "User not found"
            };
        }

        return new ApiResponse<UserDto>
        {
            Success = true,
            Data = MapToDto(user)
        };
    }

    public async Task<ApiResponse<PagedResult<UserDto>>> GetUsersAsync(
        string? department, string? role, bool? isActive, int page, int pageSize)
    {
        var query = (await _userRepository.GetAllAsync()).AsQueryable();

        if (!string.IsNullOrEmpty(department))
            query = query.Where(u => u.Department == department);

        if (!string.IsNullOrEmpty(role) && Enum.TryParse<UserRole>(role, out var userRole))
            query = query.Where(u => u.Role == userRole);

        if (isActive.HasValue)
            query = query.Where(u => u.IsActive == isActive.Value);

        var totalCount = query.Count();
        var items = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(MapToDto)
            .ToList();

        return new ApiResponse<PagedResult<UserDto>>
        {
            Success = true,
            Data = new PagedResult<UserDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            }
        };
    }

    public async Task<ApiResponse<IEnumerable<UserDto>>> GetDirectReportsAsync(int managerId)
    {
        var directReports = await _userRepository.GetDirectReportsAsync(managerId);

        return new ApiResponse<IEnumerable<UserDto>>
        {
            Success = true,
            Data = directReports.Select(MapToDto).ToList()
        };
    }

    public async Task<ApiResponse<UserDto>> UpdateUserAsync(int id, UpdateUserDto dto, int currentUserId, string currentUserRole)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "User not found"
            };
        }

        if (dto.FirstName != null) user.FirstName = dto.FirstName;
        if (dto.LastName != null) user.LastName = dto.LastName;
        if (dto.Department != null) user.Department = dto.Department;
        if (dto.JobTitle != null) user.JobTitle = dto.JobTitle;
        if (dto.ManagerId.HasValue) user.ManagerId = dto.ManagerId;
        if (dto.IsActive.HasValue) user.IsActive = dto.IsActive.Value;

        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        return new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User updated successfully",
            Data = MapToDto(user)
        };
    }

    private UserDto MapToDto(User user)
    {
        return new UserDto
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
            ManagerName = user.Manager != null ? $"{user.Manager.FirstName} {user.Manager.LastName}" : null,
            IsActive = user.IsActive,
            HireDate = user.HireDate
        };
    }
}

public class AppraisalService : IAppraisalService
{
    private readonly IAppraisalRepository _appraisalRepository;
    private readonly INotificationService _notificationService;

    public AppraisalService(IAppraisalRepository appraisalRepository, INotificationService notificationService)
    {
        _appraisalRepository = appraisalRepository;
        _notificationService = notificationService;
    }

    public async Task<ApiResponse<AppraisalDto>> CreateAppraisalAsync(CreateAppraisalDto dto)
    {
        var appraisal = new Models.Entities.Appraisal
        {
            EmployeeId = dto.EmployeeId,
            ManagerId = dto.ManagerId,
            ReviewPeriodStart = dto.ReviewPeriodStart,
            ReviewPeriodEnd = dto.ReviewPeriodEnd,
            Comments = dto.Comments,
            Status = AppraisalStatus.Draft
        };

        await _appraisalRepository.AddAsync(appraisal);

        // Send notifications
        await _notificationService.CreateNotificationAsync(
            dto.EmployeeId,
            NotificationType.AppraisalCreated,
            "New Appraisal Created",
            $"A new appraisal has been created for the period {dto.ReviewPeriodStart:d} to {dto.ReviewPeriodEnd:d}",
            $"/appraisals/{appraisal.Id}"
        );

        await _notificationService.CreateNotificationAsync(
            dto.ManagerId,
            NotificationType.AppraisalCreated,
            "New Appraisal Created",
            $"A new appraisal has been assigned to you",
            $"/appraisals/{appraisal.Id}"
        );

        var created = await _appraisalRepository.GetWithDetailsAsync(appraisal.Id);
        
        return new ApiResponse<AppraisalDto>
        {
            Success = true,
            Message = "Appraisal created successfully",
            Data = MapToDto(created!)
        };
    }

    public async Task<ApiResponse<AppraisalDto>> GetAppraisalByIdAsync(int id, int currentUserId, string currentUserRole)
    {
        var appraisal = await _appraisalRepository.GetWithDetailsAsync(id);
        if (appraisal == null)
        {
            return new ApiResponse<AppraisalDto>
            {
                Success = false,
                Message = "Appraisal not found"
            };
        }

        return new ApiResponse<AppraisalDto>
        {
            Success = true,
            Data = MapToDto(appraisal)
        };
    }

    public async Task<ApiResponse<PagedResult<AppraisalDto>>> GetAppraisalsAsync(
        int? employeeId, int? managerId, string? status, DateTime? startDate, DateTime? endDate,
        int page, int pageSize, int currentUserId, string currentUserRole)
    {
        var query = (await _appraisalRepository.GetAllAsync()).AsQueryable();

        if (employeeId.HasValue)
            query = query.Where(a => a.EmployeeId == employeeId.Value);

        if (managerId.HasValue)
            query = query.Where(a => a.ManagerId == managerId.Value);

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<AppraisalStatus>(status, out var appraisalStatus))
            query = query.Where(a => a.Status == appraisalStatus);

        if (startDate.HasValue)
            query = query.Where(a => a.ReviewPeriodStart >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(a => a.ReviewPeriodEnd <= endDate.Value);

        var totalCount = query.Count();
        var items = query
            .OrderByDescending(a => a.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(MapToDto)
            .ToList();

        return new ApiResponse<PagedResult<AppraisalDto>>
        {
            Success = true,
            Data = new PagedResult<AppraisalDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            }
        };
    }

    public async Task<ApiResponse<AppraisalDto>> UpdateAppraisalAsync(int id, UpdateAppraisalDto dto)
    {
        var appraisal = await _appraisalRepository.GetByIdAsync(id);
        if (appraisal == null)
        {
            return new ApiResponse<AppraisalDto>
            {
                Success = false,
                Message = "Appraisal not found"
            };
        }

        if (dto.ReviewPeriodStart.HasValue) appraisal.ReviewPeriodStart = dto.ReviewPeriodStart.Value;
        if (dto.ReviewPeriodEnd.HasValue) appraisal.ReviewPeriodEnd = dto.ReviewPeriodEnd.Value;
        if (dto.Status.HasValue) appraisal.Status = dto.Status.Value;
        if (dto.OverallRating.HasValue) appraisal.OverallRating = dto.OverallRating.Value;
        if (dto.Comments != null) appraisal.Comments = dto.Comments;

        appraisal.UpdatedAt = DateTime.UtcNow;
        await _appraisalRepository.UpdateAsync(appraisal);

        var updated = await _appraisalRepository.GetWithDetailsAsync(id);

        return new ApiResponse<AppraisalDto>
        {
            Success = true,
            Message = "Appraisal updated successfully",
            Data = MapToDto(updated!)
        };
    }

    private AppraisalDto MapToDto(Models.Entities.Appraisal appraisal)
    {
        return new AppraisalDto
        {
            Id = appraisal.Id,
            EmployeeId = appraisal.EmployeeId,
            EmployeeName = $"{appraisal.Employee.FirstName} {appraisal.Employee.LastName}",
            ManagerId = appraisal.ManagerId,
            ManagerName = $"{appraisal.Manager.FirstName} {appraisal.Manager.LastName}",
            ReviewPeriodStart = appraisal.ReviewPeriodStart,
            ReviewPeriodEnd = appraisal.ReviewPeriodEnd,
            Status = appraisal.Status,
            OverallRating = appraisal.OverallRating,
            Comments = appraisal.Comments,
            CreatedAt = appraisal.CreatedAt,
            CompletedAt = appraisal.CompletedAt
        };
    }
}

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<ApiResponse<PagedResult<NotificationDto>>> GetNotificationsAsync(
        int userId, bool? isRead, string? type, int page, int pageSize)
    {
        var notifications = await _notificationRepository.GetByUserIdAsync(userId);
        var query = notifications.AsQueryable();

        if (isRead.HasValue)
            query = query.Where(n => n.IsRead == isRead.Value);

        if (!string.IsNullOrEmpty(type) && Enum.TryParse<NotificationType>(type, out var notificationType))
            query = query.Where(n => n.Type == notificationType);

        var totalCount = query.Count();
        var items = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(MapToDto)
            .ToList();

        return new ApiResponse<PagedResult<NotificationDto>>
        {
            Success = true,
            Data = new PagedResult<NotificationDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            }
        };
    }

    public async Task<ApiResponse<int>> GetUnreadCountAsync(int userId)
    {
        var count = await _notificationRepository.GetUnreadCountAsync(userId);

        return new ApiResponse<int>
        {
            Success = true,
            Data = count
        };
    }

    public async Task<ApiResponse<object>> MarkAsReadAsync(int id, int userId)
    {
        await _notificationRepository.MarkAsReadAsync(id);

        return new ApiResponse<object>
        {
            Success = true,
            Message = "Notification marked as read"
        };
    }

    public async Task<ApiResponse<object>> MarkAllAsReadAsync(int userId)
    {
        await _notificationRepository.MarkAllAsReadAsync(userId);

        return new ApiResponse<object>
        {
            Success = true,
            Message = "All notifications marked as read"
        };
    }

    public async Task CreateNotificationAsync(int userId, NotificationType type, string title, string message, string? link = null)
    {
        var notification = new Models.Entities.Notification
        {
            UserId = userId,
            Type = type,
            Title = title,
            Message = message,
            Link = link
        };

        await _notificationRepository.AddAsync(notification);
    }

    private NotificationDto MapToDto(Models.Entities.Notification notification)
    {
        return new NotificationDto
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Type = notification.Type,
            Title = notification.Title,
            Message = notification.Message,
            IsRead = notification.IsRead,
            Link = notification.Link,
            CreatedAt = notification.CreatedAt,
            ReadAt = notification.ReadAt
        };
    }
}
