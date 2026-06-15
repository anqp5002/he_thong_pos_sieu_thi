using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeThongPOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDummyDataFinal : Migration
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

            migrationBuilder.InsertData(
                table: "KhachHangs",
                columns: new[] { "Id", "DiemTichLuy", "Email", "HoTen", "NgayTao", "SoDienThoai" },
                values: new object[,]
                {
                    { 1, 28, "khachhang1@example.com", "Khách hàng 1", new DateTime(2026, 6, 12, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9957), "0900000001" },
                    { 2, 120, "khachhang2@example.com", "Khách hàng 2", new DateTime(2026, 6, 11, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9967), "0900000002" },
                    { 3, 201, "khachhang3@example.com", "Khách hàng 3", new DateTime(2026, 6, 10, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9977), "0900000003" },
                    { 4, 212, "khachhang4@example.com", "Khách hàng 4", new DateTime(2026, 6, 9, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9981), "0900000004" },
                    { 5, 310, "khachhang5@example.com", "Khách hàng 5", new DateTime(2026, 6, 8, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9986), "0900000005" },
                    { 6, 17, "khachhang6@example.com", "Khách hàng 6", new DateTime(2026, 6, 7, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9990), "0900000006" },
                    { 7, 316, "khachhang7@example.com", "Khách hàng 7", new DateTime(2026, 6, 6, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9993), "0900000007" },
                    { 8, 15, "khachhang8@example.com", "Khách hàng 8", new DateTime(2026, 6, 5, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9996), "0900000008" },
                    { 9, 209, "khachhang9@example.com", "Khách hàng 9", new DateTime(2026, 6, 4, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(4), "0900000009" },
                    { 10, 158, "khachhang10@example.com", "Khách hàng 10", new DateTime(2026, 6, 3, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(10), "0900000010" },
                    { 11, 312, "khachhang11@example.com", "Khách hàng 11", new DateTime(2026, 6, 2, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(13), "0900000011" },
                    { 12, 417, "khachhang12@example.com", "Khách hàng 12", new DateTime(2026, 6, 1, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(15), "0900000012" },
                    { 13, 6, "khachhang13@example.com", "Khách hàng 13", new DateTime(2026, 5, 31, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(19), "0900000013" },
                    { 14, 473, "khachhang14@example.com", "Khách hàng 14", new DateTime(2026, 5, 30, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(22), "0900000014" },
                    { 15, 300, "khachhang15@example.com", "Khách hàng 15", new DateTime(2026, 5, 29, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(24), "0900000015" },
                    { 16, 212, "khachhang16@example.com", "Khách hàng 16", new DateTime(2026, 5, 28, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(27), "0900000016" },
                    { 17, 223, "khachhang17@example.com", "Khách hàng 17", new DateTime(2026, 5, 27, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(31), "0900000017" },
                    { 18, 497, "khachhang18@example.com", "Khách hàng 18", new DateTime(2026, 5, 26, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(35), "0900000018" },
                    { 19, 23, "khachhang19@example.com", "Khách hàng 19", new DateTime(2026, 5, 25, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(37), "0900000019" },
                    { 20, 87, "khachhang20@example.com", "Khách hàng 20", new DateTime(2026, 5, 24, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(40), "0900000020" }
                });

            migrationBuilder.InsertData(
                table: "VaiTros",
                columns: new[] { "Id", "MoTa", "TenVaiTro" },
                values: new object[] { 1, "", "Quản lý" });

            migrationBuilder.InsertData(
                table: "NHAN_VIEN",
                columns: new[] { "Id", "HoTen", "NgayTao", "PasswordHash", "TrangThai", "Username", "VaiTroId" },
                values: new object[] { 1, "Nhân viên 1", new DateTime(2026, 6, 13, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9795), "", true, "", 1 });

            migrationBuilder.InsertData(
                table: "SAN_PHAM",
                columns: new[] { "Id", "DanhMucId", "DonViTinh", "GiaBan", "MaVach", "NgayTao", "TenSanPham", "TonKho", "TrangThai" },
                values: new object[,]
                {
                    { 1, 4, "Hộp", 367000m, "8930000000001", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(75), "Sản phẩm 1", 63, true },
                    { 2, 3, "Cái", 254000m, "8930000000002", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(84), "Sản phẩm 2", 78, true },
                    { 3, 2, "Hộp", 188000m, "8930000000003", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(86), "Sản phẩm 3", 62, true },
                    { 4, 2, "Cái", 113000m, "8930000000004", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(88), "Sản phẩm 4", 24, true },
                    { 5, 3, "Hộp", 440000m, "8930000000005", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(89), "Sản phẩm 5", 20, true },
                    { 6, 2, "Cái", 50000m, "8930000000006", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(92), "Sản phẩm 6", 99, true },
                    { 7, 3, "Hộp", 349000m, "8930000000007", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(94), "Sản phẩm 7", 29, true },
                    { 8, 2, "Cái", 496000m, "8930000000008", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(95), "Sản phẩm 8", 76, true },
                    { 9, 2, "Hộp", 276000m, "8930000000009", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(97), "Sản phẩm 9", 21, true },
                    { 10, 3, "Cái", 438000m, "8930000000010", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(99), "Sản phẩm 10", 48, true },
                    { 11, 3, "Hộp", 231000m, "8930000000011", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(105), "Sản phẩm 11", 51, true },
                    { 12, 4, "Cái", 230000m, "8930000000012", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(107), "Sản phẩm 12", 20, true },
                    { 13, 2, "Hộp", 145000m, "8930000000013", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(108), "Sản phẩm 13", 96, true },
                    { 14, 4, "Cái", 494000m, "8930000000014", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(110), "Sản phẩm 14", 20, true },
                    { 15, 1, "Hộp", 368000m, "8930000000015", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(111), "Sản phẩm 15", 66, true },
                    { 16, 3, "Cái", 22000m, "8930000000016", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(112), "Sản phẩm 16", 16, true },
                    { 17, 2, "Hộp", 336000m, "8930000000017", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(114), "Sản phẩm 17", 15, true },
                    { 18, 1, "Cái", 411000m, "8930000000018", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(116), "Sản phẩm 18", 15, true },
                    { 19, 1, "Hộp", 238000m, "8930000000019", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(117), "Sản phẩm 19", 71, true },
                    { 20, 3, "Cái", 223000m, "8930000000020", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(119), "Sản phẩm 20", 55, true },
                    { 21, 3, "Hộp", 247000m, "8930000000021", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(120), "Sản phẩm 21", 63, true },
                    { 22, 3, "Cái", 352000m, "8930000000022", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(121), "Sản phẩm 22", 27, true },
                    { 23, 2, "Hộp", 426000m, "8930000000023", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(123), "Sản phẩm 23", 21, true },
                    { 24, 3, "Cái", 27000m, "8930000000024", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(124), "Sản phẩm 24", 74, true },
                    { 25, 1, "Hộp", 246000m, "8930000000025", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(125), "Sản phẩm 25", 82, true },
                    { 26, 4, "Cái", 90000m, "8930000000026", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(126), "Sản phẩm 26", 62, true },
                    { 27, 4, "Hộp", 86000m, "8930000000027", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(128), "Sản phẩm 27", 49, true },
                    { 28, 3, "Cái", 397000m, "8930000000028", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(129), "Sản phẩm 28", 86, true },
                    { 29, 4, "Hộp", 495000m, "8930000000029", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(130), "Sản phẩm 29", 40, true },
                    { 30, 1, "Cái", 35000m, "8930000000030", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(132), "Sản phẩm 30", 63, true },
                    { 31, 4, "Hộp", 355000m, "8930000000031", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(133), "Sản phẩm 31", 81, true },
                    { 32, 3, "Cái", 453000m, "8930000000032", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(134), "Sản phẩm 32", 46, true },
                    { 33, 2, "Hộp", 154000m, "8930000000033", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(136), "Sản phẩm 33", 24, true },
                    { 34, 1, "Cái", 370000m, "8930000000034", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(138), "Sản phẩm 34", 79, true },
                    { 35, 4, "Hộp", 450000m, "8930000000035", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(140), "Sản phẩm 35", 35, true },
                    { 36, 2, "Cái", 119000m, "8930000000036", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(141), "Sản phẩm 36", 81, true },
                    { 37, 3, "Hộp", 211000m, "8930000000037", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(142), "Sản phẩm 37", 78, true },
                    { 38, 3, "Cái", 428000m, "8930000000038", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(144), "Sản phẩm 38", 35, true },
                    { 39, 4, "Hộp", 174000m, "8930000000039", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(145), "Sản phẩm 39", 83, true },
                    { 40, 3, "Cái", 101000m, "8930000000040", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(146), "Sản phẩm 40", 95, true },
                    { 41, 2, "Hộp", 318000m, "8930000000041", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(147), "Sản phẩm 41", 32, true },
                    { 42, 4, "Cái", 155000m, "8930000000042", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(149), "Sản phẩm 42", 55, true },
                    { 43, 3, "Hộp", 71000m, "8930000000043", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(156), "Sản phẩm 43", 49, true },
                    { 44, 3, "Cái", 451000m, "8930000000044", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(157), "Sản phẩm 44", 19, true },
                    { 45, 3, "Hộp", 238000m, "8930000000045", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(158), "Sản phẩm 45", 64, true },
                    { 46, 2, "Cái", 36000m, "8930000000046", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(160), "Sản phẩm 46", 10, true },
                    { 47, 1, "Hộp", 216000m, "8930000000047", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(161), "Sản phẩm 47", 81, true },
                    { 48, 3, "Cái", 327000m, "8930000000048", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(163), "Sản phẩm 48", 49, true },
                    { 49, 1, "Hộp", 440000m, "8930000000049", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(164), "Sản phẩm 49", 76, true },
                    { 50, 3, "Cái", 378000m, "8930000000050", new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(165), "Sản phẩm 50", 48, true }
                });

            migrationBuilder.InsertData(
                table: "DON_HANG",
                columns: new[] { "Id", "ChietKhau", "GhiChu", "KhachHangId", "MaDonHang", "NgayTao", "NhanVienId", "ThueVAT", "TongThanhToan", "TongTienHang", "TrangThai" },
                values: new object[,]
                {
                    { 1, 0m, "Đơn hàng mẫu 1", 14, "DH202605290001", new DateTime(2026, 5, 29, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(212), 1, 0m, 126000m, 0m, 3 },
                    { 2, 0m, "Đơn hàng mẫu 2", 4, "DH202605150002", new DateTime(2026, 5, 15, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(281), 1, 0m, 482000m, 0m, 1 },
                    { 3, 0m, "Đơn hàng mẫu 3", 14, "DH202606120003", new DateTime(2026, 6, 12, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(285), 1, 0m, 214000m, 0m, 2 },
                    { 4, 0m, "Đơn hàng mẫu 4", 3, "DH202606060004", new DateTime(2026, 6, 6, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(289), 1, 0m, 488000m, 0m, 3 },
                    { 5, 0m, "Đơn hàng mẫu 5", 1, "DH202605220005", new DateTime(2026, 5, 22, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(292), 1, 0m, 463000m, 0m, 2 },
                    { 6, 0m, "Đơn hàng mẫu 6", 8, "DH202605270006", new DateTime(2026, 5, 27, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(296), 1, 0m, 204000m, 0m, 3 },
                    { 7, 0m, "Đơn hàng mẫu 7", 9, "DH202605150007", new DateTime(2026, 5, 15, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(300), 1, 0m, 417000m, 0m, 3 },
                    { 8, 0m, "Đơn hàng mẫu 8", 3, "DH202605280008", new DateTime(2026, 5, 28, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(303), 1, 0m, 237000m, 0m, 2 },
                    { 9, 0m, "Đơn hàng mẫu 9", 11, "DH202605240009", new DateTime(2026, 5, 24, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(306), 1, 0m, 482000m, 0m, 1 },
                    { 10, 0m, "Đơn hàng mẫu 10", 18, "DH202606070010", new DateTime(2026, 6, 7, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(309), 1, 0m, 67000m, 0m, 1 }
                });

            migrationBuilder.InsertData(
                table: "CHI_TIET_DON_HANG",
                columns: new[] { "DonHangId", "SanPhamId", "DonGia", "SoLuong", "ThanhTien" },
                values: new object[,]
                {
                    { 1, 16, 78000m, 4, 0m },
                    { 1, 17, 90000m, 1, 0m },
                    { 1, 21, 102000m, 3, 0m },
                    { 2, 28, 45000m, 4, 0m },
                    { 3, 32, 82000m, 2, 0m },
                    { 3, 41, 120000m, 4, 0m },
                    { 4, 33, 102000m, 3, 0m },
                    { 5, 1, 71000m, 2, 0m },
                    { 5, 5, 179000m, 2, 0m },
                    { 5, 21, 72000m, 1, 0m },
                    { 5, 40, 47000m, 3, 0m },
                    { 6, 45, 97000m, 2, 0m },
                    { 7, 33, 18000m, 1, 0m },
                    { 8, 15, 58000m, 3, 0m },
                    { 8, 20, 183000m, 4, 0m },
                    { 8, 23, 108000m, 3, 0m },
                    { 8, 33, 172000m, 3, 0m },
                    { 9, 10, 41000m, 1, 0m },
                    { 9, 37, 38000m, 3, 0m },
                    { 9, 38, 175000m, 4, 0m },
                    { 9, 39, 30000m, 1, 0m },
                    { 10, 8, 134000m, 3, 0m },
                    { 10, 22, 90000m, 4, 0m },
                    { 10, 26, 118000m, 3, 0m },
                    { 10, 39, 55000m, 1, 0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 1, 16 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 1, 17 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 1, 21 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 2, 28 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 3, 32 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 3, 41 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 4, 33 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 5, 21 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 5, 40 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 6, 45 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 7, 33 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 8, 15 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 8, 20 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 8, 23 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 8, 33 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 9, 10 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 9, 37 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 9, 38 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 9, 39 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 10, 8 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 10, 22 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 10, 26 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 10, 39 });

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 45);

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

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "NHAN_VIEN",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VaiTros",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
