namespace Trade.It
{
    partial class BollingerSettingsForm
    {
        private System.ComponentModel.IContainer components=null;
        private Label periodLabel,stdLabel;
        private NumericUpDown periodNumeric,stdNumeric;
        private CheckBox middleCheckBox,upperCheckBox,lowerCheckBox;
        private Button middleColorButton,upperColorButton,lowerColorButton,defaultButton,okButton,cancelButton;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            periodLabel=new Label();stdLabel=new Label();periodNumeric=new NumericUpDown();stdNumeric=new NumericUpDown();
            middleCheckBox=new CheckBox();upperCheckBox=new CheckBox();lowerCheckBox=new CheckBox();middleColorButton=new Button();upperColorButton=new Button();lowerColorButton=new Button();defaultButton=new Button();okButton=new Button();cancelButton=new Button();
            ((System.ComponentModel.ISupportInitialize)periodNumeric).BeginInit();((System.ComponentModel.ISupportInitialize)stdNumeric).BeginInit();SuspendLayout();
            periodLabel.Text="دوره:";periodLabel.Location=new Point(300,20);periodLabel.AutoSize=true;periodNumeric.Location=new Point(160,16);periodNumeric.Size=new Size(120,30);
            stdLabel.Text="انحراف معیار:";stdLabel.Location=new Point(300,60);stdLabel.AutoSize=true;stdNumeric.Location=new Point(160,56);stdNumeric.Size=new Size(120,30);
            ConfigureRow(middleCheckBox,middleColorButton,"نمایش خط میانی","رنگ خط میانی",100);ConfigureRow(upperCheckBox,upperColorButton,"نمایش باند بالا","رنگ باند بالا",140);ConfigureRow(lowerCheckBox,lowerColorButton,"نمایش باند پایین","رنگ باند پایین",180);
            defaultButton.Text="پیش‌فرض";defaultButton.Location=new Point(250,230);defaultButton.Size=new Size(100,36);okButton.Text="تأیید";okButton.Location=new Point(140,230);okButton.Size=new Size(90,36);cancelButton.Text="انصراف";cancelButton.Location=new Point(40,230);cancelButton.Size=new Size(90,36);
            Controls.AddRange(new Control[]{periodLabel,periodNumeric,stdLabel,stdNumeric,middleCheckBox,middleColorButton,upperCheckBox,upperColorButton,lowerCheckBox,lowerColorButton,defaultButton,okButton,cancelButton});
            ClientSize=new Size(440,290);Text="تنظیمات بولینگر";StartPosition=FormStartPosition.CenterParent;FormBorderStyle=FormBorderStyle.FixedDialog;MaximizeBox=false;MinimizeBox=false;
            ((System.ComponentModel.ISupportInitialize)periodNumeric).EndInit();((System.ComponentModel.ISupportInitialize)stdNumeric).EndInit();ResumeLayout(false);PerformLayout();
        }
        private static void ConfigureRow(CheckBox c,Button b,string ct,string bt,int y){c.Text=ct;c.Location=new Point(250,y);c.AutoSize=true;b.Text=bt;b.Location=new Point(60,y-4);b.Size=new Size(160,30);}
    }
}