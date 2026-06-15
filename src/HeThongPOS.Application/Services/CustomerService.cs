using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;

namespace HeThongPOS.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<KhachHang>> GetAllCustomersAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<IEnumerable<KhachHang>> SearchCustomersAsync(string keyword)
    {
        return await _customerRepository.SearchAsync(keyword);
    }

    public async Task<KhachHang?> GetCustomerByIdAsync(int id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    public async Task<(bool Success, string ErrorMessage)> CreateCustomerAsync(KhachHang customer)
    {
        var validationResult = await ValidateCustomerAsync(customer);
        if (!validationResult.Success)
            return validationResult;

        customer.NgayTao = DateTime.Now;
        await _customerRepository.AddAsync(customer);
        return (true, string.Empty);
    }

    public async Task<(bool Success, string ErrorMessage)> UpdateCustomerAsync(KhachHang customer)
    {
        var validationResult = await ValidateCustomerAsync(customer, isUpdate: true);
        if (!validationResult.Success)
            return validationResult;

        var existingCustomer = await _customerRepository.GetByIdAsync(customer.Id);
        if (existingCustomer == null)
            return (false, "Khách hàng không tồn tại.");

        existingCustomer.HoTen = customer.HoTen;
        existingCustomer.SoDienThoai = customer.SoDienThoai;
        existingCustomer.Email = customer.Email;
        existingCustomer.DiemTichLuy = customer.DiemTichLuy;

        await _customerRepository.UpdateAsync(existingCustomer);
        return (true, string.Empty);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
            return false;

        await _customerRepository.DeleteAsync(id);
        return true;
    }

    private async Task<(bool Success, string ErrorMessage)> ValidateCustomerAsync(KhachHang customer, bool isUpdate = false)
    {
        if (string.IsNullOrWhiteSpace(customer.HoTen))
            return (false, "Tên khách hàng không được để trống.");

        if (string.IsNullOrWhiteSpace(customer.SoDienThoai))
            return (false, "Số điện thoại không được để trống.");

        // Validate phone number format (basic format for Vietnam: 10 digits starting with 0)
        var phoneRegex = new Regex(@"^(0)[0-9]{9}$");
        if (!phoneRegex.IsMatch(customer.SoDienThoai))
            return (false, "Số điện thoại không hợp lệ (phải bắt đầu bằng 0 và gồm 10 chữ số).");

        // Check unique phone number
        var existingPhone = await _customerRepository.GetByPhoneAsync(customer.SoDienThoai);
        if (existingPhone != null && (!isUpdate || existingPhone.Id != customer.Id))
            return (false, "Số điện thoại đã được sử dụng.");

        if (!string.IsNullOrWhiteSpace(customer.Email))
        {
            // Basic email validation
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(customer.Email))
                return (false, "Email không hợp lệ.");

            // Check unique email
            var existingEmail = await _customerRepository.GetByEmailAsync(customer.Email);
            if (existingEmail != null && (!isUpdate || existingEmail.Id != customer.Id))
                return (false, "Email đã được sử dụng.");
        }

        return (true, string.Empty);
    }
}
