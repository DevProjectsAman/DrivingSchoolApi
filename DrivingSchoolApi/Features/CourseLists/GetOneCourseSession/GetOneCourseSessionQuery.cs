using MediatR;
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.CourseLists
{
    public record GetTbCourseListByIdQuery(int CourseId) : IRequest<TbCourseList>;

    public class GetByIdHandler : IRequestHandler<GetTbCourseListByIdQuery, TbCourseList>
    {
        private readonly DrivingSchoolDbContext _db;
        public GetByIdHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbCourseList> Handle(GetTbCourseListByIdQuery request, CancellationToken ct)
        {
            return await _db.TbCourseLists
                .Include(c => c.LicenseType)
                .Include(c => c.SessionAttendances)
                .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, ct);
        }
    }
}
