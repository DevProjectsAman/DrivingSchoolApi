using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchoolApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Tb_Session_Attendance_Tb_Course_Session_SessionId",
            //    table: "Tb_Session_Attendance");




            //migrationBuilder.RenameColumn(
            //    name: "TransmissionTypeName",
            //    table: "Tb_Transmission_Type",
            //    newName: "TypeName");

            //migrationBuilder.RenameColumn(
            //    name: "TransmissionTypeId",
            //    table: "Tb_Transmission_Type",
            //    newName: "TransmissionId");

            //migrationBuilder.RenameColumn(
            //    name: "SessionId",
            //    table: "Tb_Session_Attendance",
            //    newName: "InstructorId");

            //migrationBuilder.RenameColumn(
            //    name: "LicenseId",
            //    table: "Tb_Employee_License_Expertise",
            //    newName: "LicenseGroupId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Tb_Employee_License_Expertise_LicenseId",
            //    table: "Tb_Employee_License_Expertise",
            //    newName: "IX_Tb_Employee_License_Expertise_LicenseGroupId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Tb_Employee_License_Expertise_EmployeeId_LicenseId",
            //    table: "Tb_Employee_License_Expertise",
            //    newName: "IX_Tb_Employee_License_Expertise_EmployeeId_LicenseGroupId");

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceStatus",
                table: "Tb_Session_Attendance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AttendanceDate",
                table: "Tb_Session_Attendance",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            //migrationBuilder.AddColumn<int>(
            //    name: "CourseId",
            //    table: "Tb_Session_Attendance",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<TimeSpan>(
            //    name: "EndTime",
            //    table: "Tb_Session_Attendance",
            //    type: "time(6)",
            //    nullable: false,
            //    defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "SessionDate",
            //    table: "Tb_Session_Attendance",
            //    type: "datetime(6)",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<TimeSpan>(
            //    name: "StartTime",
            //    table: "Tb_Session_Attendance",
            //    type: "time(6)",
            //    nullable: false,
            //    defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            //migrationBuilder.AddColumn<int>(
            //    name: "PaymentId",
            //    table: "Tb_Reservation",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateTable(
            //    name: "Tb_Course_List",
            //    columns: table => new
            //    {
            //        CourseId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        CourseName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        LicenseId = table.Column<int>(type: "int", nullable: false),
            //        SessionType = table.Column<int>(type: "int", nullable: false),
            //        DurationHours = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tb_Course_List", x => x.CourseId);
            //        table.ForeignKey(
            //            name: "FK_Tb_Course_List_Tb_License_Type_LicenseId",
            //            column: x => x.LicenseId,
            //            principalTable: "Tb_License_Type",
            //            principalColumn: "LicenseId",
            //            onDelete: ReferentialAction.Restrict);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateTable(
            //    name: "Tb_License_Group",
            //    columns: table => new
            //    {
            //        GroupId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        GroupName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tb_License_Group", x => x.GroupId);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    PaymentLocationType = table.Column<int>(type: "int", nullable: false),
                    PaymentLocationId = table.Column<int>(type: "int", nullable: false),
                    ReceiptSerial = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ReceiptStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Tb_Payment_Tb_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Tb_Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Payment_Tb_License_Type_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Tb_License_Type",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_School_License",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_School_License", x => new { x.SchoolId, x.LicenseId });
                    table.ForeignKey(
                        name: "FK_Tb_School_License_Tb_License_Type_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Tb_License_Type",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_School_License_Tb_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Tb_School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Traffic_Unit",
                columns: table => new
                {
                    TrafficUnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UnitName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Traffic_Unit", x => x.TrafficUnitId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_License_Group_Member",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_License_Group_Member", x => new { x.GroupId, x.LicenseId });
                    table.ForeignKey(
                        name: "FK_Tb_License_Group_Member_Tb_License_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Tb_License_Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_License_Group_Member_Tb_License_Type_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Tb_License_Type",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastPasswordChangedAt", "RowGuid" },
                values: new object[] { new DateTime(2025, 11, 13, 12, 10, 16, 349, DateTimeKind.Local).AddTicks(9697), new DateTime(2025, 11, 13, 10, 10, 16, 350, DateTimeKind.Utc).AddTicks(1519), new Guid("4a13df32-9cef-42bd-addc-0285a7218725") });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Session_Attendance_CourseId",
                table: "Tb_Session_Attendance",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Session_Attendance_InstructorId_SessionDate",
                table: "Tb_Session_Attendance",
                columns: new[] { "InstructorId", "SessionDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Session_Attendance_ReservationId_SessionDate",
                table: "Tb_Session_Attendance",
                columns: new[] { "ReservationId", "SessionDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Reservation_PaymentId",
                table: "Tb_Reservation",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Course_List_LicenseId_SessionType",
                table: "Tb_Course_List",
                columns: new[] { "LicenseId", "SessionType" });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_License_Group_Member_LicenseId",
                table: "Tb_License_Group_Member",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Payment_CustomerId_PaymentDate",
                table: "Tb_Payment",
                columns: new[] { "CustomerId", "PaymentDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Payment_LicenseId",
                table: "Tb_Payment",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Payment_ReceiptSerial",
                table: "Tb_Payment",
                column: "ReceiptSerial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tb_School_License_LicenseId",
                table: "Tb_School_License",
                column: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Employee_License_Expertise_Tb_License_Group_LicenseGroupId",
                table: "Tb_Employee_License_Expertise",
                column: "LicenseGroupId",
                principalTable: "Tb_License_Group",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Reservation_Tb_Payment_PaymentId",
                table: "Tb_Reservation",
                column: "PaymentId",
                principalTable: "Tb_Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Session_Attendance_Tb_Course_List_CourseId",
                table: "Tb_Session_Attendance",
                column: "CourseId",
                principalTable: "Tb_Course_List",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Session_Attendance_Tb_Employee_InstructorId",
                table: "Tb_Session_Attendance",
                column: "InstructorId",
                principalTable: "Tb_Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_Employee_License_Expertise_Tb_License_Group_LicenseGroupId",
                table: "Tb_Employee_License_Expertise");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_Reservation_Tb_Payment_PaymentId",
                table: "Tb_Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_Session_Attendance_Tb_Course_List_CourseId",
                table: "Tb_Session_Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_Session_Attendance_Tb_Employee_InstructorId",
                table: "Tb_Session_Attendance");

            migrationBuilder.DropTable(
                name: "Tb_Course_List");

            migrationBuilder.DropTable(
                name: "Tb_License_Group_Member");

            migrationBuilder.DropTable(
                name: "Tb_Payment");

            migrationBuilder.DropTable(
                name: "Tb_School_License");

            migrationBuilder.DropTable(
                name: "Tb_Traffic_Unit");

            migrationBuilder.DropTable(
                name: "Tb_License_Group");

            migrationBuilder.DropIndex(
                name: "IX_Tb_Session_Attendance_CourseId",
                table: "Tb_Session_Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Tb_Session_Attendance_InstructorId_SessionDate",
                table: "Tb_Session_Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Tb_Session_Attendance_ReservationId_SessionDate",
                table: "Tb_Session_Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Tb_Reservation_PaymentId",
                table: "Tb_Reservation");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Tb_Session_Attendance");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Tb_Session_Attendance");

            migrationBuilder.DropColumn(
                name: "SessionDate",
                table: "Tb_Session_Attendance");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Tb_Session_Attendance");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Tb_Reservation");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "Tb_Transmission_Type",
                newName: "TransmissionTypeName");

            migrationBuilder.RenameColumn(
                name: "TransmissionId",
                table: "Tb_Transmission_Type",
                newName: "TransmissionTypeId");

            migrationBuilder.RenameColumn(
                name: "InstructorId",
                table: "Tb_Session_Attendance",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "LicenseGroupId",
                table: "Tb_Employee_License_Expertise",
                newName: "LicenseId");

            migrationBuilder.RenameIndex(
                name: "IX_Tb_Employee_License_Expertise_LicenseGroupId",
                table: "Tb_Employee_License_Expertise",
                newName: "IX_Tb_Employee_License_Expertise_LicenseId");

            migrationBuilder.RenameIndex(
                name: "IX_Tb_Employee_License_Expertise_EmployeeId_LicenseGroupId",
                table: "Tb_Employee_License_Expertise",
                newName: "IX_Tb_Employee_License_Expertise_EmployeeId_LicenseId");

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceStatus",
                table: "Tb_Session_Attendance",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AttendanceDate",
                table: "Tb_Session_Attendance",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Tb_Course_Session",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    DurationHours = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    SessionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Course_Session", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Tb_Course_Session_Tb_Employee_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Tb_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Course_Session_Tb_License_Type_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Tb_License_Type",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Course_Session_Tb_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Tb_School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastPasswordChangedAt", "RowGuid" },
                values: new object[] { new DateTime(2025, 11, 11, 13, 42, 45, 428, DateTimeKind.Local).AddTicks(242), new DateTime(2025, 11, 11, 11, 42, 45, 428, DateTimeKind.Utc).AddTicks(2546), new Guid("35195670-1818-4919-b1b9-255fa2e566a1") });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Session_Attendance_ReservationId_AttendanceDate",
                table: "Tb_Session_Attendance",
                columns: new[] { "ReservationId", "AttendanceDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Session_Attendance_SessionId",
                table: "Tb_Session_Attendance",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Course_Session_InstructorId",
                table: "Tb_Course_Session",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Course_Session_LicenseId",
                table: "Tb_Course_Session",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Course_Session_SchoolId_LicenseId_SessionType",
                table: "Tb_Course_Session",
                columns: new[] { "SchoolId", "LicenseId", "SessionType" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Employee_License_Expertise_Tb_License_Type_LicenseId",
                table: "Tb_Employee_License_Expertise",
                column: "LicenseId",
                principalTable: "Tb_License_Type",
                principalColumn: "LicenseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Session_Attendance_Tb_Course_Session_SessionId",
                table: "Tb_Session_Attendance",
                column: "SessionId",
                principalTable: "Tb_Course_Session",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
