using DrivingSchoolApi.Database;
using DrivingSchoolApi.Features.CallCenterReservation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Enrollment;

public static class EnrollmentRoutes
{
    public static void MapEnrollmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/enrollment")
            .WithTags("Enrollment")
            .WithDescription("إدارة عملية التسجيل والحجز");

        // ============================================
        // 1️⃣ البحث عن العميل والتحقق من الدفع
        // ============================================
        group.MapGet("/search-customer", async (
            string phone,
            string? nationalId,
            ISender mediator) =>
        {
            var query = new SearchCustomerPaymentQuery
            {
                Phone = phone,
                NationalId = nationalId
            };

            var result = await mediator.Send(query);

            if (result == null)
            {
                return Results.NotFound(new
                {
                    Success = false,
                    Message = "العميل غير موجود. يرجى التأكد من رقم الهاتف والرقم القومي"
                });
            }

            if (!result.Payments.Any())
            {
                return Results.Ok(new
                {
                    Success = false,
                    Message = "العميل موجود ولكن لا توجد عمليات دفع صالحة",
                    Data = new
                    {
                        result.CustomerId,
                        result.FullName,
                        result.Phone
                    }
                });
            }

            // فحص إذا كانت كل الدفعات لها حجوزات
            var unusedPayments = result.Payments.Where(p => !p.HasReservation).ToList();

            if (!unusedPayments.Any())
            {
                return Results.Ok(new
                {
                    Success = false,
                    Message = "جميع عمليات الدفع تم استخدامها في حجوزات سابقة",
                    Data = result
                });
            }

            return Results.Ok(new
            {
                Success = true,
                Message = $"تم العثور على {unusedPayments.Count} عملية دفع صالحة للحجز",
                Data = result,
                UnusedPaymentsCount = unusedPayments.Count
            });
        })
        .WithName("SearchCustomerPayment")
        .WithSummary("البحث عن العميل والتحقق من عمليات الدفع")
        .Produces(200)
        .Produces(404);

        // ============================================
        // 2️⃣ عرض المدارس المتاحة للرخصة
        // ============================================
        group.MapGet("/available-schools", async (
            int licenseId,
            int? govId,
            ISender mediator) =>
        {
            var query = new GetAvailableSchoolsQuery
            {
                LicenseId = licenseId,
                GovId = govId
            };

            var schools = await mediator.Send(query);

            if (!schools.Any())
            {
                return Results.Ok(new
                {
                    Success = false,
                    Message = "لا توجد مدارس متاحة لهذه الرخصة في الوقت الحالي"
                });
            }

            return Results.Ok(new
            {
                Success = true,
                Message = $"تم العثور على {schools.Count} مدرسة متاحة",
                Data = schools,
                TotalCount = schools.Count
            });
        })
        .WithName("GetAvailableSchools")
        .WithSummary("عرض المدارس المتاحة للرخصة")
        .Produces(200);

        // ============================================
        // 3️⃣ إنشاء الحجز
        // ============================================
        group.MapPost("/create-reservation", async (
            CreateReservationCommand cmd,
            ISender mediator) =>
        {
            try
            {
                var result = await mediator.Send(cmd);

                return Results.Ok(new
                {
                    Success = true,
                    Message = "تم إنشاء الحجز بنجاح",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        })
        .WithName("CreateReservation")
        .WithSummary("إنشاء حجز جديد")
        .Produces(200)
        .Produces(400);

        // ============================================
        // 4️⃣ استعلام سريع: معلومات الدفعة
        // ============================================
        group.MapGet("/payment-details/{paymentId:int}", async (
            int paymentId,
            ISender mediator,
            DrivingSchoolDbContext context) =>
        {
            var payment = await context.TbPayments
                .Include(p => p.Customer)
                .Include(p => p.LicenseType)
                .Include(p => p.Reservation)
                .Where(p => p.PaymentId == paymentId)
                .Select(p => new
                {
                    p.PaymentId,
                    p.ReceiptSerial,
                    p.Amount,
                    p.PaymentDate,
                    ReceiptStatus = p.ReceiptStatus.ToString(),
                    Customer = new
                    {
                        p.Customer.CustomerId,
                        p.Customer.FullName,
                        p.Customer.Phone
                    },
                    License = new
                    {
                        p.LicenseType.LicenseId,
                        p.LicenseType.LicenseName
                    },
                    HasReservation = p.Reservation != null,
                    ReservationId = p.Reservation != null ? p.Reservation.ReservationId : (int?)null
                })
                .FirstOrDefaultAsync();

            if (payment == null)
            {
                return Results.NotFound(new
                {
                    Success = false,
                    Message = "عملية الدفع غير موجودة"
                });
            }

            return Results.Ok(new
            {
                Success = true,
                Data = payment
            });
        })
        .WithName("GetPaymentDetails")
        .WithSummary("الحصول على تفاصيل عملية دفع")
        .Produces(200)
        .Produces(404);
    }
}