using MediatR;
using DrivingSchoolApi.Database;

namespace DrivingSchoolApi.Features.CourseLists
{
    public record DeleteTbCourseListCommand(int CourseId) : IRequest<bool>;

    public class DeleteHandler : IRequestHandler<DeleteTbCourseListCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public DeleteHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteTbCourseListCommand request, CancellationToken ct)
        {
            var entity = await _db.TbCourseLists.FindAsync(new object[] { request.CourseId }, ct);
            if (entity == null) return false;

            _db.TbCourseLists.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
