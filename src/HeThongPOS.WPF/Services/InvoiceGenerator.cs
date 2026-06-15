using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.WPF.Services;

public class InvoiceGenerator
{
    public FlowDocument GenerateInvoice(DonHang order)
    {
        var doc = new FlowDocument
        {
            PagePadding = new Thickness(20),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 13,
            PageWidth = 320, 
            ColumnWidth = 320
        };

        var separator = "--------------------------------------";

        // Header
        var header = new Paragraph() { TextAlignment = TextAlignment.Center, Margin = new Thickness(0, 0, 0, 10) };
        header.Inlines.Add(new Run("SIÊU THỊ POS\n") { FontSize = 18, FontWeight = FontWeights.Bold });
        header.Inlines.Add(new Run("Nhóm 8B - POS Market\n"));
        header.Inlines.Add(new Run("ĐT: 0123.456.789"));
        doc.Blocks.Add(header);

        doc.Blocks.Add(new Paragraph(new Run(separator)) { Margin = new Thickness(0) });

        var title = new Paragraph(new Run("HÓA ĐƠN BÁN HÀNG"))
        {
            FontSize = 16,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 5, 0, 5)
        };
        doc.Blocks.Add(title);

        doc.Blocks.Add(new Paragraph(new Run(separator)) { Margin = new Thickness(0) });

        // Info Table
        var infoTable = new Table() { Margin = new Thickness(0, 5, 0, 5), CellSpacing = 0 };
        infoTable.Columns.Add(new TableColumn { Width = new GridLength(100) });
        infoTable.Columns.Add(new TableColumn { Width = new GridLength(180) });
        var infoRowGroup = new TableRowGroup();
        infoTable.RowGroups.Add(infoRowGroup);

        void AddInfoRow(string label, string value)
        {
            var row = new TableRow();
            row.Cells.Add(new TableCell(new Paragraph(new Run(label)) { Margin = new Thickness(0) }));
            row.Cells.Add(new TableCell(new Paragraph(new Run(value)) { Margin = new Thickness(0), TextAlignment = TextAlignment.Right }));
            infoRowGroup.Rows.Add(row);
        }

        AddInfoRow("Số HĐ:", order.MaDonHang);
        AddInfoRow("Mã đơn:", $"#{order.Id}");
        AddInfoRow("Ngày:", order.NgayTao.ToString("HH:mm dd/MM/yyyy"));
        AddInfoRow("Thu ngân:", order.NhanVien?.HoTen ?? "Quản Trị Viên");
        AddInfoRow("Khách hàng:", order.KhachHang?.HoTen ?? "Khách vãng lai");
        doc.Blocks.Add(infoTable);

        doc.Blocks.Add(new Paragraph(new Run(separator)) { Margin = new Thickness(0) });

        // Items Table
        var itemsTable = new Table() { Margin = new Thickness(0, 5, 0, 5), CellSpacing = 0 };
        itemsTable.Columns.Add(new TableColumn { Width = new GridLength(150) }); 
        itemsTable.Columns.Add(new TableColumn { Width = new GridLength(40) });   
        itemsTable.Columns.Add(new TableColumn { Width = new GridLength(90) }); 
        var itemsRowGroup = new TableRowGroup();
        itemsTable.RowGroups.Add(itemsRowGroup);

        var headerRow = new TableRow();
        headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tên SP")) { FontWeight = FontWeights.Bold, Margin = new Thickness(0) }));
        headerRow.Cells.Add(new TableCell(new Paragraph(new Run("SL")) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center, Margin = new Thickness(0) }));
        headerRow.Cells.Add(new TableCell(new Paragraph(new Run("T.Tiền")) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Right, Margin = new Thickness(0) }));
        itemsRowGroup.Rows.Add(headerRow);

        doc.Blocks.Add(itemsTable);
        doc.Blocks.Add(new Paragraph(new Run(separator)) { Margin = new Thickness(0) });

        var dataTable = new Table() { Margin = new Thickness(0, 5, 0, 5), CellSpacing = 0 };
        dataTable.Columns.Add(new TableColumn { Width = new GridLength(150) }); 
        dataTable.Columns.Add(new TableColumn { Width = new GridLength(40) });   
        dataTable.Columns.Add(new TableColumn { Width = new GridLength(90) }); 
        var dataRowGroup = new TableRowGroup();
        dataTable.RowGroups.Add(dataRowGroup);

        if (order.ChiTietDonHangs != null)
        {
            foreach (var item in order.ChiTietDonHangs)
            {
                var row = new TableRow();
                var nameText = item.SanPham?.TenSanPham ?? "Sản phẩm";
                row.Cells.Add(new TableCell(new Paragraph(new Run(nameText)) { Margin = new Thickness(0, 2, 0, 2) }));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.SoLuong.ToString())) { TextAlignment = TextAlignment.Center, Margin = new Thickness(0, 2, 0, 2) }));
                row.Cells.Add(new TableCell(new Paragraph(new Run($"{item.ThanhTien:N0} đ")) { TextAlignment = TextAlignment.Right, Margin = new Thickness(0, 2, 0, 2) }));
                dataRowGroup.Rows.Add(row);
            }
        }
        doc.Blocks.Add(dataTable);

        doc.Blocks.Add(new Paragraph(new Run(separator)) { Margin = new Thickness(0) });

        // Summary Table
        var summaryTable = new Table() { Margin = new Thickness(0, 5, 0, 5), CellSpacing = 0 };
        summaryTable.Columns.Add(new TableColumn { Width = new GridLength(140) });
        summaryTable.Columns.Add(new TableColumn { Width = new GridLength(140) });
        var summaryRowGroup = new TableRowGroup();
        summaryTable.RowGroups.Add(summaryRowGroup);

        void AddSummaryRow(string label, string value, bool isBold = false, int fontSize = 13)
        {
            var row = new TableRow();
            var lblRun = new Run(label);
            var valRun = new Run(value);
            if (isBold) { lblRun.FontWeight = FontWeights.Bold; valRun.FontWeight = FontWeights.Bold; }
            if (fontSize != 13) { lblRun.FontSize = fontSize; valRun.FontSize = fontSize; }
            
            row.Cells.Add(new TableCell(new Paragraph(lblRun) { Margin = new Thickness(0, 2, 0, 2) }));
            row.Cells.Add(new TableCell(new Paragraph(valRun) { Margin = new Thickness(0, 2, 0, 2), TextAlignment = TextAlignment.Right }));
            summaryRowGroup.Rows.Add(row);
        }

        AddSummaryRow("Tạm tính:", $"{order.TongTienHang:N0} đ");
        AddSummaryRow("Chiết khấu:", $"{order.ChietKhau:N0} đ");
        AddSummaryRow("VAT (10%):", $"+{order.ThueVAT:N0} đ");

        doc.Blocks.Add(summaryTable);
        doc.Blocks.Add(new Paragraph(new Run(separator)) { Margin = new Thickness(0) });

        var totalTable = new Table() { Margin = new Thickness(0, 5, 0, 5), CellSpacing = 0 };
        totalTable.Columns.Add(new TableColumn { Width = new GridLength(140) });
        totalTable.Columns.Add(new TableColumn { Width = new GridLength(140) });
        var totalRowGroup = new TableRowGroup();
        totalTable.RowGroups.Add(totalRowGroup);
        
        var totalRow = new TableRow();
        totalRow.Cells.Add(new TableCell(new Paragraph(new Run("TỔNG CỘNG:")) { FontWeight = FontWeights.Bold, FontSize = 16, Margin = new Thickness(0) }));
        totalRow.Cells.Add(new TableCell(new Paragraph(new Run($"{order.TongThanhToan:N0} đ")) { FontWeight = FontWeights.Bold, FontSize = 16, TextAlignment = TextAlignment.Right, Margin = new Thickness(0) }));
        totalRowGroup.Rows.Add(totalRow);

        doc.Blocks.Add(totalTable);
        doc.Blocks.Add(new Paragraph(new Run(separator)) { Margin = new Thickness(0) });

        var paymentTable = new Table() { Margin = new Thickness(0, 5, 0, 5), CellSpacing = 0 };
        paymentTable.Columns.Add(new TableColumn { Width = new GridLength(160) });
        paymentTable.Columns.Add(new TableColumn { Width = new GridLength(120) });
        var paymentRowGroup = new TableRowGroup();
        paymentTable.RowGroups.Add(paymentRowGroup);

        var paymentMethodStr = "Tiền mặt"; 
        if (order.GiaoDichs != null && order.GiaoDichs.Any())
        {
            var firstTx = order.GiaoDichs.First();
            if (firstTx.PhuongThucThanhToanId == 2) paymentMethodStr = "Thẻ ngân hàng";
            else if (firstTx.PhuongThucThanhToanId == 3) paymentMethodStr = "Chuyển khoản";
        }

        var payRow = new TableRow();
        payRow.Cells.Add(new TableCell(new Paragraph(new Run($"Thanh toán ({paymentMethodStr}):")) { Margin = new Thickness(0) }));
        payRow.Cells.Add(new TableCell(new Paragraph(new Run($"{order.TongThanhToan:N0} đ")) { TextAlignment = TextAlignment.Right, Margin = new Thickness(0) }));
        paymentRowGroup.Rows.Add(payRow);

        doc.Blocks.Add(paymentTable);
        doc.Blocks.Add(new Paragraph(new Run(separator)) { Margin = new Thickness(0) });

        // Footer
        var footer = new Paragraph() { TextAlignment = TextAlignment.Center, Margin = new Thickness(0, 5, 0, 0) };
        footer.Inlines.Add(new Run("Cảm ơn Quý khách!\n") { FontStyle = FontStyles.Italic, FontWeight = FontWeights.Bold });
        footer.Inlines.Add(new Run("Hẹn gặp lại lần sau"));
        doc.Blocks.Add(footer);

        return doc;
    }
}
