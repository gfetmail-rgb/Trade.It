using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Trade.It;

public sealed partial class SymbolDefinitionForm : Form
{
    private bool loading;

    public SymbolDefinitionForm()
    {
        InitializeComponent();
        SetComboDefaults();
        LoadGrid();
        newButton.Click += (_, _) => ClearEditor();
        saveButton.Click += (_, _) => SaveCurrent();
        deleteButton.Click += (_, _) => DeleteCurrent();
        deleteAllButton.Click += (_, _) => DeleteAll();
        importButton.Click += (_, _) => ImportExcel();
        closeButton.Click += (_, _) => Close();
        symbolsDataGridView.SelectionChanged += (_, _) => LoadSelected();
    }

    private void SetComboDefaults()
    {
        exchangeComboBox.SelectedIndex = 0;
        marketComboBox.SelectedIndex = 0;
        boardComboBox.SelectedIndex = 0;
        assetComboBox.SelectedIndex = 0;
        groupComboBox.SelectedIndex = 0;
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
        symbolTextBox.Text = SymbolDefinitionRules.NormalizeText(x.SymbolTitle);
        nameTextBox.Text = SymbolDefinitionRules.NormalizeText(x.Name);
        SelectComboValue(exchangeComboBox, x.ExchangeTitle);
        SelectComboValue(marketComboBox, x.MarketType);
        SelectComboValue(boardComboBox, x.BoardType);
        SelectComboValue(assetComboBox, x.AssetType);
        groupComboBox.Text = SymbolDefinitionRules.NormalizeText(x.IndustryGroupOrFundType);
    }

    private static void SelectComboValue(ComboBox comboBox, string value)
    {
        var normalized = SymbolDefinitionRules.NormalizeText(value);
        if (string.IsNullOrWhiteSpace(normalized)) normalized = SymbolDefinitionRules.EmptyOption;
        for (int i = 0; i < comboBox.Items.Count; i++)
        {
            if (string.Equals(SymbolDefinitionRules.NormalizeText(Convert.ToString(comboBox.Items[i]) ?? ""), normalized, StringComparison.Ordinal))
            {
                comboBox.SelectedIndex = i;
                return;
            }
        }
        comboBox.SelectedIndex = 0;
    }

    private void ClearEditor()
    {
        symbolsDataGridView.ClearSelection();
        symbolTextBox.Clear();
        nameTextBox.Clear();
        SetComboDefaults();
        groupComboBox.SelectedIndex = 0;
        symbolTextBox.Focus();
    }

    private SymbolDefinition? ReadEditor()
    {
        var symbol = SymbolDefinitionRules.NormalizeText(symbolTextBox.Text);
        if (string.IsNullOrWhiteSpace(symbol))
        {
            MessageBox.Show(this, "عنوان نماد را وارد کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            symbolTextBox.Focus();
            return null;
        }
        if (!SymbolDefinitionRules.IsAllowed(exchangeComboBox.Text, SymbolDefinitionRules.Exchanges, out var exchange))
        {
            MessageBox.Show(this, "عنوان بورس را انتخاب کنید؛ در صورت نداشتن اطلاعات، «-» را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            exchangeComboBox.Focus();
            return null;
        }
        if (!SymbolDefinitionRules.IsAllowed(marketComboBox.Text, SymbolDefinitionRules.Markets, out var market))
        {
            MessageBox.Show(this, "نوع بازار را انتخاب کنید؛ در صورت نداشتن اطلاعات، «-» را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            marketComboBox.Focus();
            return null;
        }
        if (!SymbolDefinitionRules.IsAllowed(boardComboBox.Text, SymbolDefinitionRules.Boards, out var board))
        {
            MessageBox.Show(this, "نوع تابلو را انتخاب کنید؛ در صورت نداشتن اطلاعات، «-» را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            boardComboBox.Focus();
            return null;
        }
        if (!SymbolDefinitionRules.IsAllowed(assetComboBox.Text, SymbolDefinitionRules.Assets, out var asset))
        {
            MessageBox.Show(this, "نوع دارایی را انتخاب کنید؛ در صورت نداشتن اطلاعات، «-» را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            assetComboBox.Focus();
            return null;
        }
        return new SymbolDefinition
        {
            SymbolTitle = symbol,
            Name = SymbolDefinitionRules.NormalizeText(nameTextBox.Text),
            ExchangeTitle = exchange,
            MarketType = market,
            BoardType = board,
            AssetType = asset,
            IndustryGroupOrFundType = SymbolDefinitionRules.NormalizeText(groupComboBox.Text)
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
        var s = SymbolDefinitionRules.NormalizeText(symbolTextBox.Text);
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
            var imported = ExcelSymbolReader.Read(d.FileName, out var invalidRows, out var invalidDetails);
            if (imported.Count == 0)
            {
                var message = invalidRows > 0
                    ? $"هیچ ردیف معتبری پیدا نشد.\nردیف‌های نامعتبر: {invalidRows}\n\n{invalidDetails}"
                    : "هیچ ردیف قابل استفاده‌ای پیدا نشد.";
                MessageBox.Show(this, message, "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var map = SymbolDefinitionStore.Load().ToDictionary(x => SymbolDefinitionRules.NormalizeText(x.SymbolTitle), StringComparer.OrdinalIgnoreCase);
            int added = 0, updated = 0;
            foreach (var x in imported)
            {
                var key = SymbolDefinitionRules.NormalizeText(x.SymbolTitle);
                if (map.ContainsKey(key)) updated++; else added++;
                map[key] = x;
            }
            SymbolDefinitionStore.Save(map.Values);
            LoadGrid();
            var resultMessage = $"ورود انجام شد.\nجدید: {added}\nبه‌روزشده: {updated}";
            if (invalidRows > 0) resultMessage += $"\nنامعتبر: {invalidRows}\n\n{invalidDetails}";
            MessageBox.Show(this, resultMessage, "ورود از Excel", MessageBoxButtons.OK, invalidRows > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"خواندن فایل Excel انجام نشد:\n{ex.Message}", "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

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
            var items = JsonSerializer.Deserialize<List<SymbolDefinition>>(File.ReadAllText(FilePath, Encoding.UTF8), Options) ?? new();
            foreach (var x in items) SymbolDefinitionRules.Normalize(x);
            return items;
        }
        catch { return new(); }
    }

    public static void Save(IEnumerable<SymbolDefinition> items)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
        var normalized = items.ToList();
        foreach (var x in normalized) SymbolDefinitionRules.Normalize(x);
        File.WriteAllText(FilePath, JsonSerializer.Serialize(normalized.OrderBy(x => x.SymbolTitle, StringComparer.OrdinalIgnoreCase).ToList(), Options), new UTF8Encoding(false));
    }
}

internal static class SymbolDefinitionRules
{
    public const string EmptyOption = "-";
    public static readonly string[] Exchanges = { EmptyOption, "بورس تهران", "فرابورس ایران", "بورس کالا", "بورس انرژی" };
    public static readonly string[] Markets = { EmptyOption, "بازار اول", "بازار دوم", "بازار پایه", "بازار شرکت‌های کوچک و متوسط", "بازار نوآفرین" };
    public static readonly string[] Boards = { EmptyOption, "تابلوی اصلی", "تابلوی فرعی", "بازار اول", "بازار دوم", "پایه زرد", "پایه نارنجی", "پایه قرمز" };
    public static readonly string[] Assets = { EmptyOption, "سهام", "صندوق" };

    public static string NormalizeText(string value)
    {
        return value
            .Replace('\u064A', '\u06CC')
            .Replace('\u0649', '\u06CC')
            .Replace('\u0643', '\u06A9')
            .Replace('\u200C', ' ')
            .Replace('\u200D', ' ')
            .Replace('\uFEFF', ' ')
            .Trim();
    }

    public static void Normalize(SymbolDefinition x)
    {
        x.SymbolTitle = NormalizeText(x.SymbolTitle);
        x.Name = NormalizeText(x.Name);
        x.ExchangeTitle = NormalizeText(x.ExchangeTitle);
        x.MarketType = NormalizeText(x.MarketType);
        x.BoardType = NormalizeText(x.BoardType);
        x.AssetType = NormalizeText(x.AssetType);
        x.IndustryGroupOrFundType = NormalizeText(x.IndustryGroupOrFundType);
    }

    public static bool IsAllowed(string value, IReadOnlyCollection<string> allowed, out string standardValue)
    {
        var normalized = NormalizeText(value);
        if (string.IsNullOrWhiteSpace(normalized)) normalized = EmptyOption;
        var match = allowed.FirstOrDefault(x => string.Equals(NormalizeText(x), normalized, StringComparison.Ordinal));
        standardValue = match ?? "";
        return match != null;
    }
}

internal static class ExcelSymbolReader
{
    private static readonly string[] Headers = { "عنوان نماد", "نام نماد", "عنوان بورس", "نوع بازار", "نوع تابلو", "نوع دارایی (سهام یا صندوق)", "گروه صنعت یا نوع صندوق" };

    public static List<SymbolDefinition> Read(string path, out int invalidRows, out string invalidDetails)
    {
        invalidRows = 0;
        invalidDetails = "";
        var details = new List<string>();
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
            var normalized = SymbolDefinitionRules.NormalizeText(pair.Value);
            foreach (var expected in Headers)
                if (HeaderMatches(normalized, expected)) indexByHeader[expected] = pair.Key;
        }
        foreach (var h in Headers) if (!indexByHeader.ContainsKey(h)) throw new InvalidDataException($"ستون «{h}» در فایل پیدا نشد.");
        var result = new List<SymbolDefinition>();
        int excelRow = 1;
        foreach (var xmlRow in rows.Skip(1))
        {
            excelRow++;
            var cells = Row(xmlRow, s, shared);
            string V(string h) => cells.TryGetValue(indexByHeader[h], out var value) ? SymbolDefinitionRules.NormalizeText(value) : "";
            var symbol = V(Headers[0]);
            if (string.IsNullOrWhiteSpace(symbol))
            {
                invalidRows++;
                if (details.Count < 20) details.Add($"ردیف {excelRow}: عنوان نماد خالی است.");
                continue;
            }
            var exchange = V(Headers[2]);
            var market = V(Headers[3]);
            var board = V(Headers[4]);
            var asset = V(Headers[5]);
            if (string.IsNullOrWhiteSpace(exchange)) exchange = SymbolDefinitionRules.EmptyOption;
            if (string.IsNullOrWhiteSpace(market)) market = SymbolDefinitionRules.EmptyOption;
            if (string.IsNullOrWhiteSpace(board)) board = SymbolDefinitionRules.EmptyOption;
            if (string.IsNullOrWhiteSpace(asset)) asset = SymbolDefinitionRules.EmptyOption;
            var errors = new List<string>();
            if (!SymbolDefinitionRules.IsAllowed(exchange, SymbolDefinitionRules.Exchanges, out var standardExchange)) errors.Add("عنوان بورس");
            if (!SymbolDefinitionRules.IsAllowed(market, SymbolDefinitionRules.Markets, out var standardMarket)) errors.Add("نوع بازار");
            if (!SymbolDefinitionRules.IsAllowed(board, SymbolDefinitionRules.Boards, out var standardBoard)) errors.Add("نوع تابلو");
            if (!SymbolDefinitionRules.IsAllowed(asset, SymbolDefinitionRules.Assets, out var standardAsset)) errors.Add("نوع دارایی");
            if (errors.Count > 0)
            {
                invalidRows++;
                if (details.Count < 20) details.Add($"ردیف {excelRow}: {string.Join("، ", errors)} نامعتبر است.");
                continue;
            }
            result.Add(new SymbolDefinition
            {
                SymbolTitle = symbol,
                Name = V(Headers[1]),
                ExchangeTitle = standardExchange,
                MarketType = standardMarket,
                BoardType = standardBoard,
                AssetType = standardAsset,
                IndustryGroupOrFundType = V(Headers[6])
            });
        }
        invalidDetails = details.Count == 0 ? "" : string.Join(Environment.NewLine, details) + (invalidRows > details.Count ? Environment.NewLine + "..." : "");
        return result;
    }

    private static bool HeaderMatches(string actual, string expected) => actual == SymbolDefinitionRules.NormalizeText(expected) || actual.Replace(" ", "") == SymbolDefinitionRules.NormalizeText(expected).Replace(" ", "");

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