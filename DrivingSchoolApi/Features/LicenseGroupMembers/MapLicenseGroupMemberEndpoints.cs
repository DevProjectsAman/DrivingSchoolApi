using MediatR;

namespace DrivingSchoolApi.Features.LicenseGroupMembers;

public static class MapLicenseGroupMemberEndpoints
{
    public static void MapLicenseGroupMember(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/license-group-members");

        // CREATE
        group.MapPost("/", async (CreateLicenseGroupMemberCommand cmd, ISender mediator) =>
        {
            var result = await mediator.Send(cmd);
            return Results.Ok(new { Success = true, Data = result });
        });

        // UPDATE
        group.MapPut("/", async (UpdateLicenseGroupMemberCommand cmd, ISender mediator) =>
        {
            var result = await mediator.Send(cmd);
            return result == null
                ? Results.NotFound(new { Success = false, Message = "License group member not found" })
                : Results.Ok(new { Success = true, Data = result });
        });

        // DELETE
        group.MapDelete("/", async (int groupId, int licenseId, ISender mediator) =>
        {
            var result = await mediator.Send(new DeleteLicenseGroupMemberCommand(groupId, licenseId));
            return result
                ? Results.Ok(new { Success = true })
                : Results.NotFound(new { Success = false, Message = "License group member not found" });
        });

        // GET ONE
        group.MapGet("/", async (int groupId, int licenseId, ISender mediator) =>
        {
            var result = await mediator.Send(new GetLicenseGroupMemberByIdQuery(groupId, licenseId));
            return result == null
                ? Results.NotFound(new { Success = false, Message = "License group member not found" })
                : Results.Ok(new { Success = true, Data = result });
        });

        // GET ALL
        group.MapGet("/all", async (ISender mediator) =>
        {
            var result = await mediator.Send(new GetAllLicenseGroupMembersQuery());
            return Results.Ok(new { Success = true, Data = result });
        });
    }
}
