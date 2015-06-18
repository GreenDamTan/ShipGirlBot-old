using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System.Threading;

public partial class PVPForm : Form
{
    private int mfleetid;

    ButtonX[] ships = new ButtonX[4];


    private int cur_index = -1;
    public PVPForm()
    {
        InitializeComponent();
        ships[0] = buttonX1;
        ships[1] = buttonX2;
        ships[2] = buttonX3;
        ships[3] = buttonX4;

        formationcombo.Items.Clear();
        formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.OneRow));
        formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.TwoRow));
        formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.Cicle));
        formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.TStyle));
        formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.OneColume));

        formationcombo.SelectedIndex = 1;

        ServerRequestManager.instance.GetPVPList();


        int i = 1;
        foreach (ButtonX b in ships)
        {
            b.Tag = i;
            b.Click += this.shipButtonClick;
            b.Location = new Point(12, b.Location.Y);
            i++;
        }

        GridColumn column2 = pvplist.PrimaryGrid.Columns[4];
        column2.EditorType = typeof(PVPButton);
        column2.EditorParams = new object[] { mfleetid, this };

        updateCurFleetInfo();



        updateUserShipInfo();
    }


    internal void updateCurFleetInfo()
    {
        foreach (ButtonX b in ships)
        {
            b.Image = null;
            b.Location = new Point( 12,b.Location.Y);
            b.Visible = false;
        }
        int i = 0;
        foreach (var uf in GameData.instance.UserFleets)
        {
            i++;
            if (uf.ships == null
                || uf.ships.Length == 0
                || GameData.instance.IsFleetInRepair(uf.id)
                || GameData.instance.IsFleetInExplore(uf.id)
                )
            {
                continue;
            }
            UserShip us = GameData.instance.GetShipById(uf.ships[0]);
            if (us != null)
            {
                ships[i - 1].Visible = true;
                ships[i - 1].Image = tools.helper.getShipSmallImage(us);
            }

        }
    }

    private string getpvptstring(PVPOpponent p)
    {
        string r = "";
        if (p != null && p.ships != null && p.ships.Length > 0)
        {
            foreach (UserShip us in p.ships)
            {
                r += tools.helper.getstartstring(us.ship.star)
                    + "\t\t" +tools.helper.getshiptype(us.ship.type) + " \t"
                    + us.ship.title + "\t Lv." + us.level + " \r\n";
            }
        }
        return r;
    }
    public void updateUserShipInfo()
    {
        GridPanel panel = pvplist.PrimaryGrid;
        panel.Rows.Clear();

        int i = 0;
        if (GameData.instance.pvpOpponents == null || GameData.instance.pvpOpponents.Count == 0)
        {
            return;
        }
        foreach (var us in GameData.instance.pvpOpponents)
        {
            object[] vals = new object[5];

            vals[0] = us.uid;
            vals[1] = us.username;
            vals[2] = us.level;
            vals[3] = getpvptstring(us);
            vals[4] = us.resultLevel == WarResultLevel.none ? "挑战" : us.resultLevel.ToString();

            GridRow gr = new GridRow(vals);
            gr.RowHeight = 100;

            panel.Rows.Add(gr);
            i++;
        }


    }

    void shipButtonClick(object sender, EventArgs e)
    {
        foreach (ButtonX b in ships)
        {
            b.Checked = false;
            b.Location = new Point(12, b.Location.Y);
        }
        (sender as ButtonX).Checked = true;
        (sender as ButtonX).Location = new Point(32, (sender as ButtonX).Location.Y);
        cur_index = (int)(sender as ButtonX).Tag;

    }

    public int get_cur_index()
    {
        return cur_index;
    }

    internal int get_formation()
    {
        return formationcombo.SelectedIndex +1;
    }

    internal bool get_is_night()
    {
        return checkBoxX1.Checked;
    }

    internal class PVPButton : GridButtonXEditControl
    {
        private int fleetid;
        private PVPForm form;
        public PVPButton(int flid, PVPForm par)
        {
            fleetid = flid;
            form = par;

            Click += new EventHandler(CommandButtonClick);
        }

        #region CommandButtonClick

        void CommandButtonClick(object sender, EventArgs e)
        {
            int shipid = (int)EditorCell.GridRow.Cells[0].Value;

            if(form.get_cur_index() <=0)
            {
                MessageBox.Show("选择出战舰队");
                return;
            }

            if ((EditorCell.GridRow.Cells[4].Value as string) != "挑战")
            {
                MessageBox.Show("打过了....");
                return;
            }


            var opp = GameData.instance.getPVPOpponentbyuid(shipid);
            if(opp == null)
            {
                MessageBox.Show("找不到对战对象，请关闭刷新");
                return;
            }
            CurrentWarParameters.selectedOpponent = opp;
            var rs = ServerRequestManager.instance.SearchPVPEnemy(shipid, form.get_cur_index());
            if (rs != null && rs.responseData != null && rs.responseData.eid == 0)
            {
                MessageBox.Show("索敌中...稍等确认");
                var r = ServerRequestManager.instance.StartPVPWar(shipid, form.get_cur_index(), form.get_formation());
                if (r != null && r.responseData != null && r.responseData.eid == 0)
                {
                    string pvpdaylog = tools.helper.getPVPDetailDayWarresultstring(r.responseData as StartPVPWarResponse, form.get_cur_index());
                    DialogResult slret = MessageBox.Show("请等待足够战斗时间再点确认....或点击取消SL-----------------------------------------------------------------------------------------------\r\n" +
                        pvpdaylog, "是否SL --  PVP日战战报... ---------------------------------------------------------------------------------------------", MessageBoxButtons.OKCancel);
                    z.log(pvpdaylog);
                    if(slret == System.Windows.Forms.DialogResult.OK)
                    {
                        var ret = ServerRequestManager.instance.GetPVPWarResult(form.get_is_night());
                        if (r != null && r.responseData != null && r.responseData.eid == 0)
                        {
                            MessageBox.Show(tools.helper.getpvpwarresultstring(ret.responseData as GetPVPWarResultResponse, form.get_cur_index()));
                        }
                        form.updateUserShipInfo();
                    }
                    else
                    {
                        ServerRequestManager.instance.NotifyPVEBackHome();
                        form.updateUserShipInfo();
                        MessageBox.Show("取消战斗，SL");
                    }

                }
            }
        }

        #endregion
    }


}