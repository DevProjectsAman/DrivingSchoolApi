using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using static DrivingSchoolApi.Features.DataEntries.DataEntry;

namespace DrivingSchoolApi.Features.DataEntries
{
    public static class DataEntryEndPoint
    {
        public static void MapDataEntryEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/data-entries")
                .WithTags("Data Entries");

            group.MapPost("/", async (
                CreateDataEntryCommand command,
                ISender mediator) =>
            {
                var result = await mediator.Send(command);

                return Results.Ok(new
                {
                    Success = true,
                    Data = result
                });
               
            });
        }
    }
}

