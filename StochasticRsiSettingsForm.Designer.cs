namespace Trade.It
{
    partial class StochasticRsiSettingsForm
    {
        private System.ComponentModel.IContainer components=null;
        private Label rsiPeriodLabel,stochasticPeriodLabel,kPeriodLabel,dPeriodLabel;
        private NumericUpDown rsiPeriodNumeric,stochasticPeriodNumeric,kPeriodNumeric,dPeriodNumeric;
        private CheckBox showKCheckBox,showDCheckBox,show20CheckBox,show80CheckBox;
        private Button kColorButton,dColorButton,c20ColorButton,c80ColorButton,defaultButton,okButton,cancelButton;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            rsiPeriodLabel=new Label();stochasticPeriodLabel=new Label();kPeriodLabel=new Label();dPeriodLabel=new Label();rsiPeriodNumeric=new NumericUpDown();stochasticPeriodNumeric=new NumericUpDown();kPeriodNumeric=new NumericUpDown();dPeriodNumeric=new NumericUpDown();
            showKCheckBox=new CheckBox();showDCheckBox=new CheckBox();show20CheckBox=new CheckBox();show80CheckBox=new CheckBox();kColorButton=new Button();dColorButton=new Button();c20ColorButton=new Button();c80ColorButton=new Button();okButton=new Button();cancelButton=new Button();
            ((System.ComponentModel.ISupportInitialize)rsiPeriodNumeric).BeginInit();((System.ComponentModel.ISupportInitialize)stochasticPeriodNumeric).BeginInit();((System.ComponentModel.ISupportInitialize)kPeriodNumeric).BeginInit();((System.ComponentModel.ISupportInitialize)dPeriodNumeric).BeginInit();SuspendLayout();
            ConfigureNumber(rsiPeriodLabel,rsiPeriodNumeric,"دوره RSI:",16);ConfigureNumber(stochasticPeriodLabel,stochasticPeriodNumeric,"دوره Stochastic:",56);ConfigureNumber(kPeriodLabel,kPeriodNumeric,"هموارسازی %K:",96);ConfigureNumber(dPeriodLabel,dPeriodNumeric,"دوره %D:",136);
            ConfigureRow(showKCheckBox,kColorButton,"نمایش %K","رنگ %K",178);ConfigureRow(showDCheckBox,dColorButton,"نمایش %D","رنگ %D",218);ConfigureRow(show20CheckBox,c20ColorButton,"نمایش سطح 20","رنگ سطح 20",258);ConfigureRow(show80CheckBox,c80ColorButton,"نمایش سطح 80","رنگ سطح 80",298);
            defaultButton.Text="پیش‌فرض";defaultButton.Location=new Point(90,348);defaultButton.Size=new Size(100,34);okButton.Text="تأیید";okButton.DialogResult=DialogResult.OK;okButton.Location=new Point(310,348);okButton.Size=new Size(100,34);cancelButton.Text="انصراف";cancelButton.DialogResult=DialogResult.Cancel;cancelButton.Location=new Point(200,348);cancelButton.Size=new Size(100,34);AcceptButton=okButton;CancelButton=cancelButton;
            Controls.AddRange(new Control[]{rsiPeriodLabel,rsiPeriodNumeric,stochasticPeriodLabel,stochasticPeriodNumeric,kPeriodLabel,kPeriodNumeric,dPeriodLabel,dPeriodNumeric,showKCheckBox,kColorButton,showDCheckBox,dColorButton,show20CheckBox,c20ColorButton,show80CheckBox,c80ColorButton,defaultButton,okButton,cancelButton});
            AutoScaleDimensions=new SizeF(7F,15F);AutoScaleMode=AutoScaleMode.Font;ClientSize=new Size(420,400);FormBorderStyle=FormBorderStyle.FixedDialog;MaximizeBox=false;MinimizeBox=false;Name="StochasticRsiSettingsForm";StartPosition=FormStartPosition.CenterParent;Text="تنظیمات Stochastic RSI";RightToLeft=RightToLeft.Yes;RightToLeftLayout=true;ShowInTaskbar=false;
            ((System.ComponentModel.ISupportInitialize)rsiPeriodNumeric).EndInit();((System.ComponentModel.ISupportInitialize)stochasticPeriodNumeric).EndInit();((System.ComponentModel.ISupportInitialize)kPeriodNumeric).EndInit();((System.ComponentModel.ISupportInitialize)dPeriodNumeric).EndInit();ResumeLayout(false);PerformLayout();
        }
        private static void ConfigureNumber(Label l,NumericUpDown n,string t,int y){l.AutoSize=true;l.Text=t;l.Location=new Point(285,y+4);l.Size=new Size(115,25);n.Location=new Point(140,y);n.Size=new Size(125,33);n.TextAlign=HorizontalAlignment.Center;}
        private static void ConfigureRow(CheckBox c,Button b,string ct,string bt,int y){c.AutoSize=true;c.Text=ct;c.Location=new Point(285,y+4);c.Size=new Size(120,29);b.Text=bt;b.Location=new Point(115,y);b.Size=new Size(125,34);b.UseVisualStyleBackColor=false;}
    }
}