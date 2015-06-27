partial class EditBackup
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
        this.dislist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
        this.button1 = new System.Windows.Forms.Button();
        this.integerInput1 = new DevComponents.Editors.IntegerInput();
        ((System.ComponentModel.ISupportInitialize)(this.integerInput1)).BeginInit();
        this.SuspendLayout();
        // 
        // dislist
        // 
        this.dislist.Dock = System.Windows.Forms.DockStyle.Left;
        this.dislist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
        this.dislist.Location = new System.Drawing.Point(0, 0);
        this.dislist.Name = "dislist";
        gridColumn1.Name = "";
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
        gridColumn6.HeaderText = "移除";
        gridColumn6.Name = "";
        gridColumn6.Width = 50;
        gridColumn7.HeaderText = "id";
        gridColumn7.Name = "";
        gridColumn7.Width = 50;
        this.dislist.PrimaryGrid.Columns.Add(gridColumn1);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn2);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn3);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn4);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn5);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn6);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn7);
        this.dislist.PrimaryGrid.MultiSelect = false;
        this.dislist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
        this.dislist.Size = new System.Drawing.Size(684, 625);
        this.dislist.TabIndex = 13;
        this.dislist.Text = "superGridControl1";
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(728, 47);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 14;
        this.button1.Text = "button1";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // integerInput1
        // 
        // 
        // 
        // 
        this.integerInput1.BackgroundStyle.Class = "DateTimeInputBackground";
        this.integerInput1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
        this.integerInput1.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
        this.integerInput1.Location = new System.Drawing.Point(728, 108);
        this.integerInput1.Name = "integerInput1";
        this.integerInput1.ShowUpDown = true;
        this.integerInput1.TabIndex = 15;
        // 
        // EditBackup
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoScroll = true;
        this.AutoSize = true;
        this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.ClientSize = new System.Drawing.Size(1028, 625);
        this.Controls.Add(this.integerInput1);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.dislist);
        this.Name = "EditBackup";
        this.Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)(this.integerInput1)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl dislist;
    private System.Windows.Forms.Button button1;
    private DevComponents.Editors.IntegerInput integerInput1;
}