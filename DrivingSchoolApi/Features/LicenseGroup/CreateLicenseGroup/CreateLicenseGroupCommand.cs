using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.LicenseGroup
{
    public record CreateLicenseGroupCommand(
        string GroupName,
        string? Description
    ) : IRequest<TbLicenseGroup>;

    public class CreateHandler : IRequestHandler<CreateLicenseGroupCommand, TbLicenseGroup>
    {
        private readonly DrivingSchoolDbContext _db;
        public CreateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbLicenseGroup> Handle(CreateLicenseGroupCommand request, CancellationToken ct)
        {
            var entity = new TbLicenseGroup
            {
                GroupName = request.GroupName,
                Description = request.Description
            };

            _db.TbLicenseGroups.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
