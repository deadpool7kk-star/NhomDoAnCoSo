using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2.Migrations
{
    /// <inheritdoc />
    public partial class Phase1Upgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuocTich",
                table: "TacGias");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "NhaXuatBans");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "IsPercent",
                table: "Coupons");

            migrationBuilder.RenameColumn(
                name: "TenNXB",
                table: "NhaXuatBans",
                newName: "TenNhaXuatBan");

            migrationBuilder.RenameColumn(
                name: "UsageLimit",
                table: "Coupons",
                newName: "SoLuong");

            migrationBuilder.RenameColumn(
                name: "UsageCount",
                table: "Coupons",
                newName: "PhanTramGiam");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "Coupons",
                newName: "NgayHetHan");

            migrationBuilder.AlterColumn<string>(
                name: "TenTacGia",
                table: "TacGias",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "TacGias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "NhaXuatBans",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiaChi",
                table: "NhaXuatBans",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "NhaXuatBans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBatDau",
                table: "Coupons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "TacGias");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "NhaXuatBans");

            migrationBuilder.DropColumn(
                name: "NgayBatDau",
                table: "Coupons");

            migrationBuilder.RenameColumn(
                name: "TenNhaXuatBan",
                table: "NhaXuatBans",
                newName: "TenNXB");

            migrationBuilder.RenameColumn(
                name: "SoLuong",
                table: "Coupons",
                newName: "UsageLimit");

            migrationBuilder.RenameColumn(
                name: "PhanTramGiam",
                table: "Coupons",
                newName: "UsageCount");

            migrationBuilder.RenameColumn(
                name: "NgayHetHan",
                table: "Coupons",
                newName: "ExpiryDate");

            migrationBuilder.AlterColumn<string>(
                name: "TenTacGia",
                table: "TacGias",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "QuocTich",
                table: "TacGias",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "NhaXuatBans",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiaChi",
                table: "NhaXuatBans",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "NhaXuatBans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Coupons",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPercent",
                table: "Coupons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
