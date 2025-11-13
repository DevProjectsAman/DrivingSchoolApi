using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Payments;

public record GetAllPaymentsQuery() : IRequest<List<TbPayment>>;

public class GetAllPaymentsHandler : IRequestHandler<GetAllPaymentsQuery, List<TbPayment>>
{
    private readonly DrivingSchoolDbContext _db;
    public GetAllPaymentsHandler(DrivingSchoolDbContext db) => _db = db;

    public async Task<List<TbPayment>> Handle(GetAllPaymentsQuery request, CancellationToken ct)
    {
        return await _db.TbPayments
            .Include(p => p.Customer)
            .Include(p => p.LicenseType)
            .ToListAsync(ct);
    }
}
