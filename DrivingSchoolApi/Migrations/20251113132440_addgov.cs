using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchoolApi.Migrations
{
    /// <inheritdoc />
    public partial class addgov : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GovId",
                table: "Tb_Traffic_Unit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GovId",
                table: "Tb_School",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tb_Gov",
                columns: table => new
                {
                    GovId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GoveCode = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GovName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GovArea = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Gov", x => x.GovId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastPasswordChangedAt", "RowGuid" },
                values: new object[] { new DateTime(2025, 11, 13, 15, 24, 39, 638, DateTimeKind.Local).AddTicks(9121), new DateTime(2025, 11, 13, 13, 24, 39, 639, DateTimeKind.Utc).AddTicks(880), new Guid("523aac64-3ed9-40e3-8636-5bc8787706b8") });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Traffic_Unit_GovId",
                table: "Tb_Traffic_Unit",
                column: "GovId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_School_GovId",
                table: "Tb_School",
                column: "GovId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_School_Tb_Gov_GovId",
                table: "Tb_School",
                column: "GovId",
                principalTable: "Tb_Gov",
                principalColumn: "GovId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_Traffic_Unit_Tb_Gov_GovId",
                table: "Tb_Traffic_Unit",
                column: "GovId",
                principalTable: "Tb_Gov",
                principalColumn: "GovId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_School_Tb_Gov_GovId",
                table: "Tb_School");

            migrationBuilder.DropForeignKey(
                name: "FK_Tb_Traffic_Unit_Tb_Gov_GovId",
                table: "Tb_Traffic_Unit");

            migrationBuilder.DropTable(
                name: "Tb_Gov");

            migrationBuilder.DropIndex(
                name: "IX_Tb_Traffic_Unit_GovId",
                table: "Tb_Traffic_Unit");

            migrationBuilder.DropIndex(
                name: "IX_Tb_School_GovId",
                table: "Tb_School");

            migrationBuilder.DropColumn(
                name: "GovId",
                table: "Tb_Traffic_Unit");

            migrationBuilder.DropColumn(
                name: "GovId",
                table: "Tb_School");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastPasswordChangedAt", "RowGuid" },
                values: new object[] { new DateTime(2025, 11, 13, 13, 52, 28, 558, DateTimeKind.Local).AddTicks(3085), new DateTime(2025, 11, 13, 11, 52, 28, 558, DateTimeKind.Utc).AddTicks(4920), new Guid("e550be0e-d7cf-4649-90ca-af47dc834abc") });
        }
    }
}
