using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeThongPOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHinhAnhUrlToSanPham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HinhAnhUrl",
                table: "SAN_PHAM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhAnhUrl",
                table: "SAN_PHAM");
        }
    }
}
