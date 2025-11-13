using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.LicenseGroupMembers;

public record GetAllLicenseGroupMembersQuery() : IRequest<List<TbLicenseGroupMember>>;

public class GetAllLicenseGroupMembersHandler : IRequestHandler<GetAllLicenseGroupMembersQuery, List<TbLicenseGroupMember>>
{
    private readonly DrivingSchoolDbContext _db;
    public GetAllLicenseGroupMembersHandler(DrivingSchoolDbContext db) => _db = db;

    public async Task<List<TbLicenseGroupMember>> Handle(GetAllLicenseGroupMembersQuery request, CancellationToken ct)
    {
        return await _db.TbLicenseGroupMembers
            .Include(x => x.LicenseGroup)
            .Include(x => x.LicenseType)
            .ToListAsync(ct);
    }
}
