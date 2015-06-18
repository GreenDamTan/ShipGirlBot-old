
partial class DisShipConf
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
            this.nondislist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.dislist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nondislist
            // 
            this.nondislist.Dock = System.Windows.Forms.DockStyle.Right;
            this.nondislist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.nondislist.Location = new System.Drawing.Point(658, 0);
            this.nondislist.Name = "nondislist";
            gridColumn1.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            gridColumn1.HeaderText = "添加";
            gridColumn1.Name = "";
            gridColumn1.Width = 50;
            gridColumn2.HeaderText = "星级";
            gridColumn2.Name = "";
            gridColumn2.Width = 80;
            gridColumn3.HeaderText = "类型";
            gridColumn3.Name = "";
            gridColumn3.Width = 30;
            gridColumn4.HeaderText = "名称";
            gridColumn4.Name = "";
            gridColumn4.Width = 80;
            gridColumn5.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
            gridColumn5.HeaderText = "头像";
            gridColumn5.Name = "";
            gridColumn5.Width = 220;
            gridColumn6.HeaderText = "id";
            gridColumn6.Name = "";
            gridColumn6.Width = 50;
            this.nondislist.PrimaryGrid.Columns.Add(gridColumn1);
            this.nondislist.PrimaryGrid.Columns.Add(gridColumn2);
            this.nondislist.PrimaryGrid.Columns.Add(gridColumn3);
            this.nondislist.PrimaryGrid.Columns.Add(gridColumn4);
            this.nondislist.PrimaryGrid.Columns.Add(gridColumn5);
            this.nondislist.PrimaryGrid.Columns.Add(gridColumn6);
            this.nondislist.PrimaryGrid.MultiSelect = false;
            this.nondislist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.nondislist.Size = new System.Drawing.Size(555, 625);
            this.nondislist.TabIndex = 11;
            this.nondislist.Text = "superGridControl1";
            // 
            // dislist
            // 
            this.dislist.Dock = System.Windows.Forms.DockStyle.Left;
            this.dislist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.dislist.Location = new System.Drawing.Point(0, 0);
            this.dislist.Name = "dislist";
            gridColumn7.HeaderText = "星级";
            gridColumn7.Name = "";
            gridColumn7.Width = 80;
            gridColumn8.HeaderText = "类型";
            gridColumn8.Name = "";
            gridColumn8.Width = 30;
            gridColumn9.HeaderText = "名称";
            gridColumn9.Name = "";
            gridColumn9.Width = 80;
            gridColumn10.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
            gridColumn10.HeaderText = "头像";
            gridColumn10.Name = "";
            gridColumn10.Width = 220;
            gridColumn11.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            gridColumn11.HeaderText = "移除";
            gridColumn11.Name = "";
            gridColumn11.Width = 50;
            gridColumn12.HeaderText = "id";
            gridColumn12.Name = "";
            gridColumn12.Width = 50;
            this.dislist.PrimaryGrid.Columns.Add(gridColumn7);
            this.dislist.PrimaryGrid.Columns.Add(gridColumn8);
            this.dislist.PrimaryGrid.Columns.Add(gridColumn9);
            this.dislist.PrimaryGrid.Columns.Add(gridColumn10);
            this.dislist.PrimaryGrid.Columns.Add(gridColumn11);
            this.dislist.PrimaryGrid.Columns.Add(gridColumn12);
            this.dislist.PrimaryGrid.MultiSelect = false;
            this.dislist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.dislist.Size = new System.Drawing.Size(547, 625);
            this.dislist.TabIndex = 12;
            this.dislist.Text = "superGridControl1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(547, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 33);
            this.label1.TabIndex = 13;
            this.label1.Text = "移除=>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(547, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 33);
            this.label2.TabIndex = 14;
            this.label2.Text = "<=添加";
            // 
            // DisShipConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 625);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dislist);
            this.Controls.Add(this.nondislist);
            this.Name = "DisShipConf";
            this.Text = "DisShipConf";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl nondislist;
    private DevComponents.DotNetBar.SuperGrid.SuperGridControl dislist;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
}