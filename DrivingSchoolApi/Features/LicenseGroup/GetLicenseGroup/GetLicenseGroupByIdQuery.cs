using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.LicenseGroup
{
    public record GetLicenseGroupByIdQuery(int GroupId) : IRequest<TbLicenseGroup>;

    public class GetByIdHandler : IRequestHandler<GetLicenseGroupByIdQuery, TbLicenseGroup>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetByIdHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbLicenseGroup> Handle(GetLicenseGroupByIdQuery request, CancellationToken ct)
        {
            return await _db.TbLicenseGroups
                .Include(g => g.LicenseGroupMembers)
                .Include(g => g.EmployeeExpertises)
                .FirstOrDefaultAsync(g => g.GroupId == request.GroupId, ct);
        }
    }
}
