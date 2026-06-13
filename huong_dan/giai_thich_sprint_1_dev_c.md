# Giải thích chi tiết phần công việc Sprint 1 - Dev C (Module Khách Hàng)

Tài liệu này giải thích chi tiết các thành phần đã được lập trình trong Sprint 1 dành cho Dev C, thuộc module Quản lý Khách hàng. Kiến trúc của dự án được triển khai theo mô hình **MVVM (Model - View - ViewModel)** kết hợp với mô hình nhiều lớp (N-Tier Architecture: Core, Infrastructure, Application, WPF UI).

## 1. Tầng Core (Domain Layer)

Tầng Core chứa các định nghĩa trừu tượng (Interface) và thực thể (Entity). Việc tạo ra các Interface giúp dự án dễ dàng viết Unit Test (sử dụng Mocking) và tuân thủ nguyên lý Dependency Inversion.

- **`ICustomerRepository.cs`**: Định nghĩa các phương thức giao tiếp với cơ sở dữ liệu. Nó bao gồm các thao tác CRUD cơ bản (`AddAsync`, `UpdateAsync`, `DeleteAsync`, `GetByIdAsync`, `GetAllAsync`) và các hàm mở rộng như `SearchAsync` (tìm kiếm), `GetByEmailAsync`, `GetByPhoneAsync`.
- **`ICustomerService.cs`**: Định nghĩa các phương thức phục vụ cho Business Logic. Tầng UI sẽ gọi các hàm trong Service này thay vì gọi trực tiếp Repository.

## 2. Tầng Infrastructure (Data Access Layer)

Tầng này chịu trách nhiệm tương tác trực tiếp với cơ sở dữ liệu SQL Server thông qua Entity Framework Core.

- **`CustomerRepository.cs`**: Đây là class thực thi (implement) interface `ICustomerRepository`. Nó nhận vào `AppDbContext` thông qua Dependency Injection và dùng LINQ để truy vấn cơ sở dữ liệu. 
  - Hàm `SearchAsync` được viết linh hoạt: nếu người dùng nhập số điện thoại hoặc một phần tên/email, hệ thống sẽ chuyển về chữ thường (`ToLower()`) và dùng lệnh `Contains` để tìm kiếm tương đối giống từ khóa `LIKE` trong SQL.
- **`DataSeeder.cs`**: Class này được dùng để tự động đổ (seed) dữ liệu mẫu vào DB, giúp nhóm có sẵn dữ liệu test (20 khách hàng, 50 sản phẩm) ngay khi chạy Migration ban đầu.
- **`AppDbContext.cs`**: Tôi đã ghi đè hàm `OnModelCreating` và gọi hàm `HasData` kết hợp với `DataSeeder` để khi EF Core tạo bảng, nó sẽ chèn các dòng dữ liệu này vào luôn.

## 3. Tầng Application (Business Logic Layer)

Tầng này đứng giữa tầng UI và tầng Infrastructure, chịu trách nhiệm xử lý các quy tắc nghiệp vụ trước khi lưu xuống DB.

- **`CustomerService.cs`**: Thực thi `ICustomerService`. Class này gọi đến `CustomerRepository` để thao tác DB, nhưng đồng thời thực hiện validate dữ liệu:
  - **Kiểm tra thông tin bắt buộc**: Tên khách hàng và Số điện thoại không được để trống.
  - **Regex Số điện thoại**: Đảm bảo số điện thoại ở định dạng Việt Nam (bắt đầu bằng số 0, dài chính xác 10 chữ số).
  - **Kiểm tra trùng lặp (Unique)**: Gọi hàm `GetByPhoneAsync` và `GetByEmailAsync` để đảm bảo không có khách hàng nào bị trùng email hoặc số điện thoại. Đặc biệt, logic kiểm tra trùng lặp có phân biệt giữa trạng thái "Thêm mới" và "Cập nhật" để tránh báo lỗi trùng lặp khi người dùng cập nhật thông tin của chính khách hàng đó.

## 4. Tầng WPF UI (Presentation Layer)

Tầng này được viết bằng WPF sử dụng bộ thư viện `CommunityToolkit.Mvvm` để tối ưu hóa code.

- **`CustomersViewModel.cs`**:
  - Dùng thuộc tính `[ObservableProperty]` cho các biến trạng thái (`Customers`, `SearchKeyword`, `SelectedCustomer`) để tự động thông báo cho UI (View) biết khi dữ liệu thay đổi.
  - Dùng `[RelayCommand]` để bind các hành động tìm kiếm, thêm, sửa, xóa từ Button trên UI vào trong ViewModel mà không cần viết các lệnh thao tác sự kiện (Event Handler) rườm rà.
  - Khi người dùng click **Thêm mới** hoặc **Sửa**, hệ thống sẽ mở ra một cửa sổ popup Modal (`CustomerFormDialog`).

- **`CustomersView.xaml`**:
  - Là giao diện danh sách chính, chia Layout bằng `Grid` gồm 3 hàng: hàng 1 chứa TextBox tìm kiếm, hàng 2 chứa bảng `DataGrid` dữ liệu, hàng 3 chứa các nút chức năng.
  - Các cột của DataGrid được Binding tĩnh với các Property của object `KhachHang` (ví dụ: `HoTen`, `SoDienThoai`).

- **`CustomerFormViewModel.cs` & `CustomerFormDialog.xaml`**:
  - Để tiết kiệm thời gian và đảm bảo đồng nhất UX, tính năng "Thêm mới" và "Sửa" được gộp chung vào 1 Modal. 
  - Nếu `CustomerFormViewModel` được khởi tạo với một object `KhachHang` bị rỗng (null), nó hiểu là "Thêm mới". Ngược lại, nó sẽ điền sẵn thông tin lên TextBox để người dùng "Cập nhật". Khi ấn Lưu, ViewModel sẽ gọi hàm `CreateCustomerAsync` hoặc `UpdateCustomerAsync` tương ứng từ `CustomerService`.

## 5. Tóm tắt luồng dữ liệu (Data Flow) khi tạo 1 Khách Hàng

1. Người dùng nhập liệu trên form và ấn nút "Lưu" (UI - `CustomerFormDialog.xaml`).
2. Nút "Lưu" gọi Command `SaveAsync` trong ViewModel (`CustomerFormViewModel.cs`).
3. ViewModel đóng gói dữ liệu và gọi hàm `CreateCustomerAsync` của Service (`CustomerService.cs`).
4. Service chạy validate định dạng số điện thoại, kiểm tra SĐT/Email trùng lặp.
   - Nếu lỗi: trả về kết quả lỗi (ViewModel sẽ hiển thị MessageBox).
   - Nếu đúng: Service gọi tiếp `AddAsync` của Repository (`CustomerRepository.cs`).
5. Repository dùng Entity Framework `AppDbContext` để lưu thông tin xuống database.
6. Khi thành công, form đóng lại và danh sách (`CustomersViewModel.cs`) tự động gọi lệnh `SearchAsync` để tải lại dữ liệu mới nhất.
