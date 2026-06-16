using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Application.Interfaces;
using HeThongPOS.WPF.Services;

namespace HeThongPOS.WPF.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly INavigationService _navigationService;
    private readonly SessionManager _sessionManager;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    public LoginViewModel(IAuthService authService, INavigationService navigationService, SessionManager sessionManager)
    {
        _authService = authService;
        _navigationService = navigationService;
        _sessionManager = sessionManager;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Vui lòng nhập tên đăng nhập và mật khẩu.";
            return;
        }

        IsBusy = true;

        var (isSuccess, error, user) = await _authService.LoginAsync(Username, Password);

        IsBusy = false;

        if (isSuccess && user != null)
        {
            // Lưu thông tin user vào SessionManager (Global Auth State)
            // SessionManager sẽ phát sự kiện OnSessionChanged
            // MainWindow lắng nghe sự kiện này để chuyển sang Layout Sidebar
            _sessionManager.Login(user);

            // Reset form
            Username = string.Empty;
            Password = string.Empty;
        }
        else
        {
            ErrorMessage = error;
        }
    }
}
