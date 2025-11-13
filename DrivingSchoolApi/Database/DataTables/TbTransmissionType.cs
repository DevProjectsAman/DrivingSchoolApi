using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Transmission_Type")]

public class TbTransmissionType
{
    [Key]
    public int TransmissionId { get; set; }

    [Required]
    [MaxLength(50)]
    public string TypeName { get; set; }

    public virtual ICollection<TbVehicle> Vehicles { get; set; }
}