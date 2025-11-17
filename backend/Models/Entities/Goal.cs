using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Models.Entities;

public class Goal
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int? AppraisalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime TargetDate { get; set; }
    public GoalStatus Status { get; set; } = GoalStatus.NotStarted;
    public int Progress { get; set; } = 0; // 0-100
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User Employee { get; set; } = null!;
    public Appraisal? Appraisal { get; set; }
}
