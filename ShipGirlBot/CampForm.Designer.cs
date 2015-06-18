
partial class CampForm
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
            this.selshiplist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.camplist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.buttonX6 = new System.Windows.Forms.PictureBox();
            this.buttonX5 = new System.Windows.Forms.PictureBox();
            this.buttonX4 = new System.Windows.Forms.PictureBox();
            this.buttonX3 = new System.Windows.Forms.PictureBox();
            this.buttonX2 = new System.Windows.Forms.PictureBox();
            this.buttonX1 = new System.Windows.Forms.PictureBox();
            this.formationcombo = new System.Windows.Forms.ComboBox();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX1)).BeginInit();
            this.SuspendLayout();
            // 
            // selshiplist
            // 
            this.selshiplist.Dock = System.Windows.Forms.DockStyle.Left;
            this.selshiplist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.selshiplist.Location = new System.Drawing.Point(0, 0);
            this.selshiplist.Name = "selshiplist";
            gridColumn1.HeaderText = "星级";
            gridColumn1.Name = "";
            gridColumn2.HeaderText = "等级";
            gridColumn2.Name = "";
            gridColumn2.Width = 50;
            gridColumn3.HeaderText = "名称";
            gridColumn3.Name = "";
            gridColumn4.HeaderText = "类型";
            gridColumn4.Name = "";
            gridColumn4.Width = 30;
            gridColumn5.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
            gridColumn5.HeaderText = "少女";
            gridColumn5.Name = "";
            gridColumn5.Width = 124;
            gridColumn6.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridProgressBarXEditControl);
            gridColumn6.HeaderText = "血量";
            gridColumn6.Name = "";
            gridColumn7.HeaderText = "HP";
            gridColumn7.Name = "";
            gridColumn7.Width = 50;
            gridColumn8.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn8.HeaderText = "选择";
            gridColumn8.Name = "";
            gridColumn9.HeaderText = "id";
            gridColumn9.Name = "";
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn1);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn2);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn3);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn4);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn5);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn6);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn7);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn8);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn9);
            this.selshiplist.Size = new System.Drawing.Size(789, 624);
            this.selshiplist.TabIndex = 8;
            this.selshiplist.Text = "superGridControl1";
            // 
            // camplist
            // 
            this.camplist.Dock = System.Windows.Forms.DockStyle.Right;
            this.camplist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.camplist.Location = new System.Drawing.Point(1066, 0);
            this.camplist.Name = "camplist";
            gridColumn10.HeaderText = "id";
            gridColumn10.Name = "";
            gridColumn10.Width = 50;
            gridColumn11.HeaderText = "名称";
            gridColumn11.Name = "";
            gridColumn11.Width = 200;
            gridColumn12.HeaderText = "难度";
            gridColumn12.Name = "";
            this.camplist.PrimaryGrid.Columns.Add(gridColumn10);
            this.camplist.PrimaryGrid.Columns.Add(gridColumn11);
            this.camplist.PrimaryGrid.Columns.Add(gridColumn12);
            this.camplist.PrimaryGrid.MultiSelect = false;
            this.camplist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.camplist.Size = new System.Drawing.Size(383, 624);
            this.camplist.TabIndex = 9;
            this.camplist.Text = "superGridControl1";
            this.camplist.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.camplist_RowClick);
            // 
            // buttonX6
            // 
            this.buttonX6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonX6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonX6.Location = new System.Drawing.Point(855, 264);
            this.buttonX6.Name = "buttonX6";
            this.buttonX6.Size = new System.Drawing.Size(124, 32);
            this.buttonX6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonX6.TabIndex = 15;
            this.buttonX6.TabStop = false;
            this.buttonX6.Text = "buttonX6";
            // 
            // buttonX5
            // 
            this.buttonX5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonX5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonX5.Location = new System.Drawing.Point(855, 104);
            this.buttonX5.Name = "buttonX5";
            this.buttonX5.Size = new System.Drawing.Size(124, 32);
            this.buttonX5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonX5.TabIndex = 14;
            this.buttonX5.TabStop = false;
            this.buttonX5.Text = "buttonX5";
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonX4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonX4.Location = new System.Drawing.Point(855, 144);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(124, 32);
            this.buttonX4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonX4.TabIndex = 13;
            this.buttonX4.TabStop = false;
            this.buttonX4.Text = "buttonX4";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonX3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonX3.Location = new System.Drawing.Point(855, 184);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(124, 32);
            this.buttonX3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonX3.TabIndex = 12;
            this.buttonX3.TabStop = false;
            this.buttonX3.Text = "buttonX3";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonX2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonX2.Location = new System.Drawing.Point(855, 224);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(124, 32);
            this.buttonX2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonX2.TabIndex = 11;
            this.buttonX2.TabStop = false;
            this.buttonX2.Text = "buttonX2";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonX1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonX1.Location = new System.Drawing.Point(855, 64);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(124, 32);
            this.buttonX1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonX1.TabIndex = 10;
            this.buttonX1.TabStop = false;
            this.buttonX1.Text = "buttonX1";
            // 
            // formationcombo
            // 
            this.formationcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formationcombo.FormattingEnabled = true;
            this.formationcombo.Location = new System.Drawing.Point(854, 354);
            this.formationcombo.Name = "formationcombo";
            this.formationcombo.Size = new System.Drawing.Size(122, 20);
            this.formationcombo.TabIndex = 22;
            // 
            // checkBoxX1
            // 
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.Location = new System.Drawing.Point(855, 382);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(100, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 21;
            this.checkBoxX1.Text = "夜战";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(855, 325);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 20;
            this.labelX1.Text = "阵型";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(855, 429);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 66);
            this.button1.TabIndex = 23;
            this.button1.Text = "出击";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(855, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "本日剩余次数:12";
            // 
            // CampForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1449, 624);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.formationcombo);
            this.Controls.Add(this.checkBoxX1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.buttonX6);
            this.Controls.Add(this.buttonX5);
            this.Controls.Add(this.buttonX4);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.camplist);
            this.Controls.Add(this.selshiplist);
            this.Name = "CampForm";
            this.Text = "CampForm";
            ((System.ComponentModel.ISupportInitialize)(this.buttonX6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonX1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl selshiplist;
    private DevComponents.DotNetBar.SuperGrid.SuperGridControl camplist;
    private System.Windows.Forms.PictureBox buttonX6;
    private System.Windows.Forms.PictureBox buttonX5;
    private System.Windows.Forms.PictureBox buttonX4;
    private System.Windows.Forms.PictureBox buttonX3;
    private System.Windows.Forms.PictureBox buttonX2;
    private System.Windows.Forms.PictureBox buttonX1;
    private System.Windows.Forms.ComboBox formationcombo;
    private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
    private DevComponents.DotNetBar.LabelX labelX1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label1;
}