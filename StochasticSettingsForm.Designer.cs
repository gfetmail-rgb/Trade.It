namespace Trade.It
{
    partial class StochasticSettingsForm
    {
        private System.ComponentModel.IContainer components=null;
        private Label periodLabel,kPeriodLabel,dPeriodLabel;
        private NumericUpDown periodNumeric,kPeriodNumeric,dPeriodNumeric;
        private CheckBox showKCheckBox,showDCheckBox,show20CheckBox,show80CheckBox;
        private Button kColorButton,dColorButton,c20ColorButton,c80ColorButton,defaultButton,okButton,cancelButton;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            periodLabel = new Label();
            kPeriodLabel = new Label();
            dPeriodLabel = new Label();
            periodNumeric = new NumericUpDown();
            kPeriodNumeric = new NumericUpDown();
            dPeriodNumeric = new NumericUpDown();
            showKCheckBox = new CheckBox();
            showDCheckBox = new CheckBox();
            show20CheckBox = new CheckBox();
            show80CheckBox = new CheckBox();
            kColorButton = new Button();
            dColorButton = new Button();
            c20ColorButton = new Button();
            c80ColorButton = new Button();
            defaultButton = new Button();
            okButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)periodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kPeriodNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dPeriodNumeric).BeginInit();
            SuspendLayout();
            // 
            // periodLabel
            // 
            periodLabel.AutoSize = true;
            periodLabel.Location = new Point(71, 19);
            periodLabel.Margin = new Padding(4, 0, 4, 0);
            periodLabel.Name = "periodLabel";
            periodLabel.Size = new Size(79, 25);
            periodLabel.TabIndex = 0;
            periodLabel.Text = "دوره %K:";
            // 
            // kPeriodLabel
            // 
            kPeriodLabel.AutoSize = true;
            kPeriodLabel.Location = new Point(21, 67);
            kPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            kPeriodLabel.Name = "kPeriodLabel";
            kPeriodLabel.Size = new Size(129, 25);
            kPeriodLabel.TabIndex = 2;
            kPeriodLabel.Text = "هموارسازی %K:";
            // 
            // dPeriodLabel
            // 
            dPeriodLabel.AutoSize = true;
            dPeriodLabel.Location = new Point(59, 122);
            dPeriodLabel.Margin = new Padding(4, 0, 4, 0);
            dPeriodLabel.Name = "dPeriodLabel";
            dPeriodLabel.Size = new Size(82, 25);
            dPeriodLabel.TabIndex = 4;
            dPeriodLabel.Text = "دوره %D:";
            // 
            // periodNumeric
            // 
            periodNumeric.Location = new Point(157, 19);
            periodNumeric.Margin = new Padding(4, 5, 4, 5);
            periodNumeric.Name = "periodNumeric";
            periodNumeric.Size = new Size(107, 31);
            periodNumeric.TabIndex = 1;
            periodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // kPeriodNumeric
            // 
            kPeriodNumeric.Location = new Point(157, 67);
            kPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            kPeriodNumeric.Name = "kPeriodNumeric";
            kPeriodNumeric.Size = new Size(107, 31);
            kPeriodNumeric.TabIndex = 3;
            kPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // dPeriodNumeric
            // 
            dPeriodNumeric.Location = new Point(157, 120);
            dPeriodNumeric.Margin = new Padding(4, 5, 4, 5);
            dPeriodNumeric.Name = "dPeriodNumeric";
            dPeriodNumeric.Size = new Size(107, 31);
            dPeriodNumeric.TabIndex = 5;
            dPeriodNumeric.TextAlign = HorizontalAlignment.Center;
            // 
            // showKCheckBox
            // 
            showKCheckBox.AutoSize = true;
            showKCheckBox.Location = new Point(198, 201);
            showKCheckBox.Margin = new Padding(4, 5, 4, 5);
            showKCheckBox.Name = "showKCheckBox";
            showKCheckBox.Size = new Size(117, 29);
            showKCheckBox.TabIndex = 6;
            showKCheckBox.Text = "نمایش %K";
            // 
            // showDCheckBox
            // 
            showDCheckBox.AutoSize = true;
            showDCheckBox.Location = new Point(198, 268);
            showDCheckBox.Margin = new Padding(4, 5, 4, 5);
            showDCheckBox.Name = "showDCheckBox";
            showDCheckBox.Size = new Size(120, 29);
            showDCheckBox.TabIndex = 8;
            showDCheckBox.Text = "نمایش %D";
            // 
            // show20CheckBox
            // 
            show20CheckBox.AutoSize = true;
            show20CheckBox.Location = new Point(198, 334);
            show20CheckBox.Margin = new Padding(4, 5, 4, 5);
            show20CheckBox.Name = "show20CheckBox";
            show20CheckBox.Size = new Size(153, 29);
            show20CheckBox.TabIndex = 10;
            show20CheckBox.Text = "نمایش سطح 20";
            // 
            // show80CheckBox
            // 
            show80CheckBox.AutoSize = true;
            show80CheckBox.Location = new Point(198, 401);
            show80CheckBox.Margin = new Padding(4, 5, 4, 5);
            show80CheckBox.Name = "show80CheckBox";
            show80CheckBox.Size = new Size(157, 29);
            show80CheckBox.TabIndex = 12;
            show80CheckBox.Text = "نمایش سطح 80";
            // 
            // kColorButton
            // 
            kColorButton.BackColor = Color.FromArgb(30, 100, 220);
            kColorButton.ForeColor = Color.White;
            kColorButton.Location = new Point(31, 186);
            kColorButton.Margin = new Padding(4, 5, 4, 5);
            kColorButton.Name = "kColorButton";
            kColorButton.Size = new Size(159, 57);
            kColorButton.TabIndex = 7;
            kColorButton.Text = "رنگ %K";
            kColorButton.UseVisualStyleBackColor = false;
            // 
            // dColorButton
            // 
            dColorButton.BackColor = Color.FromArgb(220, 80, 80);
            dColorButton.ForeColor = Color.White;
            dColorButton.Location = new Point(31, 253);
            dColorButton.Margin = new Padding(4, 5, 4, 5);
            dColorButton.Name = "dColorButton";
            dColorButton.Size = new Size(159, 57);
            dColorButton.TabIndex = 9;
            dColorButton.Text = "رنگ %D";
            dColorButton.UseVisualStyleBackColor = false;
            // 
            // c20ColorButton
            // 
            c20ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            c20ColorButton.ForeColor = Color.Black;
            c20ColorButton.Location = new Point(31, 320);
            c20ColorButton.Margin = new Padding(4, 5, 4, 5);
            c20ColorButton.Name = "c20ColorButton";
            c20ColorButton.Size = new Size(159, 57);
            c20ColorButton.TabIndex = 11;
            c20ColorButton.Text = "رنگ سطح 20";
            c20ColorButton.UseVisualStyleBackColor = false;
            // 
            // c80ColorButton
            // 
            c80ColorButton.BackColor = Color.FromArgb(150, 150, 150);
            c80ColorButton.ForeColor = Color.Black;
            c80ColorButton.Location = new Point(31, 386);
            c80ColorButton.Margin = new Padding(4, 5, 4, 5);
            c80ColorButton.Name = "c80ColorButton";
            c80ColorButton.Size = new Size(159, 57);
            c80ColorButton.TabIndex = 13;
            c80ColorButton.Text = "رنگ سطح 80";
            c80ColorButton.UseVisualStyleBackColor = false;
            // 
            // defaultButton
            // 
            defaultButton.Location = new Point(134, 479);
            defaultButton.Margin = new Padding(4, 5, 4, 5);
            defaultButton.Name = "defaultButton";
            defaultButton.Size = new Size(105, 46);
            defaultButton.TabIndex = 14;
            defaultButton.Text = "پیش‌فرض";
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(21, 479);
            okButton.Margin = new Padding(4, 5, 4, 5);
            okButton.Name = "okButton";
            okButton.Size = new Size(105, 43);
            okButton.TabIndex = 15;
            okButton.Text = "تأیید";
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(246, 482);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(105, 43);
            cancelButton.TabIndex = 16;
            cancelButton.Text = "انصراف";
            // 
            // StochasticSettingsForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(380, 536);
            Controls.Add(periodLabel);
            Controls.Add(periodNumeric);
            Controls.Add(kPeriodLabel);
            Controls.Add(kPeriodNumeric);
            Controls.Add(dPeriodLabel);
            Controls.Add(dPeriodNumeric);
            Controls.Add(showKCheckBox);
            Controls.Add(kColorButton);
            Controls.Add(showDCheckBox);
            Controls.Add(dColorButton);
            Controls.Add(show20CheckBox);
            Controls.Add(c20ColorButton);
            Controls.Add(show80CheckBox);
            Controls.Add(c80ColorButton);
            Controls.Add(defaultButton);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StochasticSettingsForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تنظیمات Stochastic";
            ((System.ComponentModel.ISupportInitialize)periodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)kPeriodNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)dPeriodNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}