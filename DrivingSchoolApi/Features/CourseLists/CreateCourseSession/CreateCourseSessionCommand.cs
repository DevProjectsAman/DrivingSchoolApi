using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.CourseLists
{
    public record CreateTbCourseListCommand(
        string CourseName,
        int LicenseId,
        SessionType SessionType,
        decimal DurationHours
    ) : IRequest<TbCourseList>;

    public class CreateHandler : IRequestHandler<CreateTbCourseListCommand, TbCourseList>
    {
        private readonly DrivingSchoolDbContext _db;
        public CreateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbCourseList> Handle(CreateTbCourseListCommand request, CancellationToken ct)
        {
            var entity = new TbCourseList
            {
                CourseName = request.CourseName,
                LicenseId = request.LicenseId,
                SessionType = request.SessionType,
                DurationHours = request.DurationHours
            };

            _db.TbCourseLists.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
