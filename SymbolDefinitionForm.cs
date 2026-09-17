using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Trade.It;

public sealed class SymbolDefinition
{
    public string SymbolTitle { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ExchangeTitle { get; set; } = string.Empty;
    public string MarketType { get; set; } = string.Empty;
    public string BoardType { get; set; } = string.Empty;
    public string AssetType { get; set; } = string.Empty;
    public string IndustryGroupOrFundType { get; set; } = string.Empty;
}

public static class SymbolDefinitionStore
{
    private static readonly object SyncRoot = new();
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    private static string FilePath => Path.Combine(AppContext.BaseDirectory, "Data", "Symbols.json");

    public static List<SymbolDefinition> Load()
    {
        lock (SyncRoot)
        {
            try
            {
                if (!File.Exists(FilePath))
                    return new List<SymbolDefinition>();

                var json = File.ReadAllText(FilePath, Encoding.UTF8);
                return JsonSerializer.Deserialize<List<SymbolDefinition>>(json, JsonOptions)
                    ?? new List<SymbolDefinition>();
            }
            catch
            {
                return new List<SymbolDefinition>();
            }
        }
    }

    public static void Save(IEnumerable<SymbolDefinition> definitions)
    {
        lock (SyncRoot)
        {
            var directory = Path.GetDirectoryName(FilePath)!;
            Directory.CreateDirectory(directory);
            var json = JsonSerializer.Serialize(definitions
                .OrderBy(x => x.SymbolTitle, StringComparer.OrdinalIgnoreCase)
                .ToList(), JsonOptions);
            File.WriteAllText(FilePath, json, new UTF8Encoding(false));
        }
    }

    public static void SaveOrUpdate(SymbolDefinition definition)
    {
        var all = Load();
        var existing = all.FirstOrDefault(x => string.Equals(x.SymbolTitle.Trim(), definition.SymbolTitle.Trim(), StringComparison.OrdinalIgnoreCase));
        if (existing == null)
            all.Add(definition);
        else
        {
            existing.Name = definition.Name;
            existing.ExchangeTitle = definition.ExchangeTitle;
            existing.MarketType = definition.MarketType;
            existing.BoardType = definition.BoardType;
            existing.AssetType = definition.AssetType;
            existing.IndustryGroupOrFundType = definition.IndustryGroupOrFundType;
        }
        Save(all);
    }
}

public sealed class SymbolDefinitionForm : Form
{
    private readonly TextBox symbolTextBox = new();
    private readonly TextBox nameTextBox = new();
    private readonly TextBox exchangeTextBox = new();
    private readonly ComboBox marketTypeComboBox = new();
    private readonly ComboBox boardTypeComboBox = new();
    private readonly ComboBox assetTypeComboBox = new();
    private readonly TextBox industryGroupTextBox = new();
    private readonly DataGridView symbolsGrid = new();
    private readonly Label countLabel = new();

    private readonly Button newButton = new();
    private readonly Button saveButton = new();
    private readonly Button deleteButton = new();
    private readonly Button deleteAllButton = new();
    private readonly Button importButton = new();
    private readonly Button closeButton = new();

    private bool loadingGrid;

    public SymbolDefinitionForm()
    {
        Text = "تعریف نمادها";
        StartPosition = FormStartPosition.CenterParent;
        Width = 1180;
        Height = 720;
        MinimumSize = new Size(950, 600);
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        Font = new Font("Tahoma", 9F);

        BuildUi();
        LoadGrid();
        ClearEditor();
    }

    private void BuildUi()
    {
        var main = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(10)
        };
        main.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));
        main.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        Controls.Add(main);

        var fields = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 2,
            Padding = new Padding(5)
        };
        for (var i = 0; i < 4; i++)
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        fields.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        fields.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        main.Controls.Add(fields, 0, 0);

        AddField(fields, 0, 0, "عنوان نماد", symbolTextBox);
        AddField(fields, 1, 0, "نام نماد", nameTextBox);
        AddField(fields, 2, 0, "عنوان بورس", exchangeTextBox);
        AddField(fields, 3, 0, "نوع بازار", marketTypeComboBox);
        AddField(fields, 0, 1, "نوع تابلو", boardTypeComboBox);
        AddField(fields, 1, 1, "نوع دارایی", assetTypeComboBox);
        AddField(fields, 2, 1, "گروه صنعت / نوع صندوق", industryGroupTextBox);

        ConfigureEditableCombo(marketTypeComboBox, "بورس تهران", "فرابورس ایران", "بورس کالا", "بورس انرژی");
        ConfigureEditableCombo(boardTypeComboBox, "تابلوی اصلی", "تابلوی فرعی", "بازار اول", "بازار دوم", "پایه زرد", "پایه نارنجی", "پایه قرمز");
        ConfigureEditableCombo(assetTypeComboBox, "سهام", "صندوق");
        assetTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        symbolTextBox.CharacterCasing = CharacterCasing.Normal;
        symbolTextBox.TextAlign = HorizontalAlignment.Right;
        nameTextBox.TextAlign = HorizontalAlignment.Right;
        exchangeTextBox.TextAlign = HorizontalAlignment.Right;
        industryGroupTextBox.TextAlign = HorizontalAlignment.Right;

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false,
            Padding = new Padding(5)
        };
        main.Controls.Add(buttons, 0, 1);

        ConfigureButton(newButton, "جدید");
        ConfigureButton(saveButton, "ذخیره");
        ConfigureButton(deleteButton, "حذف");
        ConfigureButton(deleteAllButton, "حذف همه");
        ConfigureButton(importButton, "ورود از Excel");
        ConfigureButton(closeButton, "بستن");
        buttons.Controls.AddRange(new Control[] { newButton, saveButton, deleteButton, deleteAllButton, importButton, closeButton, countLabel });

        countLabel.AutoSize = true;
        countLabel.Margin = new Padding(20, 9, 10, 3);
        countLabel.Text = "تعداد: 0";

        symbolsGrid.Dock = DockStyle.Fill;
        symbolsGrid.AllowUserToAddRows = false;
        symbolsGrid.AllowUserToDeleteRows = false;
        symbolsGrid.AllowUserToResizeRows = false;
        symbolsGrid.ReadOnly = true;
        symbolsGrid.MultiSelect = false;
        symbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        symbolsGrid.AutoGenerateColumns = false;
        symbolsGrid.RowHeadersVisible = false;
        symbolsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        symbolsGrid.RightToLeft = RightToLeft.Yes;
        symbolsGrid.Columns.Add(Column("SymbolTitle", "عنوان نماد", 1.0));
        symbolsGrid.Columns.Add(Column("Name", "نام نماد", 1.5));
        symbolsGrid.Columns.Add(Column("ExchangeTitle", "عنوان بورس", 1.0));
        symbolsGrid.Columns.Add(Column("MarketType", "نوع بازار", 1.0));
        symbolsGrid.Columns.Add(Column("BoardType", "نوع تابلو", 1.0));
        symbolsGrid.Columns.Add(Column("AssetType", "نوع دارایی", 0.8));
        symbolsGrid.Columns.Add(Column("IndustryGroupOrFundType", "گروه صنعت / نوع صندوق", 1.5));
        main.Controls.Add(symbolsGrid, 0, 2);

        newButton.Click += (_, _) => ClearEditor();
        saveButton.Click += SaveCurrent;
        deleteButton.Click += DeleteCurrent;
        deleteAllButton.Click += DeleteAll;
        importButton.Click += ImportExcel;
        closeButton.Click += (_, _) => Close();
        symbolsGrid.SelectionChanged += GridSelectionChanged;
        symbolsGrid.CellDoubleClick += (_, _) => LoadSelectedIntoEditor();
    }

    private static void ConfigureButton(Button button, string text)
    {
        button.Text = text;
        button.AutoSize = true;
        button.Height = 32;
        button.Margin = new Padding(4);
    }

    private static void ConfigureEditableCombo(ComboBox combo, params string[] values)
    {
        combo.Dock = DockStyle.Fill;
        combo.DropDownStyle = ComboBoxStyle.DropDown;
        combo.Items.AddRange(values);
    }

    private static void AddField(TableLayoutPanel table, int column, int row, string caption, Control control)
    {
        var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
        var label = new Label
        {
            Text = caption,
            Dock = DockStyle.Right,
            Width = 135,
            TextAlign = ContentAlignment.MiddleRight
        };
        control.Dock = DockStyle.Fill;
        panel.Controls.Add(control);
        panel.Controls.Add(label);
        table.Controls.Add(panel, column, row);
    }

    private static DataGridViewTextBoxColumn Column(string property, string header, float weight) => new()
    {
        DataPropertyName = property,
        HeaderText = header,
        FillWeight = weight,
        SortMode = DataGridViewColumnSortMode.NotSortable
    };

    private void LoadGrid(string? selectSymbol = null)
    {
        loadingGrid = true;
        try
        {
            symbolsGrid.Rows.Clear();
            foreach (var item in SymbolDefinitionStore.Load().OrderBy(x => x.SymbolTitle, StringComparer.OrdinalIgnoreCase))
            {
                var row = symbolsGrid.Rows.Add(item.SymbolTitle, item.Name, item.ExchangeTitle, item.MarketType,
                    item.BoardType, item.AssetType, item.IndustryGroupOrFundType);
                symbolsGrid.Rows[row].Tag = item;
            }
            countLabel.Text = $"تعداد: {symbolsGrid.Rows.Count}";

            if (!string.IsNullOrWhiteSpace(selectSymbol))
            {
                foreach (DataGridViewRow row in symbolsGrid.Rows)
                {
                    if (string.Equals(Convert.ToString(row.Cells[0].Value), selectSymbol, StringComparison.OrdinalIgnoreCase))
                    {
                        row.Selected = true;
                        symbolsGrid.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
        }
        finally
        {
            loadingGrid = false;
        }
    }

    private void GridSelectionChanged(object? sender, EventArgs e)
    {
        if (loadingGrid || symbolsGrid.SelectedRows.Count == 0)
            return;
        LoadSelectedIntoEditor();
    }

    private void LoadSelectedIntoEditor()
    {
        if (symbolsGrid.SelectedRows.Count == 0)
            return;
        if (symbolsGrid.SelectedRows[0].Tag is not SymbolDefinition item)
            return;

        symbolTextBox.Text = item.SymbolTitle;
        nameTextBox.Text = item.Name;
        exchangeTextBox.Text = item.ExchangeTitle;
        marketTypeComboBox.Text = item.MarketType;
        boardTypeComboBox.Text = item.BoardType;
        assetTypeComboBox.SelectedItem = item.AssetType;
        industryGroupTextBox.Text = item.IndustryGroupOrFundType;
    }

    private void ClearEditor()
    {
        symbolsGrid.ClearSelection();
        symbolTextBox.Clear();
        nameTextBox.Clear();
        exchangeTextBox.Clear();
        marketTypeComboBox.SelectedIndex = -1;
        marketTypeComboBox.Text = string.Empty;
        boardTypeComboBox.SelectedIndex = -1;
        boardTypeComboBox.Text = string.Empty;
        assetTypeComboBox.SelectedIndex = -1;
        industryGroupTextBox.Clear();
        symbolTextBox.Focus();
    }

    private void SaveCurrent(object? sender, EventArgs e)
    {
        var item = ReadEditor();
        if (item == null)
            return;

        var all = SymbolDefinitionStore.Load();
        var duplicate = all.FirstOrDefault(x => string.Equals(x.SymbolTitle.Trim(), item.SymbolTitle.Trim(), StringComparison.OrdinalIgnoreCase));
        var selected = symbolsGrid.SelectedRows.Count > 0 ? symbolsGrid.SelectedRows[0].Tag as SymbolDefinition : null;

        if (duplicate != null && !ReferenceEquals(duplicate, selected))
        {
            // If the user is editing a selected row, the same symbol is an update; otherwise warn about duplicate symbol.
            if (selected == null || !string.Equals(selected.SymbolTitle, item.SymbolTitle, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(this, "این نماد قبلاً ثبت شده است.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        SymbolDefinitionStore.SaveOrUpdate(item);
        LoadGrid(item.SymbolTitle);
    }

    private SymbolDefinition? ReadEditor()
    {
        var symbol = symbolTextBox.Text.Trim();
        var asset = assetTypeComboBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(symbol))
        {
            MessageBox.Show(this, "عنوان نماد را وارد کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            symbolTextBox.Focus();
            return null;
        }
        if (string.IsNullOrWhiteSpace(asset))
        {
            MessageBox.Show(this, "نوع دارایی را انتخاب کنید.", "تعریف نمادها", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            assetTypeComboBox.Focus();
            return null;
        }

        return new SymbolDefinition
        {
            SymbolTitle = symbol,
            Name = nameTextBox.Text.Trim(),
            ExchangeTitle = exchangeTextBox.Text.Trim(),
            MarketType = marketTypeComboBox.Text.Trim(),
            BoardType = boardTypeComboBox.Text.Trim(),
            AssetType = asset,
            IndustryGroupOrFundType = industryGroupTextBox.Text.Trim()
        };
    }

    private void DeleteCurrent(object? sender, EventArgs e)
    {
        var symbol = symbolTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(symbol))
            return;

        if (MessageBox.Show(this, $"آیا نماد «{symbol}» حذف شود؟", "حذف نماد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        var all = SymbolDefinitionStore.Load();
        all.RemoveAll(x => string.Equals(x.SymbolTitle.Trim(), symbol, StringComparison.OrdinalIgnoreCase));
        SymbolDefinitionStore.Save(all);
        LoadGrid();
        ClearEditor();
    }

    private void DeleteAll(object? sender, EventArgs e)
    {
        var all = SymbolDefinitionStore.Load();
        if (all.Count == 0)
            return;

        if (MessageBox.Show(this, $"هر {all.Count} نماد ثبت‌شده حذف می‌شود. آیا مطمئن هستید؟", "حذف همه نمادها", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        SymbolDefinitionStore.Save(Array.Empty<SymbolDefinition>());
        LoadGrid();
        ClearEditor();
    }

    private void ImportExcel(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "انتخاب فایل Excel نمادها",
            Filter = "Excel (*.xlsx)|*.xlsx",
            CheckFileExists = true,
            Multiselect = false
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var imported = ExcelSymbolReader.Read(dialog.FileName);
            if (imported.Count == 0)
            {
                MessageBox.Show(this, "هیچ ردیف قابل استفاده‌ای در فایل پیدا نشد.", "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var all = SymbolDefinitionStore.Load();
            var map = all.ToDictionary(x => x.SymbolTitle.Trim(), StringComparer.OrdinalIgnoreCase);
            var added = 0;
            var updated = 0;

            foreach (var item in imported)
            {
                if (map.ContainsKey(item.SymbolTitle.Trim()))
                {
                    map[item.SymbolTitle.Trim()] = item;
                    updated++;
                }
                else
                {
                    map[item.SymbolTitle.Trim()] = item;
                    added++;
                }
            }

            SymbolDefinitionStore.Save(map.Values);
            LoadGrid();
            MessageBox.Show(this, $"ورود اطلاعات انجام شد.\nجدید: {added}\nبه‌روزشده: {updated}", "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"خواندن فایل Excel انجام نشد:\n{ex.Message}", "ورود از Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

internal static class ExcelSymbolReader
{
    private static readonly string[] RequiredHeaders =
    {
        "عنوان نماد", "نام نماد", "عنوان بورس", "نوع بازار", "نوع تابلو", "نوع دارایی (سهام یا صندوق)", "گروه صنعت یا نوع صندوق"
    };

    public static List<SymbolDefinition> Read(string path)
    {
        using var archive = ZipFile.OpenRead(path);
        var workbook = XDocument.Load(OpenEntry(archive, "xl/workbook.xml"));
        var rels = XDocument.Load(OpenEntry(archive, "xl/_rels/workbook.xml.rels"));
        XNamespace main = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        XNamespace rel = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        XNamespace pkgRel = "http://schemas.openxmlformats.org/package/2006/relationships";

        var firstSheet = workbook.Root?.Element(main + "sheets")?.Element(main + "sheet")
            ?? throw new InvalidDataException("هیچ Sheetای در فایل Excel پیدا نشد.");
        var relationshipId = (string?)firstSheet.Attribute(rel + "id")
            ?? throw new InvalidDataException("Sheet اول فاقد شناسه رابطه است.");
        var relationship = rels.Root?.Elements(pkgRel + "Relationship")
            .FirstOrDefault(x => string.Equals((string?)x.Attribute("Id"), relationshipId, StringComparison.Ordinal));
        var target = (string?)relationship?.Attribute("Target")
            ?? throw new InvalidDataException("مسیر Sheet اول پیدا نشد.");
        var sheetPath = target.StartsWith("/") ? target.TrimStart('/') : "xl/" + target.TrimStart('/');
        sheetPath = sheetPath.Replace("xl/xl/", "xl/", StringComparison.Ordinal);

        var sharedStrings = ReadSharedStrings(archive, main);
        var sheet = XDocument.Load(OpenEntry(archive, sheetPath));
        var rows = sheet.Root?.Element(main + "sheetData")?.Elements(main + "row").ToList()
            ?? new List<XElement>();
        if (rows.Count == 0)
            return new List<SymbolDefinition>();

        var headerCells = ReadRow(rows[0], main, sharedStrings);
        var headers = headerCells.ToDictionary(x => Normalize(x.Key), x => x.Value, StringComparer.OrdinalIgnoreCase);
        var indexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (var required in RequiredHeaders)
        {
            var match = headers.FirstOrDefault(x => HeaderMatches(x.Key, required));
            if (string.IsNullOrEmpty(match.Key))
                throw new InvalidDataException($"ستون «{required}» در فایل پیدا نشد.");
            indexes[required] = match.Value;
        }

        var result = new List<SymbolDefinition>();
        foreach (var row in rows.Skip(1))
        {
            var cells = ReadRow(row, main, sharedStrings);
            string Value(string header) => cells.TryGetValue(indexes[header].ToString(), out var value) ? value : string.Empty;

            // ReadRow keys are numeric indexes represented as strings.
            var symbol = Value(RequiredHeaders[0]).Trim();
            if (string.IsNullOrWhiteSpace(symbol))
                continue;

            result.Add(new SymbolDefinition
            {
                SymbolTitle = symbol,
                Name = Value(RequiredHeaders[1]).Trim(),
                ExchangeTitle = Value(RequiredHeaders[2]).Trim(),
                MarketType = Value(RequiredHeaders[3]).Trim(),
                BoardType = Value(RequiredHeaders[4]).Trim(),
                AssetType = Value(RequiredHeaders[5]).Trim(),
                IndustryGroupOrFundType = Value(RequiredHeaders[6]).Trim()
            });
        }
        return result;
    }

    private static bool HeaderMatches(string normalizedActual, string expected)
    {
        var e = Normalize(expected);
        return normalizedActual == e || normalizedActual.Replace(" ", "") == e.Replace(" ", "");
    }

    private static string Normalize(string value) => value.Replace("\u200c", "").Replace("ي", "ی").Replace("ك", "ک").Trim();

    private static Dictionary<string, string> ReadRow(XElement row, XNamespace main, IReadOnlyList<string> sharedStrings)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var cell in row.Elements(main + "c"))
        {
            var reference = (string?)cell.Attribute("r") ?? string.Empty;
            var index = ColumnIndex(reference);
            var value = cell.Element(main + "v")?.Value ?? string.Empty;
            if (string.Equals((string?)cell.Attribute("t"), "s", StringComparison.OrdinalIgnoreCase) && int.TryParse(value, out var sharedIndex) && sharedIndex >= 0 && sharedIndex < sharedStrings.Count)
                value = sharedStrings[sharedIndex];
            else if (string.Equals((string?)cell.Attribute("t"), "inlineStr", StringComparison.OrdinalIgnoreCase))
                value = cell.Element(main + "is")?.Value ?? string.Empty;
            result[index.ToString()] = value;
        }
        return result;
    }

    private static int ColumnIndex(string reference)
    {
        var index = 0;
        foreach (var ch in reference)
        {
            if (!char.IsLetter(ch))
                break;
            index = index * 26 + char.ToUpperInvariant(ch) - 'A' + 1;
        }
        return index - 1;
    }

    private static List<string> ReadSharedStrings(ZipArchive archive, XNamespace main)
    {
        var entry = archive.GetEntry("xl/sharedStrings.xml");
        if (entry == null)
            return new List<string>();
        var document = XDocument.Load(entry.Open());
        return document.Root?.Elements(main + "si")
            .Select(x => string.Concat(x.Descendants(main + "t").Select(t => t.Value)))
            .ToList() ?? new List<string>();
    }

    private static Stream OpenEntry(ZipArchive archive, string path)
    {
        var entry = archive.GetEntry(path) ?? throw new InvalidDataException($"فایل داخلی Excel پیدا نشد: {path}");
        return entry.Open();
    }
}
