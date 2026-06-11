using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeThongPOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPaymentAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PhuongThucThanhToans",
                columns: new[] { "Id", "TenPhuongThuc", "TrangThai" },
                values: new object[,]
                {
                    { 1, "Tiền mặt", true },
                    { 2, "Thẻ ngân hàng", true },
                    { 3, "Chuyển khoản", true }
                });

            migrationBuilder.InsertData(
                table: "VaiTros",
                columns: new[] { "Id", "MoTa", "TenVaiTro" },
                values: new object[,]
                {
                    { 1, "", "Admin" },
                    { 2, "", "Cashier" }
                });

            migrationBuilder.InsertData(
                table: "NHAN_VIEN",
                columns: new[] { "Id", "HoTen", "NgayTao", "PasswordHash", "TrangThai", "Username", "VaiTroId" },
                values: new object[] { 1, "Admin Tester", new DateTime(2026, 6, 12, 3, 8, 7, 370, DateTimeKind.Local).AddTicks(6990), "123", true, "admin", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NHAN_VIEN",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PhuongThucThanhToans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PhuongThucThanhToans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PhuongThucThanhToans",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VaiTros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VaiTros",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
