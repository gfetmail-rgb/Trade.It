namespace Trade.It
{
    public enum SymbolSource
    {
        FileName,
        InsideFile
    }

    public enum InputCalendar
    {
        Persian,
        Gregorian
    }

    public sealed class PortfolioDefinition
    {
        public string Name { get; set; } = string.Empty;
        public SymbolSource SymbolSource { get; set; }
        public string DataPath { get; set; } = string.Empty;
        public string FileType { get; set; } = "TXT";
        public string Separator { get; set; } = ",";
        public bool HasHeader { get; set; }
        public bool NoDateTime { get; set; }
        public InputCalendar Calendar { get; set; } = InputCalendar.Persian;
        public string DateFormat { get; set; } = "YYYYMMDD";
        public string TimeFormat { get; set; } = "HHMMSS";
        public List<PortfolioMapping> Mappings { get; set; } = new();
        public List<string> Symbols { get; set; } = new();
    }

    public sealed record PortfolioMapping(string Field, int Column);
}
