
partial class Enchant
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
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn18 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn19 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn20 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn21 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn22 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn23 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn24 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn25 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn26 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn27 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn28 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn29 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn30 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn31 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn32 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
        this.selshiplist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
        this.feedlist = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
        this.shiphead = new System.Windows.Forms.PictureBox();
        this.eatbutton = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.skilllvupbtn = new System.Windows.Forms.Button();
        this.evobtn = new System.Windows.Forms.Button();
        this.labelX1 = new DevComponents.DotNetBar.LabelX();
        this.labelX2 = new DevComponents.DotNetBar.LabelX();
        ((System.ComponentModel.ISupportInitialize)(this.shiphead)).BeginInit();
        this.SuspendLayout();
        // 
        // selshiplist
        // 
        this.selshiplist.Dock = System.Windows.Forms.DockStyle.Left;
        this.selshiplist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
        this.selshiplist.Location = new System.Drawing.Point(0, 0);
        this.selshiplist.Name = "selshiplist";
        gridColumn17.HeaderText = "星级";
        gridColumn17.Name = "";
        gridColumn18.HeaderText = "等级";
        gridColumn18.Name = "";
        gridColumn18.Width = 50;
        gridColumn19.HeaderText = "名称";
        gridColumn19.Name = "";
        gridColumn20.HeaderText = "类型";
        gridColumn20.Name = "";
        gridColumn20.Width = 30;
        gridColumn21.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridImageEditControl);
        gridColumn21.HeaderText = "少女";
        gridColumn21.Name = "";
        gridColumn21.Width = 124;
        gridColumn22.HeaderText = "火力";
        gridColumn22.Name = "";
        gridColumn22.Width = 80;
        gridColumn23.HeaderText = "雷装";
        gridColumn23.Name = "";
        gridColumn23.Width = 80;
        gridColumn24.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
        gridColumn24.HeaderText = "装甲";
        gridColumn24.Name = "";
        gridColumn24.Width = 80;
        gridColumn25.HeaderText = "对空";
        gridColumn25.Name = "";
        gridColumn25.Width = 80;
        gridColumn26.HeaderText = "id";
        gridColumn26.Name = "";
        gridColumn26.Width = 80;
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn17);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn18);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn19);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn20);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn21);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn22);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn23);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn24);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn25);
        this.selshiplist.PrimaryGrid.Columns.Add(gridColumn26);
        this.selshiplist.PrimaryGrid.MultiSelect = false;
        this.selshiplist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
        this.selshiplist.Size = new System.Drawing.Size(815, 660);
        this.selshiplist.TabIndex = 9;
        this.selshiplist.Text = "superGridControl1";
        this.selshiplist.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.selshiplist_RowClick);
        // 
        // feedlist
        // 
        this.feedlist.Dock = System.Windows.Forms.DockStyle.Right;
        this.feedlist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
        this.feedlist.Location = new System.Drawing.Point(1016, 0);
        this.feedlist.Name = "feedlist";
        gridColumn27.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
        gridColumn27.HeaderText = "吃掉";
        gridColumn27.Name = "";
        gridColumn27.Width = 50;
        gridColumn28.HeaderText = "等级";
        gridColumn28.Name = "";
        gridColumn28.Width = 30;
        gridColumn29.HeaderText = "类型";
        gridColumn29.Name = "";
        gridColumn29.Width = 30;
        gridColumn30.HeaderText = "名称";
        gridColumn30.Name = "";
        gridColumn30.Width = 80;
        gridColumn31.HeaderText = "属性";
        gridColumn31.Name = "";
        gridColumn31.Width = 220;
        gridColumn32.HeaderText = "id";
        gridColumn32.Name = "";
        gridColumn32.Width = 50;
        this.feedlist.PrimaryGrid.Columns.Add(gridColumn27);
        this.feedlist.PrimaryGrid.Columns.Add(gridColumn28);
        this.feedlist.PrimaryGrid.Columns.Add(gridColumn29);
        this.feedlist.PrimaryGrid.Columns.Add(gridColumn30);
        this.feedlist.PrimaryGrid.Columns.Add(gridColumn31);
        this.feedlist.PrimaryGrid.Columns.Add(gridColumn32);
        this.feedlist.PrimaryGrid.MultiSelect = false;
        this.feedlist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
        this.feedlist.Size = new System.Drawing.Size(490, 660);
        this.feedlist.TabIndex = 10;
        this.feedlist.Text = "superGridControl1";
        // 
        // shiphead
        // 
        this.shiphead.Location = new System.Drawing.Point(824, 0);
        this.shiphead.Name = "shiphead";
        this.shiphead.Size = new System.Drawing.Size(192, 256);
        this.shiphead.TabIndex = 11;
        this.shiphead.TabStop = false;
        // 
        // eatbutton
        // 
        this.eatbutton.Location = new System.Drawing.Point(824, 501);
        this.eatbutton.Name = "eatbutton";
        this.eatbutton.Size = new System.Drawing.Size(179, 46);
        this.eatbutton.TabIndex = 12;
        this.eatbutton.Text = "吃掉";
        this.eatbutton.UseVisualStyleBackColor = true;
        this.eatbutton.Click += new System.EventHandler(this.eatbutton_Click);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(823, 263);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(41, 12);
        this.label1.TabIndex = 13;
        this.label1.Text = "label1";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(823, 279);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(41, 12);
        this.label2.TabIndex = 14;
        this.label2.Text = "label2";
        this.label2.Click += new System.EventHandler(this.label2_Click);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(823, 295);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(41, 12);
        this.label3.TabIndex = 15;
        this.label3.Text = "label3";
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(823, 311);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(41, 12);
        this.label4.TabIndex = 16;
        this.label4.Text = "label4";
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(824, 331);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(41, 12);
        this.label5.TabIndex = 17;
        this.label5.Text = "label5";
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(824, 347);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(41, 12);
        this.label7.TabIndex = 19;
        this.label7.Text = "label7";
        // 
        // skilllvupbtn
        // 
        this.skilllvupbtn.Location = new System.Drawing.Point(826, 556);
        this.skilllvupbtn.Name = "skilllvupbtn";
        this.skilllvupbtn.Size = new System.Drawing.Size(179, 46);
        this.skilllvupbtn.TabIndex = 20;
        this.skilllvupbtn.Text = "技能升级";
        this.skilllvupbtn.UseVisualStyleBackColor = true;
        this.skilllvupbtn.Click += new System.EventHandler(this.skilllvupbtn_Click);
        // 
        // evobtn
        // 
        this.evobtn.Location = new System.Drawing.Point(826, 614);
        this.evobtn.Name = "evobtn";
        this.evobtn.Size = new System.Drawing.Size(179, 46);
        this.evobtn.TabIndex = 21;
        this.evobtn.Text = "改造";
        this.evobtn.UseVisualStyleBackColor = true;
        this.evobtn.Click += new System.EventHandler(this.evobtn_Click);
        // 
        // labelX1
        // 
        // 
        // 
        // 
        this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
        this.labelX1.Location = new System.Drawing.Point(826, 439);
        this.labelX1.Name = "labelX1";
        this.labelX1.Size = new System.Drawing.Size(190, 56);
        this.labelX1.TabIndex = 22;
        this.labelX1.Text = "不可改造";
        this.labelX1.WordWrap = true;
        // 
        // labelX2
        // 
        // 
        // 
        // 
        this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
        this.labelX2.Location = new System.Drawing.Point(826, 362);
        this.labelX2.Name = "labelX2";
        this.labelX2.Size = new System.Drawing.Size(179, 78);
        this.labelX2.TabIndex = 23;
        this.labelX2.Text = "不可改造";
        this.labelX2.WordWrap = true;
        // 
        // Enchant
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoScroll = true;
        this.AutoSize = true;
        this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.ClientSize = new System.Drawing.Size(1028, 675);
        this.Controls.Add(this.labelX2);
        this.Controls.Add(this.labelX1);
        this.Controls.Add(this.evobtn);
        this.Controls.Add(this.skilllvupbtn);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.eatbutton);
        this.Controls.Add(this.shiphead);
        this.Controls.Add(this.feedlist);
        this.Controls.Add(this.selshiplist);
        this.Name = "Enchant";
        this.Text = "Enchant";
        ((System.ComponentModel.ISupportInitialize)(this.shiphead)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private DevComponents.DotNetBar.SuperGrid.SuperGridControl selshiplist;
    private DevComponents.DotNetBar.SuperGrid.SuperGridControl feedlist;
    private System.Windows.Forms.PictureBox shiphead;
    private System.Windows.Forms.Button eatbutton;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button skilllvupbtn;
    private System.Windows.Forms.Button evobtn;
    private DevComponents.DotNetBar.LabelX labelX1;
    private DevComponents.DotNetBar.LabelX labelX2;
}