using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.LicenseGroup
{
    public record UpdateLicenseGroupCommand(
        int GroupId,
        string GroupName,
        string? Description
    ) : IRequest<TbLicenseGroup>;

    public class UpdateHandler : IRequestHandler<UpdateLicenseGroupCommand, TbLicenseGroup>
    {
        private readonly DrivingSchoolDbContext _db;
        public UpdateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbLicenseGroup> Handle(UpdateLicenseGroupCommand request, CancellationToken ct)
        {
            var entity = await _db.TbLicenseGroups.FindAsync(new object[] { request.GroupId }, ct);
            if (entity == null) return null;

            entity.GroupName = request.GroupName;
            entity.Description = request.Description;

            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
