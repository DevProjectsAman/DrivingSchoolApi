
using DrivingSchoolApi.Features.CourseLists;
using MediatR;

namespace DrivingSchoolApi.Features.CourseSessions
{
    public static class CourseSessionRoutes
    {
        public static void MapCourseSessionEndpoints(this IEndpointRouteBuilder app)
            {
                var group = app.MapGroup("/api/course-sessions");

            // CREATE
            group.MapPost("/", async (CreateTbCourseListCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateTbCourseListCommand cmd, ISender mediator) =>
            {
                if (id != cmd.CourseId) return Results.BadRequest(new { Success = false, Message = "ID mismatch" });

                var result = await mediator.Send(cmd);
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Course not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new DeleteTbCourseListCommand(id));
                return result
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Course not found" });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetTbCourseListByIdQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Course not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllTbCourseListsQuery());
                return Results.Ok(new { Success = true, Data = result });
            });
        }
    }
}
