using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Data;
using PerformanceManagement.Models.Entities;
using PerformanceManagement.Models.Enums;

namespace PerformanceManagement.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByEmployeeIdAsync(string employeeId);
    Task<IEnumerable<User>> GetDirectReportsAsync(int managerId);
    Task<User?> GetWithManagerAsync(int id);
}

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmployeeIdAsync(string employeeId)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.EmployeeId == employeeId);
    }

    public async Task<IEnumerable<User>> GetDirectReportsAsync(int managerId)
    {
        return await _dbSet.Where(u => u.ManagerId == managerId).ToListAsync();
    }

    public async Task<User?> GetWithManagerAsync(int id)
    {
        return await _dbSet
            .Include(u => u.Manager)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}

public interface IAppraisalRepository : IRepository<Appraisal>
{
    Task<Appraisal?> GetWithDetailsAsync(int id);
    Task<IEnumerable<Appraisal>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<Appraisal>> GetByManagerIdAsync(int managerId);
    Task<IEnumerable<Appraisal>> GetByStatusAsync(AppraisalStatus status);
}

public class AppraisalRepository : Repository<Appraisal>, IAppraisalRepository
{
    public AppraisalRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Appraisal?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(a => a.Employee)
            .Include(a => a.Manager)
            .Include(a => a.SelfAssessment)
            .Include(a => a.ManagerReview)
            .Include(a => a.Meetings)
            .Include(a => a.Goals)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Appraisal>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _dbSet
            .Include(a => a.Employee)
            .Include(a => a.Manager)
            .Where(a => a.EmployeeId == employeeId)
            .OrderByDescending(a => a.ReviewPeriodEnd)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appraisal>> GetByManagerIdAsync(int managerId)
    {
        return await _dbSet
            .Include(a => a.Employee)
            .Include(a => a.Manager)
            .Where(a => a.ManagerId == managerId)
            .OrderByDescending(a => a.ReviewPeriodEnd)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appraisal>> GetByStatusAsync(AppraisalStatus status)
    {
        return await _dbSet
            .Include(a => a.Employee)
            .Include(a => a.Manager)
            .Where(a => a.Status == status)
            .ToListAsync();
    }
}

public interface ISelfAssessmentRepository : IRepository<SelfAssessment>
{
    Task<SelfAssessment?> GetByAppraisalIdAsync(int appraisalId);
    Task<IEnumerable<SelfAssessment>> GetByEmployeeIdAsync(int employeeId);
}

public class SelfAssessmentRepository : Repository<SelfAssessment>, ISelfAssessmentRepository
{
    public SelfAssessmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<SelfAssessment?> GetByAppraisalIdAsync(int appraisalId)
    {
        return await _dbSet
            .Include(s => s.Appraisal)
            .Include(s => s.Employee)
            .FirstOrDefaultAsync(s => s.AppraisalId == appraisalId);
    }

    public async Task<IEnumerable<SelfAssessment>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _dbSet
            .Include(s => s.Appraisal)
            .Where(s => s.EmployeeId == employeeId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }
}

public interface IManagerReviewRepository : IRepository<ManagerReview>
{
    Task<ManagerReview?> GetByAppraisalIdAsync(int appraisalId);
    Task<IEnumerable<ManagerReview>> GetByManagerIdAsync(int managerId);
}

public class ManagerReviewRepository : Repository<ManagerReview>, IManagerReviewRepository
{
    public ManagerReviewRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ManagerReview?> GetByAppraisalIdAsync(int appraisalId)
    {
        return await _dbSet
            .Include(m => m.Appraisal)
            .Include(m => m.Manager)
            .FirstOrDefaultAsync(m => m.AppraisalId == appraisalId);
    }

    public async Task<IEnumerable<ManagerReview>> GetByManagerIdAsync(int managerId)
    {
        return await _dbSet
            .Include(m => m.Appraisal)
            .Where(m => m.ManagerId == managerId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }
}

public interface IMeetingRepository : IRepository<Meeting>
{
    Task<IEnumerable<Meeting>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<Meeting>> GetByManagerIdAsync(int managerId);
    Task<IEnumerable<Meeting>> GetUpcomingMeetingsAsync(int userId);
}

public class MeetingRepository : Repository<Meeting>, IMeetingRepository
{
    public MeetingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Meeting>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _dbSet
            .Include(m => m.Employee)
            .Include(m => m.Manager)
            .Include(m => m.Appraisal)
            .Where(m => m.EmployeeId == employeeId)
            .OrderByDescending(m => m.ScheduledDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Meeting>> GetByManagerIdAsync(int managerId)
    {
        return await _dbSet
            .Include(m => m.Employee)
            .Include(m => m.Manager)
            .Include(m => m.Appraisal)
            .Where(m => m.ManagerId == managerId)
            .OrderByDescending(m => m.ScheduledDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Meeting>> GetUpcomingMeetingsAsync(int userId)
    {
        var now = DateTime.UtcNow;
        return await _dbSet
            .Include(m => m.Employee)
            .Include(m => m.Manager)
            .Where(m => (m.EmployeeId == userId || m.ManagerId == userId) && 
                       m.ScheduledDate > now && 
                       m.Status == MeetingStatus.Scheduled)
            .OrderBy(m => m.ScheduledDate)
            .ToListAsync();
    }
}

public interface IAppealRepository : IRepository<Appeal>
{
    Task<IEnumerable<Appeal>> GetByEmployeeIdAsync(int employeeId);
    Task<Appeal?> GetWithDetailsAsync(int id);
}

public class AppealRepository : Repository<Appeal>, IAppealRepository
{
    public AppealRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Appeal>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _dbSet
            .Include(a => a.Appraisal)
            .Include(a => a.Employee)
            .Where(a => a.EmployeeId == employeeId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<Appeal?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(a => a.Appraisal)
            .Include(a => a.Employee)
            .Include(a => a.ReviewedBy)
            .Include(a => a.Mediations)
            .Include(a => a.Documents)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}

public interface IMediationRepository : IRepository<Mediation>
{
    Task<IEnumerable<Mediation>> GetByAppealIdAsync(int appealId);
    Task<IEnumerable<Mediation>> GetByHrRepresentativeIdAsync(int hrRepId);
}

public class MediationRepository : Repository<Mediation>, IMediationRepository
{
    public MediationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Mediation>> GetByAppealIdAsync(int appealId)
    {
        return await _dbSet
            .Include(m => m.Appeal)
            .Include(m => m.HrRepresentative)
            .Where(m => m.AppealId == appealId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Mediation>> GetByHrRepresentativeIdAsync(int hrRepId)
    {
        return await _dbSet
            .Include(m => m.Appeal)
            .Where(m => m.HrRepresentativeId == hrRepId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }
}

public interface IGoalRepository : IRepository<Goal>
{
    Task<IEnumerable<Goal>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<Goal>> GetActiveGoalsAsync(int employeeId);
}

public class GoalRepository : Repository<Goal>, IGoalRepository
{
    public GoalRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Goal>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _dbSet
            .Include(g => g.Employee)
            .Include(g => g.Appraisal)
            .Where(g => g.EmployeeId == employeeId)
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Goal>> GetActiveGoalsAsync(int employeeId)
    {
        return await _dbSet
            .Where(g => g.EmployeeId == employeeId && 
                       (g.Status == GoalStatus.NotStarted || g.Status == GoalStatus.InProgress))
            .OrderBy(g => g.TargetDate)
            .ToListAsync();
    }
}

public interface IDocumentRepository : IRepository<Document>
{
    Task<IEnumerable<Document>> GetByAppraisalIdAsync(int appraisalId);
    Task<IEnumerable<Document>> GetByAppealIdAsync(int appealId);
}

public class DocumentRepository : Repository<Document>, IDocumentRepository
{
    public DocumentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Document>> GetByAppraisalIdAsync(int appraisalId)
    {
        return await _dbSet
            .Include(d => d.UploadedBy)
            .Where(d => d.AppraisalId == appraisalId)
            .OrderByDescending(d => d.UploadedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Document>> GetByAppealIdAsync(int appealId)
    {
        return await _dbSet
            .Include(d => d.UploadedBy)
            .Where(d => d.AppealId == appealId)
            .OrderByDescending(d => d.UploadedAt)
            .ToListAsync();
    }
}

public interface INotificationRepository : IRepository<Notification>
{
    Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
    Task<int> GetUnreadCountAsync(int userId);
    Task MarkAsReadAsync(int id);
    Task MarkAllAsReadAsync(int userId);
}

public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Notification>> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> GetUnreadCountAsync(int userId)
    {
        return await _dbSet
            .Where(n => n.UserId == userId && !n.IsRead)
            .CountAsync();
    }

    public async Task MarkAsReadAsync(int id)
    {
        var notification = await _dbSet.FindAsync(id);
        if (notification != null && !notification.IsRead)
        {
            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        var unreadNotifications = await _dbSet
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToListAsync();

        foreach (var notification in unreadNotifications)
        {
            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }
}
