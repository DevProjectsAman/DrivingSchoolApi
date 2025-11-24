using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchoolApi.Migrations
{
    /// <inheritdoc />
    public partial class add_tb_school_operating_hours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Tb_School",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Tb_School",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Tb_School_Operating_Hour",
                columns: table => new
                {
                    OperatingHoursId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    IsWorkingDay = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    Notes = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_School_Operating_Hour", x => x.OperatingHoursId);
                    table.ForeignKey(
                        name: "FK_Tb_School_Operating_Hour_Tb_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Tb_School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastPasswordChangedAt", "RowGuid" },
                values: new object[] { new DateTime(2025, 11, 24, 11, 7, 40, 257, DateTimeKind.Local).AddTicks(4679), new DateTime(2025, 11, 24, 9, 7, 40, 257, DateTimeKind.Utc).AddTicks(6506), new Guid("a74caa13-8eb3-4ee3-9d0b-b6ed06573a18") });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_School_Operating_Hour_SchoolId_DayOfWeek",
                table: "Tb_School_Operating_Hour",
                columns: new[] { "SchoolId", "DayOfWeek" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_School_Operating_Hour");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Tb_School");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Tb_School");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastPasswordChangedAt", "RowGuid" },
                values: new object[] { new DateTime(2025, 11, 13, 15, 24, 39, 638, DateTimeKind.Local).AddTicks(9121), new DateTime(2025, 11, 13, 13, 24, 39, 639, DateTimeKind.Utc).AddTicks(880), new Guid("523aac64-3ed9-40e3-8636-5bc8787706b8") });
        }
    }
}
