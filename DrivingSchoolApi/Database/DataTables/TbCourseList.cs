using DrivingSchoolApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DrivingSchoolApi.Enums.EnumsList;


namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Course_List")]

public class TbCourseList
{
    [Key]
    public int CourseId { get; set; }

    [Required]
    [MaxLength(200)]
    public string CourseName { get; set; }

    [Required]
    public int LicenseId { get; set; }

    [Required]
    public SessionType SessionType { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal DurationHours { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(LicenseId))]
    public virtual TbLicenseType LicenseType { get; set; }

    public virtual ICollection<TbSessionAttendance> SessionAttendances { get; set; }

}