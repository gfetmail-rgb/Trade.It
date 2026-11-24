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

        public static void Register(Button button, Image normal, Image active, Image? success = null)
        {
            var set = new IconSet
            {
                Normal = FitImage(normal),
                Active = FitImage(active),
                Success = success == null ? null : FitImage(success)
            };

            Icons[button] = set;
            button.Image = set.Normal;
            button.ImageAlign = ContentAlignment.MiddleCenter;
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