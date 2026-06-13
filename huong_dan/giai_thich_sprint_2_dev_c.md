# Giải thích chi tiết phần công việc Sprint 2 - Dev C (Module Danh Sách & Chi Tiết Đơn Hàng)

Tài liệu này giải thích chi tiết các thành phần đã được lập trình trong **Sprint 2** dành cho **Dev C**, xoay quanh tính năng hiển thị danh sách, lọc và xem chi tiết Đơn hàng. 

Giống như Sprint 1, mã nguồn tiếp tục tuân thủ kiến trúc **MVVM** và **N-Tier (Nhiều lớp)**. Mặc dù Dev B chịu trách nhiệm viết logic tạo và tính toán giá trị đơn hàng, nhưng để hiển thị được dữ liệu, Dev C đã triển khai các thành phần cơ sở (đọc dữ liệu) sau đây:

## 1. Tầng Core (Domain Layer)

- **`IOrderRepository.cs`**: Định nghĩa 2 hàm cơ bản để lấy dữ liệu.
  - `GetAllAsync`: Lấy danh sách các đơn hàng, có tham số tùy chọn (optional parameters) là `fromDate`, `toDate` và `status` để phục vụ chức năng tìm kiếm/lọc trên màn hình danh sách.
  - `GetByIdWithItemsAsync`: Lấy thông tin một đơn hàng cụ thể thông qua ID, bao gồm cả danh sách các sản phẩm (Order Items) bên trong nó để hiển thị lên bảng chi tiết.
- **`IOrderService.cs`**: Tương tự như Repository, Service này đưa các hàm lấy dữ liệu ra giao diện. Tại đây đã để sẵn "chỗ trống" bằng comment để Dev B có thể viết đè (implement) thêm hàm `CreateOrder()` sau này mà không lo xung đột file.

## 2. Tầng Infrastructure (Data Access Layer)

- **`OrderRepository.cs`**: Thực thi các Interface từ tầng Core, tận dụng khả năng của Entity Framework Core.
  - Khi dùng `GetAllAsync`, thay vì lấy toàn bộ bảng, code dùng đối tượng `IQueryable` để kết nối liên tiếp các lệnh `Where` (chỉ khi có biến `fromDate` hoặc `toDate` hoặc `status` thì mới thêm điều kiện lọc vào câu SQL truy vấn).
  - Khi dùng `GetByIdWithItemsAsync`, lệnh `Include(o => o.ChiTietDonHangs).ThenInclude(c => c.SanPham)` được sử dụng (gọi là Eager Loading). Nó giúp Entity Framework biết cần phải join (nối) bảng `Đơn Hàng` với bảng `Chi Tiết Đơn Hàng` rồi nối tiếp sang bảng `Sản Phẩm` để lấy ra Tên Sản Phẩm thay vì chỉ lấy ra được cái ID thô kệch.
- **`DataSeeder.cs` & `AppDbContext.cs`**: Để UI (Giao diện) có dữ liệu hiển thị thử nghiệm khi chức năng Tạp Đơn Hàng của Dev B chưa làm xong, Dev C đã viết thêm phương thức sinh ngẫu nhiên 10 Đơn hàng mẫu và vài Chi tiết mặt hàng bên trong đó rồi đẩy vào `AppDbContext` thông qua hàm `HasData`.

## 3. Tầng Application (Business Logic Layer)

- **`OrderService.cs`**: Ở màn hình hiển thị danh sách, ta chưa cần thực thi những quy tắc logic nghiệp vụ gì quá rườm rà. Lớp Service này đóng vai trò là "cầu nối", nhận yêu cầu từ View Model và gọi hàm tương ứng xuống `OrderRepository`.

## 4. Tầng WPF UI (Presentation Layer)

Sử dụng `CommunityToolkit.Mvvm` cho việc Data Binding và quản lý Commands.

- **`OrdersViewModel.cs`**: 
  - Là bộ não của giao diện danh sách đơn hàng. Lưu trữ các biến trạng thái bằng `[ObservableProperty]` như ngày bắt đầu (`FromDate`), ngày kết thúc (`ToDate`) và trạng thái (`SelectedStatus`). 
  - Khai báo danh sách `Orders` dưới dạng `ObservableCollection` để khi dùng hàm `SearchAsync` nạp dữ liệu từ DB, giao diện WPF sẽ tự động cập nhật lại bảng danh sách mà không cần code F5 thủ công.
  - Lệnh `ViewDetailCommand`: Khi người dùng click nút xem chi tiết hoặc click đúp (Double-Click) vào một dòng, lệnh này sẽ kiểm tra xem đã chọn đúng dòng đơn hàng nào chưa (`SelectedOrder`), sau đó mở màn hình Modal Dialog chi tiết.

- **`OrdersView.xaml`**: 
  - Giao diện được cấu trúc bằng `Grid`.
  - Phía trên là vùng thanh công cụ tìm kiếm: gồm 2 nút chọn ngày (`DatePicker`), 1 hộp chọn trạng thái (`ComboBox`) và các nút Tìm kiếm, Xóa bộ lọc.
  - Ở giữa là bảng `DataGrid` cấu hình `AutoGenerateColumns="False"` để tự quy định độ rộng và format hiển thị (ví dụ giá tiền tự chèn định dạng số `0:N0` và ký hiệu `₫`). Cột Trạng Thái được Binding trực tiếp từ giá trị Enum của lớp `DonHang`.

- **`OrderDetailViewModel.cs` & `OrderDetailDialog.xaml`**:
  - Giao diện xem chi tiết được thiết kế dưới dạng popup Modal để người dùng không bị mất giao diện Danh sách đang tra cứu. 
  - Màn hình này hiển thị những thông tin cơ bản: Mã đơn, người tạo, ngày tạo. Kèm theo là 1 bảng DataGrid nhỏ liệt kê đầy đủ Tên sản phẩm, số lượng, đơn giá. Cuối cùng, góc dưới bên phải hiện tổng kết tài chính (Tổng tiền hàng, thuế, VAT, Tổng thanh toán).

## 5. Tóm tắt luồng dữ liệu (Data Flow) khi hiển thị Danh sách

1. Người dùng mở màn hình Đơn hàng, ViewModel `OrdersViewModel` được khởi tạo và chạy ngay Command nạp dữ liệu.
2. Dữ liệu chạy xuống `OrderService` -> `OrderRepository` để lấy 10 đơn hàng mẫu đã được sinh từ `DataSeeder` bằng câu truy vấn `SELECT`.
3. Dữ liệu quay trở lại đổ vào `ObservableCollection`, màn hình WPF tự động vẽ các dòng Đơn hàng lên `DataGrid`.
4. Người dùng có thể chọn một Khoảng ngày trên `DatePicker` và ấn nút "Tìm kiếm". 
5. Thông số ngày tháng chạy qua ViewModel đi xuống Repository. Repository viết lại câu truy vấn bằng cách gán thêm thuộc tính `Where(Ngày >= FromDate AND Ngày <= ToDate)` và gửi tới SQL Server.
6. Khi người dùng click đúp vào dòng đơn hàng. Lệnh `ViewDetailCommand` kích hoạt, truyền tham số ID của đơn hàng đó xuống Repository gọi hàm lấy chi tiết kèm các phần tử con (`.Include`).
7. Form `OrderDetailDialog` hiện lên với đầy đủ dữ liệu vừa được bóc tách và sẵn sàng để xem.
