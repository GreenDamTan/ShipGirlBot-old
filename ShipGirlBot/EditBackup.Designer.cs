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
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
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
        gridColumn8.Name = "";
        gridColumn9.HeaderText = "星级";
        gridColumn9.Name = "";
        gridColumn9.Width = 80;
        gridColumn10.HeaderText = "类型";
        gridColumn10.Name = "";
        gridColumn10.Width = 30;
        gridColumn11.HeaderText = "名称";
        gridColumn11.Name = "";
        gridColumn11.Width = 80;
        gridColumn12.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
        gridColumn12.HeaderText = "头像";
        gridColumn12.Name = "";
        gridColumn12.Width = 220;
        gridColumn13.HeaderText = "移除";
        gridColumn13.Name = "";
        gridColumn13.Width = 50;
        gridColumn14.HeaderText = "id";
        gridColumn14.Name = "";
        gridColumn14.Width = 50;
        this.dislist.PrimaryGrid.Columns.Add(gridColumn8);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn9);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn10);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn11);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn12);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn13);
        this.dislist.PrimaryGrid.Columns.Add(gridColumn14);
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
        this.integerInput1.Size = new System.Drawing.Size(80, 21);
        this.integerInput1.TabIndex = 15;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1216, 625);
        this.Controls.Add(this.integerInput1);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.dislist);
        this.Name = "Form1";
        this.Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)(this.integerInput1)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl dislist;
    private System.Windows.Forms.Button button1;
    private DevComponents.Editors.IntegerInput integerInput1;
}