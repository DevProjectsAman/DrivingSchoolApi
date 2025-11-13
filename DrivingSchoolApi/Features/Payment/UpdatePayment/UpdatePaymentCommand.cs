using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Payments;

public record UpdatePaymentCommand(
    int PaymentId,
    int CustomerId,
    int LicenseId,
    int PaymentLocationId,
    int PaymentLocationType,
    string ReceiptSerial,
    decimal Amount,
    DateTime PaymentDate,
    int ReceiptStatus
) : IRequest<TbPayment?>;

public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentCommand, TbPayment?>
{
    private readonly DrivingSchoolDbContext _db;
    public UpdatePaymentHandler(DrivingSchoolDbContext db) => _db = db;

    public async Task<TbPayment?> Handle(UpdatePaymentCommand request, CancellationToken ct)
    {
        var entity = await _db.TbPayments.FirstOrDefaultAsync(x => x.PaymentId == request.PaymentId, ct);
        if (entity == null) return null;

        entity.CustomerId = request.CustomerId;
        entity.LicenseId = request.LicenseId;
        entity.PaymentLocationId = request.PaymentLocationId;
        entity.PaymentLocationType = (Enums.EnumsList.PaymentLocationType)request.PaymentLocationType;
        entity.ReceiptSerial = request.ReceiptSerial;
        entity.Amount = request.Amount;
        entity.PaymentDate = request.PaymentDate;
        entity.ReceiptStatus = (Enums.EnumsList.ReceiptStatus)request.ReceiptStatus;

        await _db.SaveChangesAsync(ct);
        return entity;
    }
}
