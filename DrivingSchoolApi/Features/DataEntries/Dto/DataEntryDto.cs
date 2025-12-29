namespace DrivingSchoolApi.Features.DataEntries.Dto
{
    public class DataEntryDto
    {
        // ===== Customer =====
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string NationalId { get; set; }
        public string? Email { get; set; }

        // ===== Payment =====
        public int LicenseId { get; set; }
        public int PaymentLocationId { get; set; }
        public int PaymentLocationType { get; set; }

        public string? ReceiptSerial { get; set; }
        public decimal Amount { get; set; }

        public DateTime? PaymentDate { get; set; }
        public int ReceiptStatus { get; set; }
    }

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
}

