using System.Drawing.Drawing2D;
using System.Text.Json;

namespace Trade.It
{
    internal static class LineAppearanceSettings
    {
        private sealed class StoredSettings
        {
            public float ChartLineWidth { get; set; } = 1.6f;
            public int ChartLineStyle { get; set; } = (int)DashStyle.Solid;
            public float DrawingLineWidth { get; set; } = 1.8f;
            public int DrawingLineStyle { get; set; } = (int)DashStyle.Solid;
            public int CrosshairColor { get; set; } = Color.FromArgb(120, 120, 120).ToArgb();
            public float CrosshairLineWidth { get; set; } = 1.0f;
            public int CrosshairLineStyle { get; set; } = (int)DashStyle.Dot;
            public int GridColor { get; set; } = Color.FromArgb(225, 225, 225).ToArgb();
            public float GridLineWidth { get; set; } = 1.0f;
            public int GridLineStyle { get; set; } = (int)DashStyle.Solid;
        }

        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Trade.It",
            "LineAppearance.json");

        public static float ChartLineWidth { get; private set; } = 1.6f;
        public static DashStyle ChartLineStyle { get; private set; } = DashStyle.Solid;
        public static float DrawingLineWidth { get; private set; } = 1.8f;
        public static DashStyle DrawingLineStyle { get; private set; } = DashStyle.Solid;
        public static Color CrosshairColor { get; private set; } = Color.FromArgb(120, 120, 120);
        public static float CrosshairLineWidth { get; private set; } = 1.0f;
        public static DashStyle CrosshairLineStyle { get; private set; } = DashStyle.Dot;
        public static Color GridColor { get; private set; } = Color.FromArgb(225, 225, 225);
        public static float GridLineWidth { get; private set; } = 1.0f;
        public static DashStyle GridLineStyle { get; private set; } = DashStyle.Solid;

        static LineAppearanceSettings() => Load();

        public static void SetChartLine(float width, DashStyle style)
        {
            ChartLineWidth = ClampWidth(width);
            ChartLineStyle = style;
        }

        public static void SetDrawingLine(float width, DashStyle style)
        {
            DrawingLineWidth = ClampWidth(width);
            DrawingLineStyle = style;
        }

        public static void SetCrosshair(Color color, float width, DashStyle style)
        {
            CrosshairColor = color;
            CrosshairLineWidth = ClampWidth(width);
            CrosshairLineStyle = style;
        }

        public static void SetGrid(Color color, float width, DashStyle style)
        {
            GridColor = color;
            GridLineWidth = ClampWidth(width);
            GridLineStyle = style;
        }

        public static void Save()
        {
            try
            {
                var folder = Path.GetDirectoryName(FilePath);
                if (!string.IsNullOrWhiteSpace(folder)) Directory.CreateDirectory(folder);
                var stored = new StoredSettings
                {
                    ChartLineWidth = ChartLineWidth,
                    ChartLineStyle = (int)ChartLineStyle,
                    DrawingLineWidth = DrawingLineWidth,
                    DrawingLineStyle = (int)DrawingLineStyle,
                    CrosshairColor = CrosshairColor.ToArgb(),
                    CrosshairLineWidth = CrosshairLineWidth,
                    CrosshairLineStyle = (int)CrosshairLineStyle,
                    GridColor = GridColor.ToArgb(),
                    GridLineWidth = GridLineWidth,
                    GridLineStyle = (int)GridLineStyle
                };
                File.WriteAllText(FilePath, JsonSerializer.Serialize(stored, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch
            {
                // Settings are non-critical; keep the running configuration.
            }
        }

        private static void Load()
        {
            try
            {
                if (!File.Exists(FilePath)) return;
                var stored = JsonSerializer.Deserialize<StoredSettings>(File.ReadAllText(FilePath));
                if (stored == null) return;
                ChartLineWidth = ClampWidth(stored.ChartLineWidth);
                ChartLineStyle = NormalizeStyle(stored.ChartLineStyle, DashStyle.Solid);
                DrawingLineWidth = ClampWidth(stored.DrawingLineWidth);
                DrawingLineStyle = NormalizeStyle(stored.DrawingLineStyle, DashStyle.Solid);
                CrosshairColor = Color.FromArgb(stored.CrosshairColor);
                CrosshairLineWidth = ClampWidth(stored.CrosshairLineWidth);
                CrosshairLineStyle = NormalizeStyle(stored.CrosshairLineStyle, DashStyle.Dot);
                GridColor = Color.FromArgb(stored.GridColor);
                GridLineWidth = ClampWidth(stored.GridLineWidth);
                GridLineStyle = NormalizeStyle(stored.GridLineStyle, DashStyle.Solid);
            }
            catch
            {
                // Keep defaults if settings cannot be read.
            }
        }

        private static float ClampWidth(float value) => Math.Clamp(value, 0.5f, 8.0f);

        private static DashStyle NormalizeStyle(int value, DashStyle fallback)
        {
            return Enum.IsDefined(typeof(DashStyle), value) ? (DashStyle)value : fallback;
        }
    }
}
