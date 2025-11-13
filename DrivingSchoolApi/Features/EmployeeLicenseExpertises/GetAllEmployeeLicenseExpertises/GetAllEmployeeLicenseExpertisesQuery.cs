using MediatR;
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertise
{
    public record GetAllTbEmployeeLicenseExpertisesQuery() : IRequest<List<TbEmployeeLicenseExpertise>>;

    public class GetAllHandler : IRequestHandler<GetAllTbEmployeeLicenseExpertisesQuery, List<TbEmployeeLicenseExpertise>>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetAllHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbEmployeeLicenseExpertise>> Handle(GetAllTbEmployeeLicenseExpertisesQuery request, CancellationToken ct)
        {
            return await _db.TbEmployeeLicenseExpertises
                .Include(e => e.Employee)
                .Include(e => e.LicenseGroup)
                .ToListAsync(ct);
        }
    }
}
