using System.Text.Json;

namespace Trade.It
{
    public partial class PortfolioDefinitionForm
    {
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Replace the original save handler only for the displayed form instance.
            // The existing save implementation remains responsible for creating the portfolio.
            saveButton.Click -= SaveButton_Click;
            saveButton.Click += SaveButtonWithDuplicateCheck_Click;
        }

        private void SaveButtonWithDuplicateCheck_Click(object? sender, EventArgs e)
        {
            var portfolioName = portfolioNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(portfolioName))
            {
                SaveButton_Click(sender, e);
                return;
            }

            var folder = Path.Combine(AppContext.BaseDirectory, "Portfolios");
            Directory.CreateDirectory(folder);

            var safeName = string.Concat(portfolioName.Select(c =>
                Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
            var targetFile = Path.Combine(folder, safeName + ".json");

            if (IsDuplicatePortfolioName(folder, portfolioName) || File.Exists(targetFile))
            {
                MessageBox.Show(this,
                    $"سبدی با نام «{portfolioName}» قبلاً وجود دارد.\nلطفاً نام دیگری انتخاب کنید.",
                    "نام سبد تکراری",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Let the existing validated save operation do the actual creation.
            SaveButton_Click(sender, e);

            // The original handler catches its own save exceptions. A newly created file
            // whose stored name matches the requested name is therefore our success signal.
            if (File.Exists(targetFile) && SavedPortfolioHasName(targetFile, portfolioName))
                ResetButton_Click(this, EventArgs.Empty);
        }

        private static bool IsDuplicatePortfolioName(string folder, string portfolioName)
        {
            if (!Directory.Exists(folder))
                return false;

            foreach (var file in Directory.EnumerateFiles(folder, "*.json", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var definition = JsonSerializer.Deserialize<PortfolioDefinition>(json);
                    if (definition != null &&
                        string.Equals(definition.Name?.Trim(), portfolioName, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                catch
                {
                    // Ignore invalid portfolio files; they are handled by portfolio management.
                }
            }

            return false;
        }

        private static bool SavedPortfolioHasName(string file, string portfolioName)
        {
            try
            {
                var json = File.ReadAllText(file);
                var definition = JsonSerializer.Deserialize<PortfolioDefinition>(json);
                return definition != null &&
                       string.Equals(definition.Name?.Trim(), portfolioName, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}