using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_License_Type")]

public class TbLicenseType
{
    [Key]
    public int LicenseId { get; set; }

    [Required]
    [MaxLength(100)]
    public string LicenseName { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    // Navigation Properties
    public virtual ICollection<TbReservation> Reservations { get; set; }
    public virtual ICollection<TbVehicle> Vehicles { get; set; }
    public virtual ICollection<TbCourseList> CourseLists { get; set; } // ⭐ RENAMED
    public virtual ICollection<TbPayment> Payments { get; set; } // ⭐ NEW
    public virtual ICollection<TbLicenseGroupMember> LicenseGroupMembers { get; set; } // ⭐ NEW
    public virtual ICollection<TbSchoolLicense> SchoolLicenses { get; set; } // ⭐ NEW

}