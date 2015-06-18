
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
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        this.questlist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
        this.SuspendLayout();
        // 
        // questlist
        // 
        this.questlist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
        this.questlist.Location = new System.Drawing.Point(-1, -2);
        this.questlist.Name = "questlist";
        gridColumn1.HeaderText = "id";
        gridColumn1.Name = "";
        gridColumn1.Width = 50;
        gridColumn2.HeaderText = "名称";
        gridColumn2.Name = "";
        gridColumn2.Width = 150;
        gridColumn3.HeaderText = "类型";
        gridColumn3.Name = "";
        gridColumn3.Width = 40;
        gridColumn4.HeaderText = "说明";
        gridColumn4.Name = "";
        gridColumn4.Width = 400;
        gridColumn5.HeaderText = "完成状态";
        gridColumn5.Name = "";
        gridColumn5.Width = 60;
        gridColumn6.HeaderText = "奖励";
        gridColumn6.Name = "";
        gridColumn6.Width = 300;
        gridColumn7.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
        gridColumn7.HeaderText = "完成";
        gridColumn7.Name = "";
        gridColumn7.Width = 60;
        this.questlist.PrimaryGrid.Columns.Add(gridColumn1);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn2);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn3);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn4);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn5);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn6);
        this.questlist.PrimaryGrid.Columns.Add(gridColumn7);
        this.questlist.Size = new System.Drawing.Size(1123, 529);
        this.questlist.TabIndex = 10;
        this.questlist.Text = "superGridControl1";
        // 
        // QuestForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1122, 530);
        this.Controls.Add(this.questlist);
        this.Name = "QuestForm";
        this.Text = "QuestForm";
        this.ResumeLayout(false);

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl questlist;
}