using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeThongPOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DanhMucs",
                columns: new[] { "Id", "MoTa", "TenDanhMuc" },
                values: new object[,]
                {
                    { 1, "", "Đồ ăn nhanh" },
                    { 2, "", "Đồ uống" },
                    { 3, "", "Gia vị" },
                    { 4, "", "Hóa mỹ phẩm" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DanhMucs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DanhMucs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DanhMucs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DanhMucs",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
