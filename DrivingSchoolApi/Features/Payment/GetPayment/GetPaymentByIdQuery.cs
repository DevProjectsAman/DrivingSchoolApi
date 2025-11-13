using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Payments;

public record GetPaymentByIdQuery(int PaymentId) : IRequest<TbPayment?>;

public class GetPaymentByIdHandler : IRequestHandler<GetPaymentByIdQuery, TbPayment?>
{
    private readonly DrivingSchoolDbContext _db;
    public GetPaymentByIdHandler(DrivingSchoolDbContext db) => _db = db;

    public async Task<TbPayment?> Handle(GetPaymentByIdQuery request, CancellationToken ct)
    {
        return await _db.TbPayments
            .Include(p => p.Customer)
            .Include(p => p.LicenseType)
            .FirstOrDefaultAsync(x => x.PaymentId == request.PaymentId, ct);
    }
}
