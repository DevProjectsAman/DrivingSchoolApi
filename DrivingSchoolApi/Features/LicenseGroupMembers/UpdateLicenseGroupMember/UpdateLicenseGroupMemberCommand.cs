using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.LicenseGroupMembers;

public record UpdateLicenseGroupMemberCommand(
    int GroupId,
    int LicenseId,
    int NewLicenseId
) : IRequest<TbLicenseGroupMember?>;

public class UpdateLicenseGroupMemberHandler : IRequestHandler<UpdateLicenseGroupMemberCommand, TbLicenseGroupMember?>
{
    private readonly DrivingSchoolDbContext _db;
    public UpdateLicenseGroupMemberHandler(DrivingSchoolDbContext db) => _db = db;

    public async Task<TbLicenseGroupMember?> Handle(UpdateLicenseGroupMemberCommand request, CancellationToken ct)
    {
        var entity = await _db.TbLicenseGroupMembers
            .FirstOrDefaultAsync(x => x.GroupId == request.GroupId && x.LicenseId == request.LicenseId, ct);

        if (entity == null) return null;

        entity.LicenseId = request.NewLicenseId;

        await _db.SaveChangesAsync(ct);
        return entity;
    }
}
