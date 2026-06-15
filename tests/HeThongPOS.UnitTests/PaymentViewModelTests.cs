using System;
using Microsoft.EntityFrameworkCore;
using Xunit;
using HeThongPOS.WPF.ViewModels;
using HeThongPOS.Infrastructure.Data;

namespace HeThongPOS.UnitTests;

public class PaymentViewModelTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public void KhachDua_ShouldUpdateTienThoiAndCanCheckout()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var viewModel = new PaymentViewModel(context);
        
        viewModel.Initialize(100000m); // Tổng thanh toán = 100k

        // Act 1: Tiền khách đưa nhỏ hơn tổng tiền
        viewModel.KhachDua = 50000m;

        // Assert 1
        Assert.Equal(0m, viewModel.TienThoi);
        Assert.False(viewModel.CanCheckout);

        // Act 2: Tiền khách đưa lớn hơn hoặc bằng tổng tiền
        viewModel.KhachDua = 150000m;

        // Assert 2
        Assert.Equal(50000m, viewModel.TienThoi);
        Assert.True(viewModel.CanCheckout);
    }

    [Fact]
    public void PhuongThucThanhToan_ShouldUpdateCanCheckout()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var viewModel = new PaymentViewModel(context);
        viewModel.Initialize(100000m);

        // Act 1: Chọn CASH nhưng đưa thiếu tiền
        viewModel.PhuongThucThanhToan = "CASH";
        viewModel.KhachDua = 0;

        // Assert 1
        Assert.False(viewModel.CanCheckout);

        // Act 2: Chọn CARD/Chuyển khoản thì không quan tâm Tiền khách đưa
        viewModel.PhuongThucThanhToan = "CARD";
        
        // Assert 2
        Assert.True(viewModel.CanCheckout);
    }
}
