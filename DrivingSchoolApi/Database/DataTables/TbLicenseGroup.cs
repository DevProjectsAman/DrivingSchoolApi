using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_License_Group")]
public class TbLicenseGroup
{
    [Key]
    public int GroupId { get; set; }

    [Required]
    [MaxLength(200)]
    public string GroupName { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    // Navigation Properties
    public virtual ICollection<TbLicenseGroupMember> LicenseGroupMembers { get; set; }
    public virtual ICollection<TbEmployeeLicenseExpertise> EmployeeExpertises { get; set; }

}