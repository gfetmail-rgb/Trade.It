using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Trade.It
{
    internal static class ChartToolbarIcons
    {
        private sealed class IconSet
        {
            public required Bitmap Normal { get; init; }
            public required Bitmap Active { get; init; }
            public Bitmap? Success { get; init; }
        }

        private static readonly Dictionary<Button, IconSet> Icons = new();

        public static void Apply(Button button, string iconName, string tooltip, ToolTip toolTip)
        {
            if (!Icons.TryGetValue(button, out var iconSet))
            {
                iconSet = new IconSet
                {
                    Normal = DrawIcon(iconName, Color.FromArgb(45, 55, 65)),
                    Active = DrawIcon(iconName, Color.White),
                    Success = iconName == "save"
                        ? DrawIcon("save-check", Color.FromArgb(0, 97, 0))
                        : null
                };

                Icons.Add(button, iconSet);
            }

            // فقط ظاهر دکمه تغییر می‌کند؛ رویدادها، اندازه و منطق عملکرد حفظ می‌شوند.
            button.AutoSize = false;
            button.Text = string.Empty;
            button.Image = iconSet.Normal;
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.TextImageRelation = TextImageRelation.Overlay;
            button.Padding = Padding.Empty;
            button.AccessibleName = tooltip;
            toolTip.SetToolTip(button, tooltip);
        }

        public static void SetActive(Button button, bool active)
        {
            if (!Icons.TryGetValue(button, out var iconSet))
                return;

            button.Image = active ? iconSet.Active : iconSet.Normal;
            button.Invalidate();
        }

        public static void SetSaveFeedback(Button button, bool success)
        {
            if (!Icons.TryGetValue(button, out var iconSet))
                return;

            button.Image = success && iconSet.Success != null
                ? iconSet.Success
                : iconSet.Normal;
            button.Invalidate();
        }

        private static Bitmap DrawIcon(string name, Color color)
        {
            const int size = 24;
            var bitmap = new Bitmap(size, size);

            using var graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using var pen = new Pen(color, 1.8f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };
            using var thinPen = new Pen(color, 1.35f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };

            void Line(float x1, float y1, float x2, float y2) =>
                graphics.DrawLine(pen, x1, y1, x2, y2);

            void ThinLine(float x1, float y1, float x2, float y2) =>
                graphics.DrawLine(thinPen, x1, y1, x2, y2);

            void Rect(float x, float y, float width, float height) =>
                graphics.DrawRectangle(pen, x, y, width, height);

            void Ellipse(float x, float y, float width, float height) =>
                graphics.DrawEllipse(pen, x, y, width, height);

            void ArrowHead(float x, float y, bool pointsRight)
            {
                var direction = pointsRight ? -1f : 1f;
                Line(x + direction * 5, y - 4, x, y);
                Line(x + direction * 5, y + 4, x, y);
            }

            switch (name)
            {
                case "save":
                    Rect(4, 3, 16, 18);
                    Line(7, 3, 7, 9);
                    Line(7, 9, 17, 9);
                    Rect(8, 13, 8, 6);
                    break;

                case "save-check":
                    Rect(3, 3, 16, 18);
                    Line(6, 3, 6, 8);
                    Line(6, 8, 15, 8);
                    Rect(7, 12, 7, 6);
                    Line(13, 16, 16, 19);
                    Line(16, 19, 22, 12);
                    break;

                case "grid":
                    Rect(3, 3, 18, 18);
                    Line(9, 3, 9, 21);
                    Line(15, 3, 15, 21);
                    Line(3, 9, 21, 9);
                    Line(3, 15, 21, 15);
                    break;

                case "crosshair":
                    Ellipse(4, 4, 16, 16);
                    Line(12, 1, 12, 23);
                    Line(1, 12, 23, 12);
                    break;

                case "volume":
                    // ستون‌های حجم معاملات با ارتفاع‌های متفاوت و خط مبنا
                    Line(3, 21, 21, 21);
                    using (var volumeBrush = new SolidBrush(color))
                    {
                        graphics.FillRectangle(volumeBrush, 4, 14, 3, 7);
                        graphics.FillRectangle(volumeBrush, 9, 9, 3, 12);
                        graphics.FillRectangle(volumeBrush, 14, 5, 3, 16);
                        graphics.FillRectangle(volumeBrush, 19, 12, 2, 9);
                    }
                    break;

                case "test":
                    Ellipse(3, 3, 18, 18);
                    Line(12, 6, 12, 12);
                    Line(12, 12, 17, 15);
                    break;

                case "step-back":
                    Line(19, 5, 9, 12);
                    Line(9, 12, 19, 19);
                    Line(5, 5, 5, 19);
                    break;

                case "step-forward":
                    Line(5, 5, 15, 12);
                    Line(15, 12, 5, 19);
                    Line(19, 5, 19, 19);
                    break;

                case "zoom-in":
                case "zoom-out":
                    Ellipse(3, 3, 13, 13);
                    Line(15, 15, 22, 22);
                    Line(6.5f, 9.5f, 12.5f, 9.5f);
                    if (name == "zoom-in")
                        Line(9.5f, 6.5f, 9.5f, 12.5f);
                    break;

                case "reset":
                    // فلش چرخشی با سرِ فلش مشخص؛ نماد بازگشت نمای نمودار به حالت اولیه
                    graphics.DrawArc(pen, 4, 4, 16, 16, 45, 285);
                    using (var resetBrush = new SolidBrush(color))
                    {
                        graphics.FillPolygon(resetBrush, new[]
                        {
                            new PointF(18, 5),
                            new PointF(12, 3),
                            new PointF(15, 9)
                        });
                    }
                    break;

                case "hide-chart":
                    // نماد استاندارد چشم خط‌خورده: پنهان‌کردن
                    graphics.DrawArc(pen, 3, 6, 18, 12, 0, 180);
                    graphics.DrawArc(pen, 3, 6, 18, 12, 180, 180);
                    Ellipse(9, 9, 6, 6);
                    Line(3, 21, 21, 3);
                    break;

                case "erase":
                    graphics.DrawPolygon(pen, new[]
                    {
                        new PointF(4, 15), new PointF(13, 4),
                        new PointF(21, 11), new PointF(12, 20),
                        new PointF(4, 15)
                    });
                    Line(9, 19, 17, 10);
                    Line(8, 21, 21, 21);
                    break;

                case "print":
                    Rect(5, 3, 14, 6);
                    Rect(3, 9, 18, 9);
                    Rect(6, 14, 12, 7);
                    Line(7, 17, 17, 17);
                    Line(7, 19, 15, 19);
                    break;

                case "camera":
                    Rect(3, 7, 18, 13);
                    Line(8, 7, 10, 4);
                    Line(10, 4, 15, 4);
                    Line(15, 4, 17, 7);
                    Ellipse(8, 9, 8, 8);
                    break;

                case "fullscreen":
                    Line(3, 9, 3, 3);
                    Line(3, 3, 9, 3);
                    Line(15, 3, 21, 3);
                    Line(21, 3, 21, 9);
                    Line(3, 15, 3, 21);
                    Line(3, 21, 9, 21);
                    Line(15, 21, 21, 21);
                    Line(21, 15, 21, 21);
                    break;

                case "trend":
                    Line(3, 19, 20, 5);
                    Ellipse(1.5f, 17.5f, 3, 3);
                    Ellipse(18.5f, 3.5f, 3, 3);
                    break;

                case "channel":
                    // دو مرز موازی کانال؛ بدون خط میانی
                    Line(3, 18, 19, 5);
                    Line(5, 21, 21, 8);
                    break;

                case "horizontal":
                    Line(3, 12, 21, 12);
                    Line(3, 8, 3, 16);
                    Line(21, 8, 21, 16);
                    break;

                case "vertical":
                    Line(12, 3, 12, 21);
                    Line(8, 3, 16, 3);
                    Line(8, 21, 16, 21);
                    break;

                case "ray":
                    // نیم‌خط از نقطهٔ لنگر در سمت چپ به سمت راست
                    Ellipse(2, 9, 6, 6);
                    Line(8, 12, 22, 12);
                    break;

                case "arrow-trend":
                    // خط روند با نوک فلش واقعی در انتهای مسیر
                    Line(3, 19, 19, 5);
                    using (var arrowBrush = new SolidBrush(color))
                    {
                        graphics.FillPolygon(arrowBrush, new[]
                        {
                            new PointF(14, 5),
                            new PointF(21, 3),
                            new PointF(19, 10)
                        });
                    }
                    break;

                case "rectangle":
                    Rect(3, 5, 18, 14);
                    break;

                case "fibonacci":
                    Line(4, 3, 4, 21);
                    Line(4, 5, 21, 5);
                    Line(4, 9, 21, 9);
                    Line(4, 14, 21, 14);
                    Line(4, 19, 21, 19);
                    break;

                case "text":
                    using (var font = new Font("Segoe UI", 16, FontStyle.Bold, GraphicsUnit.Pixel))
                    using (var brush = new SolidBrush(color))
                        graphics.DrawString("T", font, brush, 4, 1);
                    Line(4, 21, 20, 21);
                    break;

                case "pitchfork":
                    // نماد سه‌شاخهٔ چنگال اندروز؛ سه بازوی هم‌راستا و دستهٔ مرکزی
                    Line(5, 3, 5, 11);
                    Line(12, 3, 12, 13);
                    Line(19, 3, 19, 11);
                    Line(5, 11, 8, 15);
                    Line(19, 11, 16, 15);
                    Line(8, 15, 16, 15);
                    Line(12, 13, 12, 22);
                    break;

                case "fib-extension":
                    Line(3, 4, 3, 20);
                    Line(3, 6, 20, 6);
                    Line(3, 11, 20, 11);
                    Line(3, 17, 20, 17);
                    Line(15, 3, 20, 6);
                    Line(15, 9, 20, 6);
                    break;

                case "ruler":
                    Line(4, 20, 20, 4);
                    Line(6, 17, 9, 20);
                    Line(10, 13, 13, 16);
                    Line(14, 9, 17, 12);
                    Line(18, 5, 21, 8);
                    break;

                case "close-all":
                    // پنجره‌های روی‌هم با علامت ضربدر بزرگ
                    Rect(3, 5, 13, 14);
                    Rect(7, 3, 13, 14);
                    Line(14, 14, 22, 22);
                    Line(22, 14, 14, 22);
                    break;

                default:
                    Ellipse(4, 4, 16, 16);
                    break;
            }

            return bitmap;
        }
    }
}
