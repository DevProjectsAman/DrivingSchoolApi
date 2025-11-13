using MediatR;

namespace DrivingSchoolApi.Features.LicenseGroup
{
    public static class MapLicenseGroupEndpoints
    {
        public static void MapLicenseGroup(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/license-group");

            // CREATE
            group.MapPost("/", async (CreateLicenseGroupCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateLicenseGroupCommand cmd, ISender mediator) =>
            {
                if (id != cmd.GroupId)
                    return Results.BadRequest(new { Success = false, Message = "ID mismatch" });

                var result = await mediator.Send(cmd);
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "License group not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new DeleteLicenseGroupCommand(id));
                return result
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "License group not found" });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetLicenseGroupByIdQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "License group not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllLicenseGroupsQuery());
                return Results.Ok(new { Success = true, Data = result });
            });
        }
    }
}
