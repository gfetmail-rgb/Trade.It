namespace Trade.It
{
    partial class ObvSettingsForm
    {
        private System.ComponentModel.IContainer components=null; private CheckBox showLineCheckBox,showZeroCheckBox; private Button lineColorButton,zeroColorButton,defaultButton,okButton,cancelButton;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            showLineCheckBox=new CheckBox();showZeroCheckBox=new CheckBox();lineColorButton=new Button();zeroColorButton=new Button();defaultButton=new Button();okButton=new Button();cancelButton=new Button();SuspendLayout();
            showLineCheckBox.Text="نمایش خط OBV";showLineCheckBox.Location=new Point(250,25);showLineCheckBox.AutoSize=true;lineColorButton.Text="رنگ خط";lineColorButton.Location=new Point(50,20);lineColorButton.Size=new Size(150,30);
            showZeroCheckBox.Text="نمایش خط صفر";showZeroCheckBox.Location=new Point(250,70);showZeroCheckBox.AutoSize=true;zeroColorButton.Text="رنگ خط صفر";zeroColorButton.Location=new Point(50,65);zeroColorButton.Size=new Size(150,30);
            defaultButton.Text="پیش‌فرض";defaultButton.Location=new Point(250,125);defaultButton.Size=new Size(100,36);okButton.Text="تأیید";okButton.Location=new Point(145,125);okButton.Size=new Size(90,36);cancelButton.Text="انصراف";cancelButton.Location=new Point(45,125);cancelButton.Size=new Size(90,36);
            Controls.AddRange(new Control[]{showLineCheckBox,lineColorButton,showZeroCheckBox,zeroColorButton,defaultButton,okButton,cancelButton});ClientSize=new Size(440,185);Text="تنظیمات OBV";StartPosition=FormStartPosition.CenterParent;FormBorderStyle=FormBorderStyle.FixedDialog;MaximizeBox=false;MinimizeBox=false;ResumeLayout(false);PerformLayout();
        }
    }
}