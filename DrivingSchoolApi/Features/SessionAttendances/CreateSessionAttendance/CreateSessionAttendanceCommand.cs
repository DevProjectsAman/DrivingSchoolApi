using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using DrivingSchoolApi.Enums;
using MediatR;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.SessionAttendance
{
    public record CreateSessionAttendanceCommand(
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

    public class CreateHandler : IRequestHandler<CreateSessionAttendanceCommand, TbSessionAttendance>
    {
        private readonly DrivingSchoolDbContext _db;
        public CreateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbSessionAttendance> Handle(CreateSessionAttendanceCommand request, CancellationToken ct)
        {
            var entity = new TbSessionAttendance
            {
                ReservationId = request.ReservationId,
                CourseId = request.CourseId,
                InstructorId = request.InstructorId,
                SessionDate = request.SessionDate,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                DurationTime = request.EndTime - request.StartTime,
                AttendanceStatus = request.AttendanceStatus,
                AttendanceDate = request.AttendanceDate,
                Notes = request.Notes
            };

            _db.TbSessionAttendances.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
