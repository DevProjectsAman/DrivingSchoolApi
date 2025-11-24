using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using DrivingSchoolApi.Shared.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.SchoolOperatingHours;

// ==================== CREATE ====================
public record CreateSchoolOperatingHoursCommand(
    int SchoolId,
    int DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,
    bool IsWorkingDay = true,
    string Notes = null
) : IRequest<SchoolOperatingHoursDto>;

public class CreateSchoolOperatingHoursHandler : IRequestHandler<CreateSchoolOperatingHoursCommand, SchoolOperatingHoursDto>
{
    private readonly DrivingSchoolDbContext _context;

    public CreateSchoolOperatingHoursHandler(DrivingSchoolDbContext context)
    {
        _context = context;
    }

    public async Task<SchoolOperatingHoursDto> Handle(CreateSchoolOperatingHoursCommand request, CancellationToken cancellationToken)
    {
        // Validate school exists
        var schoolExists = await _context.TbSchools.AnyAsync(s => s.SchoolId == request.SchoolId, cancellationToken);
        if (!schoolExists)
            throw new Exception($"School with ID {request.SchoolId} not found");

        // Validate day of week
        if (request.DayOfWeek < 0 || request.DayOfWeek > 6)
            throw new Exception("DayOfWeek must be between 0 (Sunday) and 6 (Saturday)");

        // Validate time range
        if (request.EndTime <= request.StartTime)
            throw new Exception("EndTime must be after StartTime");

        // Check if already exists
        var exists = await _context.TbSchoolOperatingHours
            .AnyAsync(oh => oh.SchoolId == request.SchoolId && oh.DayOfWeek == request.DayOfWeek, cancellationToken);

        if (exists)
            throw new Exception($"Operating hours already exist for this school on day {request.DayOfWeek}");

        var entity = new TbSchoolOperatingHour
        {
            SchoolId = request.SchoolId,
            DayOfWeek = request.DayOfWeek,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            IsWorkingDay = request.IsWorkingDay,
            Notes = request.Notes
        };

        _context.TbSchoolOperatingHours.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return await GetOperatingHoursDto(entity.OperatingHoursId, cancellationToken);
    }

    private async Task<SchoolOperatingHoursDto> GetOperatingHoursDto(int id, CancellationToken cancellationToken)
    {
        return await _context.TbSchoolOperatingHours
            .Where(oh => oh.OperatingHoursId == id)
            .Select(oh => new SchoolOperatingHoursDto
            {
                OperatingHoursId = oh.OperatingHoursId,
                SchoolId = oh.SchoolId,
                SchoolName = oh.School.SchoolName,
                DayOfWeek = oh.DayOfWeek,
                DayName = GetDayName(oh.DayOfWeek),
                StartTime = oh.StartTime,
                EndTime = oh.EndTime,
                IsWorkingDay = oh.IsWorkingDay,
                Notes = oh.Notes
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    private static string GetDayName(int dayOfWeek)
    {
        return dayOfWeek switch
        {
            0 => "Sunday",
            1 => "Monday",
            2 => "Tuesday",
            3 => "Wednesday",
            4 => "Thursday",
            5 => "Friday",
            6 => "Saturday",
            _ => "Unknown"
        };
    }
}