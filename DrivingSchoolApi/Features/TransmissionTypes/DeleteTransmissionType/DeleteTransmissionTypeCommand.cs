using DrivingSchoolApi.Database;
using MediatR;

namespace DrivingSchoolApi.Features.TransmissionType
{
    public record DeleteTransmissionTypeCommand(int TransmissionId) : IRequest<bool>;

    public class DeleteHandler : IRequestHandler<DeleteTransmissionTypeCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public DeleteHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteTransmissionTypeCommand request, CancellationToken ct)
        {
            var entity = await _db.TbTransmissionTypes.FindAsync(new object[] { request.TransmissionId }, ct);
            if (entity == null) return false;

            _db.TbTransmissionTypes.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
