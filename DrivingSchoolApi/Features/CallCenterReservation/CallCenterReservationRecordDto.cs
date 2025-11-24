namespace DrivingSchoolApi.Features.CallCenterReservation
{
    // Response للبحث عن العميل
    public record CustomerPaymentLookupDto
    {
        public int CustomerId { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Phone { get; init; } = string.Empty;
        public string? NationalId { get; init; }
        public List<PaymentRecordDto> Payments { get; init; } = new();
    }

    public record PaymentRecordDto
    {
        public int PaymentId { get; init; }
        public int LicenseId { get; init; }
        public string LicenseName { get; init; } = string.Empty;
        public string ReceiptSerial { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public DateTime PaymentDate { get; init; }
        public string ReceiptStatus { get; init; } = string.Empty; // Valid, Cancelled, Refunded
        public bool HasReservation { get; init; } // هل تم الحجز بالفعل؟
        public int? ReservationId { get; init; }
    }

    // Response للمدارس المتاحة
    public record AvailableSchoolDto
    {
        public int SchoolId { get; init; }
        public string SchoolName { get; init; } = string.Empty;
        public string GovName { get; init; } = string.Empty;
        public string Location { get; init; } = string.Empty;
        public int TotalCapacity { get; init; }
        public int CurrentEnrollments { get; init; } // عدد الطلاب الحاليين
        public bool IsAvailable { get; init; }
    }

    // Request للحجز
    public record CreateReservationRequest
    {
        public int PaymentId { get; init; }
        public int SchoolId { get; init; }
        public string? Notes { get; init; }
    }

    // Response للحجز
    public record ReservationCreatedDto
    {
        public int ReservationId { get; init; }
        public int CustomerId { get; init; }
        public string CustomerName { get; init; } = string.Empty;
        public string LicenseName { get; init; } = string.Empty;
        public string SchoolName { get; init; } = string.Empty;
        public DateTime ReservationDate { get; init; }
        public string Status { get; init; } = string.Empty;
        public List<CourseRequirementDto> RequiredCourses { get; init; } = new();
    }

    public record CourseRequirementDto
    {
        public int CourseId { get; init; }
        public string CourseName { get; init; } = string.Empty;
        public string SessionType { get; init; } = string.Empty; // Theory, Practical
        public decimal DurationHours { get; init; }
    }
}
