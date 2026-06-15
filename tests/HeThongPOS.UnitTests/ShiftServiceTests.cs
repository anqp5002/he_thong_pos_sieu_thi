using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using HeThongPOS.Application.Services;
using HeThongPOS.Infrastructure.Data;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;

namespace HeThongPOS.UnitTests;

public class ShiftServiceTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task OpenShiftAsync_ShouldReturnSuccess_WhenNoActiveShiftExists()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var shiftService = new ShiftService(context);

        // Act
        var (isSuccess, message, shift) = await shiftService.OpenShiftAsync(1, 500000m);

        // Assert
        Assert.True(isSuccess);
        Assert.Equal("Mở ca thành công.", message);
        Assert.NotNull(shift);
        Assert.Equal(1, shift.NhanVienId);
        Assert.Equal(500000m, shift.SoDuDauCa);
        Assert.Equal("OPEN", shift.TrangThai);
    }

    [Fact]
    public async Task OpenShiftAsync_ShouldReturnFail_WhenActiveShiftExists()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var nhanVien = new NhanVien { HoTen = "Test", Username = "test", PasswordHash = "hash" };
        context.NhanViens.Add(nhanVien);
        await context.SaveChangesAsync();

        context.CaLamViecs.Add(new CaLamViec { NhanVienId = nhanVien.Id, TrangThai = "OPEN", ThoiGianBatDau = DateTime.Now });
        await context.SaveChangesAsync();
        var shiftService = new ShiftService(context);

        // Act
        var (isSuccess, message, shift) = await shiftService.OpenShiftAsync(nhanVien.Id, 500000m);

        // Assert
        Assert.False(isSuccess);
        Assert.Equal("Nhân viên đang có ca làm việc chưa đóng. Vui lòng đóng ca trước.", message);
        Assert.Null(shift);
    }

    [Fact]
    public async Task OpenShiftAsync_ShouldReturnFail_WhenSoDuDauCaIsNegative()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var shiftService = new ShiftService(context);

        // Act
        var (isSuccess, message, shift) = await shiftService.OpenShiftAsync(1, -100m);

        // Assert
        Assert.False(isSuccess);
        Assert.Equal("Số dư đầu ca không được âm.", message);
        Assert.Null(shift);
    }

    [Fact]
    public async Task CloseShiftAsync_ShouldCalculateCorrectly_WhenShiftIsClosed()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var nhanVien = new NhanVien { HoTen = "Test", Username = "test", PasswordHash = "hash" };
        context.NhanViens.Add(nhanVien);
        await context.SaveChangesAsync();

        var shiftStartTime = DateTime.Now.AddHours(-4);
        var shift = new CaLamViec
        {
            NhanVienId = nhanVien.Id,
            SoDuDauCa = 1000000m,
            ThoiGianBatDau = shiftStartTime,
            TrangThai = "OPEN"
        };
        context.CaLamViecs.Add(shift);
        await context.SaveChangesAsync(); // Lưu để có ID

        // Thêm đơn hàng thành công trong ca
        context.DonHangs.Add(new DonHang
        {
            NhanVienId = nhanVien.Id,
            TrangThai = OrderStatus.Completed,
            NgayTao = shiftStartTime.AddHours(1),
            TongThanhToan = 500000m
        });
        
        // Thêm đơn hàng thất bại (không tính vào doanh thu)
        context.DonHangs.Add(new DonHang
        {
            NhanVienId = nhanVien.Id,
            TrangThai = OrderStatus.Cancelled,
            NgayTao = shiftStartTime.AddHours(2),
            TongThanhToan = 200000m
        });

        await context.SaveChangesAsync();
        var shiftService = new ShiftService(context);

        // Act
        var (isSuccess, message, closedShift) = await shiftService.CloseShiftAsync(shift.Id, 1400000m, "Kiểm tra đóng ca");

        // Assert
        Assert.True(isSuccess);
        Assert.Equal("CLOSED", closedShift!.TrangThai);
        Assert.Equal(500000m, closedShift.TongDoanhThu);
        Assert.Equal(1400000m, closedShift.SoDuCuoiCaThucTe);
        Assert.Equal(-100000m, closedShift.ChenhLech);
        Assert.Contains("Cảnh báo: Tiền THIẾU 100,000 đ", message);
    }
}
