using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2.Migrations
{
    /// <inheritdoc />
    public partial class AddCouponToGioHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "GioHangs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_CouponId",
                table: "GioHangs",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHangs_Coupons_CouponId",
                table: "GioHangs",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHangs_Coupons_CouponId",
                table: "GioHangs");

            migrationBuilder.DropIndex(
                name: "IX_GioHangs_CouponId",
                table: "GioHangs");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "GioHangs");
        }
    }
}
