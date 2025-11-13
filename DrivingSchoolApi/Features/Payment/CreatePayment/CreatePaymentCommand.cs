using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.Payments;

public record CreatePaymentCommand(
    int CustomerId,
    int LicenseId,
    int PaymentLocationId,
    int PaymentLocationType,
    string ReceiptSerial,
    decimal Amount,
    DateTime PaymentDate,
    int ReceiptStatus
) : IRequest<TbPayment>;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, TbPayment>
{
    private readonly DrivingSchoolDbContext _db;
    public CreatePaymentHandler(DrivingSchoolDbContext db) => _db = db;

    public async Task<TbPayment> Handle(CreatePaymentCommand request, CancellationToken ct)
    {
        var entity = new TbPayment
        {
            CustomerId = request.CustomerId,
            LicenseId = request.LicenseId,
            PaymentLocationId = request.PaymentLocationId,
            PaymentLocationType = (Enums.EnumsList.PaymentLocationType)request.PaymentLocationType,
            ReceiptSerial = request.ReceiptSerial,
            Amount = request.Amount,
            PaymentDate = request.PaymentDate,
            ReceiptStatus = (Enums.EnumsList.ReceiptStatus)request.ReceiptStatus
        };

        _db.TbPayments.Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }
}
