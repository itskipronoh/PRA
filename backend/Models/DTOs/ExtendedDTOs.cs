using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Models.DTOs;

// Meeting DTOs
public class MeetingDto
{
    public int Id { get; set; }
    public int AppraisalId { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public int ManagerId { get; set; }
    public string ManagerName { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
    public string Agenda { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public MeetingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public class CreateMeetingDto
{
    public int AppraisalId { get; set; }
    public int EmployeeId { get; set; }
    public int ManagerId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
    public string Agenda { get; set; } = string.Empty;
}

public class UpdateMeetingDto
{
    public DateTime? ScheduledDate { get; set; }
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
    public string? Agenda { get; set; }
    public string? Notes { get; set; }
    public MeetingStatus? Status { get; set; }
}

public class CompleteMeetingDto
{
    public string Notes { get; set; } = string.Empty;
}

// Appeal DTOs
public class AppealDto
{
    public int Id { get; set; }
    public int AppraisalId { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public AppealStatus Status { get; set; }
    public AppealPriority Priority { get; set; }
    public int? ReviewedById { get; set; }
    public string? ReviewedByName { get; set; }
    public string? ReviewComments { get; set; }
    public string? Resolution { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
}

public class CreateAppealDto
{
    public int AppraisalId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public AppealPriority Priority { get; set; } = AppealPriority.Medium;
}

public class UpdateAppealDto
{
    public string? Reason { get; set; }
    public string? Details { get; set; }
    public AppealPriority? Priority { get; set; }
}

public class ReviewAppealDto
{
    public AppealStatus Status { get; set; }
    public string ReviewComments { get; set; } = string.Empty;
    public string? Resolution { get; set; }
}

// Mediation DTOs
public class MediationDto
{
    public int Id { get; set; }
    public int AppealId { get; set; }
    public int HrRepresentativeId { get; set; }
    public string HrRepresentativeName { get; set; } = string.Empty;
    public DateTime? ScheduledDate { get; set; }
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
    public string? Notes { get; set; }
    public string? Outcome { get; set; }
    public MediationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public class CreateMediationDto
{
    public int AppealId { get; set; }
    public int HrRepresentativeId { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
}

public class UpdateMediationDto
{
    public DateTime? ScheduledDate { get; set; }
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
    public string? Notes { get; set; }
    public MediationStatus? Status { get; set; }
}

public class CompleteMediationDto
{
    public string Notes { get; set; } = string.Empty;
    public string Outcome { get; set; } = string.Empty;
}

// Goal DTOs
public class GoalDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int? AppraisalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime TargetDate { get; set; }
    public GoalStatus Status { get; set; }
    public int Progress { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateGoalDto
{
    public int EmployeeId { get; set; }
    public int? AppraisalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime TargetDate { get; set; }
}

public class UpdateGoalDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? TargetDate { get; set; }
    public GoalStatus? Status { get; set; }
    public string? Notes { get; set; }
}

public class UpdateGoalProgressDto
{
    public int Progress { get; set; }
    public GoalStatus? Status { get; set; }
}

// Document DTOs
public class DocumentDto
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public int? AppraisalId { get; set; }
    public int? AppealId { get; set; }
    public int UploadedById { get; set; }
    public string UploadedByName { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}

// Notification DTOs
public class NotificationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string? Link { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
}

// Dashboard DTOs
public class HRDashboardDto
{
    public int TotalAppraisals { get; set; }
    public int PendingAppraisals { get; set; }
    public int CompletedAppraisals { get; set; }
    public int TotalAppeals { get; set; }
    public int PendingAppeals { get; set; }
    public decimal AverageRating { get; set; }
    public int CompletionRate { get; set; }
    public List<RatingDistributionDto> RatingsDistribution { get; set; } = new();
    public List<AppraisalDto> RecentAppraisals { get; set; } = new();
    public List<AppealDto> RecentAppeals { get; set; } = new();
}

public class ManagerDashboardDto
{
    public int TotalDirectReports { get; set; }
    public int PendingReviews { get; set; }
    public int CompletedReviews { get; set; }
    public int UpcomingMeetings { get; set; }
    public List<AppraisalDto> PendingAppraisals { get; set; } = new();
    public List<MeetingDto> UpcomingMeetingsList { get; set; } = new();
}

public class EmployeeDashboardDto
{
    public AppraisalDto? CurrentAppraisal { get; set; }
    public SelfAssessmentDto? CurrentSelfAssessment { get; set; }
    public ManagerReviewDto? LatestReview { get; set; }
    public List<GoalDto> ActiveGoals { get; set; } = new();
    public List<MeetingDto> UpcomingMeetings { get; set; } = new();
    public List<AppealDto> MyAppeals { get; set; } = new();
}

public class RatingDistributionDto
{
    public decimal Rating { get; set; }
    public int Count { get; set; }
}

// Common response DTOs
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
