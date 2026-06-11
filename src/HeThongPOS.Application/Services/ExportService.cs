using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace HeThongPOS.Application.Services;

public class ExportService
{
    public void ExportTopProductsToExcel(List<TopProductDto> products, string filePath)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Sản Phẩm Bán Chạy");

        // Headers
        worksheet.Cell(1, 1).Value = "Tên Sản Phẩm";
        worksheet.Cell(1, 2).Value = "Số Lượng Bán";
        worksheet.Cell(1, 3).Value = "Doanh Thu";

        // Styling headers
        var headerRange = worksheet.Range(1, 1, 1, 3);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

        // Data
        for (int i = 0; i < products.Count; i++)
        {
            var p = products[i];
            worksheet.Cell(i + 2, 1).Value = p.TenSanPham;
            worksheet.Cell(i + 2, 2).Value = p.SoLuongBan;
            worksheet.Cell(i + 2, 3).Value = p.DoanhThu;
        }

        // Format column
        worksheet.Column(3).Style.NumberFormat.Format = "#,##0";
        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(filePath);
    }
    
    public void ExportDailyRevenueToExcel(List<DailyRevenueDto> data, string filePath)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Doanh Thu Theo Ngày");

        // Headers
        worksheet.Cell(1, 1).Value = "Ngày";
        worksheet.Cell(1, 2).Value = "Doanh Thu";

        // Styling headers
        var headerRange = worksheet.Range(1, 1, 1, 2);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

        // Data
        for (int i = 0; i < data.Count; i++)
        {
            var item = data[i];
            worksheet.Cell(i + 2, 1).Value = item.Date.ToString("dd/MM/yyyy");
            worksheet.Cell(i + 2, 2).Value = item.Revenue;
        }

        // Format column
        worksheet.Column(2).Style.NumberFormat.Format = "#,##0";
        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(filePath);
    }
}
