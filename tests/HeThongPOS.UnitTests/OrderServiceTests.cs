using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeThongPOS.Application.Services;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;
using Moq;
using Xunit;

namespace HeThongPOS.UnitTests;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<IProductRepository> _productRepoMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderRepoMock = new Mock<IOrderRepository>();
        _productRepoMock = new Mock<IProductRepository>();
        _orderService = new OrderService(_orderRepoMock.Object, _productRepoMock.Object, null);
    }

    [Fact]
    public async Task CreateOrderAsync_EmptyItems_ThrowsException()
    {
        // Arrange
        var items = new List<ChiTietDonHang>();

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _orderService.CreateOrderAsync(1, null, items));
        Assert.Equal("Đơn hàng phải có ít nhất 1 sản phẩm.", ex.Message);
    }

    [Fact]
    public async Task CreateOrderAsync_NotEnoughStock_ThrowsException()
    {
        // Arrange
        var product = new SanPham { Id = 1, TenSanPham = "Test", TonKho = 5, GiaBan = 100, TrangThai = true };
        _productRepoMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

        var items = new List<ChiTietDonHang>
        {
            new ChiTietDonHang { SanPhamId = 1, SoLuong = 10 } // Requesting more than stock
        };

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _orderService.CreateOrderAsync(1, null, items));
        Assert.Contains("không đủ tồn kho", ex.Message);
    }

    [Fact]
    public async Task CreateOrderAsync_ValidItems_CalculatesTotalAndDeductsStock()
    {
        // Arrange
        var product = new SanPham { Id = 1, TenSanPham = "Test", TonKho = 15, GiaBan = 100, TrangThai = true };
        _productRepoMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

        var items = new List<ChiTietDonHang>
        {
            new ChiTietDonHang { SanPhamId = 1, SoLuong = 5 }
        };

        // Act
        var order = await _orderService.CreateOrderAsync(1, null, items);

        // Assert
        Assert.NotNull(order);
        Assert.Equal(500, order.TongTienHang); // 5 * 100
        Assert.Equal(500, order.TongThanhToan);
        
        // Stock should be deducted
        Assert.Equal(10, product.TonKho);
        _productRepoMock.Verify(repo => repo.UpdateAsync(product), Times.Once);
        _orderRepoMock.Verify(repo => repo.AddAsync(It.IsAny<DonHang>()), Times.Once);
    }
}
