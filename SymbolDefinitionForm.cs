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

public sealed class SymbolDefinitionForm : Form
{
    private readonly TextBox symbol = new(), name = new(), exchange = new(), group = new();
    private readonly ComboBox market = new(), board = new(), asset = new();
    private readonly DataGridView grid = new();
    private readonly Label count = new();
    private readonly Button save = new(), delete = new(), deleteAll = new(), import = new(), newButton = new(), close = new();
    private bool loading;

    public SymbolDefinitionForm()
    {
        Text = "تعریف نمادها";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(1180, 700);
        MinimumSize = new Size(950, 600);
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        Font = new Font("Tahoma", 9F);
        BuildUi();
        LoadGrid();
    }

    private void BuildUi()
    {
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(10), RowCount = 3 };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        Controls.Add(root);

        var fields = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 2, Padding = new Padding(4) };
        for (int i = 0; i < 4; i++) fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        fields.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        fields.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        root.Controls.Add(fields, 0, 0);

        AddField(fields, 0, 0, "عنوان نماد", symbol);
        AddField(fields, 1, 0, "نام نماد", name);
        AddField(fields, 2, 0, "عنوان بورس", exchange);
        AddField(fields, 3, 0, "نوع بازار", market);
        AddField(fields, 0, 1, "نوع تابلو", board);
        AddField(fields, 1, 1, "نوع دارایی", asset);
        AddField(fields, 2, 1, "گروه صنعت / نوع صندوق", group);

        SetupCombo(market, "بورس تهران", "فرابورس ایران", "بورس کالا", "بورس انرژی");
        SetupCombo(board, "تابلوی اصلی", "تابلوی فرعی", "بازار اول", "بازار دوم", "پایه زرد", "پایه نارنجی", "پایه قرمز");
        SetupCombo(asset, "سهام", "صندوق");
        asset.DropDownStyle = ComboBoxStyle.DropDownList;

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false };
        root.Controls.Add(buttons, 0, 1);
        SetupButton(newButton, "جدید"); SetupButton(save, "ذخیره"); SetupButton(delete, "حذف");
        SetupButton(deleteAll, "حذف همه"); SetupButton(import, "ورود از Excel"); SetupButton(close, "بستن");
        count.AutoSize = true; count.Margin = new Padding(20, 9, 10, 3);
        buttons.Controls.AddRange(new Control[] { newButton, save, delete, deleteAll, import, close, count });

        grid.Dock = DockStyle.Fill;
        grid.ReadOnly = true; grid.AllowUserToAddRows = false; grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false; grid.MultiSelect = false; grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.RightToLeft = RightToLeft.Yes;
        AddColumn("عنوان نماد", 1.0); AddColumn("نام نماد", 1.5); AddColumn("عنوان بورس", 1.0);
        AddColumn("نوع بازار", 1.0); AddColumn("نوع تابلو", 1.0); AddColumn("نوع دارایی", .8); AddColumn("گروه صنعت / نوع صندوق", 1.5);
        root.Controls.Add(grid, 0, 2);

        newButton.Click += (_, _) => ClearEditor();
        save.Click += (_, _) => SaveCurrent();
        delete.Click += (_, _) => DeleteCurrent();
        deleteAll.Click += (_, _) => DeleteAll();
        import.Click += (_, _) => ImportExcel();
        close.Click += (_, _) => Close();
        grid.SelectionChanged += (_, _) => LoadSelected();
    }

    private static void AddField(TableLayoutPanel table, int col, int row, string caption, Control control)
    {
        var p = new Panel { Dock = DockStyle.Fill, Padding = new Padding(4) };
        var label = new Label { Text = caption, Dock = DockStyle.Right, Width = 135, TextAlign = ContentAlignment.MiddleRight };
        control.Dock = DockStyle.Fill;
        p.Controls.Add(control); p.Controls.Add(label); table.Controls.Add(p, col, row);
    }

    private static void SetupCombo(ComboBox combo, params string[] items)
    {
        combo.Dock = DockStyle.Fill; combo.DropDownStyle = ComboBoxStyle.DropDown; combo.Items.AddRange(items);
    }

    private static void SetupButton(Button button, string text)
    {
        button.Text = text; button.AutoSize = true; button.Height = 32; button.Margin = new Padding(4);
    }

    private void AddColumn(string header, float weight)
    {
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = header, FillWeight = weight, SortMode = DataGridViewColumnSortMode.NotSortable });
    }

    private void LoadGrid(string? selectSymbol = null)
    {
        loading = true;
        try
        {
            grid.Rows.Clear();
            foreach (var item in SymbolDefinitionStore.Load().OrderBy(x => x.SymbolTitle, StringComparer.OrdinalIgnoreCase))
            {
                int r = grid.Rows.Add(item.SymbolTitle, item.Name, item.ExchangeTitle, item.MarketType, item.BoardType, item.AssetType, item.IndustryGroupOrFundType);
                grid.Rows[r].Tag = item;
            }
            count.Text = $"تعداد: {grid.Rows.Count}";
            if (!string.IsNullOrWhiteSpace(selectSymbol))
                foreach (DataGridViewRow r in grid.Rows)
                    if (string.Equals(Convert.ToString(r.Cells[0].Value), selectSymbol, StringComparison.OrdinalIgnoreCase)) { r.Selected = true; break; }
        }
        finally { loading = false; }
    }

    private void LoadSelected()
    {
        if (loading || grid.SelectedRows.Count == 0 || grid.SelectedRows[0].Tag is not SymbolDefinition x) return;
        symbol.Text = x.SymbolTitle; name.Text = x.Name; exchange.Text = x.ExchangeTitle;
        market.Text = x.MarketType; board.Text = x.BoardType; asset.SelectedItem = x.AssetType; group.Text = x.IndustryGroupOrFundType;
    }

    private void ClearEditor()
    {
        grid.ClearSelection(); symbol.Clear(); name.Clear(); exchange.Clear(); market.Text = ""; board.Text = ""; asset.SelectedIndex = -1; group.Clear(); symbol.Focus();
    }

    private SymbolDefinition? ReadEditor()
    {
        if (string.IsNullOrWhiteSpace(symbol.Text)) { MessageBox.Show(this, "عنوان نماد را وارد کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning); symbol.Focus(); return null; }
        if (string.IsNullOrWhiteSpace(asset.Text)) { MessageBox.Show(this, "نوع دارایی را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning); asset.Focus(); return null; }
        return new SymbolDefinition { SymbolTitle = symbol.Text.Trim(), Name = name.Text.Trim(), ExchangeTitle = exchange.Text.Trim(), MarketType = market.Text.Trim(), BoardType = board.Text.Trim(), AssetType = asset.Text.Trim(), IndustryGroupOrFundType = group.Text.Trim() };
    }

    private void SaveCurrent()
    {
        var item = ReadEditor(); if (item == null) return;
        var all = SymbolDefinitionStore.Load();
        var existing = all.FirstOrDefault(x => string.Equals(x.SymbolTitle.Trim(), item.SymbolTitle, StringComparison.OrdinalIgnoreCase));
        if (existing != null && grid.SelectedRows.Count == 0)
        {
            MessageBox.Show(this, "این نماد قبلاً ثبت شده است. برای ویرایش، ابتدا نماد را از جدول انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
        }
        if (existing == null) all.Add(item);
        else
        {
            existing.SymbolTitle = item.SymbolTitle; existing.Name = item.Name; existing.ExchangeTitle = item.ExchangeTitle;
            existing.MarketType = item.MarketType; existing.BoardType = item.BoardType; existing.AssetType = item.AssetType; existing.IndustryGroupOrFundType = item.IndustryGroupOrFundType;
        }
        SymbolDefinitionStore.Save(all); LoadGrid(item.SymbolTitle);
    }

    private void DeleteCurrent()
    {
        var s = symbol.Text.Trim(); if (string.IsNullOrWhiteSpace(s)) return;
        if (MessageBox.Show(this, $"آیا نماد «{s}» حذف شود؟", "حذف نماد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        var all = SymbolDefinitionStore.Load(); all.RemoveAll(x => string.Equals(x.SymbolTitle.Trim(), s, StringComparison.OrdinalIgnoreCase));
        SymbolDefinitionStore.Save(all); LoadGrid(); ClearEditor();
    }

    private void DeleteAll()
    {
        var all = SymbolDefinitionStore.Load(); if (all.Count == 0) return;
        if (MessageBox.Show(this, $"هر {all.Count} نماد ثبت‌شده حذف می‌شود. آیا مطمئن هستید؟", "حذف همه نمادها", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        SymbolDefinitionStore.Save(Array.Empty<SymbolDefinition>()); LoadGrid(); ClearEditor();
    }

    private void ImportExcel()
    {
        using var d = new OpenFileDialog { Title = "انتخاب فایل Excel نمادها", Filter = "Excel (*.xlsx)|*.xlsx", CheckFileExists = true };
        if (d.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            var imported = ExcelSymbolReader.Read(d.FileName);
            if (imported.Count == 0) { MessageBox.Show(this, "هیچ ردیف قابل استفاده‌ای پیدا نشد.", "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            var map = SymbolDefinitionStore.Load().ToDictionary(x => x.SymbolTitle.Trim(), StringComparer.OrdinalIgnoreCase);
            int added = 0, updated = 0;
            foreach (var x in imported) { if (map.ContainsKey(x.SymbolTitle.Trim())) updated++; else added++; map[x.SymbolTitle.Trim()] = x; }
            SymbolDefinitionStore.Save(map.Values); LoadGrid();
            MessageBox.Show(this, $"ورود انجام شد.\nجدید: {added}\nبه‌روزشده: {updated}", "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex) { MessageBox.Show(this, $"خواندن فایل Excel انجام نشد:\n{ex.Message}", "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
            var symbol = V(Headers[0]); if (string.IsNullOrWhiteSpace(symbol)) continue;
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
            var reference = (string?)c.Attribute("r") ?? ""; int col = ColumnIndex(reference);
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
        int n = 0; foreach (char c in reference) { if (!char.IsLetter(c)) break; n = n * 26 + char.ToUpperInvariant(c) - 'A' + 1; } return n - 1;
    }

    private static List<string> SharedStrings(ZipArchive zip, XNamespace s)
    {
        var e = zip.GetEntry("xl/sharedStrings.xml"); if (e == null) return new();
        using var stream = e.Open(); var doc = XDocument.Load(stream);
        return doc.Root?.Elements(s + "si").Select(x => string.Concat(x.Descendants(s + "t").Select(t => t.Value))).ToList() ?? new();
    }

    private static Stream Entry(ZipArchive zip, string path) => (zip.GetEntry(path) ?? throw new InvalidDataException($"فایل داخلی Excel پیدا نشد: {path}")).Open();
}
