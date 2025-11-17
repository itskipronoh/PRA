using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.Entities;

namespace PerformanceManagement.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Appraisal> Appraisals { get; set; }
    public DbSet<SelfAssessment> SelfAssessments { get; set; }
    public DbSet<ManagerReview> ManagerReviews { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<Appeal> Appeals { get; set; }
    public DbSet<Mediation> Mediations { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.EmployeeId).IsUnique();
            entity.HasIndex(e => e.ManagerId);

            entity.HasOne(e => e.Manager)
                .WithMany(e => e.DirectReports)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.EmployeeId).IsRequired().HasMaxLength(50);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
        });

        // Appraisal configuration
        modelBuilder.Entity<Appraisal>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.EmployeeId);
            entity.HasIndex(e => e.ManagerId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.ReviewPeriodEnd);

            entity.HasOne(e => e.Employee)
                .WithMany(e => e.AppraisalsAsEmployee)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Manager)
                .WithMany(e => e.AppraisalsAsManager)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.OverallRating).HasPrecision(3, 2);
        });

        // SelfAssessment configuration
        modelBuilder.Entity<SelfAssessment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.AppraisalId).IsUnique();
            entity.HasIndex(e => e.EmployeeId);

            entity.HasOne(e => e.Appraisal)
                .WithOne(e => e.SelfAssessment)
                .HasForeignKey<SelfAssessment>(e => e.AppraisalId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Employee)
                .WithMany(e => e.SelfAssessments)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.SelfRating).HasPrecision(3, 2);
            entity.Property(e => e.Accomplishments).IsRequired();
            entity.Property(e => e.Challenges).IsRequired();
        });

        // ManagerReview configuration
        modelBuilder.Entity<ManagerReview>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.AppraisalId).IsUnique();
            entity.HasIndex(e => e.ManagerId);

            entity.HasOne(e => e.Appraisal)
                .WithOne(e => e.ManagerReview)
                .HasForeignKey<ManagerReview>(e => e.AppraisalId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Manager)
                .WithMany(e => e.ManagerReviews)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.PerformanceRating).HasPrecision(3, 2).IsRequired();
            entity.Property(e => e.TechnicalSkillsRating).HasPrecision(3, 2).IsRequired();
            entity.Property(e => e.CommunicationRating).HasPrecision(3, 2).IsRequired();
            entity.Property(e => e.TeamworkRating).HasPrecision(3, 2).IsRequired();
            entity.Property(e => e.LeadershipRating).HasPrecision(3, 2).IsRequired();
        });

        // Meeting configuration
        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.EmployeeId);
            entity.HasIndex(e => e.ManagerId);
            entity.HasIndex(e => e.ScheduledDate);
            entity.HasIndex(e => e.Status);

            entity.HasOne(e => e.Appraisal)
                .WithMany(e => e.Meetings)
                .HasForeignKey(e => e.AppraisalId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Employee)
                .WithMany(e => e.MeetingsAsEmployee)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Manager)
                .WithMany(e => e.MeetingsAsManager)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Appeal configuration
        modelBuilder.Entity<Appeal>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.AppraisalId);
            entity.HasIndex(e => e.EmployeeId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.SubmittedAt);

            entity.HasOne(e => e.Appraisal)
                .WithMany(e => e.Appeals)
                .HasForeignKey(e => e.AppraisalId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Employee)
                .WithMany(e => e.Appeals)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ReviewedBy)
                .WithMany()
                .HasForeignKey(e => e.ReviewedById)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.Reason).IsRequired();
            entity.Property(e => e.Details).IsRequired();
        });

        // Mediation configuration
        modelBuilder.Entity<Mediation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.AppealId);
            entity.HasIndex(e => e.HrRepresentativeId);

            entity.HasOne(e => e.Appeal)
                .WithMany(e => e.Mediations)
                .HasForeignKey(e => e.AppealId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.HrRepresentative)
                .WithMany()
                .HasForeignKey(e => e.HrRepresentativeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Goal configuration
        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.EmployeeId);
            entity.HasIndex(e => e.AppraisalId);
            entity.HasIndex(e => e.Status);

            entity.HasOne(e => e.Employee)
                .WithMany(e => e.Goals)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Appraisal)
                .WithMany(e => e.Goals)
                .HasForeignKey(e => e.AppraisalId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
        });

        // Document configuration
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.AppraisalId);
            entity.HasIndex(e => e.AppealId);
            entity.HasIndex(e => e.UploadedById);

            entity.HasOne(e => e.Appraisal)
                .WithMany(e => e.Documents)
                .HasForeignKey(e => e.AppraisalId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Appeal)
                .WithMany(e => e.Documents)
                .HasForeignKey(e => e.AppealId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.UploadedBy)
                .WithMany()
                .HasForeignKey(e => e.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.FilePath).IsRequired();
        });

        // Notification configuration
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.IsRead);
            entity.HasIndex(e => e.CreatedAt);

            entity.HasOne(e => e.User)
                .WithMany(e => e.Notifications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Message).IsRequired();
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed an admin user with default password "Admin@123"
        // Password hash for "Admin@123" using BCrypt
        var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                EmployeeId = "EMP001",
                Email = "admin@performancemanagement.com",
                PasswordHash = adminPasswordHash,
                FirstName = "System",
                LastName = "Administrator",
                Role = Models.Enums.UserRole.Admin,
                Department = "IT",
                JobTitle = "System Administrator",
                IsActive = true,
                HireDate = new DateTime(2020, 1, 1),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
