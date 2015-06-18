using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public class TransparentPicture : PictureBox
{
    public bool IsTransparent { get; set; }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;

            if (this.IsTransparent)
            {
                cp.ExStyle |= 0x20;
            }

            return cp;
        }
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        if (!this.IsTransparent)
        {
            base.OnPaintBackground(e);
        }
    }

    protected override void OnMove(EventArgs e)
    {
        if (this.IsTransparent)
        {
            RecreateHandle();
        }
        else
        {
            base.OnMove(e);
        }
    }
}