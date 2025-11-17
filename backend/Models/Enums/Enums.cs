namespace PerformanceManagement.Models.Enums;

public enum UserRole
{
    Employee,
    Manager,
    HR,
    Admin
}

public enum AppraisalStatus
{
    Draft,
    Submitted,
    UnderReview,
    Completed,
    Appealed
}

public enum AssessmentStatus
{
    Draft,
    Submitted
}

public enum MeetingStatus
{
    Scheduled,
    Completed,
    Cancelled,
    Rescheduled
}

public enum AppealStatus
{
    Draft,
    Submitted,
    UnderReview,
    Approved,
    Rejected,
    PendingMediation
}

public enum AppealPriority
{
    Low,
    Medium,
    High,
    Urgent
}

public enum MediationStatus
{
    Pending,
    Scheduled,
    InProgress,
    Completed
}

public enum GoalStatus
{
    NotStarted,
    InProgress,
    Completed,
    Cancelled
}

public enum NotificationType
{
    AppraisalCreated,
    SelfAssessmentSubmitted,
    ManagerReviewSubmitted,
    MeetingScheduled,
    MeetingCompleted,
    AppealSubmitted,
    AppealReviewed,
    MediationScheduled,
    General
}
