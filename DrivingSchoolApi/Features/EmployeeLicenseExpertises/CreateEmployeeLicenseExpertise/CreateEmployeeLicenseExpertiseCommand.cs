using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertise
{
    public record CreateTbEmployeeLicenseExpertiseCommand(
        int EmployeeId,
        int LicenseGroupId,
        bool CanTeachTheory,
        bool CanTeachPractical,
        DateTime? CertificationDate
    ) : IRequest<TbEmployeeLicenseExpertise>;

    public class CreateHandler : IRequestHandler<CreateTbEmployeeLicenseExpertiseCommand, TbEmployeeLicenseExpertise>
    {
        private readonly DrivingSchoolDbContext _db;
        public CreateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbEmployeeLicenseExpertise> Handle(CreateTbEmployeeLicenseExpertiseCommand request, CancellationToken ct)
        {
            var entity = new TbEmployeeLicenseExpertise
            {
                EmployeeId = request.EmployeeId,
                LicenseGroupId = request.LicenseGroupId,
                CanTeachTheory = request.CanTeachTheory,
                CanTeachPractical = request.CanTeachPractical,
                CertificationDate = request.CertificationDate
            };

            _db.TbEmployeeLicenseExpertises.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
