using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Models.Entities;

public class SelfAssessment
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
    public AssessmentStatus Status { get; set; } = AssessmentStatus.Draft;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SubmittedAt { get; set; }

    // Navigation properties
    public Appraisal Appraisal { get; set; } = null!;
    public User Employee { get; set; } = null!;
}
