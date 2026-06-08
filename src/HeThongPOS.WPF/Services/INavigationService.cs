namespace HeThongPOS.WPF.Services;

public interface INavigationService
{
    void NavigateTo<TViewModel>() where TViewModel : class;
}
