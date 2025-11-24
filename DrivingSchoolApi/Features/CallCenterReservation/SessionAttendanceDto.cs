namespace DrivingSchoolApi.Features.CallCenterReservation
{


    // ==================== CREATE SCHEDULE ====================
    public record CreateSessionScheduleRequest
    {
        public int ReservationId { get; init; }
        public DateTime StartDate { get; init; }
        public List<SessionItemRequest> Sessions { get; init; } = new();
    }

    public record SessionItemRequest
    {
        public int CourseId { get; init; }
        public int InstructorId { get; init; }
        public DateTime SessionDate { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan DurationTime { get; init; }
        public string? Notes { get; init; }
    }

    public record SessionScheduleCreatedDto
    {
        public int ReservationId { get; init; }
        public string CustomerName { get; init; } = string.Empty;
        public string LicenseName { get; init; } = string.Empty;
        public int TotalSessionsCreated { get; init; }
        public List<SessionCreatedDto> Sessions { get; init; } = new();
    }

    public record SessionCreatedDto
    {
        public int AttendanceId { get; init; }
        public int CourseId { get; init; }
        public string CourseName { get; init; } = string.Empty;
        public string SessionType { get; init; } = string.Empty;
        public string InstructorName { get; init; } = string.Empty;
        public DateTime SessionDate { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
        public string Status { get; init; } = string.Empty;
    }

    // ==================== AVAILABLE INSTRUCTORS ====================
    public record AvailableInstructorDto
    {
        public int InstructorId { get; init; }
        public string InstructorName { get; init; } = string.Empty;
        public string RoleName { get; init; } = string.Empty;
        public bool CanTeachTheory { get; init; }
        public bool CanTeachPractical { get; init; }
        public List<string> ExpertiseLicenses { get; init; } = new();
        public int ScheduledSessionsCount { get; init; }
    }

    // ==================== STUDENT SCHEDULE ====================
    public record StudentScheduleDto
    {
        public int CustomerId { get; init; }
        public string CustomerName { get; init; } = string.Empty;
        public int ReservationId { get; init; }
        public string LicenseName { get; init; } = string.Empty;
        public string SchoolName { get; init; } = string.Empty;
        public int TotalSessions { get; init; }
        public int CompletedSessions { get; init; }
        public int PendingSessions { get; init; }
        public int AbsentSessions { get; init; }
        public List<SessionDetailDto> Sessions { get; init; } = new();
    }

    public record SessionDetailDto
    {
        public int AttendanceId { get; init; }
        public string CourseName { get; init; } = string.Empty;
        public string SessionType { get; init; } = string.Empty;
        public string InstructorName { get; init; } = string.Empty;
        public DateTime SessionDate { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
        public TimeSpan DurationTime { get; init; }
        public string Status { get; init; } = string.Empty;
        public DateTime? AttendanceDate { get; init; }
        public string? Notes { get; init; }
    }

    // ==================== INSTRUCTOR SCHEDULE ====================
    public record InstructorScheduleDto
    {
        public int InstructorId { get; init; }
        public string InstructorName { get; init; } = string.Empty;
        public string RoleName { get; init; } = string.Empty;
        public DateTime Date { get; init; }
        public int TotalSessions { get; init; }
        public List<InstructorSessionDto> Sessions { get; init; } = new();
    }

    public record InstructorSessionDto
    {
        public int AttendanceId { get; init; }
        public string StudentName { get; init; } = string.Empty;
        public string CourseName { get; init; } = string.Empty;
        public string SessionType { get; init; } = string.Empty;
        public DateTime SessionDate { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
        public double DurationHours { get; init; }
        public string Status { get; init; } = string.Empty;
        public string? Notes { get; init; }
    }

    // ==================== MARK ATTENDANCE ====================
    public record MarkAttendanceRequest
    {
        public int AttendanceId { get; init; }
        public string Status { get; init; } = string.Empty; // Present, Absent, Excused
        public string? Notes { get; init; }
    }

    public record MarkAttendanceResponse
    {
        public int AttendanceId { get; init; }
        public string Status { get; init; } = string.Empty;
        public DateTime AttendanceDate { get; init; }
        public string? Notes { get; init; }
    }

    // ==================== RESCHEDULE SESSION ====================
    public record RescheduleSessionRequest
    {
        public int AttendanceId { get; init; }
        public DateTime NewDate { get; init; }
        public TimeSpan NewStartTime { get; init; }
        public int? NewInstructorId { get; init; }
        public string? Reason { get; init; }
    }

    public record RescheduleSessionResponse
    {
        public int AttendanceId { get; init; }
        public OldScheduleInfo OldSchedule { get; init; } = null!;
        public NewScheduleInfo NewSchedule { get; init; } = null!;
    }

    public record OldScheduleInfo
    {
        public DateTime Date { get; init; }
        public TimeSpan Time { get; init; }
        public string Instructor { get; init; } = string.Empty;
    }

    public record NewScheduleInfo
    {
        public DateTime SessionDate { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
        public string InstructorName { get; init; } = string.Empty;
    }
}
