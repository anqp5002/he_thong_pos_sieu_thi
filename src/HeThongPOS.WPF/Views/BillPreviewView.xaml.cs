using System.Windows;
using System.Windows.Controls;
using HeThongPOS.WPF.ViewModels;

namespace HeThongPOS.WPF.Views;

public partial class BillPreviewView : UserControl
{
    public BillPreviewView()
    {
        InitializeComponent();
        DataContextChanged += BillPreviewView_DataContextChanged;
    }

    private void BillPreviewView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is BillPreviewViewModel viewModel)
        {
            // Set the document dynamically in code-behind because FlowDocumentScrollViewer.Document is not a dependency property
            DocViewer.Document = viewModel.InvoiceDocument;
        }
        else
        {
            DocViewer.Document = null;
        }
    }
}
