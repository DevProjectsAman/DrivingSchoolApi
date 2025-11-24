using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_School_Operating_Hour")]
public class TbSchoolOperatingHour
{
    [Key]
    public int OperatingHoursId { get; set; }

    [Required]
    public int SchoolId { get; set; }

    /// <summary>
    /// Day of week: 0 = Sunday, 1 = Monday, ..., 6 = Saturday
    /// </summary>
    [Required]
    [Range(0, 6)]
    public int DayOfWeek { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    /// <summary>
    /// Is this day a working day for the school?
    /// </summary>
    public bool IsWorkingDay { get; set; } = true;

    [MaxLength(200)]
    public string Notes { get; set; }

    // Navigation Property
    [ForeignKey(nameof(SchoolId))]
    public virtual TbSchool School { get; set; }
}