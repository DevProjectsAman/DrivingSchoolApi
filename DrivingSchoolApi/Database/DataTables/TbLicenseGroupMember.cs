using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_License_Group_Member")]
public class TbLicenseGroupMember
{
    [Required]
    public int GroupId { get; set; }

    [Required]
    public int LicenseId { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(GroupId))]
    public virtual TbLicenseGroup LicenseGroup { get; set; }

    [ForeignKey(nameof(LicenseId))]
    public virtual TbLicenseType LicenseType { get; set; }
}