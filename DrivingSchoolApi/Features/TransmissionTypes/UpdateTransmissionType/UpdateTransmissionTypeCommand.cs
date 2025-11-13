using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.TransmissionType
{
    public record UpdateTransmissionTypeCommand(int TransmissionId, string TypeName) : IRequest<TbTransmissionType>;

    public class UpdateHandler : IRequestHandler<UpdateTransmissionTypeCommand, TbTransmissionType>
    {
        private readonly DrivingSchoolDbContext _db;
        public UpdateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbTransmissionType> Handle(UpdateTransmissionTypeCommand request, CancellationToken ct)
        {
            var entity = await _db.TbTransmissionTypes.FindAsync(new object[] { request.TransmissionId }, ct);
            if (entity == null) return null;

            entity.TypeName = request.TypeName;
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
