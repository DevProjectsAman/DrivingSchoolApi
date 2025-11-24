using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using DrivingSchoolApi.Shared.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.SchoolOperatingHours;

// ==================== BULK UPDATE ====================
public record BulkUpdateSchoolOperatingHoursCommand(
    int SchoolId,
    List<DayOperatingHoursDto> Days
) : IRequest<List<SchoolOperatingHoursDto>>;

public class DayOperatingHoursDto
{
    public int DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsWorkingDay { get; set; } = true;
    public string Notes { get; set; }
}

public class BulkUpdateSchoolOperatingHoursHandler : IRequestHandler<BulkUpdateSchoolOperatingHoursCommand, List<SchoolOperatingHoursDto>>
{
    private readonly DrivingSchoolDbContext _context;

    public BulkUpdateSchoolOperatingHoursHandler(DrivingSchoolDbContext context)
    {
        _context = context;
    }

    public async Task<List<SchoolOperatingHoursDto>> Handle(BulkUpdateSchoolOperatingHoursCommand request, CancellationToken cancellationToken)
    {
        // Validate school exists
        var schoolExists = await _context.TbSchools.AnyAsync(s => s.SchoolId == request.SchoolId, cancellationToken);
        if (!schoolExists)
            throw new Exception($"School with ID {request.SchoolId} not found");

        // Validate days
        if (request.Days == null || !request.Days.Any())
            throw new Exception("Days list cannot be empty");

        foreach (var day in request.Days)
        {
            if (day.DayOfWeek < 0 || day.DayOfWeek > 6)
                throw new Exception($"Invalid DayOfWeek: {day.DayOfWeek}. Must be between 0 and 6");

            if (day.IsWorkingDay && day.EndTime <= day.StartTime)
                throw new Exception($"EndTime must be after StartTime for day {day.DayOfWeek}");
        }

        // Check for duplicate days in request
        var duplicateDays = request.Days.GroupBy(d => d.DayOfWeek).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
        if (duplicateDays.Any())
            throw new Exception($"Duplicate days found in request: {string.Join(", ", duplicateDays)}");

        // Get existing operating hours for this school
        var existingHours = await _context.TbSchoolOperatingHours
            .Where(oh => oh.SchoolId == request.SchoolId)
            .ToListAsync(cancellationToken);

        // Update or create each day
        foreach (var day in request.Days)
        {
            var existing = existingHours.FirstOrDefault(oh => oh.DayOfWeek == day.DayOfWeek);

            if (existing != null)
            {
                // Update existing
                existing.StartTime = day.StartTime;
                existing.EndTime = day.EndTime;
                existing.IsWorkingDay = day.IsWorkingDay;
                existing.Notes = day.Notes;
            }
            else
            {
                // Create new
                var newHours = new TbSchoolOperatingHour
                {
                    SchoolId = request.SchoolId,
                    DayOfWeek = day.DayOfWeek,
                    StartTime = day.StartTime,
                    EndTime = day.EndTime,
                    IsWorkingDay = day.IsWorkingDay,
                    Notes = day.Notes
                };
                _context.TbSchoolOperatingHours.Add(newHours);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        // Return updated list
        return await _context.TbSchoolOperatingHours
            .Where(oh => oh.SchoolId == request.SchoolId)
            .OrderBy(oh => oh.DayOfWeek)
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
            .ToListAsync(cancellationToken);
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