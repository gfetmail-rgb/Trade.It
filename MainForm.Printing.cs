using System.Drawing.Printing;

namespace Trade.It
{
    public partial class MainForm
    {
        private Button? symbolsPrintButton;
        private PrintDocument? symbolsPrintDocument;
        private PrintPreviewDialog? symbolsPrintPreviewDialog;
        private int symbolsPrintRowIndex;

        private void InitializeSymbolsPrintButton()
        {
            symbolsPrintButton = new Button
            {
                Name = "symbolsPrintButton",
                Text = "پرینت",
                Size = new Size(72, 34),
                Location = new Point(160, 16),
                Anchor = AnchorStyles.Bottom,
                RightToLeft = RightToLeft.Yes,
                UseVisualStyleBackColor = true
            };

            symbolsPrintButton.Click += SymbolsPrintButton_Click;
            groupBox2.Controls.Add(symbolsPrintButton);
            symbolsPrintButton.BringToFront();
        }

        private void SymbolsPrintButton_Click(object? sender, EventArgs e)
        {
            if (stocksDataGridView.Rows.Count == 0)
            {
                MessageBox.Show(
                    this,
                    "در حال حاضر نمادی برای چاپ در جدول وجود ندارد.",
                    "پرینت",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            symbolsPrintRowIndex = 0;

            symbolsPrintDocument?.Dispose();
            symbolsPrintDocument = new PrintDocument
            {
                DocumentName = "فهرست نمادها و تاریخ آخرین معامله"
            };
            symbolsPrintDocument.DefaultPageSettings.Landscape = false;
            symbolsPrintDocument.PrintPage += SymbolsPrintDocument_PrintPage;

            using var printDialog = new PrintDialog
            {
                Document = symbolsPrintDocument,
                UseEXDialog = true,
                AllowCurrentPage = false,
                AllowSelection = false,
                AllowSomePages = false
            };

            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                symbolsPrintDocument.Print();
            }

            symbolsPrintDocument.PrintPage -= SymbolsPrintDocument_PrintPage;
            symbolsPrintDocument.Dispose();
            symbolsPrintDocument = null;
        }

        private void SymbolsPrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
        {
            var graphics = e.Graphics;
            var bounds = e.MarginBounds;

            using var titleFont = new Font("Tahoma", 14, FontStyle.Bold);
            using var headerFont = new Font("Tahoma", 10, FontStyle.Bold);
            using var bodyFont = new Font("Tahoma", 10, FontStyle.Regular);
            using var linePen = new Pen(Color.Black, 1);

            var titleFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };

            var cellFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft,
                Trimming = StringTrimming.EllipsisCharacter
            };

            var titleRect = new RectangleF(bounds.Left, bounds.Top, bounds.Width, 45);
            graphics.DrawString(
                "فهرست نمادها و تاریخ آخرین معامله",
                titleFont,
                Brushes.Black,
                titleRect,
                titleFormat);

            var top = bounds.Top + 55f;
            const float rowHeight = 30;

            // The grid is printed in its current visual order.
            var columnWidth = bounds.Width / 3f;
            var rowColumnRect = new RectangleF(bounds.Left, top, columnWidth, rowHeight);
            var symbolColumnRect = new RectangleF(bounds.Left + columnWidth, top, columnWidth, rowHeight);
            var dateColumnRect = new RectangleF(bounds.Left + columnWidth * 2, top, columnWidth, rowHeight);

            DrawCell(graphics, "ردیف", rowColumnRect, headerFont, cellFormat, linePen, true);
            DrawCell(graphics, "نماد", symbolColumnRect, headerFont, cellFormat, linePen, true);
            DrawCell(graphics, "آخرین معامله", dateColumnRect, headerFont, cellFormat, linePen, true);

            top += rowHeight;

            while (symbolsPrintRowIndex < stocksDataGridView.Rows.Count)
            {
                var row = stocksDataGridView.Rows[symbolsPrintRowIndex];
                if (row.IsNewRow)
                {
                    symbolsPrintRowIndex++;
                    continue;
                }

                if (top + rowHeight > bounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                var rowNumber = GetPrintCellText(row, rowColumn);
                var symbol = GetPrintCellText(row, symbolColumn);
                var lastTrade = GetPrintCellText(row, lastTradeColumn);

                rowColumnRect = new RectangleF(bounds.Left, top, columnWidth, rowHeight);
                symbolColumnRect = new RectangleF(bounds.Left + columnWidth, top, columnWidth, rowHeight);
                dateColumnRect = new RectangleF(bounds.Left + columnWidth * 2, top, columnWidth, rowHeight);

                DrawCell(graphics, rowNumber, rowColumnRect, bodyFont, cellFormat, linePen, false);
                DrawCell(graphics, symbol, symbolColumnRect, bodyFont, cellFormat, linePen, false);
                DrawCell(graphics, lastTrade, dateColumnRect, bodyFont, cellFormat, linePen, false);

                top += rowHeight;
                symbolsPrintRowIndex++;
            }

            e.HasMorePages = false;
        }

        private static string GetPrintCellText(DataGridViewRow row, DataGridViewColumn column)
        {
            return Convert.ToString(row.Cells[column.Index].Value)?.Trim() ?? string.Empty;
        }

        private static void DrawCell(
            Graphics graphics,
            string text,
            RectangleF rectangle,
            Font font,
            StringFormat format,
            Pen pen,
            bool header)
        {
            using var backgroundBrush = new SolidBrush(
                header ? SystemColors.Control : Color.White);

            graphics.FillRectangle(backgroundBrush, rectangle);
            graphics.DrawRectangle(
                pen,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height);

            graphics.DrawString(
                text,
                font,
                Brushes.Black,
                rectangle,
                format);
        }
    }
}
