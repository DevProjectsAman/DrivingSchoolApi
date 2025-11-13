using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.LicenseGroup
{
    public record GetAllLicenseGroupsQuery() : IRequest<List<TbLicenseGroup>>;

    public class GetAllHandler : IRequestHandler<GetAllLicenseGroupsQuery, List<TbLicenseGroup>>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetAllHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbLicenseGroup>> Handle(GetAllLicenseGroupsQuery request, CancellationToken ct)
        {
            return await _db.TbLicenseGroups
                .Include(g => g.LicenseGroupMembers)
                .Include(g => g.EmployeeExpertises)
                .ToListAsync(ct);
        }
    }
}
