using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Models.Entities;

public class ManagerReview
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
    public AssessmentStatus Status { get; set; } = AssessmentStatus.Draft;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SubmittedAt { get; set; }

    // Navigation properties
    public Appraisal Appraisal { get; set; } = null!;
    public User Manager { get; set; } = null!;
}
