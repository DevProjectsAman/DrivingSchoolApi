using DrivingSchoolApi.Database;
using MediatR;

namespace DrivingSchoolApi.Features.TrafficUnit
{
    public record DeleteTrafficUnitCommand(int TrafficUnitId) : IRequest<bool>;

    public class DeleteHandler : IRequestHandler<DeleteTrafficUnitCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public DeleteHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteTrafficUnitCommand request, CancellationToken ct)
        {
            var entity = await _db.TbTrafficUnits.FindAsync(new object[] { request.TrafficUnitId }, ct);
            if (entity == null) return false;

            _db.TbTrafficUnits.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
