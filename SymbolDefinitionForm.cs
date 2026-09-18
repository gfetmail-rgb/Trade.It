using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Trade.It;

public sealed partial class SymbolDefinitionForm : Form
{
    private bool loading;
    private int sortColumnIndex = -1;
    private SortOrder sortOrder = SortOrder.None;

    public SymbolDefinitionForm()
    {
        InitializeComponent();
        foreach (DataGridViewColumn column in symbolsDataGridView.Columns)
            column.SortMode = column == rowNumberColumn ? DataGridViewColumnSortMode.NotSortable : DataGridViewColumnSortMode.Programmatic;
        SetComboDefaults();
        LoadGrid();
        newButton.Click += (_, _) => ClearEditor();
        saveButton.Click += (_, _) => SaveCurrent();
        deleteButton.Click += (_, _) => DeleteCurrent();
        deleteAllButton.Click += (_, _) => DeleteAll();
        importButton.Click += (_, _) => ImportExcel();
        exportButton.Click += (_, _) => ExportExcel();
        closeButton.Click += (_, _) => Close();
        symbolsDataGridView.SelectionChanged += (_, _) => LoadSelected();
        symbolsDataGridView.ColumnHeaderMouseClick += SymbolsDataGridView_ColumnHeaderMouseClick;
    }

    internal void CopyFilterItemsTo(ComboBox exchange,ComboBox market,ComboBox board,ComboBox asset,ComboBox fundType,ComboBox industryGroup)
    {
        CopyItems(exchangeComboBox, exchange);
        CopyItems(marketComboBox, market);
        CopyItems(boardComboBox, board);
        CopyItems(assetComboBox, asset);
        CopyItems(groupComboBox, fundType);
        CopyItems(industryGroupComboBox, industryGroup);
    }

    private static void CopyItems(ComboBox source, ComboBox target)
    {
        target.Items.Clear();

        foreach (var item in source.Items)
            target.Items.Add(item);

        target.SelectedIndex = -1;
    }
    

    private void SetComboDefaults()
    {
        exchangeComboBox.SelectedIndex = -1;
        marketComboBox.SelectedIndex = -1;
        boardComboBox.SelectedIndex = -1;
        assetComboBox.SelectedIndex = -1;
        groupComboBox.SelectedIndex = -1;
        industryGroupComboBox.SelectedIndex = -1;
    }

    private void LoadGrid(string? selectSymbol = null)
    {
        loading = true;
        try
        {
            symbolsDataGridView.Rows.Clear();
            int rowNumber = 1;
            foreach (var item in SymbolDefinitionStore.Load().OrderBy(x => x.SymbolTitle, StringComparer.OrdinalIgnoreCase))
            {
                var groupDisplay = string.IsNullOrWhiteSpace(item.IndustryGroup) || item.IndustryGroup == SymbolDefinitionRules.EmptyOption
                    ? item.FundType
                    : string.IsNullOrWhiteSpace(item.FundType) || item.FundType == SymbolDefinitionRules.EmptyOption
                        ? item.IndustryGroup
                        : $"{item.IndustryGroup} / {item.FundType}";

                int r = symbolsDataGridView.Rows.Add(rowNumber++, item.SymbolTitle, item.Name, item.ExchangeTitle, item.MarketType, item.BoardType, item.AssetType, groupDisplay);
                symbolsDataGridView.Rows[r].Tag = item;
            }
            countLabel.Text = $"تعداد: {symbolsDataGridView.Rows.Count}";
            if (!string.IsNullOrWhiteSpace(selectSymbol))
                foreach (DataGridViewRow r in symbolsDataGridView.Rows)
                    if (string.Equals(Convert.ToString(r.Cells[1].Value), selectSymbol, StringComparison.OrdinalIgnoreCase)) { r.Selected = true; break; }
        }
        finally { loading = false; }
    }

    private void SymbolsDataGridView_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex == rowNumberColumn.Index) return;

        var nextOrder = sortColumnIndex == e.ColumnIndex && sortOrder == SortOrder.Ascending
            ? SortOrder.Descending
            : SortOrder.Ascending;

        SortSymbolsGrid(e.ColumnIndex, nextOrder);
    }

    private void SortSymbolsGrid(int columnIndex, SortOrder order)
    {
        var selectedSymbol = symbolsDataGridView.SelectedRows.Count > 0
            ? symbolsDataGridView.SelectedRows[0].Tag as SymbolDefinition
            : null;

        var items = symbolsDataGridView.Rows.Cast<DataGridViewRow>()
            .Where(r => r.Tag is SymbolDefinition)
            .Select(r => (SymbolDefinition)r.Tag!)
            .ToList();

        items.Sort((a, b) =>
        {
            var aKey = GetSortValue(a, columnIndex);
            var bKey = GetSortValue(b, columnIndex);
            var result = StringComparer.OrdinalIgnoreCase.Compare(aKey, bKey);
            return order == SortOrder.Descending ? -result : result;
        });

        loading = true;
        try
        {
            symbolsDataGridView.Rows.Clear();
            int rowNumber = 1;
            foreach (var item in items)
            {
                var groupDisplay = string.IsNullOrWhiteSpace(item.IndustryGroup) || item.IndustryGroup == SymbolDefinitionRules.EmptyOption
                    ? item.FundType
                    : string.IsNullOrWhiteSpace(item.FundType) || item.FundType == SymbolDefinitionRules.EmptyOption
                        ? item.IndustryGroup
                        : $"{item.IndustryGroup} / {item.FundType}";

                int rowIndex = symbolsDataGridView.Rows.Add(rowNumber++, item.SymbolTitle, item.Name, item.ExchangeTitle, item.MarketType, item.BoardType, item.AssetType, groupDisplay);
                symbolsDataGridView.Rows[rowIndex].Tag = item;
                if (selectedSymbol != null && ReferenceEquals(item, selectedSymbol))
                    symbolsDataGridView.Rows[rowIndex].Selected = true;
            }

            sortColumnIndex = columnIndex;
            sortOrder = order;
            foreach (DataGridViewColumn column in symbolsDataGridView.Columns)
                column.HeaderCell.SortGlyphDirection = column.Index == columnIndex ? order : SortOrder.None;
        }
        finally
        {
            loading = false;
        }

        if (selectedSymbol != null && symbolsDataGridView.SelectedRows.Count > 0)
            LoadSelected();
    }

    private static string GetSortValue(SymbolDefinition item, int columnIndex) => columnIndex switch
    {
        1 => item.SymbolTitle ?? string.Empty,
        2 => item.Name ?? string.Empty,
        3 => item.ExchangeTitle ?? string.Empty,
        4 => item.MarketType ?? string.Empty,
        5 => item.BoardType ?? string.Empty,
        6 => item.AssetType ?? string.Empty,
        7 => string.IsNullOrWhiteSpace(item.FundType) || item.FundType == SymbolDefinitionRules.EmptyOption
            ? string.Empty
            : item.FundType,
        8 => string.IsNullOrWhiteSpace(item.IndustryGroup) || item.IndustryGroup == SymbolDefinitionRules.EmptyOption
            ? string.Empty
            : item.IndustryGroup,
        _ => string.Empty
    };

    private void LoadSelected()
    {
        if (loading || symbolsDataGridView.SelectedRows.Count == 0 || symbolsDataGridView.SelectedRows[0].Tag is not SymbolDefinition x) return;
        symbolTextBox.Text = SymbolDefinitionRules.NormalizeText(x.SymbolTitle);
        nameTextBox.Text = SymbolDefinitionRules.NormalizeText(x.Name);
        SelectComboValue(exchangeComboBox, x.ExchangeTitle);
        SelectComboValue(marketComboBox, x.MarketType);
        SelectComboValue(boardComboBox, x.BoardType);
        SelectComboValue(assetComboBox, x.AssetType);

        var fundType = x.FundType;
        var industryGroup = x.IndustryGroup;
        if (string.IsNullOrWhiteSpace(fundType) && string.IsNullOrWhiteSpace(industryGroup) && !string.IsNullOrWhiteSpace(x.IndustryGroupOrFundType))
            fundType = x.IndustryGroupOrFundType;

        SelectComboValue(groupComboBox, fundType);
        SelectComboValue(industryGroupComboBox, industryGroup);
    }

    private static void SelectComboValue(ComboBox comboBox, string value)
    {
        var normalized = SymbolDefinitionRules.NormalizeText(value);
        if (normalized == SymbolDefinitionRules.EmptyOption) normalized = "";
        if (string.IsNullOrWhiteSpace(normalized))
        {
            comboBox.SelectedIndex = -1;
            return;
        }
        for (int i = 0; i < comboBox.Items.Count; i++)
        {
            if (string.Equals(SymbolDefinitionRules.NormalizeText(Convert.ToString(comboBox.Items[i]) ?? ""), normalized, StringComparison.Ordinal))
            {
                comboBox.SelectedIndex = i;
                return;
            }
        }
        comboBox.SelectedIndex = -1;
    }

    private static string[] ComboValues(ComboBox comboBox) =>
        comboBox.Items.Cast<object>().Select(x => SymbolDefinitionRules.NormalizeText(Convert.ToString(x) ?? "")).ToArray();

    private void ClearEditor()
    {
        symbolsDataGridView.ClearSelection();
        symbolTextBox.Clear();
        nameTextBox.Clear();
        SetComboDefaults();
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
        if (!SymbolDefinitionRules.IsAllowed(exchangeComboBox.Text, exchangeComboBox.Items, out var exchange))
        {
            MessageBox.Show(this, "عنوان بورس را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            exchangeComboBox.Focus();
            return null;
        }
        if (!SymbolDefinitionRules.IsAllowed(marketComboBox.Text, marketComboBox.Items, out var market))
        {
            MessageBox.Show(this, "نوع بازار را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            marketComboBox.Focus();
            return null;
        }
        if (!SymbolDefinitionRules.IsAllowed(boardComboBox.Text, boardComboBox.Items, out var board))
        {
            MessageBox.Show(this, "نوع تابلو را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            boardComboBox.Focus();
            return null;
        }
        if (!SymbolDefinitionRules.IsAllowed(assetComboBox.Text, assetComboBox.Items, out var asset))
        {
            MessageBox.Show(this, "نوع دارایی را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            FundType = SymbolDefinitionRules.NormalizeText(groupComboBox.Text),
            IndustryGroup = SymbolDefinitionRules.NormalizeText(industryGroupComboBox.Text),
            IndustryGroupOrFundType = ""
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
            existing.FundType = item.FundType;
            existing.IndustryGroup = item.IndustryGroup;
            existing.IndustryGroupOrFundType = "";
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

    private void ExportExcel()
    {
        using var d = new SaveFileDialog
        {
            Title = "ذخیره نمادها در فایل Excel",
            Filter = "Excel (*.xlsx)|*.xlsx",
            DefaultExt = "xlsx",
            AddExtension = true,
            FileName = $"Symbols_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
            OverwritePrompt = true
        };
        if (d.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            var items = SymbolDefinitionStore.Load();
            ExcelSymbolWriter.Write(d.FileName, items);
            MessageBox.Show(this, $"خروجی Excel با موفقیت ایجاد شد.\nتعداد نمادها: {items.Count}", "خروجی به Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"ایجاد فایل Excel انجام نشد:\n{ex.Message}", "خروجی به Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void ImportExcel()
    {
        using var d = new OpenFileDialog { Title = "انتخاب فایل Excel نمادها", Filter = "Excel (*.xlsx)|*.xlsx", CheckFileExists = true };
        if (d.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            var imported = ExcelSymbolReader.Read(
                d.FileName,
                ComboValues(exchangeComboBox),
                ComboValues(marketComboBox),
                ComboValues(boardComboBox),
                ComboValues(assetComboBox),
                out var invalidRows,
                out var invalidDetails);
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
    public string FundType { get; set; } = "";
    public string IndustryGroup { get; set; } = "";
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
        x.ExchangeTitle = NormalizeOption(x.ExchangeTitle);
        x.MarketType = NormalizeOption(x.MarketType);
        x.BoardType = NormalizeOption(x.BoardType);
        x.AssetType = NormalizeOption(x.AssetType);
        x.FundType = NormalizeOption(x.FundType);
        x.IndustryGroup = NormalizeOption(x.IndustryGroup);
        x.IndustryGroupOrFundType = NormalizeOption(x.IndustryGroupOrFundType);
    }

    private static string NormalizeOption(string value)
    {
        var normalized = NormalizeText(value);
        return normalized == EmptyOption ? "" : normalized;
    }

    public static bool IsAllowed(string value, ComboBox.ObjectCollection items, out string standardValue) =>
        IsAllowed(value, items.Cast<object>(), out standardValue);

    public static bool IsAllowed(string value, IEnumerable<object> items, out string standardValue)
    {
        var normalized = NormalizeText(value);
        if (normalized == EmptyOption) normalized = "";
        if (string.IsNullOrWhiteSpace(normalized))
        {
            standardValue = "";
            return true;
        }
        var match = items
            .Select(x => NormalizeText(Convert.ToString(x) ?? ""))
            .FirstOrDefault(x => string.Equals(x, normalized, StringComparison.Ordinal));
        standardValue = match ?? "";
        return match != null;
    }

    public static bool IsAllowed(string value, IReadOnlyCollection<string> allowed, out string standardValue)
    {
        var normalized = NormalizeText(value);
        if (normalized == EmptyOption) normalized = "";
        if (string.IsNullOrWhiteSpace(normalized))
        {
            standardValue = "";
            return true;
        }
        var match = allowed.FirstOrDefault(x => string.Equals(NormalizeText(x), normalized, StringComparison.Ordinal));
        standardValue = match ?? "";
        return match != null;
    }
}

internal static class ExcelSymbolReader
{
    private static readonly string[] Headers = { "نماد", "نام", "بورس", "بازار", "تابلو", "دارایی", "نوع صندوق", "گروه صنعت" };

    public static List<SymbolDefinition> Read(
        string path,
        IReadOnlyCollection<string> exchanges,
        IReadOnlyCollection<string> markets,
        IReadOnlyCollection<string> boards,
        IReadOnlyCollection<string> assets,
        out int invalidRows,
        out string invalidDetails)
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
            var errors = new List<string>();
            if (!SymbolDefinitionRules.IsAllowed(exchange, exchanges, out var standardExchange)) errors.Add($"عنوان بورس «{exchange}»");
            if (!SymbolDefinitionRules.IsAllowed(market, markets, out var standardMarket)) errors.Add($"نوع بازار «{market}»");
            if (!SymbolDefinitionRules.IsAllowed(board, boards, out var standardBoard)) errors.Add($"نوع تابلو «{board}»");
            if (!SymbolDefinitionRules.IsAllowed(asset, assets, out var standardAsset)) errors.Add($"نوع دارایی «{asset}»");
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
                FundType = V(Headers[6]),
                IndustryGroup = V(Headers[7]),
                IndustryGroupOrFundType = ""
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
