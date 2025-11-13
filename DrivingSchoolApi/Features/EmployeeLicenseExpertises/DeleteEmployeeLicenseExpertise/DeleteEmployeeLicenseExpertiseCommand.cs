using MediatR;
using DrivingSchoolApi.Database;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertise
{
    public record DeleteTbEmployeeLicenseExpertiseCommand(int ExpertiseId) : IRequest<bool>;

    public class DeleteHandler : IRequestHandler<DeleteTbEmployeeLicenseExpertiseCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public DeleteHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteTbEmployeeLicenseExpertiseCommand request, CancellationToken ct)
        {
            var entity = await _db.TbEmployeeLicenseExpertises.FindAsync(new object[] { request.ExpertiseId }, ct);
            if (entity == null) return false;

            _db.TbEmployeeLicenseExpertises.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
