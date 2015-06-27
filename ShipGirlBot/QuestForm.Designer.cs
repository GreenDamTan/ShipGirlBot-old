
partial class QuestForm
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
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn18 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn19 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn20 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn21 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        this.questlist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
        this.SuspendLayout();
        // 
        // questlist
        // 
        this.questlist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
        this.questlist.Location = new System.Drawing.Point(-1, -2);
        this.questlist.Name = "questlist";
        gridColumn15.HeaderText = "id";
        gridColumn15.Name = "";
        gridColumn15.Width = 50;
        gridColumn16.HeaderText = "名称";
        gridColumn16.Name = "";
        gridColumn16.Width = 150;
        gridColumn17.HeaderText = "类型";
        gridColumn17.Name = "";
        gridColumn17.Width = 40;
        gridColumn18.HeaderText = "说明";
        gridColumn18.Name = "";
        gridColumn18.Width = 400;
        gridColumn19.HeaderText = "完成状态";
        gridColumn19.Name = "";
        gridColumn19.Width = 60;
        gridColumn20.HeaderText = "奖励";
        gridColumn20.Name = "";
        gridColumn20.Width = 300;
        gridColumn21.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
        gridColumn21.HeaderText = "完成";
        gridColumn21.Name = "";
        gridColumn21.Width = 60;
        this.questlist.PrimaryGrid.Columns.Add(gridColumn15);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn16);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn17);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn18);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn19);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn20);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn21);
        this.questlist.Size = new System.Drawing.Size(1123, 529);
        this.questlist.TabIndex = 10;
        this.questlist.Text = "superGridControl1";
        // 
        // QuestForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoScroll = true;
        this.AutoSize = true;
        this.ClientSize = new System.Drawing.Size(1028, 530);
        this.Controls.Add(this.questlist);
        this.Name = "QuestForm";
        this.Text = "QuestForm";
        this.ResumeLayout(false);

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl questlist;
}