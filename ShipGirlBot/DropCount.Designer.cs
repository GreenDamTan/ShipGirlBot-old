
partial class DropCount
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.nodelist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.droplist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(317, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "显示武器";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // nodelist
            // 
            this.nodelist.Dock = System.Windows.Forms.DockStyle.Left;
            this.nodelist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.nodelist.Location = new System.Drawing.Point(0, 0);
            this.nodelist.Name = "nodelist";
            gridColumn1.HeaderText = "配方";
            gridColumn1.Name = "";
            gridColumn1.Width = 160;
            gridColumn2.HeaderText = "使用次数";
            gridColumn2.Name = "";
            gridColumn2.Width = 80;
            this.nodelist.PrimaryGrid.Columns.Add(gridColumn1);
            this.nodelist.PrimaryGrid.Columns.Add(gridColumn2);
            this.nodelist.PrimaryGrid.MultiSelect = false;
            this.nodelist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.nodelist.Size = new System.Drawing.Size(293, 677);
            this.nodelist.TabIndex = 25;
            this.nodelist.Text = "superGridControl1";
            this.nodelist.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.nodelist_RowClick);
            // 
            // droplist
            // 
            this.droplist.Dock = System.Windows.Forms.DockStyle.Right;
            this.droplist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.droplist.Location = new System.Drawing.Point(416, 0);
            this.droplist.Name = "droplist";
            gridColumn3.HeaderText = "名称";
            gridColumn3.Name = "";
            gridColumn3.Width = 160;
            gridColumn4.HeaderText = "数量";
            gridColumn4.Name = "";
            gridColumn5.HeaderText = "几率";
            gridColumn5.Name = "";
            gridColumn5.Width = 80;
            this.droplist.PrimaryGrid.Columns.Add(gridColumn3);
            this.droplist.PrimaryGrid.Columns.Add(gridColumn4);
            this.droplist.PrimaryGrid.Columns.Add(gridColumn5);
            this.droplist.PrimaryGrid.MultiSelect = false;
            this.droplist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.droplist.Size = new System.Drawing.Size(530, 677);
            this.droplist.TabIndex = 24;
            this.droplist.Text = "superGridControl1";
            // 
            // DropCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 677);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.nodelist);
            this.Controls.Add(this.droplist);
            this.Name = "DropCount";
            this.Text = "建造统计，每小时更新";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox checkBox1;
    private DevComponents.DotNetBar.SuperGrid.SuperGridControl nodelist;
    private DevComponents.DotNetBar.SuperGrid.SuperGridControl droplist;
}