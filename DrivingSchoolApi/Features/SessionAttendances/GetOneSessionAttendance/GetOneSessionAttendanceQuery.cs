using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.SessionAttendance
{
    public record GetSessionAttendanceByIdQuery(int AttendanceId) : IRequest<TbSessionAttendance>;

    public class GetByIdHandler : IRequestHandler<GetSessionAttendanceByIdQuery, TbSessionAttendance>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetByIdHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbSessionAttendance> Handle(GetSessionAttendanceByIdQuery request, CancellationToken ct)
        {
            return await _db.TbSessionAttendances
                .Include(s => s.Reservation)
                .Include(s => s.Course)
                .Include(s => s.Instructor)
                .FirstOrDefaultAsync(s => s.AttendanceId == request.AttendanceId, ct);
        }
    }
}
