using MediatR;

namespace DrivingSchoolApi.Features.Payments;

public static class MapPaymentEndpoints
{
    public static void MapPayment(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payments");

        // CREATE
        group.MapPost("/", async (CreatePaymentCommand cmd, ISender mediator) =>
        {
            var result = await mediator.Send(cmd);
            return Results.Ok(new { Success = true, Data = result });
        });

        // UPDATE
        group.MapPut("/{id}", async (int id, UpdatePaymentCommand cmd, ISender mediator) =>
        {
            if (id != cmd.PaymentId)
                return Results.BadRequest(new { Success = false, Message = "ID mismatch" });

            var result = await mediator.Send(cmd);
            return result == null
                ? Results.NotFound(new { Success = false, Message = "Payment not found" })
                : Results.Ok(new { Success = true, Data = result });
        });

        // DELETE
        group.MapDelete("/{id}", async (int id, ISender mediator) =>
        {
            var result = await mediator.Send(new DeletePaymentCommand(id));
            return result
                ? Results.Ok(new { Success = true })
                : Results.NotFound(new { Success = false, Message = "Payment not found" });
        });

        // GET ONE
        group.MapGet("/{id}", async (int id, ISender mediator) =>
        {
            var result = await mediator.Send(new GetPaymentByIdQuery(id));
            return result == null
                ? Results.NotFound(new { Success = false, Message = "Payment not found" })
                : Results.Ok(new { Success = true, Data = result });
        });

        // GET ALL
        group.MapGet("/", async (ISender mediator) =>
        {
            var result = await mediator.Send(new GetAllPaymentsQuery());
            return Results.Ok(new { Success = true, Data = result });
        });
    }
}
