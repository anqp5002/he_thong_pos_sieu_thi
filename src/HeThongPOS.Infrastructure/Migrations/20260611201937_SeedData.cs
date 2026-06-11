using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeThongPOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "KhachHangs",
                columns: new[] { "Id", "DiemTichLuy", "Email", "HoTen", "NgayTao", "SoDienThoai" },
                values: new object[,]
                {
                    { 1001, 0, "", "Khách hàng 1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000001" },
                    { 1002, 0, "", "Khách hàng 2", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000002" },
                    { 1003, 0, "", "Khách hàng 3", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000003" },
                    { 1004, 0, "", "Khách hàng 4", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000004" },
                    { 1005, 0, "", "Khách hàng 5", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000005" },
                    { 1006, 0, "", "Khách hàng 6", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000006" },
                    { 1007, 0, "", "Khách hàng 7", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000007" },
                    { 1008, 0, "", "Khách hàng 8", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000008" },
                    { 1009, 0, "", "Khách hàng 9", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000009" },
                    { 1010, 0, "", "Khách hàng 10", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000010" },
                    { 1011, 0, "", "Khách hàng 11", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000011" },
                    { 1012, 0, "", "Khách hàng 12", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000012" },
                    { 1013, 0, "", "Khách hàng 13", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000013" },
                    { 1014, 0, "", "Khách hàng 14", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000014" },
                    { 1015, 0, "", "Khách hàng 15", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000015" },
                    { 1016, 0, "", "Khách hàng 16", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000016" },
                    { 1017, 0, "", "Khách hàng 17", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000017" },
                    { 1018, 0, "", "Khách hàng 18", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000018" },
                    { 1019, 0, "", "Khách hàng 19", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000019" },
                    { 1020, 0, "", "Khách hàng 20", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0900000020" }
                });

            migrationBuilder.UpdateData(
                table: "NHAN_VIEN",
                keyColumn: "Id",
                keyValue: 1,
                column: "NgayTao",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "SAN_PHAM",
                columns: new[] { "Id", "DanhMucId", "DonViTinh", "GiaBan", "MaVach", "NgayTao", "TenSanPham", "TonKho", "TrangThai" },
                values: new object[,]
                {
                    { 1001, 2, "", 11000m, "8930000000001", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 1", 100, true },
                    { 1002, 3, "", 12000m, "8930000000002", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 2", 100, true },
                    { 1003, 4, "", 13000m, "8930000000003", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 3", 100, true },
                    { 1004, 1, "", 14000m, "8930000000004", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 4", 100, true },
                    { 1005, 2, "", 15000m, "8930000000005", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 5", 100, true },
                    { 1006, 3, "", 16000m, "8930000000006", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 6", 100, true },
                    { 1007, 4, "", 17000m, "8930000000007", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 7", 100, true },
                    { 1008, 1, "", 18000m, "8930000000008", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 8", 100, true },
                    { 1009, 2, "", 19000m, "8930000000009", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 9", 100, true },
                    { 1010, 3, "", 20000m, "8930000000010", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 10", 100, true },
                    { 1011, 4, "", 21000m, "8930000000011", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 11", 100, true },
                    { 1012, 1, "", 22000m, "8930000000012", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 12", 100, true },
                    { 1013, 2, "", 23000m, "8930000000013", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 13", 100, true },
                    { 1014, 3, "", 24000m, "8930000000014", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 14", 100, true },
                    { 1015, 4, "", 25000m, "8930000000015", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 15", 100, true },
                    { 1016, 1, "", 26000m, "8930000000016", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 16", 100, true },
                    { 1017, 2, "", 27000m, "8930000000017", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 17", 100, true },
                    { 1018, 3, "", 28000m, "8930000000018", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 18", 100, true },
                    { 1019, 4, "", 29000m, "8930000000019", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 19", 100, true },
                    { 1020, 1, "", 30000m, "8930000000020", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 20", 100, true },
                    { 1021, 2, "", 31000m, "8930000000021", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 21", 100, true },
                    { 1022, 3, "", 32000m, "8930000000022", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 22", 100, true },
                    { 1023, 4, "", 33000m, "8930000000023", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 23", 100, true },
                    { 1024, 1, "", 34000m, "8930000000024", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 24", 100, true },
                    { 1025, 2, "", 35000m, "8930000000025", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 25", 100, true },
                    { 1026, 3, "", 36000m, "8930000000026", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 26", 100, true },
                    { 1027, 4, "", 37000m, "8930000000027", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 27", 100, true },
                    { 1028, 1, "", 38000m, "8930000000028", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 28", 100, true },
                    { 1029, 2, "", 39000m, "8930000000029", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 29", 100, true },
                    { 1030, 3, "", 40000m, "8930000000030", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 30", 100, true },
                    { 1031, 4, "", 41000m, "8930000000031", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 31", 100, true },
                    { 1032, 1, "", 42000m, "8930000000032", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 32", 100, true },
                    { 1033, 2, "", 43000m, "8930000000033", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 33", 100, true },
                    { 1034, 3, "", 44000m, "8930000000034", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 34", 100, true },
                    { 1035, 4, "", 45000m, "8930000000035", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 35", 100, true },
                    { 1036, 1, "", 46000m, "8930000000036", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 36", 100, true },
                    { 1037, 2, "", 47000m, "8930000000037", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 37", 100, true },
                    { 1038, 3, "", 48000m, "8930000000038", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 38", 100, true },
                    { 1039, 4, "", 49000m, "8930000000039", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 39", 100, true },
                    { 1040, 1, "", 50000m, "8930000000040", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 40", 100, true },
                    { 1041, 2, "", 51000m, "8930000000041", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 41", 100, true },
                    { 1042, 3, "", 52000m, "8930000000042", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 42", 100, true },
                    { 1043, 4, "", 53000m, "8930000000043", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 43", 100, true },
                    { 1044, 1, "", 54000m, "8930000000044", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 44", 100, true },
                    { 1045, 2, "", 55000m, "8930000000045", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 45", 100, true },
                    { 1046, 3, "", 56000m, "8930000000046", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 46", 100, true },
                    { 1047, 4, "", 57000m, "8930000000047", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 47", 100, true },
                    { 1048, 1, "", 58000m, "8930000000048", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 48", 100, true },
                    { 1049, 2, "", 59000m, "8930000000049", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 49", 100, true },
                    { 1050, 3, "", 60000m, "8930000000050", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sản phẩm mẫu 50", 100, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1026);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1027);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1028);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1029);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1034);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1035);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1036);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1037);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1038);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1039);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1040);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1042);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1043);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1044);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1045);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1046);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1047);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1048);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1049);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1050);

            migrationBuilder.UpdateData(
                table: "NHAN_VIEN",
                keyColumn: "Id",
                keyValue: 1,
                column: "NgayTao",
                value: new DateTime(2026, 6, 12, 3, 8, 7, 370, DateTimeKind.Local).AddTicks(6990));
        }
    }
}
