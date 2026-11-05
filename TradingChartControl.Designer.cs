namespace Trade.It
{
    internal sealed partial class TradingChartControl
    {
        private System.Windows.Forms.Label noDateAxisLabel = null!;
        private System.Windows.Forms.Button priceIndicatorButton1 = null!;
        private System.Windows.Forms.Button priceIndicatorButton2 = null!;
        private System.Windows.Forms.Button priceIndicatorButton3 = null!;
        private System.Windows.Forms.Button priceIndicatorButton4 = null!;
        private System.Windows.Forms.Button priceIndicatorButton5 = null!;
        private System.Windows.Forms.Button priceIndicatorButton6 = null!;
        private System.Windows.Forms.Button priceIndicatorButton7 = null!;
        private System.Windows.Forms.Button priceIndicatorButton8 = null!;
        private System.Windows.Forms.Button priceIndicatorButton9 = null!;
        private System.Windows.Forms.Button priceIndicatorButton10 = null!;
        private System.Windows.Forms.Button priceIndicatorButton11 = null!;
        private System.Windows.Forms.Button priceIndicatorButton12 = null!;
        private System.Windows.Forms.Button[] priceIndicatorButtons = null!;

        private void InitializeComponent()
        {
            noDateAxisLabel = new System.Windows.Forms.Label();
            priceIndicatorButton1 = new System.Windows.Forms.Button();
            priceIndicatorButton2 = new System.Windows.Forms.Button();
            priceIndicatorButton3 = new System.Windows.Forms.Button();
            priceIndicatorButton4 = new System.Windows.Forms.Button();
            priceIndicatorButton5 = new System.Windows.Forms.Button();
            priceIndicatorButton6 = new System.Windows.Forms.Button();
            priceIndicatorButton7 = new System.Windows.Forms.Button();
            priceIndicatorButton8 = new System.Windows.Forms.Button();
            priceIndicatorButton9 = new System.Windows.Forms.Button();
            priceIndicatorButton10 = new System.Windows.Forms.Button();
            priceIndicatorButton11 = new System.Windows.Forms.Button();
            priceIndicatorButton12 = new System.Windows.Forms.Button();
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
            // priceIndicatorButton1
            priceIndicatorButton1.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton1.Location = new System.Drawing.Point(500, 24);
            priceIndicatorButton1.Name = "priceIndicatorButton1";
            priceIndicatorButton1.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton1.TabIndex = 1;
            priceIndicatorButton1.TabStop = false;
            priceIndicatorButton1.Text = "Indicator 1";
            priceIndicatorButton1.UseVisualStyleBackColor = false;
            priceIndicatorButton1.Visible = true;
            priceIndicatorButton1.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton2
            priceIndicatorButton2.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton2.Location = new System.Drawing.Point(500, 62);
            priceIndicatorButton2.Name = "priceIndicatorButton2";
            priceIndicatorButton2.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton2.TabIndex = 2;
            priceIndicatorButton2.TabStop = false;
            priceIndicatorButton2.Text = "Indicator 2";
            priceIndicatorButton2.UseVisualStyleBackColor = false;
            priceIndicatorButton2.Visible = true;
            priceIndicatorButton2.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton3
            priceIndicatorButton3.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton3.Location = new System.Drawing.Point(500, 100);
            priceIndicatorButton3.Name = "priceIndicatorButton3";
            priceIndicatorButton3.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton3.TabIndex = 3;
            priceIndicatorButton3.TabStop = false;
            priceIndicatorButton3.Text = "Indicator 3";
            priceIndicatorButton3.UseVisualStyleBackColor = false;
            priceIndicatorButton3.Visible = true;
            priceIndicatorButton3.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton4
            priceIndicatorButton4.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton4.Location = new System.Drawing.Point(500, 138);
            priceIndicatorButton4.Name = "priceIndicatorButton4";
            priceIndicatorButton4.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton4.TabIndex = 4;
            priceIndicatorButton4.TabStop = false;
            priceIndicatorButton4.Text = "Indicator 4";
            priceIndicatorButton4.UseVisualStyleBackColor = false;
            priceIndicatorButton4.Visible = true;
            priceIndicatorButton4.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton5
            priceIndicatorButton5.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton5.Location = new System.Drawing.Point(500, 176);
            priceIndicatorButton5.Name = "priceIndicatorButton5";
            priceIndicatorButton5.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton5.TabIndex = 5;
            priceIndicatorButton5.TabStop = false;
            priceIndicatorButton5.Text = "Indicator 5";
            priceIndicatorButton5.UseVisualStyleBackColor = false;
            priceIndicatorButton5.Visible = true;
            priceIndicatorButton5.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton6
            priceIndicatorButton6.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton6.Location = new System.Drawing.Point(500, 214);
            priceIndicatorButton6.Name = "priceIndicatorButton6";
            priceIndicatorButton6.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton6.TabIndex = 6;
            priceIndicatorButton6.TabStop = false;
            priceIndicatorButton6.Text = "Indicator 6";
            priceIndicatorButton6.UseVisualStyleBackColor = false;
            priceIndicatorButton6.Visible = true;
            priceIndicatorButton6.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton7
            priceIndicatorButton7.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton7.Location = new System.Drawing.Point(500, 252);
            priceIndicatorButton7.Name = "priceIndicatorButton7";
            priceIndicatorButton7.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton7.TabIndex = 7;
            priceIndicatorButton7.TabStop = false;
            priceIndicatorButton7.Text = "Indicator 7";
            priceIndicatorButton7.UseVisualStyleBackColor = false;
            priceIndicatorButton7.Visible = true;
            priceIndicatorButton7.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton8
            priceIndicatorButton8.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton8.Location = new System.Drawing.Point(500, 290);
            priceIndicatorButton8.Name = "priceIndicatorButton8";
            priceIndicatorButton8.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton8.TabIndex = 8;
            priceIndicatorButton8.TabStop = false;
            priceIndicatorButton8.Text = "Indicator 8";
            priceIndicatorButton8.UseVisualStyleBackColor = false;
            priceIndicatorButton8.Visible = true;
            priceIndicatorButton8.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton9
            priceIndicatorButton9.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton9.Location = new System.Drawing.Point(500, 328);
            priceIndicatorButton9.Name = "priceIndicatorButton9";
            priceIndicatorButton9.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton9.TabIndex = 9;
            priceIndicatorButton9.TabStop = false;
            priceIndicatorButton9.Text = "Indicator 9";
            priceIndicatorButton9.UseVisualStyleBackColor = false;
            priceIndicatorButton9.Visible = true;
            priceIndicatorButton9.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton10
            priceIndicatorButton10.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton10.Location = new System.Drawing.Point(500, 366);
            priceIndicatorButton10.Name = "priceIndicatorButton10";
            priceIndicatorButton10.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton10.TabIndex = 10;
            priceIndicatorButton10.TabStop = false;
            priceIndicatorButton10.Text = "Indicator 10";
            priceIndicatorButton10.UseVisualStyleBackColor = false;
            priceIndicatorButton10.Visible = true;
            priceIndicatorButton10.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton11
            priceIndicatorButton11.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton11.Location = new System.Drawing.Point(500, 404);
            priceIndicatorButton11.Name = "priceIndicatorButton11";
            priceIndicatorButton11.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton11.TabIndex = 11;
            priceIndicatorButton11.TabStop = false;
            priceIndicatorButton11.Text = "Indicator 11";
            priceIndicatorButton11.UseVisualStyleBackColor = false;
            priceIndicatorButton11.Visible = true;
            priceIndicatorButton11.Click += PriceIndicatorButton_Click;
            // priceIndicatorButton12
            priceIndicatorButton12.BackColor = System.Drawing.SystemColors.Control;
            priceIndicatorButton12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            priceIndicatorButton12.Location = new System.Drawing.Point(500, 442);
            priceIndicatorButton12.Name = "priceIndicatorButton12";
            priceIndicatorButton12.Size = new System.Drawing.Size(120, 34);
            priceIndicatorButton12.TabIndex = 12;
            priceIndicatorButton12.TabStop = false;
            priceIndicatorButton12.Text = "Indicator 12";
            priceIndicatorButton12.UseVisualStyleBackColor = false;
            priceIndicatorButton12.Visible = true;
            priceIndicatorButton12.Click += PriceIndicatorButton_Click;
            priceIndicatorButtons = new System.Windows.Forms.Button[]
            {
                priceIndicatorButton1,
                priceIndicatorButton2,
                priceIndicatorButton3,
                priceIndicatorButton4,
                priceIndicatorButton5,
                priceIndicatorButton6,
                priceIndicatorButton7,
                priceIndicatorButton8,
                priceIndicatorButton9,
                priceIndicatorButton10,
                priceIndicatorButton11,
                priceIndicatorButton12
            };
            Controls.Add(noDateAxisLabel);
            Controls.Add(priceIndicatorButton1);
            Controls.Add(priceIndicatorButton2);
            Controls.Add(priceIndicatorButton3);
            Controls.Add(priceIndicatorButton4);
            Controls.Add(priceIndicatorButton5);
            Controls.Add(priceIndicatorButton6);
            Controls.Add(priceIndicatorButton7);
            Controls.Add(priceIndicatorButton8);
            Controls.Add(priceIndicatorButton9);
            Controls.Add(priceIndicatorButton10);
            Controls.Add(priceIndicatorButton11);
            Controls.Add(priceIndicatorButton12);
            ResumeLayout(false);
        }
    }
}
