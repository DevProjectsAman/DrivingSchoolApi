using DrivingSchoolApi.Features.EmployeeLicenseExpertise;
using MediatR;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertises
{
    public static class EmployeeLicenseExpertiseRoutes
    {
        public static void MapEmployeeLicenseExpertiseEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/employee-license-expertises");

            // CREATE
            group.MapPost("/", async (CreateTbEmployeeLicenseExpertiseCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateTbEmployeeLicenseExpertiseCommand cmd, ISender mediator) =>
            {
                if (id != cmd.ExpertiseId)
                    return Results.BadRequest(new { Success = false, Message = "ID mismatch" });

                var result = await mediator.Send(cmd);
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Expertise not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new DeleteTbEmployeeLicenseExpertiseCommand(id));
                return result
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Expertise not found" });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetTbEmployeeLicenseExpertiseByIdQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Expertise not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllTbEmployeeLicenseExpertisesQuery());
                return Results.Ok(new { Success = true, Data = result });
            });
        }
    }
}
