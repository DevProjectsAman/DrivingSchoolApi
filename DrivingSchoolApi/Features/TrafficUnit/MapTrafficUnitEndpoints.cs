using MediatR;

namespace DrivingSchoolApi.Features.TrafficUnit
{
    public static class MapTrafficUnitEndpoints
    {
        public static void MapTrafficUnit(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/traffic-units");

            // CREATE
            group.MapPost("/", async (CreateTrafficUnitCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateTrafficUnitCommand cmd, ISender mediator) =>
            {
                if (id != cmd.TrafficUnitId)
                    return Results.BadRequest(new { Success = false, Message = "ID mismatch" });

                var result = await mediator.Send(cmd);
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Traffic unit not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new DeleteTrafficUnitCommand(id));
                return result
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Traffic unit not found" });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetTrafficUnitByIdQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Traffic unit not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllTrafficUnitsQuery());
                return Results.Ok(new { Success = true, Data = result });
            });
        }
    }
}
