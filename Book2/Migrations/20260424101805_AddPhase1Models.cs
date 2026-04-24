using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2.Migrations
{
    /// <inheritdoc />
    public partial class AddPhase1Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NhaXuatBanId",
                table: "Saches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TacGiaId",
                table: "Saches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPercent = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageLimit = table.Column<int>(type: "int", nullable: false),
                    UsageCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhaXuatBans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNXB = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaXuatBans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TacGias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTacGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QuocTich = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TieuSu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacGias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Saches_NhaXuatBanId",
                table: "Saches",
                column: "NhaXuatBanId");

            migrationBuilder.CreateIndex(
                name: "IX_Saches_TacGiaId",
                table: "Saches",
                column: "TacGiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saches_NhaXuatBans_NhaXuatBanId",
                table: "Saches",
                column: "NhaXuatBanId",
                principalTable: "NhaXuatBans",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Saches_TacGias_TacGiaId",
                table: "Saches",
                column: "TacGiaId",
                principalTable: "TacGias",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saches_NhaXuatBans_NhaXuatBanId",
                table: "Saches");

            migrationBuilder.DropForeignKey(
                name: "FK_Saches_TacGias_TacGiaId",
                table: "Saches");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "NhaXuatBans");

            migrationBuilder.DropTable(
                name: "TacGias");

            migrationBuilder.DropIndex(
                name: "IX_Saches_NhaXuatBanId",
                table: "Saches");

            migrationBuilder.DropIndex(
                name: "IX_Saches_TacGiaId",
                table: "Saches");

            migrationBuilder.DropColumn(
                name: "NhaXuatBanId",
                table: "Saches");

            migrationBuilder.DropColumn(
                name: "TacGiaId",
                table: "Saches");
        }
    }
}
