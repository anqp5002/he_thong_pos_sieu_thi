using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeThongPOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentDummyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 1, 16 });

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

            migrationBuilder.UpdateData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 1, 17 },
                columns: new[] { "DonGia", "SoLuong" },
                values: new object[] { 146000m, 3 });

            migrationBuilder.InsertData(
                table: "CHI_TIET_DON_HANG",
                columns: new[] { "DonHangId", "SanPhamId", "DonGia", "SoLuong", "ThanhTien" },
                values: new object[,]
                {
                    { 1, 3, 122000m, 1, 0m },
                    { 1, 33, 118000m, 3, 0m },
                    { 1, 36, 196000m, 4, 0m },
                    { 2, 5, 132000m, 3, 0m },
                    { 2, 38, 86000m, 3, 0m },
                    { 2, 43, 34000m, 2, 0m },
                    { 3, 3, 18000m, 2, 0m },
                    { 4, 15, 176000m, 3, 0m },
                    { 4, 16, 178000m, 3, 0m },
                    { 4, 35, 172000m, 3, 0m },
                    { 5, 19, 98000m, 1, 0m },
                    { 5, 30, 107000m, 3, 0m },
                    { 5, 41, 33000m, 1, 0m },
                    { 6, 34, 107000m, 2, 0m },
                    { 6, 47, 92000m, 3, 0m },
                    { 6, 48, 150000m, 3, 0m },
                    { 7, 4, 68000m, 1, 0m },
                    { 7, 9, 84000m, 3, 0m },
                    { 7, 27, 133000m, 1, 0m },
                    { 7, 49, 102000m, 1, 0m },
                    { 8, 25, 139000m, 4, 0m },
                    { 8, 35, 177000m, 4, 0m },
                    { 8, 48, 103000m, 4, 0m },
                    { 9, 4, 170000m, 3, 0m },
                    { 9, 13, 81000m, 1, 0m },
                    { 9, 24, 88000m, 3, 0m },
                    { 10, 30, 136000m, 1, 0m },
                    { 10, 47, 93000m, 1, 0m }
                });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan" },
                values: new object[] { 7, "DH202605210001", new DateTime(2026, 5, 21, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2584), 204000m });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 5, "DH202606050002", new DateTime(2026, 6, 5, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2695), 175000m, 2 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan" },
                values: new object[] { 19, "DH202606010003", new DateTime(2026, 6, 1, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2712), 174000m });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 2, "DH202605170004", new DateTime(2026, 5, 17, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2726), 321000m, 1 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { "DH202606020005", new DateTime(2026, 6, 2, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2740), 474000m, 1 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan" },
                values: new object[] { 10, "DH202605170006", new DateTime(2026, 5, 17, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2754), 262000m });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 2, "DH202605300007", new DateTime(2026, 5, 30, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2768), 160000m, 1 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 7, "DH202605180008", new DateTime(2026, 5, 18, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2781), 231000m, 1 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 12, "DH202606020009", new DateTime(2026, 6, 2, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2796), 406000m, 3 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 19, "DH202606030010", new DateTime(2026, 6, 3, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2824), 476000m, 2 });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 466, new DateTime(2026, 6, 13, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(1812) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 401, new DateTime(2026, 6, 12, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 350, new DateTime(2026, 6, 11, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 112, new DateTime(2026, 6, 10, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(1888) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 322, new DateTime(2026, 6, 9, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(1901) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 195, new DateTime(2026, 6, 8, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(1917) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 125, new DateTime(2026, 6, 7, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(1953) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 216, new DateTime(2026, 6, 6, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(1971) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 304, new DateTime(2026, 6, 5, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(1985) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 122, new DateTime(2026, 6, 4, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2002) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 208, new DateTime(2026, 6, 3, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2014) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 169, new DateTime(2026, 6, 2, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2029) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 326, new DateTime(2026, 6, 1, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2043) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 117, new DateTime(2026, 5, 31, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2055) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 472, new DateTime(2026, 5, 30, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2067) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 353, new DateTime(2026, 5, 29, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2083) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 98, new DateTime(2026, 5, 28, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2096) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 180, new DateTime(2026, 5, 27, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2110) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 295, new DateTime(2026, 5, 26, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2122) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 496, new DateTime(2026, 5, 25, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2137) });

            migrationBuilder.UpdateData(
                table: "NHAN_VIEN",
                keyColumn: "Id",
                keyValue: 1,
                column: "NgayTao",
                value: new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(1565));

            migrationBuilder.InsertData(
                table: "PhuongThucThanhToans",
                columns: new[] { "Id", "TenPhuongThuc", "TrangThai" },
                values: new object[,]
                {
                    { 1, "Tiền mặt", true },
                    { 2, "Thẻ ngân hàng", true },
                    { 3, "Chuyển khoản", true }
                });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 416000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2252), 81 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 150000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2272), 75 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 499000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2277), 94 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 265000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2280), 22 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 479000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2283), 83 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 248000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2287), 59 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 327000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2290), 18 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 324000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2304), 71 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 66000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2306), 14 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 325000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2310), 22 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 148000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2314), 66 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 471000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2317), 18 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 53000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2320), 39 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 125000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2323), 50 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 138000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2325), 93 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 139000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2328), 40 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 373000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2330), 27 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 316000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2334), 42 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 319000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2337), 93 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 51000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2339), 20 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 115000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2342), 89 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 268000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2345), 88 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 148000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2347), 98 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 284000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2350), 91 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 329000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2353), 12 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 444000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2355), 68 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 417000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2358), 29 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 349000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2360), 33 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 433000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2363), 22 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 45000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2366), 56 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 450000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2368), 13 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 360000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2371), 72 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 433000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2373), 20 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 243000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2377), 40 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 69000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2379), 45 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 112000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2382), 60 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 195000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2385), 48 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 279000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2387), 17 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 480000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2400), 70 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 313000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2403), 67 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 369000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2405), 63 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 394000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2408), 45 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 110000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2410), 38 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 439000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2413), 51 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 100000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2415), 47 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 352000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2418), 76 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 306000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2421), 88 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 241000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2423), 28 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 310000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2426), 35 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 95000m, new DateTime(2026, 6, 14, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(2428), 11 });

            migrationBuilder.InsertData(
                table: "GIAO_DICH",
                columns: new[] { "Id", "DonHangId", "MaGiaoDichDoiTac", "NgayGiaoDich", "PhuongThucThanhToanId", "SoTien", "TrangThai" },
                values: new object[,]
                {
                    { 1, 1, "", new DateTime(2026, 5, 16, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(3071), 1, 126000m, "SUCCESS" },
                    { 2, 2, "", new DateTime(2026, 5, 30, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(3076), 1, 482000m, "SUCCESS" },
                    { 3, 3, "", new DateTime(2026, 6, 13, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(3078), 2, 214000m, "SUCCESS" },
                    { 4, 4, "", new DateTime(2026, 6, 8, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(3081), 1, 488000m, "SUCCESS" },
                    { 5, 5, "", new DateTime(2026, 5, 23, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(3083), 2, 463000m, "SUCCESS" },
                    { 6, 6, "", new DateTime(2026, 5, 18, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(3087), 1, 204000m, "SUCCESS" },
                    { 7, 7, "", new DateTime(2026, 5, 30, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(3089), 3, 417000m, "SUCCESS" },
                    { 8, 8, "", new DateTime(2026, 5, 17, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(3092), 2, 237000m, "SUCCESS" },
                    { 9, 9, "", new DateTime(2026, 5, 21, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(3094), 1, 482000m, "SUCCESS" },
                    { 10, 10, "", new DateTime(2026, 6, 7, 2, 57, 47, 815, DateTimeKind.Local).AddTicks(3108), 1, 67000m, "SUCCESS" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 1, 33 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 1, 36 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 2, 38 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 2, 43 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 4, 15 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 4, 16 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 4, 35 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 5, 19 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 5, 30 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 5, 41 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 6, 34 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 6, 47 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 6, 48 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 7, 9 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 7, 27 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 7, 49 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 8, 25 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 8, 35 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 8, 48 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 9, 4 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 9, 13 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 9, 24 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 10, 30 });

            migrationBuilder.DeleteData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 10, 47 });

            migrationBuilder.DeleteData(
                table: "GIAO_DICH",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GIAO_DICH",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GIAO_DICH",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GIAO_DICH",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GIAO_DICH",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GIAO_DICH",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GIAO_DICH",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GIAO_DICH",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GIAO_DICH",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "GIAO_DICH",
                keyColumn: "Id",
                keyValue: 10);

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

            migrationBuilder.UpdateData(
                table: "CHI_TIET_DON_HANG",
                keyColumns: new[] { "DonHangId", "SanPhamId" },
                keyValues: new object[] { 1, 17 },
                columns: new[] { "DonGia", "SoLuong" },
                values: new object[] { 90000m, 1 });

            migrationBuilder.InsertData(
                table: "CHI_TIET_DON_HANG",
                columns: new[] { "DonHangId", "SanPhamId", "DonGia", "SoLuong", "ThanhTien" },
                values: new object[,]
                {
                    { 1, 16, 78000m, 4, 0m },
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

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan" },
                values: new object[] { 14, "DH202605290001", new DateTime(2026, 5, 29, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(212), 126000m });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 4, "DH202605150002", new DateTime(2026, 5, 15, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(281), 482000m, 1 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan" },
                values: new object[] { 14, "DH202606120003", new DateTime(2026, 6, 12, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(285), 214000m });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 3, "DH202606060004", new DateTime(2026, 6, 6, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(289), 488000m, 3 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { "DH202605220005", new DateTime(2026, 5, 22, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(292), 463000m, 2 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan" },
                values: new object[] { 8, "DH202605270006", new DateTime(2026, 5, 27, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(296), 204000m });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 9, "DH202605150007", new DateTime(2026, 5, 15, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(300), 417000m, 3 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 3, "DH202605280008", new DateTime(2026, 5, 28, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(303), 237000m, 2 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 11, "DH202605240009", new DateTime(2026, 5, 24, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(306), 482000m, 1 });

            migrationBuilder.UpdateData(
                table: "DON_HANG",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "KhachHangId", "MaDonHang", "NgayTao", "TongThanhToan", "TrangThai" },
                values: new object[] { 18, "DH202606070010", new DateTime(2026, 6, 7, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(309), 67000m, 1 });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 28, new DateTime(2026, 6, 12, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9957) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 120, new DateTime(2026, 6, 11, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9967) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 201, new DateTime(2026, 6, 10, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9977) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 212, new DateTime(2026, 6, 9, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9981) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 310, new DateTime(2026, 6, 8, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9986) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 17, new DateTime(2026, 6, 7, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9990) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 316, new DateTime(2026, 6, 6, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9993) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 15, new DateTime(2026, 6, 5, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9996) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 209, new DateTime(2026, 6, 4, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(4) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 158, new DateTime(2026, 6, 3, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(10) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 312, new DateTime(2026, 6, 2, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(13) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 417, new DateTime(2026, 6, 1, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(15) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 6, new DateTime(2026, 5, 31, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(19) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 473, new DateTime(2026, 5, 30, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(22) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 300, new DateTime(2026, 5, 29, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(24) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 212, new DateTime(2026, 5, 28, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(27) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 223, new DateTime(2026, 5, 27, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(31) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 497, new DateTime(2026, 5, 26, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(35) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 23, new DateTime(2026, 5, 25, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(37) });

            migrationBuilder.UpdateData(
                table: "KhachHangs",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DiemTichLuy", "NgayTao" },
                values: new object[] { 87, new DateTime(2026, 5, 24, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(40) });

            migrationBuilder.UpdateData(
                table: "NHAN_VIEN",
                keyColumn: "Id",
                keyValue: 1,
                column: "NgayTao",
                value: new DateTime(2026, 6, 13, 3, 50, 5, 174, DateTimeKind.Local).AddTicks(9795));

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 367000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(75), 63 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 254000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(84), 78 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 188000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(86), 62 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 113000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(88), 24 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 440000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(89), 20 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 50000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(92), 99 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 349000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(94), 29 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 496000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(95), 76 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 276000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(97), 21 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 438000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(99), 48 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 231000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(105), 51 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 230000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(107), 20 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 145000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(108), 96 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 494000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(110), 20 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 368000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(111), 66 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 22000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(112), 16 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 336000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(114), 15 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 411000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(116), 15 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 238000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(117), 71 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 223000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(119), 55 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 247000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(120), 63 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 352000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(121), 27 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 426000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(123), 21 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 27000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(124), 74 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 246000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(125), 82 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 90000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(126), 62 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 86000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(128), 49 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 397000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(129), 86 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 495000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(130), 40 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 35000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(132), 63 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 355000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(133), 81 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 453000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(134), 46 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 154000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(136), 24 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 370000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(138), 79 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 450000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(140), 35 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 119000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(141), 81 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 211000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(142), 78 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 428000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(144), 35 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 174000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(145), 83 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 101000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(146), 95 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 318000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(147), 32 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 4, 155000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(149), 55 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 71000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(156), 49 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 451000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(157), 19 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 238000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(158), 64 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 2, 36000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(160), 10 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 1, 216000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(161), 81 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DanhMucId", "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 3, 327000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(163), 49 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 440000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(164), 76 });

            migrationBuilder.UpdateData(
                table: "SAN_PHAM",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "GiaBan", "NgayTao", "TonKho" },
                values: new object[] { 378000m, new DateTime(2026, 6, 13, 3, 50, 5, 175, DateTimeKind.Local).AddTicks(165), 48 });
        }
    }
}
