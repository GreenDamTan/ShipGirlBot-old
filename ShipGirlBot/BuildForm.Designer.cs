

partial class BuildForm
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
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.shipcheck = new System.Windows.Forms.CheckBox();
        this.equipcheck = new System.Windows.Forms.CheckBox();
        this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
        this.r1 = new DevComponents.Editors.IntegerInput();
        this.r2 = new DevComponents.Editors.IntegerInput();
        this.r3 = new DevComponents.Editors.IntegerInput();
        this.r4 = new DevComponents.Editors.IntegerInput();
        this.recipelist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
        this.v1 = new System.Windows.Forms.Label();
        this.v2 = new System.Windows.Forms.Label();
        this.v3 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.r1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.r2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.r3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.r4)).BeginInit();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(13, 42);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(29, 12);
        this.label1.TabIndex = 1;
        this.label1.Text = "燃油";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(13, 69);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(29, 12);
        this.label2.TabIndex = 3;
        this.label2.Text = "弹药";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(13, 96);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(29, 12);
        this.label3.TabIndex = 5;
        this.label3.Text = "钢铁";
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(13, 123);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(29, 12);
        this.label4.TabIndex = 7;
        this.label4.Text = "铝材";
        // 
        // shipcheck
        // 
        this.shipcheck.AutoSize = true;
        this.shipcheck.Checked = true;
        this.shipcheck.CheckState = System.Windows.Forms.CheckState.Checked;
        this.shipcheck.Location = new System.Drawing.Point(25, 13);
        this.shipcheck.Name = "shipcheck";
        this.shipcheck.Size = new System.Drawing.Size(72, 16);
        this.shipcheck.TabIndex = 8;
        this.shipcheck.Text = "少女召唤";
        this.shipcheck.UseVisualStyleBackColor = true;
        this.shipcheck.CheckedChanged += new System.EventHandler(this.shipcheck_CheckedChanged);
        // 
        // equipcheck
        // 
        this.equipcheck.AutoSize = true;
        this.equipcheck.Location = new System.Drawing.Point(109, 12);
        this.equipcheck.Name = "equipcheck";
        this.equipcheck.Size = new System.Drawing.Size(72, 16);
        this.equipcheck.TabIndex = 9;
        this.equipcheck.Text = "装备开发";
        this.equipcheck.UseVisualStyleBackColor = true;
        this.equipcheck.CheckedChanged += new System.EventHandler(this.equipcheck_CheckedChanged);
        // 
        // buttonX1
        // 
        this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
        this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
        this.buttonX1.ImageFixedSize = new System.Drawing.Size(256, 256);
        this.buttonX1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
        this.buttonX1.Location = new System.Drawing.Point(38, 338);
        this.buttonX1.Name = "buttonX1";
        this.buttonX1.Size = new System.Drawing.Size(192, 192);
        this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
        this.buttonX1.TabIndex = 10;
        this.buttonX1.Text = "sdfsdfsdf";
        this.buttonX1.TextColor = System.Drawing.Color.Black;
        this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
        // 
        // r1
        // 
        // 
        // 
        // 
        this.r1.BackgroundStyle.Class = "DateTimeInputBackground";
        this.r1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
        this.r1.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
        this.r1.Location = new System.Drawing.Point(57, 35);
        this.r1.Name = "r1";
        this.r1.ShowUpDown = true;
        this.r1.Size = new System.Drawing.Size(142, 21);
        this.r1.TabIndex = 12;
        this.r1.Value = 30;
        // 
        // r2
        // 
        // 
        // 
        // 
        this.r2.BackgroundStyle.Class = "DateTimeInputBackground";
        this.r2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
        this.r2.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
        this.r2.Location = new System.Drawing.Point(57, 62);
        this.r2.Name = "r2";
        this.r2.ShowUpDown = true;
        this.r2.Size = new System.Drawing.Size(142, 21);
        this.r2.TabIndex = 13;
        this.r2.Value = 30;
        // 
        // r3
        // 
        // 
        // 
        // 
        this.r3.BackgroundStyle.Class = "DateTimeInputBackground";
        this.r3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
        this.r3.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
        this.r3.Location = new System.Drawing.Point(57, 89);
        this.r3.Name = "r3";
        this.r3.ShowUpDown = true;
        this.r3.Size = new System.Drawing.Size(142, 21);
        this.r3.TabIndex = 14;
        this.r3.Value = 30;
        // 
        // r4
        // 
        // 
        // 
        // 
        this.r4.BackgroundStyle.Class = "DateTimeInputBackground";
        this.r4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
        this.r4.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
        this.r4.Location = new System.Drawing.Point(56, 116);
        this.r4.Name = "r4";
        this.r4.ShowUpDown = true;
        this.r4.Size = new System.Drawing.Size(142, 21);
        this.r4.TabIndex = 15;
        this.r4.Value = 30;
        // 
        // recipelist
        // 
        this.recipelist.BackColor = System.Drawing.Color.Transparent;
        this.recipelist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
        this.recipelist.Location = new System.Drawing.Point(252, 12);
        this.recipelist.Name = "recipelist";
        this.recipelist.PrimaryGrid.AllowRowHeaderResize = true;
        this.recipelist.PrimaryGrid.AllowRowResize = true;
        gridColumn1.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridComboBoxExEditControl);
        gridColumn1.HeaderText = "名称";
        gridColumn1.Name = "";
        gridColumn1.Width = 120;
        gridColumn2.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridComboBoxExEditControl);
        gridColumn2.HeaderText = "星级";
        gridColumn2.Name = "";
        gridColumn3.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
        gridColumn3.HeaderText = "头像";
        gridColumn3.Name = "";
        gridColumn3.Width = 248;
        gridColumn4.HeaderText = "燃油";
        gridColumn4.Name = "";
        gridColumn4.Width = 50;
        gridColumn5.HeaderText = "弹药";
        gridColumn5.Name = "";
        gridColumn5.Width = 50;
        gridColumn6.HeaderText = "钢铁";
        gridColumn6.Name = "";
        gridColumn6.Width = 50;
        gridColumn7.HeaderText = "铝材";
        gridColumn7.Name = "";
        gridColumn7.Width = 50;
        gridColumn8.HeaderText = "创建时间";
        gridColumn8.Name = "";
        gridColumn9.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
        gridColumn9.HeaderText = "套用";
        gridColumn9.Name = "";
        this.recipelist.PrimaryGrid.Columns.Add(gridColumn1);
        this.recipelist.PrimaryGrid.Columns.Add(gridColumn2);
        this.recipelist.PrimaryGrid.Columns.Add(gridColumn3);
        this.recipelist.PrimaryGrid.Columns.Add(gridColumn4);
        this.recipelist.PrimaryGrid.Columns.Add(gridColumn5);
        this.recipelist.PrimaryGrid.Columns.Add(gridColumn6);
        this.recipelist.PrimaryGrid.Columns.Add(gridColumn7);
        this.recipelist.PrimaryGrid.Columns.Add(gridColumn8);
        this.recipelist.PrimaryGrid.Columns.Add(gridColumn9);
        this.recipelist.PrimaryGrid.EnableColumnFiltering = true;
        this.recipelist.PrimaryGrid.EnableFiltering = true;
        this.recipelist.PrimaryGrid.Filter.ShowPanelFilterExpr = true;
        this.recipelist.PrimaryGrid.Filter.Visible = true;
        this.recipelist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
        this.recipelist.Size = new System.Drawing.Size(899, 708);
        this.recipelist.TabIndex = 16;
        this.recipelist.Text = "superGridControl2";
        // 
        // v1
        // 
        this.v1.AutoSize = true;
        this.v1.Location = new System.Drawing.Point(36, 155);
        this.v1.Name = "v1";
        this.v1.Size = new System.Drawing.Size(41, 12);
        this.v1.TabIndex = 17;
        this.v1.Text = "label5";
        // 
        // v2
        // 
        this.v2.AutoSize = true;
        this.v2.Location = new System.Drawing.Point(36, 185);
        this.v2.Name = "v2";
        this.v2.Size = new System.Drawing.Size(41, 12);
        this.v2.TabIndex = 18;
        this.v2.Text = "label6";
        // 
        // v3
        // 
        this.v3.AutoSize = true;
        this.v3.Location = new System.Drawing.Point(36, 216);
        this.v3.Name = "v3";
        this.v3.Size = new System.Drawing.Size(41, 12);
        this.v3.TabIndex = 19;
        this.v3.Text = "label7";
        // 
        // BuildForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1151, 719);
        this.Controls.Add(this.v3);
        this.Controls.Add(this.v2);
        this.Controls.Add(this.v1);
        this.Controls.Add(this.recipelist);
        this.Controls.Add(this.r4);
        this.Controls.Add(this.r3);
        this.Controls.Add(this.r2);
        this.Controls.Add(this.r1);
        this.Controls.Add(this.buttonX1);
        this.Controls.Add(this.equipcheck);
        this.Controls.Add(this.shipcheck);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Name = "BuildForm";
        this.Text = "玄不改命，氪不救非";
        ((System.ComponentModel.ISupportInitialize)(this.r1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.r2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.r3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.r4)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox shipcheck;
    private System.Windows.Forms.CheckBox equipcheck;
    private DevComponents.DotNetBar.ButtonX buttonX1;
    private DevComponents.Editors.IntegerInput r1;
    private DevComponents.Editors.IntegerInput r2;
    private DevComponents.Editors.IntegerInput r3;
    private DevComponents.Editors.IntegerInput r4;
    private DevComponents.DotNetBar.SuperGrid.SuperGridControl recipelist;
    private System.Windows.Forms.Label v1;
    private System.Windows.Forms.Label v2;
    private System.Windows.Forms.Label v3;
}