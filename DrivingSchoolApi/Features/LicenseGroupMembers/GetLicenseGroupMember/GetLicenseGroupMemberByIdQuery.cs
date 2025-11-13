using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.LicenseGroupMembers;

public record GetLicenseGroupMemberByIdQuery(int GroupId, int LicenseId) : IRequest<TbLicenseGroupMember?>;

public class GetLicenseGroupMemberByIdHandler : IRequestHandler<GetLicenseGroupMemberByIdQuery, TbLicenseGroupMember?>
{
    private readonly DrivingSchoolDbContext _db;
    public GetLicenseGroupMemberByIdHandler(DrivingSchoolDbContext db) => _db = db;

    public async Task<TbLicenseGroupMember?> Handle(GetLicenseGroupMemberByIdQuery request, CancellationToken ct)
    {
        return await _db.TbLicenseGroupMembers
            .Include(x => x.LicenseGroup)
            .Include(x => x.LicenseType)
            .FirstOrDefaultAsync(x => x.GroupId == request.GroupId && x.LicenseId == request.LicenseId, ct);
    }
}
