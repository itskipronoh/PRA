using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Models.Entities;

public class Meeting
{
    public int Id { get; set; }
    public int AppraisalId { get; set; }
    public int EmployeeId { get; set; }
    public int ManagerId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
    public string Agenda { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public MeetingStatus Status { get; set; } = MeetingStatus.Scheduled;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    public Appraisal Appraisal { get; set; } = null!;
    public User Employee { get; set; } = null!;
    public User Manager { get; set; } = null!;
}
