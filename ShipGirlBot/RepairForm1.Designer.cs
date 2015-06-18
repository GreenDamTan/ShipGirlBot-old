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
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn18 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        this.selshiplist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
        this.SuspendLayout();
        // 
        // selshiplist
        // 
        this.selshiplist.Dock = System.Windows.Forms.DockStyle.Fill;
        this.selshiplist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
        this.selshiplist.Location = new System.Drawing.Point(0, 0);
        this.selshiplist.Name = "selshiplist";
        gridColumn10.HeaderText = "舰队";
        gridColumn10.Name = "";
        gridColumn11.HeaderText = "等级";
        gridColumn11.Name = "";
        gridColumn11.Width = 50;
        gridColumn12.HeaderText = "名称";
        gridColumn12.Name = "";
        gridColumn13.HeaderText = "类型";
        gridColumn13.Name = "";
        gridColumn13.Width = 30;
        gridColumn14.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
        gridColumn14.HeaderText = "少女";
        gridColumn14.Name = "";
        gridColumn14.Width = 124;
        gridColumn15.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridProgressBarXEditControl);
        gridColumn15.HeaderText = "血量";
        gridColumn15.Name = "";
        gridColumn16.HeaderText = "HP";
        gridColumn16.Name = "";
        gridColumn16.Width = 50;
        gridColumn17.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
        gridColumn17.HeaderText = "选择";
        gridColumn17.Name = "";
        gridColumn18.HeaderText = "id";
        gridColumn18.Name = "";
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn10);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn11);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn12);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn13);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn14);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn15);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn16);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn17);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn18);
        this.selshiplist.Size = new System.Drawing.Size(794, 523);
        this.selshiplist.TabIndex = 7;
        this.selshiplist.Text = "superGridControl1";
        // 
        // RepairForm1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(794, 523);
        this.Controls.Add(this.selshiplist);
        this.Name = "RepairForm1";
        this.Text = "修理船只";
        this.ResumeLayout(false);

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl selshiplist;
}