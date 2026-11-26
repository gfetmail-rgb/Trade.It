using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Trade.It
{
    internal static class ChartToolbarIcons
    {
        private const int MaxIconSize = 24;

        private sealed class IconSet
        {
            public required Image Normal { get; init; }
            public required Image Active { get; init; }
            public Image? Success { get; init; }
        }

        private static readonly Dictionary<Button, IconSet> Icons = new();

        public static void Register(Button button, Image? normal, Image? active, Image? success = null)
        {
            if (button == null)
                throw new ArgumentNullException(nameof(button));

            if (normal == null)
                throw new InvalidOperationException(
                    $"Normal image is missing for toolbar button: {button.Name}");

            // منابع ActiveImage و SuccessImage در MainForm.resx تعریف نشده‌اند.
            // در صورت نبود تصویر جایگزین، از تصویر عادی استفاده می‌کنیم تا فرم هنگام اجرا متوقف نشود.
            var normalImage = FitImage(normal);

            var set = new IconSet
            {
                Normal = normalImage,
                Active = active == null ? CreateActiveImage(normalImage) : FitImage(active),
                Success = success == null ? null : FitImage(success)
            };

            Icons[button] = set;
            button.Image = set.Normal;
            button.ImageAlign = ContentAlignment.MiddleCenter;
        }

        // در نبود تصویر Active اختصاصی، نسخه آبی‌رنگی از آیکون عادی می‌سازیم.
        // شفافیت و جزئیات روشنایی تصویر اصلی حفظ می‌شوند.
        private static Image CreateActiveImage(Image source)
        {
            var bitmap = new Bitmap(source);
            const double originalWeight = 0.20;
            const double blueWeight = 0.80;
            var accent = Color.FromArgb(0, 120, 215);

            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    if (pixel.A == 0)
                        continue;

                    var red = (int)Math.Round(pixel.R * originalWeight + accent.R * blueWeight);
                    var green = (int)Math.Round(pixel.G * originalWeight + accent.G * blueWeight);
                    var blue = (int)Math.Round(pixel.B * originalWeight + accent.B * blueWeight);

                    bitmap.SetPixel(x, y, Color.FromArgb(pixel.A, red, green, blue));
                }
            }

            return bitmap;
        }

        // آیکون تمام دکمه‌های نوار ابزار، حتی دکمه‌هایی که حالت فعال ندارند،
        // در محدوده‌ای ثابت و متناسب با ارتفاع دکمه نمایش داده می‌شوند.
        public static void FitImages(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button && button.Image != null &&
                    !Icons.ContainsKey(button))
                {
                    button.Image = FitImage(button.Image);
                    button.ImageAlign = ContentAlignment.MiddleCenter;
                }

                if (control.HasChildren)
                    FitImages(control);
            }
        }

        private static Image FitImage(Image source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var scale = Math.Min(
                1.0,
                Math.Min(MaxIconSize / (double)source.Width, MaxIconSize / (double)source.Height));

            var width = Math.Max(1, (int)Math.Round(source.Width * scale));
            var height = Math.Max(1, (int)Math.Round(source.Height * scale));

            if (width == source.Width && height == source.Height)
                return (Image)source.Clone();

            var result = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(result))
            {
                graphics.Clear(Color.Transparent);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(source, new Rectangle(0, 0, width, height));
            }

            return result;
        }

        public static Image CreateTrendLineArrowIcon(Color color)
        {
            var bitmap = new Bitmap(24, 24, PixelFormat.Format32bppArgb);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.Transparent);

            using var pen = new Pen(color, 2.1f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };
            using var brush = new SolidBrush(color);

            var start = new PointF(3.5f, 20.5f);
            var tip = new PointF(20.5f, 3.5f);
            var baseCenter = new PointF(13.2f, 10.8f);
            var left = new PointF(10.0f, 4.4f);
            var right = new PointF(19.6f, 14.0f);

            graphics.DrawLine(pen, start, baseCenter);
            graphics.FillPolygon(brush, new[] { tip, left, right });
            return bitmap;
        }

        public static void SetActive(Button button, bool active)
        {
            if (!Icons.TryGetValue(button, out var set))
                return;

            button.Image = active ? set.Active : set.Normal;
            button.Invalidate();
        }

        public static void SetSaveFeedback(Button button, bool success)
        {
            if (!Icons.TryGetValue(button, out var set))
                return;

            button.Image = success && set.Success != null ? set.Success : set.Normal;
            button.Invalidate();
        }
    }
}