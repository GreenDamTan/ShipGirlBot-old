using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class Coupon : Form
{
    public Coupon()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox1.Text == null || textBox1.Text == "")
        {
            MessageBox.Show("请输入兑换码");
            return;
        }
        else
        {
            var q = ServerRequestManager.instance.UseCoupon(textBox1.Text);
            if (q != null && q.responseData != null && q.responseData.eid == 0)
            {
                MessageBox.Show("兑换成功");
            }
            else if (q != null && q.responseData != null)
            {
                MessageBox.Show(q.responseData.eidstring);
            }
            else
            {
                MessageBox.Show("兑换失败");
            }
        }
    }
}
