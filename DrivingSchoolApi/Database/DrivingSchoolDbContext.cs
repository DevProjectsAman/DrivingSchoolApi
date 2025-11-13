using DrivingSchoolApi.Database.DataSeeder;
using DrivingSchoolApi.Database.DataTables;

//using DrivingSchoolApi.Database.DataTables;
using DrivingSchoolApi.Database.Entities;
using DrivingSchoolApi.Shared.DTO;
using HRsystem.Api.Database.DataTables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Database;

public class DrivingSchoolDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public DrivingSchoolDbContext(DbContextOptions<DrivingSchoolDbContext> options)
        : base(options)
    {
    }


    // DbSets
    public DbSet<TbSchool> TbSchools { get; set; }
    public DbSet<TbLicenseType> TbLicenseTypes { get; set; }
    public DbSet<TbRole> TbRoles { get; set; }
    public DbSet<TbGov> TbGov { get; set; }
    public DbSet<TbEmployee> TbEmployees { get; set; }
    public DbSet<TbTransmissionType> TbTransmissionTypes { get; set; }
    public DbSet<TbVehicle> TbVehicles { get; set; }
    public DbSet<TbCustomer> TbCustomers { get; set; }
    public DbSet<TbReservation> TbReservations { get; set; }
    public DbSet<TbCourseList> TbCourseLists { get; set; }
    public DbSet<TbSessionAttendance> TbSessionAttendances { get; set; }
    public DbSet<TbEmployeeLicenseExpertise> TbEmployeeLicenseExpertises { get; set; }
    public DbSet<TbTrafficUnit> TbTrafficUnits { get; set; }
    public DbSet<TbPayment> TbPayments { get; set; }
    public DbSet<TbLicenseGroup> TbLicenseGroups { get; set; }
    public DbSet<TbLicenseGroupMember> TbLicenseGroupMembers { get; set; }
    public DbSet<TbSchoolLicense> TbSchoolLicenses { get; set; }
    public virtual DbSet<AspPermission> AspPermissions { get; set; }
    public virtual DbSet<AspRolePermissions> AspRolePermissions { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply Identity configurations
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

        // 🔹 Global case-insensitive JSON options
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        };

        // 🔹 Helper for localized data conversion
        ValueConverter<LocalizedData, string> LocalizedConverter =
            new ValueConverter<LocalizedData, string>(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => JsonSerializer.Deserialize<LocalizedData>(v, jsonOptions)!
            );


        // 🔹 Composite key for Role-Permission
        modelBuilder.Entity<AspRolePermissions>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        modelBuilder.Entity<AspRolePermissions>()
            .HasOne(rp => rp.Role)
            .WithMany()
            .HasForeignKey(rp => rp.RoleId);

        modelBuilder.Entity<AspRolePermissions>()
            .HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);

        // 🔹 Unique index on permission names
        modelBuilder.Entity<AspPermission>()
            .HasIndex(p => p.PermissionName)
            .IsUnique();


        //modelBuilder.Entity<TbShift>(entity =>
        //{
        //    entity.Property(e => e.ShiftName)
        //          .HasConversion(LocalizedConverter)
        //          .HasColumnType("json");
        //});


        // 🔹 Composite key for Role-Permission
        // ==================== COMPOSITE KEYS ====================

        // TbLicenseGroupMembers - Composite PK
        modelBuilder.Entity<TbLicenseGroupMember>()
            .HasKey(lgm => new { lgm.GroupId, lgm.LicenseId });

        // TbSchoolLicenses - Composite PK
        modelBuilder.Entity<TbSchoolLicense>()
            .HasKey(sl => new { sl.SchoolId, sl.LicenseId });

        // ==================== UNIQUE INDEXES ====================

        modelBuilder.Entity<TbVehicle>()
            .HasIndex(v => v.PlateNumber)
            .IsUnique();

        modelBuilder.Entity<TbCustomer>()
            .HasIndex(c => c.Phone)
            .IsUnique();

        modelBuilder.Entity<TbCustomer>()
            .HasIndex(c => c.NationalId)
            .IsUnique();

        modelBuilder.Entity<TbPayment>()
            .HasIndex(p => p.ReceiptSerial)
            .IsUnique();

        modelBuilder.Entity<TbEmployeeLicenseExpertise>()
            .HasIndex(e => new { e.EmployeeId, e.LicenseGroupId })
            .IsUnique();

        // ==================== PERFORMANCE INDEXES ====================

        modelBuilder.Entity<TbReservation>()
            .HasIndex(r => new { r.CustomerId, r.Status });

        modelBuilder.Entity<TbReservation>()
            .HasIndex(r => new { r.SchoolId, r.LicenseId });

        modelBuilder.Entity<TbReservation>()
            .HasIndex(r => r.PaymentId)
            .IsUnique(); // 1:1 relationship

        modelBuilder.Entity<TbPayment>()
            .HasIndex(p => new { p.CustomerId, p.PaymentDate });

        modelBuilder.Entity<TbSessionAttendance>()
            .HasIndex(sa => new { sa.ReservationId, sa.SessionDate });

        modelBuilder.Entity<TbSessionAttendance>()
            .HasIndex(sa => new { sa.InstructorId, sa.SessionDate });

        modelBuilder.Entity<TbCourseList>()
            .HasIndex(cl => new { cl.LicenseId, cl.SessionType });

        // ==================== RELATIONSHIPS ====================

        modelBuilder.Entity<TbTrafficUnit>()
            .HasOne(t => t.Gov)
            .WithMany(g => g.TrafficUnits)
            .HasForeignKey(t => t.GovId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TbSchool>()
            .HasOne(s => s.Gov)
            .WithMany(g => g.Schools)
            .HasForeignKey(s => s.GovId)
            .OnDelete(DeleteBehavior.Restrict);



        // School -> Employees (1:M)
        modelBuilder.Entity<TbEmployee>()
            .HasOne(e => e.School)
            .WithMany(s => s.Employees)
            .HasForeignKey(e => e.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        // Role -> Employees (1:M)
        modelBuilder.Entity<TbEmployee>()
            .HasOne(e => e.Role)
            .WithMany(r => r.Employees)
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // School -> Vehicles (1:M)
        modelBuilder.Entity<TbVehicle>()
            .HasOne(v => v.School)
            .WithMany(s => s.Vehicles)
            .HasForeignKey(v => v.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        // TransmissionType -> Vehicles (1:M)
        modelBuilder.Entity<TbVehicle>()
            .HasOne(v => v.TransmissionType)
            .WithMany(t => t.Vehicles)
            .HasForeignKey(v => v.TransmissionId)
            .OnDelete(DeleteBehavior.Restrict);

        // LicenseType -> Vehicles (1:M)
        modelBuilder.Entity<TbVehicle>()
            .HasOne(v => v.LicenseType)
            .WithMany(l => l.Vehicles)
            .HasForeignKey(v => v.LicenseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Customer -> Reservations (1:M)
        modelBuilder.Entity<TbReservation>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // LicenseType -> Reservations (1:M)
        modelBuilder.Entity<TbReservation>()
            .HasOne(r => r.LicenseType)
            .WithMany(l => l.Reservations)
            .HasForeignKey(r => r.LicenseId)
            .OnDelete(DeleteBehavior.Restrict);

        // School -> Reservations (1:M)
        modelBuilder.Entity<TbReservation>()
            .HasOne(r => r.School)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        // Payment -> Reservation (1:1) ⭐
        modelBuilder.Entity<TbReservation>()
            .HasOne(r => r.Payment)
            .WithOne(p => p.Reservation)
            .HasForeignKey<TbReservation>(r => r.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Customer -> Payments (1:M) ⭐
        modelBuilder.Entity<TbPayment>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // LicenseType -> Payments (1:M) ⭐
        modelBuilder.Entity<TbPayment>()
            .HasOne(p => p.LicenseType)
            .WithMany(l => l.Payments)
            .HasForeignKey(p => p.LicenseId)
            .OnDelete(DeleteBehavior.Restrict);

        // LicenseGroup -> LicenseGroupMembers (1:M) ⭐
        modelBuilder.Entity<TbLicenseGroupMember>()
            .HasOne(lgm => lgm.LicenseGroup)
            .WithMany(lg => lg.LicenseGroupMembers)
            .HasForeignKey(lgm => lgm.GroupId)
            .OnDelete(DeleteBehavior.Restrict);

        // LicenseType -> LicenseGroupMembers (1:M) ⭐
        modelBuilder.Entity<TbLicenseGroupMember>()
            .HasOne(lgm => lgm.LicenseType)
            .WithMany(l => l.LicenseGroupMembers)
            .HasForeignKey(lgm => lgm.LicenseId)
            .OnDelete(DeleteBehavior.Restrict);

        // School -> SchoolLicenses (1:M) ⭐
        modelBuilder.Entity<TbSchoolLicense>()
            .HasOne(sl => sl.School)
            .WithMany(s => s.SchoolLicenses)
            .HasForeignKey(sl => sl.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        // LicenseType -> SchoolLicenses (1:M) ⭐
        modelBuilder.Entity<TbSchoolLicense>()
            .HasOne(sl => sl.LicenseType)
            .WithMany(l => l.SchoolLicenses)
            .HasForeignKey(sl => sl.LicenseId)
            .OnDelete(DeleteBehavior.Restrict);

        // CourseList Relationships
        modelBuilder.Entity<TbCourseList>()
            .HasOne(cl => cl.LicenseType)
            .WithMany(l => l.CourseLists)
            .HasForeignKey(cl => cl.LicenseId)
            .OnDelete(DeleteBehavior.Restrict);

        // SessionAttendance Relationships ⭐
        modelBuilder.Entity<TbSessionAttendance>()
            .HasOne(sa => sa.Reservation)
            .WithMany(r => r.SessionAttendances)
            .HasForeignKey(sa => sa.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TbSessionAttendance>()
            .HasOne(sa => sa.Course)
            .WithMany(c => c.SessionAttendances)
            .HasForeignKey(sa => sa.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TbSessionAttendance>()
            .HasOne(sa => sa.Instructor)
            .WithMany(e => e.SessionAttendances)
            .HasForeignKey(sa => sa.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        // EmployeeLicenseExpertise Relationships ⭐
        modelBuilder.Entity<TbEmployeeLicenseExpertise>()
            .HasOne(ele => ele.Employee)
            .WithMany(e => e.LicenseExpertises)
            .HasForeignKey(ele => ele.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TbEmployeeLicenseExpertise>()
            .HasOne(ele => ele.LicenseGroup)
            .WithMany(lg => lg.EmployeeExpertises)
            .HasForeignKey(ele => ele.LicenseGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==================== DEFAULT VALUES ====================

        modelBuilder.Entity<TbReservation>()
            .Property(r => r.ReservationDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<TbEmployee>()
            .Property(e => e.IsActive)
            .HasDefaultValue(true);

        modelBuilder.Entity<TbPayment>()
            .Property(p => p.PaymentDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<TbPayment>()
            .Property(p => p.ReceiptStatus)
            .HasDefaultValue(ReceiptStatus.Valid);

        modelBuilder.Entity<TbSchoolLicense>()
            .Property(sl => sl.IsAvailable)
            .HasDefaultValue(true);

        modelBuilder.Entity<TbSessionAttendance>()
            .Property(sa => sa.AttendanceStatus)
            .HasDefaultValue(AttendanceStatus.Scheduled);
    }

}