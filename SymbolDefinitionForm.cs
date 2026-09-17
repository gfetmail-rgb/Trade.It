using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Trade.It;

public sealed class SymbolDefinition
{
    public string SymbolTitle { get; set; } = "";
    public string Name { get; set; } = "";
    public string ExchangeTitle { get; set; } = "";
    public string MarketType { get; set; } = "";
    public string BoardType { get; set; } = "";
    public string AssetType { get; set; } = "";
    public string IndustryGroupOrFundType { get; set; } = "";
}

public static class SymbolDefinitionStore
{
    private static string FilePath => Path.Combine(AppContext.BaseDirectory, "Data", "Symbols.json");
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public static List<SymbolDefinition> Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return new();
            return JsonSerializer.Deserialize<List<SymbolDefinition>>(File.ReadAllText(FilePath, Encoding.UTF8), Options) ?? new();
        }
        catch { return new(); }
    }

    public static void Save(IEnumerable<SymbolDefinition> items)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
        File.WriteAllText(FilePath, JsonSerializer.Serialize(items.OrderBy(x => x.SymbolTitle, StringComparer.OrdinalIgnoreCase).ToList(), Options), new UTF8Encoding(false));
    }
}

public sealed partial class SymbolDefinitionForm : Form
{
    private bool loading;

    public SymbolDefinitionForm()
    {
        InitializeComponent();
        LoadGrid();
        newButton.Click += (_, _) => ClearEditor();
        saveButton.Click += (_, _) => SaveCurrent();
        deleteButton.Click += (_, _) => DeleteCurrent();
        deleteAllButton.Click += (_, _) => DeleteAll();
        importButton.Click += (_, _) => ImportExcel();
        closeButton.Click += (_, _) => Close();
        symbolsDataGridView.SelectionChanged += (_, _) => LoadSelected();
    }

    private void LoadGrid(string? selectSymbol = null)
    {
        loading = true;
        try
        {
            symbolsDataGridView.Rows.Clear();
            foreach (var item in SymbolDefinitionStore.Load().OrderBy(x => x.SymbolTitle, StringComparer.OrdinalIgnoreCase))
            {
                int r = symbolsDataGridView.Rows.Add(item.SymbolTitle, item.Name, item.ExchangeTitle, item.MarketType, item.BoardType, item.AssetType, item.IndustryGroupOrFundType);
                symbolsDataGridView.Rows[r].Tag = item;
            }
            countLabel.Text = $"تعداد: {symbolsDataGridView.Rows.Count}";
            if (!string.IsNullOrWhiteSpace(selectSymbol))
                foreach (DataGridViewRow r in symbolsDataGridView.Rows)
                    if (string.Equals(Convert.ToString(r.Cells[0].Value), selectSymbol, StringComparison.OrdinalIgnoreCase)) { r.Selected = true; break; }
        }
        finally { loading = false; }
    }

    private void LoadSelected()
    {
        if (loading || symbolsDataGridView.SelectedRows.Count == 0 || symbolsDataGridView.SelectedRows[0].Tag is not SymbolDefinition x) return;
        symbolTextBox.Text = x.SymbolTitle;
        nameTextBox.Text = x.Name;
        exchangeTextBox.Text = x.ExchangeTitle;
        marketComboBox.Text = x.MarketType;
        boardComboBox.Text = x.BoardType;
        assetComboBox.SelectedItem = x.AssetType;
        groupTextBox.Text = x.IndustryGroupOrFundType;
    }

    private void ClearEditor()
    {
        symbolsDataGridView.ClearSelection();
        symbolTextBox.Clear();
        nameTextBox.Clear();
        exchangeTextBox.Clear();
        marketComboBox.Text = "";
        boardComboBox.Text = "";
        assetComboBox.SelectedIndex = -1;
        groupTextBox.Clear();
        symbolTextBox.Focus();
    }

    private SymbolDefinition? ReadEditor()
    {
        if (string.IsNullOrWhiteSpace(symbolTextBox.Text))
        {
            MessageBox.Show(this, "عنوان نماد را وارد کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            symbolTextBox.Focus();
            return null;
        }
        if (string.IsNullOrWhiteSpace(assetComboBox.Text))
        {
            MessageBox.Show(this, "نوع دارایی را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            assetComboBox.Focus();
            return null;
        }
        return new SymbolDefinition
        {
            SymbolTitle = symbolTextBox.Text.Trim(),
            Name = nameTextBox.Text.Trim(),
            ExchangeTitle = exchangeTextBox.Text.Trim(),
            MarketType = marketComboBox.Text.Trim(),
            BoardType = boardComboBox.Text.Trim(),
            AssetType = assetComboBox.Text.Trim(),
            IndustryGroupOrFundType = groupTextBox.Text.Trim()
        };
    }

    private void SaveCurrent()
    {
        var item = ReadEditor();
        if (item == null) return;
        var all = SymbolDefinitionStore.Load();
        var existing = all.FirstOrDefault(x => string.Equals(x.SymbolTitle.Trim(), item.SymbolTitle, StringComparison.OrdinalIgnoreCase));
        if (existing != null && symbolsDataGridView.SelectedRows.Count == 0)
        {
            MessageBox.Show(this, "این نماد قبلاً ثبت شده است. برای ویرایش، ابتدا نماد را از جدول انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (existing == null) all.Add(item);
        else
        {
            existing.SymbolTitle = item.SymbolTitle;
            existing.Name = item.Name;
            existing.ExchangeTitle = item.ExchangeTitle;
            existing.MarketType = item.MarketType;
            existing.BoardType = item.BoardType;
            existing.AssetType = item.AssetType;
            existing.IndustryGroupOrFundType = item.IndustryGroupOrFundType;
        }
        SymbolDefinitionStore.Save(all);
        LoadGrid(item.SymbolTitle);
    }

    private void DeleteCurrent()
    {
        var s = symbolTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(s)) return;
        if (MessageBox.Show(this, $"آیا نماد «{s}» حذف شود؟", "حذف نماد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        var all = SymbolDefinitionStore.Load();
        all.RemoveAll(x => string.Equals(x.SymbolTitle.Trim(), s, StringComparison.OrdinalIgnoreCase));
        SymbolDefinitionStore.Save(all);
        LoadGrid();
        ClearEditor();
    }

    private void DeleteAll()
    {
        var all = SymbolDefinitionStore.Load();
        if (all.Count == 0) return;
        if (MessageBox.Show(this, $"هر {all.Count} نماد ثبت‌شده حذف می‌شود. آیا مطمئن هستید؟", "حذف همه نمادها", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        SymbolDefinitionStore.Save(Array.Empty<SymbolDefinition>());
        LoadGrid();
        ClearEditor();
    }

    private void ImportExcel()
    {
        using var d = new OpenFileDialog { Title = "انتخاب فایل Excel نمادها", Filter = "Excel (*.xlsx)|*.xlsx", CheckFileExists = true };
        if (d.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            var imported = ExcelSymbolReader.Read(d.FileName);
            if (imported.Count == 0)
            {
                MessageBox.Show(this, "هیچ ردیف قابل استفاده‌ای پیدا نشد.", "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var map = SymbolDefinitionStore.Load().ToDictionary(x => x.SymbolTitle.Trim(), StringComparer.OrdinalIgnoreCase);
            int added = 0, updated = 0;
            foreach (var x in imported)
            {
                if (map.ContainsKey(x.SymbolTitle.Trim())) updated++; else added++;
                map[x.SymbolTitle.Trim()] = x;
            }
            SymbolDefinitionStore.Save(map.Values);
            LoadGrid();
            MessageBox.Show(this, $"ورود انجام شد.\nجدید: {added}\nبه‌روزشده: {updated}", "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"خواندن فایل Excel انجام نشد:\n{ex.Message}", "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

internal static class ExcelSymbolReader
{
    private static readonly string[] Headers = { "عنوان نماد", "نام نماد", "عنوان بورس", "نوع بازار", "نوع تابلو", "نوع دارایی (سهام یا صندوق)", "گروه صنعت یا نوع صندوق" };

    public static List<SymbolDefinition> Read(string path)
    {
        using var zip = ZipFile.OpenRead(path);
        XNamespace s = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        XNamespace r = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        XNamespace pr = "http://schemas.openxmlformats.org/package/2006/relationships";
        var wb = XDocument.Load(Entry(zip, "xl/workbook.xml"));
        var rels = XDocument.Load(Entry(zip, "xl/_rels/workbook.xml.rels"));
        var sheet = wb.Root?.Element(s + "sheets")?.Element(s + "sheet") ?? throw new InvalidDataException("Sheet پیدا نشد.");
        var rid = (string?)sheet.Attribute(r + "id") ?? throw new InvalidDataException("رابطه Sheet پیدا نشد.");
        var target = (string?)rels.Root?.Elements(pr + "Relationship").FirstOrDefault(x => (string?)x.Attribute("Id") == rid)?.Attribute("Target") ?? throw new InvalidDataException("مسیر Sheet پیدا نشد.");
        var sheetPath = target.StartsWith("/") ? target.TrimStart('/') : "xl/" + target.TrimStart('/');
        sheetPath = sheetPath.Replace("xl/xl/", "xl/");
        var shared = SharedStrings(zip, s);
        var doc = XDocument.Load(Entry(zip, sheetPath));
        var rows = doc.Root?.Element(s + "sheetData")?.Elements(s + "row").ToList() ?? new();
        if (rows.Count < 2) return new();
        var headerRow = Row(rows[0], s, shared);
        var indexByHeader = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (var pair in headerRow)
        {
            var normalized = Normalize(pair.Value);
            foreach (var expected in Headers)
                if (HeaderMatches(normalized, expected)) indexByHeader[expected] = pair.Key;
        }
        foreach (var h in Headers) if (!indexByHeader.ContainsKey(h)) throw new InvalidDataException($"ستون «{h}» در فایل پیدا نشد.");
        var result = new List<SymbolDefinition>();
        foreach (var xmlRow in rows.Skip(1))
        {
            var cells = Row(xmlRow, s, shared);
            string V(string h) => cells.TryGetValue(indexByHeader[h], out var value) ? value.Trim() : "";
            var symbol = V(Headers[0]);
            if (string.IsNullOrWhiteSpace(symbol)) continue;
            result.Add(new SymbolDefinition { SymbolTitle = symbol, Name = V(Headers[1]), ExchangeTitle = V(Headers[2]), MarketType = V(Headers[3]), BoardType = V(Headers[4]), AssetType = V(Headers[5]), IndustryGroupOrFundType = V(Headers[6]) });
        }
        return result;
    }

    private static bool HeaderMatches(string actual, string expected) => actual == Normalize(expected) || actual.Replace(" ", "") == Normalize(expected).Replace(" ", "");
    private static string Normalize(string x) => x.Replace("\u200c", "").Replace("ي", "ی").Replace("ك", "ک").Trim();

    private static Dictionary<int, string> Row(XElement row, XNamespace s, IReadOnlyList<string> shared)
    {
        var result = new Dictionary<int, string>();
        foreach (var c in row.Elements(s + "c"))
        {
            var reference = (string?)c.Attribute("r") ?? "";
            int col = ColumnIndex(reference);
            var value = c.Element(s + "v")?.Value ?? "";
            var type = (string?)c.Attribute("t");
            if (type == "s" && int.TryParse(value, out var n) && n >= 0 && n < shared.Count) value = shared[n];
            else if (type == "inlineStr") value = c.Element(s + "is")?.Value ?? "";
            result[col] = value;
        }
        return result;
    }

    private static int ColumnIndex(string reference)
    {
        int n = 0;
        foreach (char c in reference)
        {
            if (!char.IsLetter(c)) break;
            n = n * 26 + char.ToUpperInvariant(c) - 'A' + 1;
        }
        return n - 1;
    }

    private static List<string> SharedStrings(ZipArchive zip, XNamespace s)
    {
        var e = zip.GetEntry("xl/sharedStrings.xml");
        if (e == null) return new();
        using var stream = e.Open();
        var doc = XDocument.Load(stream);
        return doc.Root?.Elements(s + "si").Select(x => string.Concat(x.Descendants(s + "t").Select(t => t.Value))).ToList() ?? new();
    }

    private static Stream Entry(ZipArchive zip, string path) => (zip.GetEntry(path) ?? throw new InvalidDataException($"فایل داخلی Excel پیدا نشد: {path}")).Open();
}
