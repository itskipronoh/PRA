using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Models.Entities;

public class Appeal
{
    public int Id { get; set; }
    public int AppraisalId { get; set; }
    public int EmployeeId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public AppealStatus Status { get; set; } = AppealStatus.Draft;
    public AppealPriority Priority { get; set; } = AppealPriority.Medium;
    public int? ReviewedById { get; set; }
    public string? ReviewComments { get; set; }
    public string? Resolution { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }

    // Navigation properties
    public Appraisal Appraisal { get; set; } = null!;
    public User Employee { get; set; } = null!;
    public User? ReviewedBy { get; set; }
    public ICollection<Mediation> Mediations { get; set; } = new List<Mediation>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}
