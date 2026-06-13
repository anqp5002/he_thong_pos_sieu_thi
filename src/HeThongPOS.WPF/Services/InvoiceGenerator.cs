using System;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;

namespace HeThongPOS.WPF.Services;

public class InvoiceGenerator : IInvoiceService
{
    private static readonly FontFamily ReceiptFont = new FontFamily("Segoe UI");
    private const double ReceiptWidth = 300; // ~80mm thermal paper
    private const double ContentWidth = 280;

    public object GenerateInvoiceDocument(DonHang order, decimal cashReceived, decimal changeAmount)
    {
        var doc = new FlowDocument
        {
            PageWidth = ReceiptWidth,
            PagePadding = new Thickness(10, 15, 10, 15),
            FontFamily = ReceiptFont,
            FontSize = 11,
            Foreground = new SolidColorBrush(Color.FromRgb(30, 41, 59)), // Slate 800
            Background = Brushes.White
        };

        // 1. Store Header
        var headerPara = new Paragraph
        {
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 0, 0, 10)
        };
        headerPara.Inlines.Add(new Bold(new Run("Cua hang ABC\n")) { FontSize = 14 });
        headerPara.Inlines.Add(new Run("123 ABC Street, EFG Ward, TD District, HCM\n") { FontSize = 9 });
        headerPara.Inlines.Add(new Run("Hotline: 1900 2001") { FontSize = 9 });
        doc.Blocks.Add(headerPara);

        // Divider
        doc.Blocks.Add(CreateDivider(false));

        // 2. Receipt Title
        var titlePara = new Paragraph
        {
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 5, 0, 5)
        };
        titlePara.Inlines.Add(new Bold(new Run("PHIẾU THANH TOÁN")) { FontSize = 12 });
        doc.Blocks.Add(titlePara);

        doc.Blocks.Add(CreateDivider(false));

        // 3. Metadata Info
        var infoTable = new Table { Margin = new Thickness(0, 5, 0, 5) };
        infoTable.Columns.Add(new TableColumn { Width = new GridLength(80) });
        infoTable.Columns.Add(new TableColumn { Width = new GridLength(ContentWidth - 80) });
        
        var infoRowGroup = new TableRowGroup();
        
        // Order Code
        var rowCode = new TableRow();
        rowCode.Cells.Add(new TableCell(new Paragraph(new Run("Số CT:")) { Margin = new Thickness(0, 2, 0, 2) }));
        rowCode.Cells.Add(new TableCell(new Paragraph(new Bold(new Run(order.MaDonHang))) { Margin = new Thickness(0, 2, 0, 2) }));
        infoRowGroup.Rows.Add(rowCode);

        // Date
        var rowDate = new TableRow();
        rowDate.Cells.Add(new TableCell(new Paragraph(new Run("Ngày CT:")) { Margin = new Thickness(0, 2, 0, 2) }));
        rowDate.Cells.Add(new TableCell(new Paragraph(new Run(order.NgayTao.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture))) { Margin = new Thickness(0, 2, 0, 2) }));
        infoRowGroup.Rows.Add(rowDate);

        // Cashier
        var rowCashier = new TableRow();
        rowCashier.Cells.Add(new TableCell(new Paragraph(new Run("Nhân viên:")) { Margin = new Thickness(0, 2, 0, 2) }));
        rowCashier.Cells.Add(new TableCell(new Paragraph(new Run(order.NhanVien?.HoTen ?? "Nguyễn Văn A")) { Margin = new Thickness(0, 2, 0, 2) }));
        infoRowGroup.Rows.Add(rowCashier);

        // Customer
        var rowCustomer = new TableRow();
        rowCustomer.Cells.Add(new TableCell(new Paragraph(new Run("Khách hàng:")) { Margin = new Thickness(0, 2, 0, 2) }));
        rowCustomer.Cells.Add(new TableCell(new Paragraph(new Run(order.KhachHang?.HoTen ?? "Khách lẻ / Walk-in")) { Margin = new Thickness(0, 2, 0, 2) }));
        infoRowGroup.Rows.Add(rowCustomer);

        infoTable.RowGroups.Add(infoRowGroup);
        doc.Blocks.Add(infoTable);

        doc.Blocks.Add(CreateDivider(true));

        // 4. Items Table
        var itemsTable = new Table { Margin = new Thickness(0, 5, 0, 5) };
        itemsTable.Columns.Add(new TableColumn { Width = new GridLength(50) });  // Code/Barcode abbreviation
        itemsTable.Columns.Add(new TableColumn { Width = new GridLength(130) }); // Product Name
        itemsTable.Columns.Add(new TableColumn { Width = new GridLength(30) });  // Qty
        itemsTable.Columns.Add(new TableColumn { Width = new GridLength(70) });  // Total
        
        var headerGroup = new TableRowGroup();
        var headerRow = new TableRow { FontWeight = FontWeights.SemiBold };
        headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Mã")) { Margin = new Thickness(0, 2, 0, 2) }));
        headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tên hàng")) { Margin = new Thickness(0, 2, 0, 2) }));
        headerRow.Cells.Add(new TableCell(new Paragraph(new Run("SL")) { Margin = new Thickness(0, 2, 0, 2), TextAlignment = TextAlignment.Right }));
        headerRow.Cells.Add(new TableCell(new Paragraph(new Run("T.Tiền")) { Margin = new Thickness(0, 2, 0, 2), TextAlignment = TextAlignment.Right }));
        headerGroup.Rows.Add(headerRow);
        itemsTable.RowGroups.Add(headerGroup);

        var itemsGroup = new TableRowGroup();
        foreach (var item in order.ChiTietDonHangs)
        {
            var productCode = item.SanPham?.MaVach ?? "N/A";
            // Show only last 4 digits of barcode as abbreviation
            var codeAbbr = productCode.Length > 4 ? "#" + productCode[^4..] : "#" + productCode;
            var productName = item.SanPham?.TenSanPham ?? "Sản phẩm";
            var itemTotal = item.SoLuong * item.DonGia;

            var row = new TableRow();
            row.Cells.Add(new TableCell(new Paragraph(new Run(codeAbbr)) { Margin = new Thickness(0, 2, 0, 2), FontSize = 9.5 }));
            row.Cells.Add(new TableCell(new Paragraph(new Run(productName)) { Margin = new Thickness(0, 2, 0, 2) }));
            row.Cells.Add(new TableCell(new Paragraph(new Run(item.SoLuong.ToString())) { Margin = new Thickness(0, 2, 0, 2), TextAlignment = TextAlignment.Right }));
            row.Cells.Add(new TableCell(new Paragraph(new Run(FormatCurrency(itemTotal))) { Margin = new Thickness(0, 2, 0, 2), TextAlignment = TextAlignment.Right }));
            itemsGroup.Rows.Add(row);
        }
        itemsTable.RowGroups.Add(itemsGroup);
        doc.Blocks.Add(itemsTable);

        doc.Blocks.Add(CreateDivider(true));

        // 5. Totals Table
        var totalsTable = new Table { Margin = new Thickness(0, 5, 0, 5) };
        totalsTable.Columns.Add(new TableColumn { Width = new GridLength(140) });
        totalsTable.Columns.Add(new TableColumn { Width = new GridLength(140) });

        var totalsGroup = new TableRowGroup();

        // Subtotal (TongTienHang)
        var rowSub = new TableRow();
        rowSub.Cells.Add(new TableCell(new Paragraph(new Run("Tổng tiền:")) { Margin = new Thickness(0, 2, 0, 2) }));
        rowSub.Cells.Add(new TableCell(new Paragraph(new Run(FormatCurrency(order.TongTienHang))) { Margin = new Thickness(0, 2, 0, 2), TextAlignment = TextAlignment.Right }));
        totalsGroup.Rows.Add(rowSub);

        // Discount
        if (order.ChietKhau > 0)
        {
            var rowDisc = new TableRow();
            rowDisc.Cells.Add(new TableCell(new Paragraph(new Run("Chiết khấu:")) { Margin = new Thickness(0, 2, 0, 2) }));
            rowDisc.Cells.Add(new TableCell(new Paragraph(new Run("-" + FormatCurrency(order.ChietKhau))) { Margin = new Thickness(0, 2, 0, 2), TextAlignment = TextAlignment.Right }));
            totalsGroup.Rows.Add(rowDisc);
        }

        // VAT
        if (order.ThueVAT > 0)
        {
            var rowVat = new TableRow();
            rowVat.Cells.Add(new TableCell(new Paragraph(new Run("Thuế VAT (10%):")) { Margin = new Thickness(0, 2, 0, 2) }));
            rowVat.Cells.Add(new TableCell(new Paragraph(new Run(FormatCurrency(order.ThueVAT))) { Margin = new Thickness(0, 2, 0, 2), TextAlignment = TextAlignment.Right }));
            totalsGroup.Rows.Add(rowVat);
        }

        // Total Payment
        var rowTotal = new TableRow { FontWeight = FontWeights.Bold };
        rowTotal.Cells.Add(new TableCell(new Paragraph(new Run("Thanh toán:")) { Margin = new Thickness(0, 4, 0, 4), FontSize = 12 }));
        rowTotal.Cells.Add(new TableCell(new Paragraph(new Run(FormatCurrency(order.TongThanhToan))) { Margin = new Thickness(0, 4, 0, 4), TextAlignment = TextAlignment.Right, FontSize = 12 }));
        totalsGroup.Rows.Add(rowTotal);

        // Divider
        totalsTable.RowGroups.Add(totalsGroup);
        doc.Blocks.Add(totalsTable);

        doc.Blocks.Add(CreateDivider(false));

        // 6. Payment method specific details
        var payTable = new Table { Margin = new Thickness(0, 5, 0, 5) };
        payTable.Columns.Add(new TableColumn { Width = new GridLength(140) });
        payTable.Columns.Add(new TableColumn { Width = new GridLength(140) });

        var payGroup = new TableRowGroup();
        
        // Find if there is a transaction to get the payment method name
        var transaction = order.GiaoDichs?.FirstOrDefault(t => t.TrangThai == "SUCCESS");
        string payMethodName = transaction?.PhuongThucThanhToan?.TenPhuongThuc ?? "Tiền mặt";
        
        var rowMethod = new TableRow();
        rowMethod.Cells.Add(new TableCell(new Paragraph(new Run($"{payMethodName}:")) { Margin = new Thickness(0, 2, 0, 2) }));
        rowMethod.Cells.Add(new TableCell(new Paragraph(new Run(FormatCurrency(cashReceived > 0 ? cashReceived : order.TongThanhToan))) { Margin = new Thickness(0, 2, 0, 2), TextAlignment = TextAlignment.Right }));
        payGroup.Rows.Add(rowMethod);

        if (cashReceived > order.TongThanhToan)
        {
            var rowChange = new TableRow();
            rowChange.Cells.Add(new TableCell(new Paragraph(new Run("Tiền thối lại:")) { Margin = new Thickness(0, 2, 0, 2), Foreground = new SolidColorBrush(Color.FromRgb(16, 185, 129)) })); // Success Green
            rowChange.Cells.Add(new TableCell(new Paragraph(new Run(FormatCurrency(changeAmount))) { Margin = new Thickness(0, 2, 0, 2), TextAlignment = TextAlignment.Right, FontWeight = FontWeights.SemiBold, Foreground = new SolidColorBrush(Color.FromRgb(16, 185, 129)) }));
            payGroup.Rows.Add(rowChange);
        }

        payTable.RowGroups.Add(payGroup);
        doc.Blocks.Add(payTable);

        doc.Blocks.Add(CreateDivider(true));

        // 7. Footer Message
        var footerPara = new Paragraph
        {
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 10, 0, 0)
        };
        footerPara.Inlines.Add(new Bold(new Run("Cảm ơn quý khách!\n")) { FontSize = 11 });
        footerPara.Inlines.Add(new Run("*** Hẹn gặp lại ***") { FontSize = 10 });
        doc.Blocks.Add(footerPara);

        return doc;
    }

    private Paragraph CreateDivider(bool isDotted)
    {
        var runText = isDotted
            ? "------------------------------------------------"
            : "================================================";

        return new Paragraph(new Run(runText))
        {
            TextAlignment = TextAlignment.Center,
            FontSize = 9,
            Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225)), // Slate 300
            Margin = new Thickness(0, 2, 0, 2),
            LineHeight = 1
        };
    }

    private string FormatCurrency(decimal amount)
    {
        return amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + "đ";
    }
}
