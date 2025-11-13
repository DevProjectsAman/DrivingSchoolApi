using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.TrafficUnit
{
    public record CreateTrafficUnitCommand(
        string UnitName,
        string? Location,
        string? Code
    ) : IRequest<TbTrafficUnit>;

    public class CreateHandler : IRequestHandler<CreateTrafficUnitCommand, TbTrafficUnit>
    {
        private readonly DrivingSchoolDbContext _db;
        public CreateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbTrafficUnit> Handle(CreateTrafficUnitCommand request, CancellationToken ct)
        {
            var entity = new TbTrafficUnit
            {
                UnitName = request.UnitName,
                Location = request.Location,
                Code = request.Code
            };

            _db.TbTrafficUnits.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
