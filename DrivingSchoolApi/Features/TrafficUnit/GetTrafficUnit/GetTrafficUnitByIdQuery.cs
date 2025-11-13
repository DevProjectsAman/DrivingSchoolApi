using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.TrafficUnit
{
    public record GetTrafficUnitByIdQuery(int TrafficUnitId) : IRequest<TbTrafficUnit>;

    public class GetByIdHandler : IRequestHandler<GetTrafficUnitByIdQuery, TbTrafficUnit>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetByIdHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbTrafficUnit> Handle(GetTrafficUnitByIdQuery request, CancellationToken ct)
        {
            return await _db.TbTrafficUnits.FindAsync(new object[] { request.TrafficUnitId }, ct);
        }
    }
}
