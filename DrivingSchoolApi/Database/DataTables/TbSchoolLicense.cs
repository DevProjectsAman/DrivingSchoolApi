using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_School_License")]
public class TbSchoolLicense
{
    [Required]
    public int SchoolId { get; set; }

    [Required]
    public int LicenseId { get; set; }

    public bool IsAvailable { get; set; } = true;

    // Navigation Properties
    [ForeignKey(nameof(SchoolId))]
    public virtual TbSchool School { get; set; }

    [ForeignKey(nameof(LicenseId))]
    public virtual TbLicenseType LicenseType { get; set; }
}