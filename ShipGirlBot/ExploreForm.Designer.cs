
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
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        this.explorelist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
        this.SuspendLayout();
        // 
        // explorelist
        // 
        this.explorelist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
        this.explorelist.Location = new System.Drawing.Point(0, -1);
        this.explorelist.Name = "explorelist";
        gridColumn1.HeaderText = "id";
        gridColumn1.Name = "";
        gridColumn1.Width = 50;
        gridColumn2.HeaderText = "名称";
        gridColumn2.Name = "";
        gridColumn2.Width = 120;
        gridColumn3.HeaderText = "奖励";
        gridColumn3.Name = "";
        gridColumn3.Width = 200;
        gridColumn4.HeaderText = "耗时";
        gridColumn4.Name = "";
        gridColumn4.Width = 80;
        gridColumn5.HeaderText = "舰数";
        gridColumn5.Name = "";
        gridColumn5.Width = 30;
        gridColumn6.HeaderText = "旗舰Lv";
        gridColumn6.Name = "";
        gridColumn6.Width = 60;
        gridColumn7.HeaderText = "类型需求";
        gridColumn7.Name = "";
        gridColumn8.HeaderText = "额外奖励";
        gridColumn8.Name = "";
        gridColumn8.Width = 80;
        gridColumn9.HeaderText = "大成功率";
        gridColumn9.Name = "";
        gridColumn9.Width = 60;
        gridColumn10.HeaderText = "油弹消耗";
        gridColumn10.Name = "";
        gridColumn10.Width = 60;
        gridColumn11.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
        gridColumn11.HeaderText = "自动执行";
        gridColumn11.Name = "";
        gridColumn11.Width = 60;
        gridColumn12.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
        gridColumn12.HeaderText = "执行";
        gridColumn12.Name = "";
        gridColumn12.Width = 50;
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn1);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn2);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn3);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn4);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn5);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn6);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn7);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn8);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn9);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn10);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn11);
        this.explorelist.PrimaryGrid.Columns.Add(gridColumn12);
        this.explorelist.Size = new System.Drawing.Size(1120, 542);
        this.explorelist.TabIndex = 7;
        this.explorelist.Text = "superGridControl1";
        // 
        // ExploreForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1120, 539);
        this.Controls.Add(this.explorelist);
        this.Name = "ExploreForm";
        this.Text = "选择远征";
        this.ResumeLayout(false);

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl explorelist;
}