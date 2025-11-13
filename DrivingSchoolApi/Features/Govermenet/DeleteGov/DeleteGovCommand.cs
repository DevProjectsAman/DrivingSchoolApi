using DrivingSchoolApi.Database;
using HRsystem.Api.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRsystem.Api.Features.Organization.Govermenet.DeleteGov
{
    public record DeleteGovCommand(int Id) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteGovCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteGovCommand request, CancellationToken ct)
        {
            var entity = await _db.TbGov.FirstOrDefaultAsync(g => g.GovId == request.Id, ct);
            if (entity == null) return false;

            _db.TbGov.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
