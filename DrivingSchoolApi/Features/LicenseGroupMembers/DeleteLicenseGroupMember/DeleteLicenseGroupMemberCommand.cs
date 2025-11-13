using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.LicenseGroupMembers;

public record DeleteLicenseGroupMemberCommand(int GroupId, int LicenseId) : IRequest<bool>;

public class DeleteLicenseGroupMemberHandler : IRequestHandler<DeleteLicenseGroupMemberCommand, bool>
{
    private readonly DrivingSchoolDbContext _db;
    public DeleteLicenseGroupMemberHandler(DrivingSchoolDbContext db) => _db = db;

    public async Task<bool> Handle(DeleteLicenseGroupMemberCommand request, CancellationToken ct)
    {
        var entity = await _db.TbLicenseGroupMembers
            .FirstOrDefaultAsync(x => x.GroupId == request.GroupId && x.LicenseId == request.LicenseId, ct);

        if (entity == null) return false;

        _db.TbLicenseGroupMembers.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
