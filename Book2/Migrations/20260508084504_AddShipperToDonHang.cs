using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2.Migrations
{
    /// <inheritdoc />
    public partial class AddShipperToDonHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayGiao",
                table: "DonHangs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipperId",
                table: "DonHangs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_ShipperId",
                table: "DonHangs",
                column: "ShipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonHangs_AspNetUsers_ShipperId",
                table: "DonHangs",
                column: "ShipperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonHangs_AspNetUsers_ShipperId",
                table: "DonHangs");

            migrationBuilder.DropIndex(
                name: "IX_DonHangs_ShipperId",
                table: "DonHangs");

            migrationBuilder.DropColumn(
                name: "NgayGiao",
                table: "DonHangs");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                table: "DonHangs");
        }
    }
}
