using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Models.Entities;

public class Appraisal
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int ManagerId { get; set; }
    public DateTime ReviewPeriodStart { get; set; }
    public DateTime ReviewPeriodEnd { get; set; }
    public AppraisalStatus Status { get; set; } = AppraisalStatus.Draft;
    public decimal? OverallRating { get; set; }
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    public User Employee { get; set; } = null!;
    public User Manager { get; set; } = null!;
    public SelfAssessment? SelfAssessment { get; set; }
    public ManagerReview? ManagerReview { get; set; }
    public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public ICollection<Appeal> Appeals { get; set; } = new List<Appeal>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}
