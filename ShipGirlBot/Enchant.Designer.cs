
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
            gridColumn6.HeaderText = "火力";
            gridColumn6.Name = "";
            gridColumn6.Width = 80;
            gridColumn7.HeaderText = "雷装";
            gridColumn7.Name = "";
            gridColumn7.Width = 80;
            gridColumn8.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn8.HeaderText = "装甲";
            gridColumn8.Name = "";
            gridColumn8.Width = 80;
            gridColumn9.HeaderText = "对空";
            gridColumn9.Name = "";
            gridColumn9.Width = 80;
            gridColumn10.HeaderText = "id";
            gridColumn10.Name = "";
            gridColumn10.Width = 80;
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn1);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn2);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn3);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn4);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn5);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn6);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn7);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn8);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn9);
            this.selshiplist.PrimaryGrid.Columns.Add(gridColumn10);
            this.selshiplist.PrimaryGrid.MultiSelect = false;
            this.selshiplist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.selshiplist.Size = new System.Drawing.Size(815, 675);
            this.selshiplist.TabIndex = 9;
            this.selshiplist.Text = "superGridControl1";
            this.selshiplist.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.selshiplist_RowClick);
            // 
            // feedlist
            // 
            this.feedlist.Dock = System.Windows.Forms.DockStyle.Right;
            this.feedlist.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.feedlist.Location = new System.Drawing.Point(1022, 0);
            this.feedlist.Name = "feedlist";
            gridColumn11.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            gridColumn11.HeaderText = "吃掉";
            gridColumn11.Name = "";
            gridColumn11.Width = 50;
            gridColumn12.HeaderText = "等级";
            gridColumn12.Name = "";
            gridColumn12.Width = 30;
            gridColumn13.HeaderText = "类型";
            gridColumn13.Name = "";
            gridColumn13.Width = 30;
            gridColumn14.HeaderText = "名称";
            gridColumn14.Name = "";
            gridColumn14.Width = 80;
            gridColumn15.HeaderText = "属性";
            gridColumn15.Name = "";
            gridColumn15.Width = 220;
            gridColumn16.HeaderText = "id";
            gridColumn16.Name = "";
            gridColumn16.Width = 50;
            this.feedlist.PrimaryGrid.Columns.Add(gridColumn11);
            this.feedlist.PrimaryGrid.Columns.Add(gridColumn12);
            this.feedlist.PrimaryGrid.Columns.Add(gridColumn13);
            this.feedlist.PrimaryGrid.Columns.Add(gridColumn14);
            this.feedlist.PrimaryGrid.Columns.Add(gridColumn15);
            this.feedlist.PrimaryGrid.Columns.Add(gridColumn16);
            this.feedlist.PrimaryGrid.MultiSelect = false;
            this.feedlist.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.feedlist.Size = new System.Drawing.Size(490, 675);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1512, 675);
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