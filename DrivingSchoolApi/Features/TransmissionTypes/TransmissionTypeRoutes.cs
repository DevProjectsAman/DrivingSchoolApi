
using DrivingSchoolApi.Features.TransmissionType;
using MediatR;

namespace DrivingSchool.Api.Features.TransmissionTypes
{
    public static class TransmissionTypeRoutes
    {
        public static void MapTransmissionTypeEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/transmission-types");

            // CREATE
            group.MapPost("/", async (CreateTransmissionTypeCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateTransmissionTypeCommand cmd, ISender mediator) =>
            {
                if (id != cmd.TransmissionId)
                    return Results.BadRequest(new { Success = false, Message = "ID mismatch" });

                var result = await mediator.Send(cmd);
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Transmission type not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new DeleteTransmissionTypeCommand(id));
                return result
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Transmission type not found" });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetTransmissionTypeByIdQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Transmission type not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllTransmissionTypesQuery());
                return Results.Ok(new { Success = true, Data = result });
            });
        }
    }
}
