using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DrivingSchoolApi.Enums.EnumsList;


namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Session_Attendance")]
public class TbSessionAttendance
{
    [Key]
    public int AttendanceId { get; set; }

    [Required]
    public int ReservationId { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    public int InstructorId { get; set; } // ⭐ المحاضر المسؤول عن الحصة

    [Required]
    public DateTime SessionDate { get; set; } // ⭐ تاريخ الحصة

    [Required]
    public TimeSpan StartTime { get; set; } // ⭐ وقت البداية

    public TimeSpan DurationTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; } // ⭐ وقت النهاية

    [Required]
    public AttendanceStatus AttendanceStatus { get; set; } = AttendanceStatus.Scheduled;

    public DateTime? AttendanceDate { get; set; } // متى تم تسجيل الحضور الفعلي

    [MaxLength(500)]
    public string Notes { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(ReservationId))]
    public virtual TbReservation Reservation { get; set; }

    [ForeignKey(nameof(CourseId))]
    public virtual TbCourseList Course { get; set; }

    [ForeignKey(nameof(InstructorId))]
    public virtual TbEmployee Instructor { get; set; }
}
