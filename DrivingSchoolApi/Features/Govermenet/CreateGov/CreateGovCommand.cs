using DrivingSchoolApi.Database;
using FluentValidation;
using HRsystem.Api.Database;
using HRsystem.Api.Database.DataTables;
using MediatR;

namespace HRsystem.Api.Features.Organization.Govermenet.CreateGov
{
    public record CreateGovCommand(string? GoveCode, string? GovName, string? GovArea) : IRequest<TbGov>;

    public class Handler : IRequestHandler<CreateGovCommand, TbGov>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbGov> Handle(CreateGovCommand request, CancellationToken ct)
        {
            var entity = new TbGov
            {
                GoveCode = request.GoveCode,
                GovName = request.GovName,
                GovArea = request.GovArea
            };

            _db.TbGov.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}


namespace HRsystem.Api.Features.Organization.Govermenet.CreateGov
{
    public class CreateGovValidator : AbstractValidator<CreateGovCommand>
    {
        public CreateGovValidator()
        {
            RuleFor(x => x.GovName).NotEmpty().MaximumLength(60);
            RuleFor(x => x.GoveCode).MaximumLength(25);
            RuleFor(x => x.GovArea).MaximumLength(100);
        }
    }
}
