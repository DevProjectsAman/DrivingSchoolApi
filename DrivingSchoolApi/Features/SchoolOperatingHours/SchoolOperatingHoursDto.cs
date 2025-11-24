namespace DrivingSchoolApi.Shared.DTO;

public class SchoolOperatingHoursDto
{
    public int OperatingHoursId { get; set; }
    public int SchoolId { get; set; }
    public string SchoolName { get; set; }
    public int DayOfWeek { get; set; }
    public string DayName { get; set; } // Sunday, Monday, etc.
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsWorkingDay { get; set; }
    public string Notes { get; set; }
}

public class CreateSchoolOperatingHoursDto
{
    public int SchoolId { get; set; }
    public int DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsWorkingDay { get; set; } = true;
    public string Notes { get; set; }
}

public class UpdateSchoolOperatingHoursDto
{
    public int OperatingHoursId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsWorkingDay { get; set; }
    public string Notes { get; set; }
}

public class BulkSchoolOperatingHoursDto
{
    public int SchoolId { get; set; }
    public List<DayOperatingHours> Days { get; set; }
}

public class DayOperatingHours
{
    public int DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsWorkingDay { get; set; } = true;
    public string Notes { get; set; }
}