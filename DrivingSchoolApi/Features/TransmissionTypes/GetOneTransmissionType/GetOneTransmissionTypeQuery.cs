using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.TransmissionType
{
    public record GetTransmissionTypeByIdQuery(int TransmissionId) : IRequest<TbTransmissionType>;

    public class GetByIdHandler : IRequestHandler<GetTransmissionTypeByIdQuery, TbTransmissionType>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetByIdHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbTransmissionType> Handle(GetTransmissionTypeByIdQuery request, CancellationToken ct)
        {
            return await _db.TbTransmissionTypes
                .Include(t => t.Vehicles)
                .FirstOrDefaultAsync(t => t.TransmissionId == request.TransmissionId, ct);
        }
    }
}
