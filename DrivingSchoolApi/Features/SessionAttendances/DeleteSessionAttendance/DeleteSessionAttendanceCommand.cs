using DrivingSchoolApi.Database;
using MediatR;

namespace DrivingSchoolApi.Features.SessionAttendance
{
    public record DeleteSessionAttendanceCommand(int AttendanceId) : IRequest<bool>;

    public class DeleteHandler : IRequestHandler<DeleteSessionAttendanceCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public DeleteHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteSessionAttendanceCommand request, CancellationToken ct)
        {
            var entity = await _db.TbSessionAttendances.FindAsync(new object[] { request.AttendanceId }, ct);
            if (entity == null) return false;

            _db.TbSessionAttendances.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
