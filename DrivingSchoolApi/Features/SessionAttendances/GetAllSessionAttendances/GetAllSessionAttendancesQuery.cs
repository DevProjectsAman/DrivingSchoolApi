using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.SessionAttendance
{
    public record GetAllSessionAttendancesQuery() : IRequest<List<TbSessionAttendance>>;

    public class GetAllHandler : IRequestHandler<GetAllSessionAttendancesQuery, List<TbSessionAttendance>>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetAllHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbSessionAttendance>> Handle(GetAllSessionAttendancesQuery request, CancellationToken ct)
        {
            return await _db.TbSessionAttendances
                .Include(s => s.Reservation)
                .Include(s => s.Course)
                .Include(s => s.Instructor)
                .ToListAsync(ct);
        }
    }
}
