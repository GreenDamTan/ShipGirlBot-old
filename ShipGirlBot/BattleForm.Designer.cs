

partial class BattleForm
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn25 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.levellist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.backupshiplist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // levellist
            // 
            this.levellist.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.levellist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.levellist.Location = new System.Drawing.Point(0, 269);
            this.levellist.Name = "levellist";
            gridColumn1.HeaderText = "id";
            gridColumn1.Name = "";
            gridColumn1.Width = 50;
            gridColumn2.HeaderText = "关卡";
            gridColumn2.Name = "";
            gridColumn2.Width = 30;
            gridColumn3.HeaderText = "章节名";
            gridColumn3.Name = "";
            gridColumn3.Width = 150;
            gridColumn4.HeaderText = "关卡名";
            gridColumn4.Name = "";
            gridColumn4.Width = 150;
            gridColumn5.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
            gridColumn5.HeaderText = "地图";
            gridColumn5.Name = "";
            gridColumn5.Width = 300;
            gridColumn6.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            gridColumn6.HeaderText = "自动执行";
            gridColumn6.Name = "";
            gridColumn6.Width = 60;
            gridColumn7.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn7.HeaderText = "执行";
            gridColumn7.Name = "";
            gridColumn7.Width = 50;
            gridColumn8.HeaderText = "id";
            gridColumn8.Name = "";
            this.levellist.PrimaryGrid.Columns.Add(gridColumn1);
            this.levellist.PrimaryGrid.Columns.Add(gridColumn2);
            this.levellist.PrimaryGrid.Columns.Add(gridColumn3);
            this.levellist.PrimaryGrid.Columns.Add(gridColumn4);
            this.levellist.PrimaryGrid.Columns.Add(gridColumn5);
            this.levellist.PrimaryGrid.Columns.Add(gridColumn6);
            this.levellist.PrimaryGrid.Columns.Add(gridColumn7);
            this.levellist.PrimaryGrid.Columns.Add(gridColumn8);
            this.levellist.Size = new System.Drawing.Size(943, 369);
            this.levellist.TabIndex = 8;
            this.levellist.Text = "superGridControl1";
            // 
            // backupshiplist
            // 
            this.backupshiplist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.backupshiplist.Location = new System.Drawing.Point(-2, 32);
            this.backupshiplist.Name = "backupshiplist";
            gridColumn9.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            gridColumn9.HeaderText = "选中";
            gridColumn9.Name = "";
            gridColumn10.HeaderText = "等级";
            gridColumn10.Name = "";
            gridColumn10.Width = 50;
            gridColumn11.HeaderText = "类型";
            gridColumn11.Name = "";
            gridColumn11.Width = 40;
            gridColumn12.HeaderText = "少女";
            gridColumn12.Name = "";
            gridColumn12.Width = 90;
            gridColumn13.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
            gridColumn13.HeaderText = "头像";
            gridColumn13.Name = "";
            gridColumn13.Width = 124;
            gridColumn14.HeaderText = "hp";
            gridColumn14.Name = "";
            gridColumn14.Width = 60;
            gridColumn15.HeaderText = "装甲";
            gridColumn15.Name = "";
            gridColumn15.Width = 30;
            gridColumn16.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
            gridColumn16.HeaderText = "火力";
            gridColumn16.Name = "";
            gridColumn16.Width = 30;
            gridColumn17.HeaderText = "雷装";
            gridColumn17.Name = "";
            gridColumn17.Width = 30;
            gridColumn18.HeaderText = "对空";
            gridColumn18.Name = "";
            gridColumn18.Width = 30;
            gridColumn19.HeaderText = "对潜";
            gridColumn19.Name = "";
            gridColumn19.Width = 30;
            gridColumn20.HeaderText = "索敌";
            gridColumn20.Name = "";
            gridColumn20.Width = 30;
            gridColumn21.HeaderText = "速度";
            gridColumn21.Name = "";
            gridColumn21.Width = 30;
            gridColumn22.HeaderText = "幸运";
            gridColumn22.Name = "";
            gridColumn22.Width = 30;
            gridColumn23.HeaderText = "miss";
            gridColumn23.Name = "";
            gridColumn23.Width = 40;
            gridColumn24.HeaderText = "好感";
            gridColumn24.Name = "";
            gridColumn24.Width = 30;
            gridColumn25.HeaderText = "id";
            gridColumn25.Name = "";
            gridColumn25.Width = 30;
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn9);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn10);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn11);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn12);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn13);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn14);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn15);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn16);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn17);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn18);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn19);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn20);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn21);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn22);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn23);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn24);
            this.backupshiplist.PrimaryGrid.Columns.Add(gridColumn25);
            this.backupshiplist.Size = new System.Drawing.Size(940, 240);
            this.backupshiplist.TabIndex = 9;
            this.backupshiplist.Text = "superGridControl1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "选中参与战斗的后备队";
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 638);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backupshiplist);
            this.Controls.Add(this.levellist);
            this.Name = "BattleForm";
            this.Text = "BattleForm";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl levellist;
    private DevComponents.DotNetBar.SuperGrid.SuperGridControl backupshiplist;
    private System.Windows.Forms.Label label1;
}