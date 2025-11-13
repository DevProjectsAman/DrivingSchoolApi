using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.TransmissionType
{
    public record CreateTransmissionTypeCommand(string TypeName) : IRequest<TbTransmissionType>;

    public class CreateHandler : IRequestHandler<CreateTransmissionTypeCommand, TbTransmissionType>
    {
        private readonly DrivingSchoolDbContext _db;
        public CreateHandler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbTransmissionType> Handle(CreateTransmissionTypeCommand request, CancellationToken ct)
        {
            var entity = new TbTransmissionType
            {
                TypeName = request.TypeName
            };

            _db.TbTransmissionTypes.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
