using System.Drawing;
using System.Windows.Forms;

namespace Trade.It
{
    internal sealed class FullScreenToggleButton : Button
    {
        private bool active;

        public bool Active
        {
            get => active;
            set
            {
                if (active == value)
                    return;

                active = value;
                Invalidate();
            }
        }

        public FullScreenToggleButton()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 1;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rect = ClientRectangle;
            rect.Width--;
            rect.Height--;

            var backgroundColor = active
                ? SystemColors.Highlight
                : SystemColors.Control;
            var foregroundColor = active
                ? SystemColors.HighlightText
                : SystemColors.ControlText;
            var borderColor = active
                ? SystemColors.Highlight
                : SystemColors.ControlDark;

            using var background = new SolidBrush(backgroundColor);
            using var border = new Pen(borderColor);

            e.Graphics.FillRectangle(background, rect);
            e.Graphics.DrawRectangle(border, rect);

            if (Image != null)
            {
                var imageBounds = new Rectangle(
                    (ClientRectangle.Width - Image.Width) / 2,
                    (ClientRectangle.Height - Image.Height) / 2,
                    Image.Width,
                    Image.Height);
                e.Graphics.DrawImage(Image, imageBounds);
            }
            else
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    Text,
                    Font,
                    ClientRectangle,
                    foregroundColor,
                    TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.SingleLine);
            }
        }
    }
}