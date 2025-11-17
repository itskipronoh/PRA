using PerformanceManagement.Models.DTOs;
using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Services;

public interface IUserService
{
    Task<ApiResponse<UserDto>> GetUserByIdAsync(int id, int currentUserId, string currentUserRole);
    Task<ApiResponse<UserDto>> GetCurrentUserAsync(int userId);
    Task<ApiResponse<PagedResult<UserDto>>> GetUsersAsync(
        string? department, string? role, bool? isActive, int page, int pageSize);
    Task<ApiResponse<IEnumerable<UserDto>>> GetDirectReportsAsync(int managerId);
    Task<ApiResponse<UserDto>> UpdateUserAsync(int id, UpdateUserDto dto, int currentUserId, string currentUserRole);
}

public interface IAppraisalService
{
    Task<ApiResponse<AppraisalDto>> CreateAppraisalAsync(CreateAppraisalDto dto);
    Task<ApiResponse<AppraisalDto>> GetAppraisalByIdAsync(int id, int currentUserId, string currentUserRole);
    Task<ApiResponse<PagedResult<AppraisalDto>>> GetAppraisalsAsync(
        int? employeeId, int? managerId, string? status, DateTime? startDate, DateTime? endDate, 
        int page, int pageSize, int currentUserId, string currentUserRole);
    Task<ApiResponse<AppraisalDto>> UpdateAppraisalAsync(int id, UpdateAppraisalDto dto);
}

public interface ISelfAssessmentService
{
    Task<ApiResponse<SelfAssessmentDto>> CreateSelfAssessmentAsync(CreateSelfAssessmentDto dto, int employeeId);
    Task<ApiResponse<SelfAssessmentDto>> GetSelfAssessmentByIdAsync(int id, int currentUserId, string currentUserRole);
    Task<ApiResponse<SelfAssessmentDto>> UpdateSelfAssessmentAsync(int id, UpdateSelfAssessmentDto dto, int currentUserId);
    Task<ApiResponse<SelfAssessmentDto>> SubmitSelfAssessmentAsync(int id, int currentUserId);
    Task<ApiResponse<SelfAssessmentDto>> GetByAppraisalIdAsync(int appraisalId, int currentUserId, string currentUserRole);
}

public interface IManagerReviewService
{
    Task<ApiResponse<ManagerReviewDto>> CreateManagerReviewAsync(CreateManagerReviewDto dto, int managerId);
    Task<ApiResponse<ManagerReviewDto>> GetManagerReviewByIdAsync(int id, int currentUserId, string currentUserRole);
    Task<ApiResponse<ManagerReviewDto>> UpdateManagerReviewAsync(int id, UpdateManagerReviewDto dto, int currentUserId);
    Task<ApiResponse<ManagerReviewDto>> SubmitManagerReviewAsync(int id, int currentUserId);
    Task<ApiResponse<ManagerReviewDto>> GetByAppraisalIdAsync(int appraisalId, int currentUserId, string currentUserRole);
}

public interface IMeetingService
{
    Task<ApiResponse<MeetingDto>> CreateMeetingAsync(CreateMeetingDto dto, int currentUserId);
    Task<ApiResponse<MeetingDto>> GetMeetingByIdAsync(int id, int currentUserId, string currentUserRole);
    Task<ApiResponse<PagedResult<MeetingDto>>> GetMeetingsAsync(
        int? employeeId, int? managerId, string? status, DateTime? startDate, DateTime? endDate,
        int page, int pageSize, int currentUserId, string currentUserRole);
    Task<ApiResponse<MeetingDto>> UpdateMeetingAsync(int id, UpdateMeetingDto dto, int currentUserId);
    Task<ApiResponse<MeetingDto>> CompleteMeetingAsync(int id, CompleteMeetingDto dto, int currentUserId);
    Task<ApiResponse<MeetingDto>> CancelMeetingAsync(int id, int currentUserId);
}

public interface IAppealService
{
    Task<ApiResponse<AppealDto>> CreateAppealAsync(CreateAppealDto dto, int employeeId);
    Task<ApiResponse<AppealDto>> GetAppealByIdAsync(int id, int currentUserId, string currentUserRole);
    Task<ApiResponse<PagedResult<AppealDto>>> GetAppealsAsync(
        int? employeeId, string? status, string? priority, DateTime? startDate, DateTime? endDate,
        int page, int pageSize, string currentUserRole);
    Task<ApiResponse<AppealDto>> UpdateAppealAsync(int id, UpdateAppealDto dto, int currentUserId);
    Task<ApiResponse<AppealDto>> ReviewAppealAsync(int id, ReviewAppealDto dto, int reviewerId);
}

public interface IMediationService
{
    Task<ApiResponse<MediationDto>> CreateMediationAsync(CreateMediationDto dto);
    Task<ApiResponse<MediationDto>> GetMediationByIdAsync(int id, int currentUserId, string currentUserRole);
    Task<ApiResponse<IEnumerable<MediationDto>>> GetMediationsAsync(int? appealId, string? status, int? hrRepId);
    Task<ApiResponse<MediationDto>> UpdateMediationAsync(int id, UpdateMediationDto dto);
    Task<ApiResponse<MediationDto>> CompleteMediationAsync(int id, CompleteMediationDto dto);
}

public interface IGoalService
{
    Task<ApiResponse<GoalDto>> CreateGoalAsync(CreateGoalDto dto, int currentUserId);
    Task<ApiResponse<GoalDto>> GetGoalByIdAsync(int id, int currentUserId, string currentUserRole);
    Task<ApiResponse<IEnumerable<GoalDto>>> GetGoalsByEmployeeIdAsync(int employeeId, int currentUserId, string currentUserRole);
    Task<ApiResponse<GoalDto>> UpdateGoalAsync(int id, UpdateGoalDto dto, int currentUserId);
    Task<ApiResponse<GoalDto>> UpdateGoalProgressAsync(int id, UpdateGoalProgressDto dto, int currentUserId);
    Task<ApiResponse<object>> DeleteGoalAsync(int id, int currentUserId, string currentUserRole);
}

public interface IDocumentService
{
    Task<ApiResponse<DocumentDto>> UploadDocumentAsync(IFormFile file, int? appraisalId, int? appealId, int uploadedById);
    Task<ApiResponse<DocumentDto>> GetDocumentByIdAsync(int id, int currentUserId, string currentUserRole);
    Task<ApiResponse<IEnumerable<DocumentDto>>> GetDocumentsAsync(int? appraisalId, int? appealId);
    Task<ApiResponse<object>> DeleteDocumentAsync(int id, int currentUserId, string currentUserRole);
}

public interface INotificationService
{
    Task<ApiResponse<PagedResult<NotificationDto>>> GetNotificationsAsync(
        int userId, bool? isRead, string? type, int page, int pageSize);
    Task<ApiResponse<int>> GetUnreadCountAsync(int userId);
    Task<ApiResponse<object>> MarkAsReadAsync(int id, int userId);
    Task<ApiResponse<object>> MarkAllAsReadAsync(int userId);
    Task CreateNotificationAsync(int userId, NotificationType type, string title, string message, string? link = null);
}

public interface IDashboardService
{
    Task<ApiResponse<HRDashboardDto>> GetHRDashboardAsync();
    Task<ApiResponse<ManagerDashboardDto>> GetManagerDashboardAsync(int managerId);
    Task<ApiResponse<EmployeeDashboardDto>> GetEmployeeDashboardAsync(int employeeId);
}
