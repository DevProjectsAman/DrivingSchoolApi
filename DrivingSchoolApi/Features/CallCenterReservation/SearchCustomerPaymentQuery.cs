using DrivingSchoolApi.Database;
using MediatR;
using static DrivingSchoolApi.Enums.EnumsList;
using DrivingSchoolApi.Features.CallCenterReservation;
using Microsoft.EntityFrameworkCore;
namespace DrivingSchoolApi.Features.CallCenterReservation
{
    // QUERY
    public record SearchCustomerPaymentQuery : IRequest<CustomerPaymentLookupDto?>
    {
        public string Phone { get; init; } = string.Empty;
        public string? NationalId { get; init; }
    }

    // HANDLER
    public class SearchCustomerPaymentHandler : IRequestHandler<SearchCustomerPaymentQuery, CustomerPaymentLookupDto?>
    {
        private readonly DrivingSchoolDbContext _context;

        public SearchCustomerPaymentHandler(DrivingSchoolDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerPaymentLookupDto?> Handle(SearchCustomerPaymentQuery request, CancellationToken ct)
        {
            // البحث عن العميل
            var customer = await _context.TbCustomers
                .Where(c => c.Phone == request.Phone)
                .Where(c => string.IsNullOrEmpty(request.NationalId) || c.NationalId == request.NationalId)
                .Select(c => new
                {
                    c.CustomerId,
                    c.FullName,
                    c.Phone,
                    c.NationalId,
                    Payments = c.Payments
                        .Where(p => p.ReceiptStatus == ReceiptStatus.Valid)
                        .Select(p => new PaymentRecordDto
                        {
                            PaymentId = p.PaymentId,
                            LicenseId = p.LicenseId,
                            LicenseName = p.LicenseType.LicenseName,
                            ReceiptSerial = p.ReceiptSerial,
                            Amount = p.Amount,
                            PaymentDate = p.PaymentDate,
                            ReceiptStatus = p.ReceiptStatus.ToString(),
                            HasReservation = p.Reservation != null,
                            ReservationId = p.Reservation != null ? p.Reservation.ReservationId : null
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(ct);

            if (customer == null)
                return null;

            return new CustomerPaymentLookupDto
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Phone = customer.Phone,
                NationalId = customer.NationalId,
                Payments = customer.Payments
            };
        }
    }

}

