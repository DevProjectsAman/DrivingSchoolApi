using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.TransmissionType
{
    public record GetAllTransmissionTypesQuery() : IRequest<List<TbTransmissionType>>;

    public class GetAllHandler : IRequestHandler<GetAllTransmissionTypesQuery, List<TbTransmissionType>>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetAllHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbTransmissionType>> Handle(GetAllTransmissionTypesQuery request, CancellationToken ct)
        {
            return await _db.TbTransmissionTypes
                .Include(t => t.Vehicles)
                .ToListAsync(ct);
        }
    }
}
