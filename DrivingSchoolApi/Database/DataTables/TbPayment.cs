using DrivingSchoolApi.Database.DataTables;
using DrivingSchoolApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DrivingSchoolApi.Enums.EnumsList;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Payment")]
public class TbPayment
{
    [Key]
    public int PaymentId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int LicenseId { get; set; }

    [Required]
    public PaymentLocationType PaymentLocationType { get; set; }

    /// <summary>
    /// مرن: ممكن يكون SchoolId أو TrafficUnitId حسب PaymentLocationType
    /// </summary>
    [Required]
    public int PaymentLocationId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ReceiptSerial { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    [Required]
    public ReceiptStatus ReceiptStatus { get; set; } = ReceiptStatus.Valid;

    // Navigation Properties
    [ForeignKey(nameof(CustomerId))]
    public virtual TbCustomer Customer { get; set; }

    [ForeignKey(nameof(LicenseId))]
    public virtual TbLicenseType LicenseType { get; set; }

    public virtual TbReservation Reservation { get; set; } // 1:1



}