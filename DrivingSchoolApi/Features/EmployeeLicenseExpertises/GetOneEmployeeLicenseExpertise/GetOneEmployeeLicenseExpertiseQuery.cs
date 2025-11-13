using MediatR;
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertise
{
    public record GetTbEmployeeLicenseExpertiseByIdQuery(int ExpertiseId) : IRequest<TbEmployeeLicenseExpertise>;

    public class GetByIdHandler : IRequestHandler<GetTbEmployeeLicenseExpertiseByIdQuery, TbEmployeeLicenseExpertise>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetByIdHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbEmployeeLicenseExpertise> Handle(GetTbEmployeeLicenseExpertiseByIdQuery request, CancellationToken ct)
        {
            return await _db.TbEmployeeLicenseExpertises
                .Include(e => e.Employee)
                .Include(e => e.LicenseGroup)
                .FirstOrDefaultAsync(e => e.ExpertiseId == request.ExpertiseId, ct);
        }
    }
}
