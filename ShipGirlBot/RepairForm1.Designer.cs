partial class RepairForm1
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
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        this.selshiplist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
        this.SuspendLayout();
        // 
        // selshiplist
        // 
        this.selshiplist.Dock = System.Windows.Forms.DockStyle.Fill;
        this.selshiplist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
        this.selshiplist.Location = new System.Drawing.Point(0, 0);
        this.selshiplist.Name = "selshiplist";
        gridColumn1.HeaderText = "舰队";
        gridColumn1.Name = "";
        gridColumn2.HeaderText = "等级";
        gridColumn2.Name = "";
        gridColumn2.Width = 50;
        gridColumn3.HeaderText = "名称";
        gridColumn3.Name = "";
        gridColumn4.HeaderText = "类型";
        gridColumn4.Name = "";
        gridColumn4.Width = 30;
        gridColumn5.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
        gridColumn5.HeaderText = "少女";
        gridColumn5.Name = "";
        gridColumn5.Width = 124;
        gridColumn6.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridProgressBarXEditControl);
        gridColumn6.HeaderText = "血量";
        gridColumn6.Name = "";
        gridColumn7.HeaderText = "HP";
        gridColumn7.Name = "";
        gridColumn7.Width = 50;
        gridColumn8.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
        gridColumn8.HeaderText = "选择";
        gridColumn8.Name = "";
        gridColumn9.HeaderText = "id";
        gridColumn9.Name = "";
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn1);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn2);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn3);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn4);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn5);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn6);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn7);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn8);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn9);
        this.selshiplist.Size = new System.Drawing.Size(794, 523);
        this.selshiplist.TabIndex = 7;
        this.selshiplist.Text = "superGridControl1";
        // 
        // RepairForm1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.ClientSize = new System.Drawing.Size(794, 523);
        this.Controls.Add(this.selshiplist);
        this.Name = "RepairForm1";
        this.Text = "修理船只";
        this.ResumeLayout(false);

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl selshiplist;
}