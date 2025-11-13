using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Payments;

public record DeletePaymentCommand(int PaymentId) : IRequest<bool>;

public class DeletePaymentHandler : IRequestHandler<DeletePaymentCommand, bool>
{
    private readonly DrivingSchoolDbContext _db;
    public DeletePaymentHandler(DrivingSchoolDbContext db) => _db = db;

    public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken ct)
    {
        var entity = await _db.TbPayments.FirstOrDefaultAsync(x => x.PaymentId == request.PaymentId, ct);
        if (entity == null) return false;

        _db.TbPayments.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
