using System.Text.Json;

namespace Trade.It
{
    internal static class ChartAppearanceSettings
    {
        private sealed class StoredSettings
        {
            public double ChartRightEmptyPercent { get; set; } = 25.0;
            public int RisingCandleColor { get; set; } = Color.FromArgb(35, 150, 80).ToArgb();
            public int FallingCandleColor { get; set; } = Color.FromArgb(205, 70, 70).ToArgb();
            public int LineChartColor { get; set; } = Color.FromArgb(35, 90, 160).ToArgb();
            public int TrendLineColor { get; set; } = Color.FromArgb(30, 90, 160).ToArgb();
            public int TrendChannelColor { get; set; } = Color.FromArgb(30, 90, 160).ToArgb();
            public int HorizontalDoubleArrowColor { get; set; } = Color.FromArgb(30, 90, 160).ToArgb();
            public int VerticalDoubleArrowColor { get; set; } = Color.FromArgb(30, 90, 160).ToArgb();
            public int HorizontalRayColor { get; set; } = Color.FromArgb(30, 90, 160).ToArgb();
            public int TrendLineWithArrowColor { get; set; } = Color.FromArgb(30, 90, 160).ToArgb();
            public int RectangleColor { get; set; } = Color.FromArgb(30, 90, 160).ToArgb();
            public int FibonacciRetracementColor { get; set; } = Color.FromArgb(45, 105, 170).ToArgb();
            public int TextLabelColor { get; set; } = Color.FromArgb(45, 105, 170).ToArgb();
            public int PitchforkColor { get; set; } = Color.FromArgb(155, 80, 45).ToArgb();
            public int FibonacciExtensionColor { get; set; } = Color.FromArgb(155, 80, 45).ToArgb();
            public int MeasureColor { get; set; } = Color.FromArgb(155, 80, 45).ToArgb();
        }

        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Trade.It",
            "ChartAppearance.json");

        public static double ChartRightEmptyPercent { get; private set; } = 25.0;
        public static Color RisingCandleColor { get; private set; } = Color.FromArgb(35, 150, 80);
        public static Color FallingCandleColor { get; private set; } = Color.FromArgb(205, 70, 70);
        public static Color LineChartColor { get; private set; } = Color.FromArgb(35, 90, 160);
        public static Color TrendLineColor { get; private set; } = Color.FromArgb(30, 90, 160);
        public static Color TrendChannelColor { get; private set; } = Color.FromArgb(30, 90, 160);
        public static Color HorizontalDoubleArrowColor { get; private set; } = Color.FromArgb(30, 90, 160);
        public static Color VerticalDoubleArrowColor { get; private set; } = Color.FromArgb(30, 90, 160);
        public static Color HorizontalRayColor { get; private set; } = Color.FromArgb(30, 90, 160);
        public static Color TrendLineWithArrowColor { get; private set; } = Color.FromArgb(30, 90, 160);
        public static Color RectangleColor { get; private set; } = Color.FromArgb(30, 90, 160);
        public static Color FibonacciRetracementColor { get; private set; } = Color.FromArgb(45, 105, 170);
        public static Color TextLabelColor { get; private set; } = Color.FromArgb(45, 105, 170);
        public static Color PitchforkColor { get; private set; } = Color.FromArgb(155, 80, 45);
        public static Color FibonacciExtensionColor { get; private set; } = Color.FromArgb(155, 80, 45);
        public static Color MeasureColor { get; private set; } = Color.FromArgb(155, 80, 45);

        public static void Load()
        {
            try
            {
                if (!File.Exists(FilePath)) return;
                var stored = JsonSerializer.Deserialize<StoredSettings>(File.ReadAllText(FilePath));
                if (stored == null) return;
                Apply(stored);
            }
            catch
            {
                // Keep defaults if settings cannot be read.
            }
        }

        public static void Save()
        {
            try
            {
                var folder = Path.GetDirectoryName(FilePath);
                if (!string.IsNullOrWhiteSpace(folder)) Directory.CreateDirectory(folder);
                var stored = new StoredSettings
                {
                    ChartRightEmptyPercent = ChartRightEmptyPercent,
                    RisingCandleColor = RisingCandleColor.ToArgb(),
                    FallingCandleColor = FallingCandleColor.ToArgb(),
                    LineChartColor = LineChartColor.ToArgb(),
                    TrendLineColor = TrendLineColor.ToArgb(),
                    TrendChannelColor = TrendChannelColor.ToArgb(),
                    HorizontalDoubleArrowColor = HorizontalDoubleArrowColor.ToArgb(),
                    VerticalDoubleArrowColor = VerticalDoubleArrowColor.ToArgb(),
                    HorizontalRayColor = HorizontalRayColor.ToArgb(),
                    TrendLineWithArrowColor = TrendLineWithArrowColor.ToArgb(),
                    RectangleColor = RectangleColor.ToArgb(),
                    FibonacciRetracementColor = FibonacciRetracementColor.ToArgb(),
                    TextLabelColor = TextLabelColor.ToArgb(),
                    PitchforkColor = PitchforkColor.ToArgb(),
                    FibonacciExtensionColor = FibonacciExtensionColor.ToArgb(),
                    MeasureColor = MeasureColor.ToArgb()
                };
                File.WriteAllText(FilePath, JsonSerializer.Serialize(stored, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch
            {
                // Settings are non-critical; keep the running configuration.
            }
        }

        public static void ResetChartColors()
        {
            var defaults = new StoredSettings();
            RisingCandleColor = Color.FromArgb(defaults.RisingCandleColor);
            FallingCandleColor = Color.FromArgb(defaults.FallingCandleColor);
            LineChartColor = Color.FromArgb(defaults.LineChartColor);
        }

        public static void ResetDrawingColors()
        {
            var defaults = new StoredSettings();
            TrendLineColor = Color.FromArgb(defaults.TrendLineColor);
            TrendChannelColor = Color.FromArgb(defaults.TrendChannelColor);
            HorizontalDoubleArrowColor = Color.FromArgb(defaults.HorizontalDoubleArrowColor);
            VerticalDoubleArrowColor = Color.FromArgb(defaults.VerticalDoubleArrowColor);
            HorizontalRayColor = Color.FromArgb(defaults.HorizontalRayColor);
            TrendLineWithArrowColor = Color.FromArgb(defaults.TrendLineWithArrowColor);
            RectangleColor = Color.FromArgb(defaults.RectangleColor);
            FibonacciRetracementColor = Color.FromArgb(defaults.FibonacciRetracementColor);
            TextLabelColor = Color.FromArgb(defaults.TextLabelColor);
            PitchforkColor = Color.FromArgb(defaults.PitchforkColor);
            FibonacciExtensionColor = Color.FromArgb(defaults.FibonacciExtensionColor);
            MeasureColor = Color.FromArgb(defaults.MeasureColor);
        }

        public static void SetChartColors(Color rising, Color falling, Color line)
        {
            RisingCandleColor = rising;
            FallingCandleColor = falling;
            LineChartColor = line;
        }

        public static void SetDrawingColor(string key, Color color)
        {
            switch (key)
            {
                case nameof(TrendLineColor): TrendLineColor = color; break;
                case nameof(TrendChannelColor): TrendChannelColor = color; break;
                case nameof(HorizontalDoubleArrowColor): HorizontalDoubleArrowColor = color; break;
                case nameof(VerticalDoubleArrowColor): VerticalDoubleArrowColor = color; break;
                case nameof(HorizontalRayColor): HorizontalRayColor = color; break;
                case nameof(TrendLineWithArrowColor): TrendLineWithArrowColor = color; break;
                case nameof(RectangleColor): RectangleColor = color; break;
                case nameof(FibonacciRetracementColor): FibonacciRetracementColor = color; break;
                case nameof(TextLabelColor): TextLabelColor = color; break;
                case nameof(PitchforkColor): PitchforkColor = color; break;
                case nameof(FibonacciExtensionColor): FibonacciExtensionColor = color; break;
                case nameof(MeasureColor): MeasureColor = color; break;
            }
        }

        private static void Apply(StoredSettings stored)
        {
            ChartRightEmptyPercent = Math.Clamp(stored.ChartRightEmptyPercent, 0, 90);
            RisingCandleColor = Color.FromArgb(stored.RisingCandleColor);
            FallingCandleColor = Color.FromArgb(stored.FallingCandleColor);
            LineChartColor = Color.FromArgb(stored.LineChartColor);
            TrendLineColor = Color.FromArgb(stored.TrendLineColor);
            TrendChannelColor = Color.FromArgb(stored.TrendChannelColor);
            HorizontalDoubleArrowColor = Color.FromArgb(stored.HorizontalDoubleArrowColor);
            VerticalDoubleArrowColor = Color.FromArgb(stored.VerticalDoubleArrowColor);
            HorizontalRayColor = Color.FromArgb(stored.HorizontalRayColor);
            TrendLineWithArrowColor = Color.FromArgb(stored.TrendLineWithArrowColor);
            RectangleColor = Color.FromArgb(stored.RectangleColor);
            FibonacciRetracementColor = Color.FromArgb(stored.FibonacciRetracementColor);
            TextLabelColor = Color.FromArgb(stored.TextLabelColor);
            PitchforkColor = Color.FromArgb(stored.PitchforkColor);
            FibonacciExtensionColor = Color.FromArgb(stored.FibonacciExtensionColor);
            MeasureColor = Color.FromArgb(stored.MeasureColor);
        }

        public static void SetChartRightEmptyPercent(double value) => ChartRightEmptyPercent = Math.Clamp(value, 0, 90);
    }
}
