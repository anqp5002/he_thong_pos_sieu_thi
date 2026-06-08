using System;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace HeThongPOS.WPF.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private Frame _mainFrame = null!;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Initialize(Frame mainFrame)
    {
        _mainFrame = mainFrame;
    }

    public void NavigateTo<TViewModel>() where TViewModel : class
    {
        // View resolution by naming convention: ViewModel -> View
        var viewModelType = typeof(TViewModel);
        var viewTypeName = viewModelType.AssemblyQualifiedName!.Replace("ViewModel", "View");
        var viewType = Type.GetType(viewTypeName);

        if (viewType == null)
            throw new Exception($"Không tìm thấy View cho {viewModelType.Name}");

        var view = (Page)_serviceProvider.GetRequiredService(viewType);
        view.DataContext = _serviceProvider.GetRequiredService<TViewModel>();
        
        _mainFrame.Navigate(view);
    }
}
