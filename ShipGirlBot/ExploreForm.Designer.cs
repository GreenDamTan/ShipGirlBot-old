
partial class ExploreForm
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
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn18 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn19 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn20 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn21 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn22 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn23 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn24 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        this.explorelist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
        this.SuspendLayout();
        // 
        // explorelist
        // 
        this.explorelist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
        this.explorelist.Location = new System.Drawing.Point(0, -1);
        this.explorelist.Name = "explorelist";
        gridColumn13.HeaderText = "id";
        gridColumn13.Name = "";
        gridColumn13.Width = 50;
        gridColumn14.HeaderText = "名称";
        gridColumn14.Name = "";
        gridColumn14.Width = 120;
        gridColumn15.HeaderText = "奖励";
        gridColumn15.Name = "";
        gridColumn15.Width = 200;
        gridColumn16.HeaderText = "耗时";
        gridColumn16.Name = "";
        gridColumn16.Width = 80;
        gridColumn17.HeaderText = "舰数";
        gridColumn17.Name = "";
        gridColumn17.Width = 30;
        gridColumn18.HeaderText = "旗舰Lv";
        gridColumn18.Name = "";
        gridColumn18.Width = 60;
        gridColumn19.HeaderText = "类型需求";
        gridColumn19.Name = "";
        gridColumn20.HeaderText = "额外奖励";
        gridColumn20.Name = "";
        gridColumn20.Width = 80;
        gridColumn21.HeaderText = "大成功率";
        gridColumn21.Name = "";
        gridColumn21.Width = 60;
        gridColumn22.HeaderText = "油弹消耗";
        gridColumn22.Name = "";
        gridColumn22.Width = 60;
        gridColumn23.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
        gridColumn23.HeaderText = "自动执行";
        gridColumn23.Name = "";
        gridColumn23.Width = 60;
        gridColumn24.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
        gridColumn24.HeaderText = "执行";
        gridColumn24.Name = "";
        gridColumn24.Width = 50;
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn13);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn14);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn15);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn16);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn17);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn18);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn19);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn20);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn21);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn22);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn23);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn24);
        this.explorelist.Size = new System.Drawing.Size(1120, 542);
        this.explorelist.TabIndex = 7;
        this.explorelist.Text = "superGridControl1";
        // 
        // ExploreForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoSize = true;
        this.ClientSize = new System.Drawing.Size(1028, 539);
        this.Controls.Add(this.explorelist);
        this.Name = "ExploreForm";
        this.Text = "选择远征";
        this.ResumeLayout(false);

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl explorelist;
}