using System;
using System.Windows.Controls;
using System.Windows.Documents;

namespace HeThongPOS.WPF.Services;

public class PrintService
{
    public bool PrintDocument(FlowDocument document, string documentName)
    {
        var printDialog = new PrintDialog();
        if (printDialog.ShowDialog() == true)
        {
            try
            {
                IDocumentPaginatorSource idpSource = document;
                printDialog.PrintDocument(idpSource.DocumentPaginator, documentName);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Lỗi kết nối máy in. Vui lòng kiểm tra lại thiết bị.");
            }
        }
        return false;
    }
}
