using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace HeThongPOS.WPF.Services;

public class PrintService
{
    /// <summary>
    /// Prints a WPF FlowDocument using the standard PrintDialog.
    /// </summary>
    /// <param name="document">The FlowDocument to print.</param>
    /// <param name="description">The job description seen in the printer queue.</param>
    /// <returns>True if the user proceeded to print, false if they canceled.</returns>
    public bool PrintDocument(FlowDocument document, string description)
    {
        try
        {
            var printDialog = new PrintDialog();
            
            // Show print dialog to let user select printer
            if (printDialog.ShowDialog() == true)
            {
                // Clone the document so we can format it dynamically for the printer's printable area
                var clonedDoc = CloneDocument(document);

                // Set printing properties based on selected printer size
                double printableWidth = printDialog.PrintableAreaWidth;
                clonedDoc.PageWidth = printableWidth;
                clonedDoc.PageHeight = double.NaN; // Auto-height for roll paper
                clonedDoc.PagePadding = new Thickness(10);
                clonedDoc.ColumnWidth = printableWidth - 20;

                // Execute print job
                IDocumentPaginatorSource paginatorSource = clonedDoc;
                printDialog.PrintDocument(paginatorSource.DocumentPaginator, description);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            // Wrap in user friendly exception as per Sprint 3 Dev C guidelines
            throw new Exception("Lỗi kết nối máy in: Không thể gửi lệnh in đến thiết bị. Vui lòng kiểm tra lại dây cáp, nguồn điện hoặc giấy in.", ex);
        }
    }

    private FlowDocument CloneDocument(FlowDocument doc)
    {
        using (var stream = new MemoryStream())
        {
            XamlWriter.Save(doc, stream);
            stream.Position = 0;
            return (FlowDocument)XamlReader.Load(stream);
        }
    }
}
