using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Models.Entities;

public class Mediation
{
    public int Id { get; set; }
    public int AppealId { get; set; }
    public int HrRepresentativeId { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
    public string? Notes { get; set; }
    public string? Outcome { get; set; }
    public MediationStatus Status { get; set; } = MediationStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    public Appeal Appeal { get; set; } = null!;
    public User HrRepresentative { get; set; } = null!;
}
