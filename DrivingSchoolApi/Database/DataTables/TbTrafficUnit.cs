using HRsystem.Api.Database.DataTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;


[Table("Tb_Traffic_Unit")]
public class TbTrafficUnit
{
    [Key]
    public int TrafficUnitId { get; set; }

    [Required]
    [MaxLength(200)]
    public string UnitName { get; set; }
    
    [Required]
    public int GovId { get; set; }

    [MaxLength(500)]
    public string Location { get; set; }

    [MaxLength(50)]
    public string Code { get; set; }

    // Navigation Properties
    // Note: No direct FK to Payment (flexible PaymentLocationId)
    [ForeignKey(nameof(GovId))]
    public virtual TbGov Gov { get; set; }
}