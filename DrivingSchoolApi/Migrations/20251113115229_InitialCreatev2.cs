using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchoolApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "DurationTime",
                table: "Tb_Session_Attendance",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastPasswordChangedAt", "RowGuid" },
                values: new object[] { new DateTime(2025, 11, 13, 13, 52, 28, 558, DateTimeKind.Local).AddTicks(3085), new DateTime(2025, 11, 13, 11, 52, 28, 558, DateTimeKind.Utc).AddTicks(4920), new Guid("e550be0e-d7cf-4649-90ca-af47dc834abc") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationTime",
                table: "Tb_Session_Attendance");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastPasswordChangedAt", "RowGuid" },
                values: new object[] { new DateTime(2025, 11, 13, 12, 10, 16, 349, DateTimeKind.Local).AddTicks(9697), new DateTime(2025, 11, 13, 10, 10, 16, 350, DateTimeKind.Utc).AddTicks(1519), new Guid("4a13df32-9cef-42bd-addc-0285a7218725") });
        }
    }
}
