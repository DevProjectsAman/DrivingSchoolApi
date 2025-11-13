using MediatR;
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.CourseLists
{
    public record GetAllTbCourseListsQuery() : IRequest<List<TbCourseList>>;

    public class GetAllHandler : IRequestHandler<GetAllTbCourseListsQuery, List<TbCourseList>>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetAllHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbCourseList>> Handle(GetAllTbCourseListsQuery request, CancellationToken ct)
        {
            return await _db.TbCourseLists
                .Include(c => c.LicenseType)
                .Include(c => c.SessionAttendances)
                .ToListAsync(ct);
        }
    }
}
