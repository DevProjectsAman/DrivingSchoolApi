using MediatR;

namespace DrivingSchoolApi.Features.SchoolOperatingHours
{
    public static class SchoolOperatingHoursRoutes
    {
        public static void MapSchoolOperatingHoursEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/school-operating-hours");

            // CREATE
            group.MapPost("/", async (CreateSchoolOperatingHoursCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            //// UPDATE
            //group.MapPut("/{id}", async (int id, BulkUpdateSchoolOperatingHoursCommand cmd, ISender mediator) =>
            //{
            //    if (id != cmd.OperatingHoursId)
            //        return Results.BadRequest(new { Success = false, Message = "ID mismatch" });

            //    var result = await mediator.Send(cmd);
            //    return result == null
            //        ? Results.NotFound(new { Success = false, Message = "Operating hours not found" })
            //        : Results.Ok(new { Success = true, Data = result });
            //});

            //// DELETE
            //group.MapDelete("/{id}", async (int id, ISender mediator) =>
            //{
            //    var result = await mediator.Send(new DeleteSchoolOperatingHoursCommand(id));
            //    return result
            //        ? Results.Ok(new { Success = true })
            //        : Results.NotFound(new { Success = false, Message = "Operating hours not found" });
            //});

            //// GET ONE
            //group.MapGet("/{id}", async (int id, ISender mediator) =>
            //{
            //    var result = await mediator.Send(new GetSchoolOperatingHoursByIdQuery(id));
            //    return result == null
            //        ? Results.NotFound(new { Success = false, Message = "Operating hours not found" })
            //        : Results.Ok(new { Success = true, Data = result });
            //});

            //// GET ALL
            //group.MapGet("/", async (ISender mediator) =>
            //{
            //    var result = await mediator.Send(new GetAllSchoolOperatingHoursQuery());
            //    return Results.Ok(new { Success = true, Data = result });
            //});

            //// GET BY SCHOOL ID - Get all operating hours for a specific school
            //group.MapGet("/school/{schoolId}", async (int schoolId, ISender mediator) =>
            //{
            //    var result = await mediator.Send(new GetSchoolOperatingHoursBySchoolIdQuery(schoolId));
            //    return Results.Ok(new { Success = true, Data = result });
            //});

            //// BULK UPDATE - Set operating hours for all days of the week for a school
            //group.MapPost("/bulk", async (BulkUpdateSchoolOperatingHoursCommand cmd, ISender mediator) =>
            //{
            //    var result = await mediator.Send(cmd);
            //    return Results.Ok(new { Success = true, Data = result });
            //});
        }
    }
}