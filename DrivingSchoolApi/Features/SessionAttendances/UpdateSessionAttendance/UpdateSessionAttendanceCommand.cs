using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using DrivingSchoolApi.Enums;
using MediatR;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.SessionAttendance
{
    public record UpdateSessionAttendanceCommand(
        int AttendanceId,
        int ReservationId,
        int CourseId,
        int InstructorId,
        DateTime SessionDate,
        TimeSpan StartTime,
        TimeSpan EndTime,
        AttendanceStatus AttendanceStatus,
        DateTime? AttendanceDate,
        string Notes
    ) : IRequest<TbSessionAttendance>;

    public class UpdateHandler : IRequestHandler<UpdateSessionAttendanceCommand, TbSessionAttendance>
    {
        private readonly DrivingSchoolDbContext _db;
        public UpdateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbSessionAttendance> Handle(UpdateSessionAttendanceCommand request, CancellationToken ct)
        {
            var entity = await _db.TbSessionAttendances.FindAsync(new object[] { request.AttendanceId }, ct);
            if (entity == null) return null;

            entity.ReservationId = request.ReservationId;
            entity.CourseId = request.CourseId;
            entity.InstructorId = request.InstructorId;
            entity.SessionDate = request.SessionDate;
            entity.StartTime = request.StartTime;
            entity.EndTime = request.EndTime;
            entity.DurationTime = request.EndTime - request.StartTime;
            entity.AttendanceStatus = request.AttendanceStatus;
            entity.AttendanceDate = request.AttendanceDate;
            entity.Notes = request.Notes;

            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
