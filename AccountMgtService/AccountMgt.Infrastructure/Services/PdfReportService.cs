using AccountMgt.Application.Abstraction.Services;
using AccountMgt.Models.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace AccountMgt.Infrastructure.Services
{
    public class PdfReportService : IPdfReportService
    {
        private const float TitleFontSize = 18;
        private const string TitleText = "Detailed Transaction Report";
        private const double HeaderHeight = 20; // Height for headers
        private const double RowHeight = 20; // Height for each row
        private const double Margin = 40; // Margin from top for the first page

        public async Task<byte[]?> GenerateTransactionReportPdfAsync(List<DetailedTransactionReport> reportData)
        {
            return await Task.Run(() =>
            {
                using var memoryStream = new MemoryStream();
                using (var document = new PdfDocument())
                {
                    // Create a new page
                    var page = document.AddPage();
                    page.Size = PdfSharp.PageSize.A4;
                    page.Orientation = PdfSharp.PageOrientation.Portrait;

                    // Create XGraphics for drawing
                    var gfx = XGraphics.FromPdfPage(page);

                    // Draw title
                    var titleFont = new XFont("Verdana", TitleFontSize, XFontStyleEx.Bold);
                    gfx.DrawString(TitleText, titleFont, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.TopCenter);

                    // Define starting position for table
                    double yPoint = Margin; // Start down for the title

                    // Draw table headers
                    var headerFont = new XFont("Verdana", 10, XFontStyleEx.Bold);
                    DrawHeader(gfx, headerFont, ref yPoint);

                    // Draw data rows
                    var contentFont = new XFont("Verdana", 7, XFontStyleEx.Regular);
                    foreach (var item in reportData)
                    {
                        // Check if we need to create a new page
                        if (yPoint + RowHeight > page.Height - Margin)
                        {
                            // Create a new page
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            yPoint = Margin; // Reset yPoint for the new page
                            DrawHeader(gfx, headerFont, ref yPoint); // Redraw headers
                        }

                        // Draw the data row
                        DrawDataRow(gfx, contentFont, item, yPoint);
                        yPoint += RowHeight; // Move down for the next row
                    }

                    // Save the document to the memory stream
                    document.Save(memoryStream, false);
                }

                return memoryStream.ToArray();
            });
        }

        private void DrawHeader(XGraphics gfx, XFont headerFont, ref double yPoint)
        {
            gfx.DrawString("User", headerFont, XBrushes.Black, new XRect(20, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString("Merchant", headerFont, XBrushes.Black, new XRect(120, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString("Currency", headerFont, XBrushes.Black, new XRect(220, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString("Transaction Type", headerFont, XBrushes.Black, new XRect(300, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString("Total Amount", headerFont, XBrushes.Black, new XRect(400, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString("Transaction Count", headerFont, XBrushes.Black, new XRect(500, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString("Average Amount", headerFont, XBrushes.Black, new XRect(600, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString("Total Fees", headerFont, XBrushes.Black, new XRect(700, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString("Description", headerFont, XBrushes.Black, new XRect(800, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString("Last Transaction Date", headerFont, XBrushes.Black, new XRect(900, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString("Time Interval", headerFont, XBrushes.Black, new XRect(1000, yPoint, 0, 0), XStringFormats.TopLeft);
            yPoint += HeaderHeight; // Move down for the next row
        }

        private void DrawDataRow(XGraphics gfx, XFont contentFont, DetailedTransactionReport item, double yPoint)
        {
            gfx.DrawString(item.UserName ?? string.Empty, contentFont, XBrushes.Black, new XRect(20, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString(item.MerchantName ?? string.Empty, contentFont, XBrushes.Black, new XRect(120, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString(item.CurrencyCode ?? string.Empty, contentFont, XBrushes.Black, new XRect(220, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString(item.TransactionType ?? string.Empty, contentFont, XBrushes.Black, new XRect(300, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString(item.TotalAmount.ToString("C"), contentFont, XBrushes.Black, new XRect(400, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString(item.TransactionCount.ToString(), contentFont, XBrushes.Black, new XRect(500, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString(item.AverageAmount.ToString("C"), contentFont, XBrushes.Black, new XRect(600, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString(item.TotalFees.ToString("C"), contentFont, XBrushes.Black, new XRect(700, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString(item.LatestChangeDescription ?? string.Empty, contentFont, XBrushes.Black, new XRect(800, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString(item.LastTransactionDate.ToString("yyyy-MM-dd"), contentFont, XBrushes.Black, new XRect(900, yPoint, 0, 0), XStringFormats.TopLeft);
            gfx.DrawString(item.TimeInterval ?? string.Empty, contentFont, XBrushes.Black, new XRect(1000, yPoint, 0, 0), XStringFormats.TopLeft);
        }
    }
}
