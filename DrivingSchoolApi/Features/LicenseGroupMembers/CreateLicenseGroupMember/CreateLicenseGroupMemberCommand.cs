using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.LicenseGroupMembers;

public record CreateLicenseGroupMemberCommand(
    int GroupId,
    int LicenseId
) : IRequest<TbLicenseGroupMember>;

public class CreateLicenseGroupMemberHandler : IRequestHandler<CreateLicenseGroupMemberCommand, TbLicenseGroupMember>
{
    private readonly DrivingSchoolDbContext _db;
    public CreateLicenseGroupMemberHandler(DrivingSchoolDbContext db) => _db = db;

    public async Task<TbLicenseGroupMember> Handle(CreateLicenseGroupMemberCommand request, CancellationToken ct)
    {
        var entity = new TbLicenseGroupMember
        {
            GroupId = request.GroupId,
            LicenseId = request.LicenseId
        };

        _db.TbLicenseGroupMembers.Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }
}
