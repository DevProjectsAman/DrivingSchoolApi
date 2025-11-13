using HRsystem.Api.Database.DataTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_School")]
public class TbSchool
{
    [Key]
    public int SchoolId { get; set; }

    [Required]
    [MaxLength(200)]
    public string SchoolName { get; set; }

    [Required]
    public int GovId { get; set; }

    [MaxLength(500)]
    public string Location { get; set; }

    public int TotalLectureHalls { get; set; }

    public int SeatsPerHall { get; set; }

    [NotMapped]
    public int TotalCapacity => TotalLectureHalls * SeatsPerHall;

    // Navigation Properties
    public virtual ICollection<TbEmployee> Employees { get; set; }
    public virtual ICollection<TbVehicle> Vehicles { get; set; }
    public virtual ICollection<TbReservation> Reservations { get; set; }
    public virtual ICollection<TbSchoolLicense> SchoolLicenses { get; set; } // ⭐ NEW

    // Navigation Property
    [ForeignKey(nameof(GovId))]
    public virtual TbGov Gov { get; set; }
}
