
partial class Analyze
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.droplist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.nodelist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // gridColumn6
            // 
            this.gridColumn6.HeaderText = "名称";
            this.gridColumn6.Name = "";
            this.gridColumn6.Width = 120;
            // 
            // gridColumn7
            // 
            this.gridColumn7.HeaderText = "数量";
            this.gridColumn7.Name = "";
            // 
            // gridColumn8
            // 
            this.gridColumn8.HeaderText = "几率";
            this.gridColumn8.Name = "";
            this.gridColumn8.Width = 80;
            // 
            // gridColumn9
            // 
            this.gridColumn9.HeaderText = "点";
            this.gridColumn9.Name = "";
            // 
            // gridColumn10
            // 
            this.gridColumn10.HeaderText = "总掉落数";
            this.gridColumn10.Name = "";
            // 
            // droplist
            // 
            this.droplist.Dock = System.Windows.Forms.DockStyle.Right;
            this.droplist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.droplist.Location = new System.Drawing.Point(388, 0);
            this.droplist.Name = "droplist";
            // 
            // 
            // 
            this.droplist.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.droplist.PrimaryGrid.Columns.Add(this.gridColumn7);
            this.droplist.PrimaryGrid.Columns.Add(this.gridColumn8);
            this.droplist.PrimaryGrid.MultiSelect = false;
            this.droplist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.droplist.Size = new System.Drawing.Size(411, 639);
            this.droplist.TabIndex = 11;
            this.droplist.Text = "superGridControl1";
            // 
            // nodelist
            // 
            this.nodelist.Dock = System.Windows.Forms.DockStyle.Left;
            this.nodelist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.nodelist.Location = new System.Drawing.Point(0, 0);
            this.nodelist.Name = "nodelist";
            // 
            // 
            // 
            this.nodelist.PrimaryGrid.Columns.Add(this.gridColumn9);
            this.nodelist.PrimaryGrid.Columns.Add(this.gridColumn10);
            this.nodelist.PrimaryGrid.MultiSelect = false;
            this.nodelist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.nodelist.Size = new System.Drawing.Size(266, 639);
            this.nodelist.TabIndex = 12;
            this.nodelist.Text = "superGridControl1";
            this.nodelist.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.nodelist_RowClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "口粮口感(统计平均)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "火力";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "雷装";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(272, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "防空";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(272, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "装甲";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(272, 284);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "钢";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(272, 304);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "铝";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(272, 264);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "弹";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(272, 244);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "油";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(269, 218);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "炼钢收入(3星及以下)";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(274, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 23;
            this.checkBox1.Text = "按胜率查看";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Analyze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 639);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nodelist);
            this.Controls.Add(this.droplist);
            this.Name = "Analyze";
            this.Text = "掉落统计-每小时更新";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl droplist;
    private DevComponents.DotNetBar.SuperGrid.SuperGridControl nodelist;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
    private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7;
    private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8;
    private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
    private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10;
    private System.Windows.Forms.CheckBox checkBox1;
}