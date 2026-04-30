using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2.Migrations
{
    /// <inheritdoc />
    public partial class AddBookTechnicalDetailsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KichThuoc",
                table: "Saches",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NamXuatBan",
                table: "Saches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NgonNgu",
                table: "Saches",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhaXuatBan",
                table: "Saches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoTrang",
                table: "Saches",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KichThuoc",
                table: "Saches");

            migrationBuilder.DropColumn(
                name: "NamXuatBan",
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
        }
    }
}
