from pathlib import Path
import subprocess

BASE = "72e13936ff75ca67475b33e45df76b332fbb3199"
for name in ("MainForm.cs", "MainForm.Designer.cs"):
    content = subprocess.check_output(["git", "show", f"{BASE}:{name}"], text=True)
    Path(name).write_text(content, encoding="utf-8")

# ----- PortfolioManagementForm.Designer.cs -----
d = Path("PortfolioManagementForm.Designer.cs")
s = d.read_text(encoding="utf-8")

fields = """        private GroupBox previewGroup;\n        private DataGridView previewGrid;\n""" + "".join(f"        private DataGridViewTextBoxColumn previewColumn{i};\n" for i in range(1, 19))
if "private GroupBox previewGroup;" not in s:
    s = s.replace("        private Button closeButton;\n", "        private Button closeButton;\n" + fields)

init = """            previewGroup = new GroupBox();
            previewGrid = new DataGridView();
""" + "".join(f"            previewColumn{i} = new DataGridViewTextBoxColumn();\n" for i in range(1, 19))
if "previewGroup = new GroupBox();" not in s:
    s = s.replace("            closeButton = new Button();\n", "            closeButton = new Button();\n" + init)

if "((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();" not in s:
    s = s.replace("            ((System.ComponentModel.ISupportInitialize)symbolsGrid).BeginInit();\n", "            ((System.ComponentModel.ISupportInitialize)symbolsGrid).BeginInit();\n            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();\n")

s = s.replace("symbolsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;", "symbolsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Left;")
s = s.replace("symbolsGrid.Size = new Size(1368, 373);", "symbolsGrid.Size = new Size(1368, 250);")

if "previewGroup.Controls.Add(previewGrid);" not in s:
    block = """            // 
            // previewGroup
            // 
            previewGroup.Controls.Add(previewGrid);
            previewGroup.Location = new Point(20, 650);
            previewGroup.Name = "previewGroup";
            previewGroup.Size = new Size(1368, 270);
            previewGroup.TabIndex = 34;
            previewGroup.TabStop = false;
            previewGroup.Text = "پیش نمایش";
            // 
            // previewGrid
            // 
            previewGrid.AllowUserToAddRows = false;
            previewGrid.AllowUserToDeleteRows = false;
            previewGrid.BackgroundColor = SystemColors.Window;
            previewGrid.ColumnHeadersHeight = 34;
            previewGrid.Columns.AddRange(new DataGridViewColumn[]
            {
""" + "                " + ", ".join(f"previewColumn{i}" for i in range(1, 19)) + ",\n"""            });
            previewGrid.Dock = DockStyle.Fill;
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RightToLeft = RightToLeft.Yes;
            previewGrid.RowHeadersVisible = false;
            previewGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            previewGrid.TabIndex = 0;
""" + "".join(f"            previewColumn{i}.HeaderText = \"ستون {i}\";\n            previewColumn{i}.Name = \"previewColumn{i}\";\n            previewColumn{i}.ReadOnly = true;\n            previewColumn{i}.Width = 120;\n" for i in range(1, 19))
    s = s.replace("            // \n            // deleteSymbolsButton\n", block + "            // \n            // deleteSymbolsButton\n")

s = s.replace("ClientSize = new Size(1400, 820);", "ClientSize = new Size(1400, 1000);")
s = s.replace("deleteSymbolsButton.Location = new Point(1263, 766);", "deleteSymbolsButton.Location = new Point(1263, 936);")
s = s.replace("reloadButton.Location = new Point(581, 769);", "reloadButton.Location = new Point(581, 939);")
s = s.replace("closeButton.Location = new Point(715, 769);", "closeButton.Location = new Point(715, 939);")
if "            Controls.Add(previewGroup);\n" not in s:
    s = s.replace("            Controls.Add(symbolsGrid);\n", "            Controls.Add(symbolsGrid);\n            Controls.Add(previewGroup);\n")
if "((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();" not in s:
    s = s.replace("            ((System.ComponentModel.ISupportInitialize)symbolsGrid).EndInit();\n", "            ((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();\n            ((System.ComponentModel.ISupportInitialize)symbolsGrid).EndInit();\n")
d.write_text(s, encoding="utf-8")

# ----- PortfolioManagementForm.cs -----
c = Path("PortfolioManagementForm.cs")
s = c.read_text(encoding="utf-8")
if "symbolsGrid.CellClick += SymbolsGrid_CellClick;" not in s:
    s = s.replace("            symbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;\n", "            symbolsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;\n            symbolsGrid.CellClick += SymbolsGrid_CellClick;\n")

method = r'''
        private void SymbolsGrid_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= symbolsGrid.Rows.Count)
                return;

            var symbol = Convert.ToString(symbolsGrid.Rows[e.RowIndex].Cells["symbolNameColumn"].Value)?.Trim();
            if (string.IsNullOrWhiteSpace(symbol))
                return;

            var portfolioName = portfolioNameLabel.Text.Trim();
            if (string.IsNullOrWhiteSpace(portfolioName) || portfolioName == "—" ||
                !portfolioFiles.TryGetValue(portfolioName, out var portfolioFile) || !File.Exists(portfolioFile))
            {
                ClearPreview();
                return;
            }

            try
            {
                var definition = JsonSerializer.Deserialize<PortfolioDefinition>(File.ReadAllText(portfolioFile));
                if (definition == null)
                {
                    ClearPreview();
                    return;
                }

                var dataFiles = GetPortfolioDataFiles(definition).ToList();
                string? matchingFile;
                if (definition.SymbolSource == SymbolSource.FileName)
                {
                    matchingFile = dataFiles.FirstOrDefault(f => string.Equals(
                        Path.GetFileNameWithoutExtension(f), symbol, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    matchingFile = dataFiles.FirstOrDefault(f => FileContainsSymbol(f, definition, symbol));
                }

                if (matchingFile == null)
                {
                    ClearPreview();
                    return;
                }

                LoadSymbolPreview(matchingFile, definition, symbol);
            }
            catch (Exception ex)
            {
                ClearPreview();
                MessageBox.Show(this, $"نمایش اطلاعات نماد انجام نشد:\n{ex.Message}", "پیش نمایش", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IEnumerable<string> GetPortfolioDataFiles(PortfolioDefinition definition)
        {
            if (!Directory.Exists(definition.DataPath))
                yield break;

            var extension = definition.FileType?.Trim().ToUpperInvariant() switch
            {
                "CSV" => ".csv",
                "PRN" => ".prn",
                _ => ".txt"
            };

            foreach (var file in Directory.EnumerateFiles(definition.DataPath, "*" + extension, SearchOption.TopDirectoryOnly)
                         .OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase))
                yield return file;
        }

        private bool FileContainsSymbol(string filePath, PortfolioDefinition definition, string symbol)
        {
            var rows = File.ReadLines(filePath, DetectEncoding(filePath))
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => SplitLine(line, definition.Separator))
                .Take(101)
                .ToList();
            if (rows.Count == 0)
                return false;

            var header = definition.HasHeader ? rows[0] : null;
            var dataRows = rows.Skip(definition.HasHeader ? 1 : 0).ToList();
            var symbolColumn = GetSymbolColumn(definition, header);
            return symbolColumn > 0 && dataRows.Any(row => symbolColumn <= row.Length &&
                string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase));
        }

        private void LoadSymbolPreview(string filePath, PortfolioDefinition definition, string symbol)
        {
            var rows = File.ReadLines(filePath, DetectEncoding(filePath))
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => SplitLine(line, definition.Separator))
                .ToList();
            if (rows.Count == 0)
            {
                ClearPreview();
                return;
            }

            var header = definition.HasHeader ? rows[0] : null;
            var dataRows = rows.Skip(definition.HasHeader ? 1 : 0).ToList();
            var width = Math.Min(18, Math.Max(header?.Length ?? 0, dataRows.Count == 0 ? 0 : dataRows.Max(r => r.Length)));
            if (width <= 0)
            {
                ClearPreview();
                return;
            }

            var symbolColumn = GetSymbolColumn(definition, header);
            var selectedRows = definition.SymbolSource == SymbolSource.FileName || symbolColumn <= 0
                ? dataRows
                : dataRows.Where(row => symbolColumn <= row.Length &&
                    string.Equals(row[symbolColumn - 1].Trim(), symbol, StringComparison.OrdinalIgnoreCase)).ToList();

            previewGrid.Rows.Clear();
            for (var i = 0; i < 18; i++)
            {
                var title = header != null && i < header.Length && !string.IsNullOrWhiteSpace(header[i])
                    ? $"{i + 1}: {header[i].Trim()}"
                    : $"ستون {i + 1}";
                previewGrid.Columns[i].HeaderText = title;
                previewGrid.Columns[i].Visible = i < width;
            }

            foreach (var row in selectedRows)
            {
                var values = new object[18];
                for (var i = 0; i < 18; i++)
                    values[i] = i < row.Length ? row[i].Trim() : string.Empty;
                previewGrid.Rows.Add(values);
            }

            previewGroup.Text = selectedRows.Count == 0
                ? $"پیش نمایش — {symbol} (داده‌ای یافت نشد)"
                : $"پیش نمایش — {symbol}";
        }

        private int GetSymbolColumn(PortfolioDefinition definition, string[]? header)
        {
            var mapped = definition.Mappings?.FirstOrDefault(m => string.Equals(m.Field, "نماد", StringComparison.Ordinal));
            if (mapped != null && mapped.Column > 0)
                return mapped.Column;

            if (header != null)
            {
                for (var i = 0; i < header.Length; i++)
                {
                    var h = NormalizeHeader(header[i]);
                    if (h.Contains("tickerfa") || h.Contains("symbol") || h.Contains("ticker") || h.Contains("نماد"))
                        return i + 1;
                }
            }
            return 0;
        }

        private static Encoding DetectEncoding(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            Span<byte> bom = stackalloc byte[4];
            var read = stream.Read(bom);
            if (read >= 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF) return new UTF8Encoding(true);
            if (read >= 2 && bom[0] == 0xFF && bom[1] == 0xFE) return Encoding.Unicode;
            if (read >= 2 && bom[0] == 0xFE && bom[1] == 0xFF) return Encoding.BigEndianUnicode;
            return new UTF8Encoding(false);
        }

        private static string[] SplitLine(string line, string separator)
        {
            if (separator != ",")
                return line.Split(new[] { separator }, StringSplitOptions.None);

            var result = new List<string>();
            var current = new System.Text.StringBuilder();
            var quoted = false;
            for (var i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (c == '"')
                {
                    if (quoted && i + 1 < line.Length && line[i + 1] == '"') { current.Append('"'); i++; }
                    else quoted = !quoted;
                }
                else if (c == ',' && !quoted) { result.Add(current.ToString()); current.Clear(); }
                else current.Append(c);
            }
            result.Add(current.ToString());
            return result.ToArray();
        }

        private static string NormalizeHeader(string value) =>
            value.Trim().Trim('<', '>').Replace("_", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();

        private void ClearPreview()
        {
            previewGrid.Rows.Clear();
            for (var i = 0; i < previewGrid.Columns.Count; i++)
            {
                previewGrid.Columns[i].Visible = false;
                previewGrid.Columns[i].HeaderText = $"ستون {i + 1}";
            }
            previewGroup.Text = "پیش نمایش";
        }
'''
if "private void SymbolsGrid_CellClick" not in s:
    s = s.replace("        private void DeletePortfoliosButton_Click(object? sender, EventArgs e)\n", method + "\n        private void DeletePortfoliosButton_Click(object? sender, EventArgs e)\n")

# Make clearing details clear preview too.
s = s.replace("            symbolsGrid.Rows.Clear();\n        }\n\n        private static string GetSeparatorText", "            symbolsGrid.Rows.Clear();\n            ClearPreview();\n        }\n\n        private static string GetSeparatorText")
c.write_text(s, encoding="utf-8")
