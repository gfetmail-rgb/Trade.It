namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private System.Windows.Forms.Label noDateAxisLabel = null!;

        private void InitializeComponent()
        {
            noDateAxisLabel = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // noDateAxisLabel
            // 
            noDateAxisLabel.AutoSize = false;
            noDateAxisLabel.BackColor = System.Drawing.Color.FromArgb(120, 120, 120);
            noDateAxisLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            noDateAxisLabel.ForeColor = System.Drawing.Color.White;
            noDateAxisLabel.Location = new System.Drawing.Point(0, 0);
            noDateAxisLabel.Name = "noDateAxisLabel";
            noDateAxisLabel.Size = new System.Drawing.Size(170, 20);
            noDateAxisLabel.TabIndex = 0;
            noDateAxisLabel.TabStop = false;
            noDateAxisLabel.Text = " ";
            noDateAxisLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            noDateAxisLabel.Visible = true;
            Controls.Add(noDateAxisLabel);
            ResumeLayout(false);
        }
    }
}
