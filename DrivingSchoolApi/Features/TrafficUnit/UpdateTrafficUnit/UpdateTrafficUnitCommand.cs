using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.TrafficUnit
{
    public record UpdateTrafficUnitCommand(
        int TrafficUnitId,
        string UnitName,
        string? Location,
        string? Code
    ) : IRequest<TbTrafficUnit>;

    public class UpdateHandler : IRequestHandler<UpdateTrafficUnitCommand, TbTrafficUnit>
    {
        private readonly DrivingSchoolDbContext _db;
        public UpdateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbTrafficUnit> Handle(UpdateTrafficUnitCommand request, CancellationToken ct)
        {
            var entity = await _db.TbTrafficUnits.FindAsync(new object[] { request.TrafficUnitId }, ct);
            if (entity == null) return null;

            entity.UnitName = request.UnitName;
            entity.Location = request.Location;
            entity.Code = request.Code;

            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
