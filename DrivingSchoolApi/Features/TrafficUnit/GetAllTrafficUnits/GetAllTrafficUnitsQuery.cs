using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.TrafficUnit
{
    public record GetAllTrafficUnitsQuery() : IRequest<List<TbTrafficUnit>>;

    public class GetAllHandler : IRequestHandler<GetAllTrafficUnitsQuery, List<TbTrafficUnit>>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetAllHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbTrafficUnit>> Handle(GetAllTrafficUnitsQuery request, CancellationToken ct)
        {
            return await _db.TbTrafficUnits.ToListAsync(ct);
        }
    }
}
