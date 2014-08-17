using System;
using System.Windows.Forms;

class MyPanel : Panel
{
    public MyPanel()
    {
        this.SetStyle(ControlStyles.Opaque, true);
    }
    protected override CreateParams CreateParams
    {
        get
        {
            // Turn on the WS_EX_TRANSPARENT style
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x20;
            return cp;
        }
    }
}
