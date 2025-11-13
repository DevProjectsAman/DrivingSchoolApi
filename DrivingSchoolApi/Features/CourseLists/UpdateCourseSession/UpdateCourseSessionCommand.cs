using MediatR;
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.CourseLists
{
    public record UpdateTbCourseListCommand(
        int CourseId,
        string CourseName,
        int LicenseId,
        SessionType SessionType,
        decimal DurationHours
    ) : IRequest<TbCourseList>;

    public class UpdateHandler : IRequestHandler<UpdateTbCourseListCommand, TbCourseList>
    {
        private readonly DrivingSchoolDbContext _db;
        public UpdateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbCourseList> Handle(UpdateTbCourseListCommand request, CancellationToken ct)
        {
            var entity = await _db.TbCourseLists.FindAsync(new object[] { request.CourseId }, ct);
            if (entity == null) return null;

            entity.CourseName = request.CourseName;
            entity.LicenseId = request.LicenseId;
            entity.SessionType = request.SessionType;
            entity.DurationHours = request.DurationHours;

            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
