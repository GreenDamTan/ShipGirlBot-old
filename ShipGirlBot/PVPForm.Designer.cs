
partial class PVPForm
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
            this.pvplist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.formationcombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pvplist
            // 
            this.pvplist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.pvplist.Location = new System.Drawing.Point(207, 12);
            this.pvplist.Name = "pvplist";
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
            gridColumn4.Width = 450;
            gridColumn5.HeaderText = "挑战";
            gridColumn5.Name = "";
            gridColumn5.Width = 60;
            this.pvplist.PrimaryGrid.Columns.Add(gridColumn1);
            this.pvplist.PrimaryGrid.Columns.Add(gridColumn2);
            this.pvplist.PrimaryGrid.Columns.Add(gridColumn3);
            this.pvplist.PrimaryGrid.Columns.Add(gridColumn4);
            this.pvplist.PrimaryGrid.Columns.Add(gridColumn5);
            this.pvplist.Size = new System.Drawing.Size(798, 582);
            this.pvplist.TabIndex = 11;
            this.pvplist.Text = "superGridControl1";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(13, 68);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(136, 37);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 12;
            this.buttonX1.Text = "buttonX1";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(13, 111);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(136, 37);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 13;
            this.buttonX2.Text = "buttonX2";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(13, 154);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(136, 37);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 14;
            this.buttonX3.Text = "buttonX3";
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX4.Location = new System.Drawing.Point(12, 197);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(136, 37);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 15;
            this.buttonX4.Text = "buttonX4";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(27, 262);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 17;
            this.labelX1.Text = "阵型";
            // 
            // checkBoxX1
            // 
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.Location = new System.Drawing.Point(27, 319);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(100, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 18;
            this.checkBoxX1.Text = "夜战";
            // 
            // formationcombo
            // 
            this.formationcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formationcombo.FormattingEnabled = true;
            this.formationcombo.Location = new System.Drawing.Point(26, 291);
            this.formationcombo.Name = "formationcombo";
            this.formationcombo.Size = new System.Drawing.Size(122, 20);
            this.formationcombo.TabIndex = 19;
            // 
            // PVPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 595);
            this.Controls.Add(this.formationcombo);
            this.Controls.Add(this.checkBoxX1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.buttonX4);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.pvplist);
            this.Name = "PVPForm";
            this.Text = "PVP";
            this.ResumeLayout(false);

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl pvplist;
    private DevComponents.DotNetBar.ButtonX buttonX1;
    private DevComponents.DotNetBar.ButtonX buttonX2;
    private DevComponents.DotNetBar.ButtonX buttonX3;
    private DevComponents.DotNetBar.ButtonX buttonX4;
    private DevComponents.DotNetBar.LabelX labelX1;
    private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
    private System.Windows.Forms.ComboBox formationcombo;
}