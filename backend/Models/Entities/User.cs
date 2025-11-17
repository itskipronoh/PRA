using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? Department { get; set; }
    public string? JobTitle { get; set; }
    public int? ManagerId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime HireDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    // Navigation properties
    public User? Manager { get; set; }
    public ICollection<User> DirectReports { get; set; } = new List<User>();
    public ICollection<Appraisal> AppraisalsAsEmployee { get; set; } = new List<Appraisal>();
    public ICollection<Appraisal> AppraisalsAsManager { get; set; } = new List<Appraisal>();
    public ICollection<SelfAssessment> SelfAssessments { get; set; } = new List<SelfAssessment>();
    public ICollection<ManagerReview> ManagerReviews { get; set; } = new List<ManagerReview>();
    public ICollection<Meeting> MeetingsAsEmployee { get; set; } = new List<Meeting>();
    public ICollection<Meeting> MeetingsAsManager { get; set; } = new List<Meeting>();
    public ICollection<Appeal> Appeals { get; set; } = new List<Appeal>();
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
