using MediatR;
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertise
{
    public record UpdateTbEmployeeLicenseExpertiseCommand(
        int ExpertiseId,
        int EmployeeId,
        int LicenseGroupId,
        bool CanTeachTheory,
        bool CanTeachPractical,
        DateTime? CertificationDate
    ) : IRequest<TbEmployeeLicenseExpertise>;

    public class UpdateHandler : IRequestHandler<UpdateTbEmployeeLicenseExpertiseCommand, TbEmployeeLicenseExpertise>
    {
        private readonly DrivingSchoolDbContext _db;
        public UpdateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbEmployeeLicenseExpertise> Handle(UpdateTbEmployeeLicenseExpertiseCommand request, CancellationToken ct)
        {
            var entity = await _db.TbEmployeeLicenseExpertises.FindAsync(new object[] { request.ExpertiseId }, ct);
            if (entity == null) return null;

            entity.EmployeeId = request.EmployeeId;
            entity.LicenseGroupId = request.LicenseGroupId;
            entity.CanTeachTheory = request.CanTeachTheory;
            entity.CanTeachPractical = request.CanTeachPractical;
            entity.CertificationDate = request.CertificationDate;

            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
