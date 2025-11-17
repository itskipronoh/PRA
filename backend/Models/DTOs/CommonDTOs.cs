using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Models.DTOs;

// Auth DTOs
public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public UserDto User { get; set; } = null!;
}

public class RefreshTokenDto
{
    public string RefreshToken { get; set; } = string.Empty;
}

public class ChangePasswordDto
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

// User DTOs
public class UserDto
{
    public int Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? Department { get; set; }
    public string? JobTitle { get; set; }
    public int? ManagerId { get; set; }
    public string? ManagerName { get; set; }
    public bool IsActive { get; set; }
    public DateTime HireDate { get; set; }
}

public class CreateUserDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? Department { get; set; }
    public string? JobTitle { get; set; }
    public int? ManagerId { get; set; }
    public DateTime HireDate { get; set; }
}

public class UpdateUserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Department { get; set; }
    public string? JobTitle { get; set; }
    public int? ManagerId { get; set; }
    public bool? IsActive { get; set; }
}

// Appraisal DTOs
public class AppraisalDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public int ManagerId { get; set; }
    public string ManagerName { get; set; } = string.Empty;
    public DateTime ReviewPeriodStart { get; set; }
    public DateTime ReviewPeriodEnd { get; set; }
    public AppraisalStatus Status { get; set; }
    public decimal? OverallRating { get; set; }
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public class CreateAppraisalDto
{
    public int EmployeeId { get; set; }
    public int ManagerId { get; set; }
    public DateTime ReviewPeriodStart { get; set; }
    public DateTime ReviewPeriodEnd { get; set; }
    public string? Comments { get; set; }
}

public class UpdateAppraisalDto
{
    public DateTime? ReviewPeriodStart { get; set; }
    public DateTime? ReviewPeriodEnd { get; set; }
    public AppraisalStatus? Status { get; set; }
    public decimal? OverallRating { get; set; }
    public string? Comments { get; set; }
}

// Self Assessment DTOs
public class SelfAssessmentDto
{
    public int Id { get; set; }
    public int AppraisalId { get; set; }
    public int EmployeeId { get; set; }
    public string Accomplishments { get; set; } = string.Empty;
    public string Challenges { get; set; } = string.Empty;
    public string GoalsAchieved { get; set; } = string.Empty;
    public string FutureDevelopment { get; set; } = string.Empty;
    public decimal? SelfRating { get; set; }
    public string? AdditionalComments { get; set; }
    public AssessmentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SubmittedAt { get; set; }
}

public class CreateSelfAssessmentDto
{
    public int AppraisalId { get; set; }
    public string Accomplishments { get; set; } = string.Empty;
    public string Challenges { get; set; } = string.Empty;
    public string GoalsAchieved { get; set; } = string.Empty;
    public string FutureDevelopment { get; set; } = string.Empty;
    public decimal? SelfRating { get; set; }
    public string? AdditionalComments { get; set; }
}

public class UpdateSelfAssessmentDto
{
    public string? Accomplishments { get; set; }
    public string? Challenges { get; set; }
    public string? GoalsAchieved { get; set; }
    public string? FutureDevelopment { get; set; }
    public decimal? SelfRating { get; set; }
    public string? AdditionalComments { get; set; }
}

// Manager Review DTOs
public class ManagerReviewDto
{
    public int Id { get; set; }
    public int AppraisalId { get; set; }
    public int ManagerId { get; set; }
    public decimal PerformanceRating { get; set; }
    public decimal TechnicalSkillsRating { get; set; }
    public decimal CommunicationRating { get; set; }
    public decimal TeamworkRating { get; set; }
    public decimal LeadershipRating { get; set; }
    public string Strengths { get; set; } = string.Empty;
    public string AreasForImprovement { get; set; } = string.Empty;
    public string ManagerComments { get; set; } = string.Empty;
    public string RecommendedActions { get; set; } = string.Empty;
    public AssessmentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SubmittedAt { get; set; }
}

public class CreateManagerReviewDto
{
    public int AppraisalId { get; set; }
    public decimal PerformanceRating { get; set; }
    public decimal TechnicalSkillsRating { get; set; }
    public decimal CommunicationRating { get; set; }
    public decimal TeamworkRating { get; set; }
    public decimal LeadershipRating { get; set; }
    public string Strengths { get; set; } = string.Empty;
    public string AreasForImprovement { get; set; } = string.Empty;
    public string ManagerComments { get; set; } = string.Empty;
    public string RecommendedActions { get; set; } = string.Empty;
}

public class UpdateManagerReviewDto
{
    public decimal? PerformanceRating { get; set; }
    public decimal? TechnicalSkillsRating { get; set; }
    public decimal? CommunicationRating { get; set; }
    public decimal? TeamworkRating { get; set; }
    public decimal? LeadershipRating { get; set; }
    public string? Strengths { get; set; }
    public string? AreasForImprovement { get; set; }
    public string? ManagerComments { get; set; }
    public string? RecommendedActions { get; set; }
}
