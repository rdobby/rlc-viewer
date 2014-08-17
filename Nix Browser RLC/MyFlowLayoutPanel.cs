using System;
using System.Windows.Forms;

class MyFlowLayoutPanel : FlowLayoutPanel
{
    public MyFlowLayoutPanel()
    {
        this.SetStyle(ControlStyles.Opaque, true);
        this.DoubleBuffered = true;
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