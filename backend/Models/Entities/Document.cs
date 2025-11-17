namespace PerformanceManagement.Models.Entities;

public class Document
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public int? AppraisalId { get; set; }
    public int? AppealId { get; set; }
    public int UploadedById { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Appraisal? Appraisal { get; set; }
    public Appeal? Appeal { get; set; }
    public User UploadedBy { get; set; } = null!;
}
