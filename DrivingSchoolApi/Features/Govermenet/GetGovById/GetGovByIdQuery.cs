using DrivingSchoolApi.Database;
using HRsystem.Api.Database;
using HRsystem.Api.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRsystem.Api.Features.Organization.Govermenet.GetGovById
{
    public record GetGovByIdQuery(int Id) : IRequest<TbGov?>;

    public class Handler : IRequestHandler<GetGovByIdQuery, TbGov?>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbGov?> Handle(GetGovByIdQuery request, CancellationToken ct)
        {
            return await _db.TbGov.FirstOrDefaultAsync(g => g.GovId == request.Id, ct);
        }
    }
}
