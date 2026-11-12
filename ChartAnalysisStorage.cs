using System.Text.Json;
using System.Text.Json.Serialization;
using System.Drawing;

namespace Trade.It
{
    internal sealed class ChartAnalysisDocument
    {
        public int Version { get; set; } = 2;
        public string Symbol { get; set; } = string.Empty;
        public DateTime SavedAt { get; set; } = DateTime.Now;
        public string ChartType { get; set; } = nameof(TradingChartType.Candlestick);
        public bool GridVisible { get; set; }
        public bool CrosshairVisible { get; set; }
        public int VisibleCount { get; set; }
        public int FirstIndex { get; set; }
        public DateTime? VisibleStartDate { get; set; }
        public DateTime? VisibleEndDate { get; set; }
        public double VerticalZoom { get; set; } = 1.0;
        public double VerticalPanOffset { get; set; }
        public double HorizontalPanOffset { get; set; }
        public double ChartPanCompensation { get; set; }
        public double VolumePanelRatio { get; set; } = 0.22;
        public bool VolumePanelVisible { get; set; } = true;
        // null means a legacy file that predates indicator persistence; an empty list is an intentional empty layout.
        public List<ChartIndicator>? Indicators { get; set; }
        public bool OnFuMoradiAutoMode { get; set; }
        public bool OnFuMoradiHasOptimizedParameters { get; set; }
        public List<ChartAnalysisDrawing> Drawings { get; set; } = new();
        public List<ChartAnalysisDrawing> AdvancedDrawings { get; set; } = new();
        public List<ChartAnalysisDrawing> ExtraDrawings { get; set; } = new();
    }

    internal sealed class ChartAnalysisDrawing
    {
        public string Tool { get; set; } = string.Empty;
        public double X1 { get; set; }
        public DateTime? Date1 { get; set; }
        public double Y1 { get; set; }
        public DateTime? Date2 { get; set; }
        public double X2 { get; set; }
        public DateTime? Date3 { get; set; }
        public double Y2 { get; set; }
        public double X3 { get; set; }
        public double Y3 { get; set; }
        public string? Text { get; set; }
    }

    internal sealed class DrawingColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.Number)
                throw new JsonException("Invalid color value.");

            return Color.FromArgb(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.ToArgb());
        }
    }

    internal static class ChartAnalysisStorage
    {
        private static readonly JsonSerializerOptions SerializerOptions = CreateSerializerOptions();

        private static JsonSerializerOptions CreateSerializerOptions()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new DrawingColorJsonConverter());
            return options;
        }

        private static readonly string FolderPath = Path.Combine(
            AppContext.BaseDirectory,
            "Analysis");

        private static string GetFilePath(string symbol)
        {
            var safeName = string.Concat(
                symbol.Trim().Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));

            return Path.Combine(FolderPath, safeName + ".json");
        }

        public static void Save(ChartAnalysisDocument document)
        {
            if (string.IsNullOrWhiteSpace(document.Symbol))
                throw new ArgumentException("نماد تحلیل مشخص نشده است.", nameof(document));

            Directory.CreateDirectory(FolderPath);

            var json = JsonSerializer.Serialize(document, SerializerOptions);

            File.WriteAllText(GetFilePath(document.Symbol), json);
        }

        public static ChartAnalysisDocument? Load(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return null;

            var filePath = GetFilePath(symbol);
            if (!File.Exists(filePath))
                return null;

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ChartAnalysisDocument>(json, SerializerOptions);
        }
    }
}
