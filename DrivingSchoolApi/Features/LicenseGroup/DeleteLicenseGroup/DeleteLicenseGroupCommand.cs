using DrivingSchoolApi.Database;
using MediatR;

namespace DrivingSchoolApi.Features.LicenseGroup
{
    public record DeleteLicenseGroupCommand(int GroupId) : IRequest<bool>;

    public class DeleteHandler : IRequestHandler<DeleteLicenseGroupCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public DeleteHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteLicenseGroupCommand request, CancellationToken ct)
        {
            var entity = await _db.TbLicenseGroups.FindAsync(new object[] { request.GroupId }, ct);
            if (entity == null) return false;

            _db.TbLicenseGroups.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
