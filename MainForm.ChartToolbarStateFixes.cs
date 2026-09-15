using System.Drawing;
using System.Windows.Forms;

namespace Trade.It
{
    public partial class MainForm
    {
        private bool chartToolbarStateFixesInitialized;
        private bool chartToolbarStateSyncing;
        private Button? closeAllChartsToolbarButton;

        private readonly Dictionary<string, ChartToolbarState> chartToolbarStates =
            new(StringComparer.OrdinalIgnoreCase);

        private string? pendingChartStateSymbol;
        private ChartToolbarState? pendingChartState;

        private sealed class ChartToolbarState
        {
            public TradingChartType ChartType { get; init; }
            public bool GridVisible { get; init; }
            public bool CrosshairVisible { get; init; }
            public bool Hidden { get; init; }
        }

        private void InitializeChartToolbarStateFixes()
        {
            if (chartToolbarStateFixesInitialized ||
                IsDisposed ||
                System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            chartToolbarStateFixesInitialized = true;

            if (mainMenuStrip.Items.Contains(closeAllChartsMenuItem))
                mainMenuStrip.Items.Remove(closeAllChartsMenuItem);

            closeAllChartsToolbarButton = new Button
            {
                Name = "closeAllChartsToolbarButton",
                Text = "بستن همه",
                Size = new Size(88, 34),
                Location = new Point(820, 9),
                RightToLeft = RightToLeft.Yes,
                TabIndex = 11,
                UseVisualStyleBackColor = true
            };
            closeAllChartsToolbarButton.Click += CloseAllChartsToolbarButton_Click;
            chartToolbarPanel.Controls.Add(closeAllChartsToolbarButton);
            closeAllChartsToolbarButton.BringToFront();

            hideToolsButton.MouseDown += HideToolsButton_ConfirmMouseDown;
            hideToolsButton.KeyDown += HideToolsButton_ConfirmKeyDown;

            chartTabControl.SelectedIndexChanged += ChartToolbarTabChanged;
            chartTypeComboBox.SelectedIndexChanged += ChartToolbarTypeChanged;
            gridButton.Click += ChartToolbarGridChanged;
            crossButton.Click += ChartToolbarCrossChanged;
            hideChartButton.Click += ChartToolbarHideChanged;

            stocksDataGridView.CellMouseDown += StocksDataGridView_CaptureChartState;
            stocksDataGridView.CellClick += StocksDataGridView_RestoreChartStateAfterOpen;

            SyncChartToolbarFromActiveChart();
        }

        private void CloseAllChartsToolbarButton_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show(
                this,
                "آیا از بستن همه چارت‌های باز مطمئن هستید؟",
                "بستن همه چارت‌ها",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            chartToolbarStates.Clear();
            pendingChartStateSymbol = null;
            pendingChartState = null;
            CloseAllChartTabs();
        }

        private void HideToolsButton_ConfirmMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (ConfirmDeleteDrawingTools())
                return;

            hideToolsButton.Capture = false;
            hideToolsButton.Enabled = false;
            BeginInvoke(new Action(() =>
            {
                if (!IsDisposed)
                    hideToolsButton.Enabled = true;
            }));
        }

        private void HideToolsButton_ConfirmKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Space)
                return;

            if (ConfirmDeleteDrawingTools())
                return;

            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private bool ConfirmDeleteDrawingTools()
        {
            return MessageBox.Show(
                this,
                "آیا از حذف همه ابزارهای رسم روی این چارت مطمئن هستید؟",
                "حذف ابزارها",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        private void ChartToolbarTabChanged(object? sender, EventArgs e)
        {
            if (TryRestorePendingChartState())
                return;

            SyncChartToolbarFromActiveChart();
        }

        private void ChartToolbarTypeChanged(object? sender, EventArgs e)
        {
            if (chartToolbarStateSyncing)
                return;

            RememberActiveChartToolbarState();
        }

        private void ChartToolbarGridChanged(object? sender, EventArgs e)
        {
            if (chartToolbarStateSyncing)
                return;

            RememberActiveChartToolbarState();
        }

        private void ChartToolbarCrossChanged(object? sender, EventArgs e)
        {
            if (chartToolbarStateSyncing)
                return;

            RememberActiveChartToolbarState();
        }

        private void ChartToolbarHideChanged(object? sender, EventArgs e)
        {
            if (chartToolbarStateSyncing)
                return;

            RememberActiveChartToolbarState();
        }

        private void StocksDataGridView_CaptureChartState(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != symbolColumn.Index)
                return;

            var symbol = Convert.ToString(
                stocksDataGridView.Rows[e.RowIndex].Cells[symbolColumn.Index].Value)?.Trim();

            if (string.IsNullOrWhiteSpace(symbol) ||
                !chartControls.TryGetValue(symbol, out var chart))
            {
                pendingChartStateSymbol = null;
                pendingChartState = null;
                return;
            }

            pendingChartStateSymbol = symbol;
            pendingChartState = GetChartToolbarState(symbol, chart);
        }

        private void StocksDataGridView_RestoreChartStateAfterOpen(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != symbolColumn.Index)
                return;

            var symbol = Convert.ToString(
                stocksDataGridView.Rows[e.RowIndex].Cells[symbolColumn.Index].Value)?.Trim();

            if (string.IsNullOrWhiteSpace(symbol))
                return;

            BeginInvoke(new Action(() =>
            {
                if (IsDisposed || !string.Equals(activeChartSymbol, symbol, StringComparison.OrdinalIgnoreCase))
                    return;

                if (string.Equals(pendingChartStateSymbol, symbol, StringComparison.OrdinalIgnoreCase) &&
                    pendingChartState != null)
                {
                    ApplyChartToolbarState(symbol, pendingChartState);
                    pendingChartStateSymbol = null;
                    pendingChartState = null;
                }
                else
                {
                    SyncChartToolbarFromActiveChart();
                }
            }));
        }

        private bool TryRestorePendingChartState()
        {
            if (string.IsNullOrWhiteSpace(activeChartSymbol) ||
                pendingChartState == null ||
                !string.Equals(pendingChartStateSymbol, activeChartSymbol, StringComparison.OrdinalIgnoreCase))
                return false;

            ApplyChartToolbarState(activeChartSymbol, pendingChartState);
            pendingChartStateSymbol = null;
            pendingChartState = null;
            return true;
        }

        private void SyncChartToolbarFromActiveChart()
        {
            var chart = GetActiveChart();
            if (chart == null)
            {
                chartToolbarStateSyncing = true;
                try
                {
                    SetToggleButtonState(gridButton, false);
                    SetToggleButtonState(crossButton, false);
                    SetToggleButtonState(hideChartButton, false);
                }
                finally
                {
                    chartToolbarStateSyncing = false;
                }
                return;
            }

            var symbol = GetActiveChartTabSymbol();
            var state = GetChartToolbarState(symbol, chart);

            chartToolbarStateSyncing = true;
            try
            {
                chart.SetChartType(state.ChartType);
                SetChartTypeComboBox(state.ChartType);
                SetToggleButtonState(gridButton, state.GridVisible);
                SetToggleButtonState(crossButton, state.CrosshairVisible);
                SetToggleButtonState(hideChartButton, state.Hidden);
            }
            finally
            {
                chartToolbarStateSyncing = false;
            }
        }

        private void ApplyChartToolbarState(string symbol, ChartToolbarState state)
        {
            if (!chartControls.TryGetValue(symbol, out var chart))
                return;

            if (chart.GridVisible != state.GridVisible)
                chart.ToggleGrid();

            if (chart.CrosshairVisible != state.CrosshairVisible)
                chart.ToggleCrosshair();

            chart.SetChartType(state.ChartType);
            chart.Visible = !state.Hidden;
            chartToolbarStates[symbol] = state;

            if (ReferenceEquals(chart, GetActiveChart()))
                SyncChartToolbarFromActiveChart();
        }

        private ChartToolbarState GetChartToolbarState(string symbol, TradingChartControl chart)
        {
            if (chartToolbarStates.TryGetValue(symbol, out var stored))
                return stored;

            var state = new ChartToolbarState
            {
                ChartType = GetSelectedChartType(),
                GridVisible = chart.GridVisible,
                CrosshairVisible = chart.CrosshairVisible,
                Hidden = !chart.Visible
            };

            chartToolbarStates[symbol] = state;
            return state;
        }

        private void RememberActiveChartToolbarState()
        {
            var chart = GetActiveChart();
            if (chart == null)
                return;

            var symbol = GetActiveChartTabSymbol();
            if (string.IsNullOrWhiteSpace(symbol))
                return;

            chartToolbarStates[symbol] = new ChartToolbarState
            {
                ChartType = GetSelectedChartType(),
                GridVisible = chart.GridVisible,
                CrosshairVisible = chart.CrosshairVisible,
                Hidden = !chart.Visible
            };
        }

        private string GetActiveChartTabSymbol()
        {
            if (chartDisplayMode == ChartDisplayMode.SingleTab)
                return chartTabPage.Text?.Trim() ?? string.Empty;

            return chartTabControl.SelectedTab?.Text?.Trim() ?? activeChartSymbol?.Trim() ?? string.Empty;
        }

        private void SetChartTypeComboBox(TradingChartType type)
        {
            var index = type switch
            {
                TradingChartType.Line => 1,
                TradingChartType.Bar => 2,
                _ => 0
            };

            if (chartTypeComboBox.SelectedIndex != index)
                chartTypeComboBox.SelectedIndex = index;
        }
    }
}
