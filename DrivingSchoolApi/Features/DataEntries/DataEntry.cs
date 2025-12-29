using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.DataEntries
{
    public class DataEntry
    {
        // ===================== RESPONSE DTO =====================
        public class PaymentResponseDto
        {
            public int PaymentId { get; set; }
            public int CustomerId { get; set; }
            public decimal Amount { get; set; }
            public DateTime? PaymentDate { get; set; }
            public int ReceiptStatus { get; set; }

            public string FullName { get; set; }
            public string Phone { get; set; }
        }

        // ===================== COMMAND =====================
        public record CreateDataEntryCommand(
            string FullName,
            string Phone,
            string NationalId,
            string? Email,

            int LicenseId,
            int PaymentLocationId,
            int PaymentLocationType,
            string? ReceiptSerial,
            decimal Amount,
            DateTime? PaymentDate,
            int ReceiptStatus
        ) : IRequest<PaymentResponseDto>;

        // ===================== VALIDATOR =====================
        public class CreateDataEntryValidator
            : AbstractValidator<CreateDataEntryCommand>
        {
            public CreateDataEntryValidator()
            {
                RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required");
                RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required");
                RuleFor(x => x.NationalId).NotEmpty().WithMessage("National ID is required");
                RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero");
            }
        }

        // ===================== HANDLER =====================
        public class CreateDataEntryHandler
            : IRequestHandler<CreateDataEntryCommand, PaymentResponseDto>
        {
            private readonly DrivingSchoolDbContext _db;

            public CreateDataEntryHandler(DrivingSchoolDbContext db)
            {
                _db = db;
            }

            public async Task<PaymentResponseDto> Handle(
                CreateDataEntryCommand request,
                CancellationToken cancellationToken)
            {
                using var transaction =
                    await _db.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    // ===== تحقق من وجود Customer بنفس NationalId =====
                    var existingCustomer = await _db.TbCustomers
                        .FirstOrDefaultAsync(c => c.NationalId == request.NationalId, cancellationToken);

                    if (existingCustomer != null)
                    {
                        throw new Exception("Customer with this National ID already exists.");
                    }

                    // ===== Create Customer =====
                    var customer = new TbCustomer
                    {
                        FullName = request.FullName,
                        Phone = request.Phone,
                        NationalId = request.NationalId,
                        Email = request.Email
                    };

                    _db.TbCustomers.Add(customer);
                    await _db.SaveChangesAsync(cancellationToken);

                    // ===== Create Payment =====
                    var payment = new TbPayment
                    {
                        CustomerId = customer.CustomerId,
                        LicenseId = request.LicenseId,

                        PaymentLocationId = request.PaymentLocationId,
                        PaymentLocationType =
                            (Enums.EnumsList.PaymentLocationType)
                            request.PaymentLocationType,

                        ReceiptSerial = request.ReceiptSerial,
                        Amount = request.Amount,
                        PaymentDate = request.PaymentDate ?? DateTime.UtcNow,

                        ReceiptStatus =
                            (Enums.EnumsList.ReceiptStatus)
                            request.ReceiptStatus
                    };

                    _db.TbPayments.Add(payment);
                    await _db.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    // ===== RESPONSE DTO =====
                    return new PaymentResponseDto
                    {
                        PaymentId = payment.PaymentId,
                        CustomerId = customer.CustomerId,
                        Amount = payment.Amount,
                        PaymentDate = payment.PaymentDate,
                        ReceiptStatus = (int)payment.ReceiptStatus,

                        FullName = customer.FullName,
                        Phone = customer.Phone
                    };
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
