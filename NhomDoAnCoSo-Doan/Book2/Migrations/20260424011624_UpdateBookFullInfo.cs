using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookFullInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayXuatBan",
                table: "Saches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NgonNgu",
                table: "Saches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhaXuatBan",
                table: "Saches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoTrang",
                table: "Saches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TrongLuong",
                table: "Saches",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayXuatBan",
                table: "Saches");

            migrationBuilder.DropColumn(
                name: "NgonNgu",
                table: "Saches");

            migrationBuilder.DropColumn(
                name: "NhaXuatBan",
                table: "Saches");

            migrationBuilder.DropColumn(
                name: "SoTrang",
                table: "Saches");

            migrationBuilder.DropColumn(
                name: "TrongLuong",
                table: "Saches");
        }
    }
}
