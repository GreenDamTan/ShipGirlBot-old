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


public partial class CampForm : Form
{
    PictureBox[] ships = new PictureBox[6];

    private int cur_lvid =0;
    private int cur_shipindex = -1;

    public CampForm()
    {
        InitializeComponent();

        ships[0] = buttonX1;
        ships[1] = buttonX2;
        ships[2] = buttonX3;
        ships[3] = buttonX4;
        ships[4] = buttonX5;
        ships[5] = buttonX6;
        int i = 0;
        foreach (PictureBox b in ships)
        {
            b.Tag = i;
            b.Click += this.shipButtonClick;
            b.Location = new Point(855, b.Location.Y);
            i++;
        }

        formationcombo.Items.Clear();
        formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.OneRow));
        formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.TwoRow));
        formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.Cicle));
        formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.TStyle));
        formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.OneColume));

        formationcombo.SelectedIndex = 1;

        GridColumn column1 = selshiplist.PrimaryGrid.Columns[4];
        if (column1.EditControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        if (column1.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }

        GridColumn column3 = selshiplist.PrimaryGrid.Columns[7];
        column3.EditorType = typeof(SwapShipButton);
        column3.EditorParams = new object[] { this };

        ServerRequestManager.instance.GetCampaignData();

        updateCampInfo();

        updateShipList();


    }

    void shipButtonClick(object sender, EventArgs e)
    {
        foreach (PictureBox b in ships)
        {
            b.Location = new Point(855, b.Location.Y);
        }
        (sender as PictureBox).Location = new Point(830, (sender as PictureBox).Location.Y);
        cur_shipindex = (int)(sender as PictureBox).Tag;

        updateShipList();
        
    }


    private void updateCampInfo()
    {
        GridPanel panel = camplist.PrimaryGrid;
        panel.Rows.Clear();
        
        foreach(var cl in PVEConfigs.instance.CampaignLevels)
        {
            object[] vals = new object[19];

            vals[0] = cl.id;
            vals[1] = cl.title;
            vals[2] = cl.difficulty == 1?"简单":"困难";


            GridRow gr = new GridRow(vals);
            panel.Rows.Add(gr);
            gr.RowHeight = 30;
        }
    }
    private void updateCampFleet(int lvid)
    {



        PVECampaignLevel lv = PVEConfigs.instance.GetCampaignLevel(lvid);
        if(lv == null)
        {
            return;
        }
        cur_lvid = lvid;
        ServerRequestManager.instance.GetCampaignFleet(lv);

        var camp = GameData.instance.GetCampaignInfo(lv.campaignId);
        var totalcamp = GameData.instance.TotalCampainInfo;
        label1.Text = "本日剩余次数:" + (camp==null?"??":(camp.remainNum.ToString()+ "/"+ (camp.remainNum+ totalcamp.passNum).ToString()));
 

        int[] campships = GameData.instance.GetCampaignFleetInfo(lv.id);
        if(campships == null)
        {
            campships = new int[6] { 0, 0, 0, 0, 0, 0 };
        }
        for(int i=0;i<6;i++)
        {
            int shiprule = lv.fleetRuleRe[i];
            ships[i].Text = shiprule == 0 ? "任意" : tools.helper.getshiptype((ShipType)shiprule);
            if(campships.Length>i)
            {
                UserShip us = GameData.instance.GetShipById(campships[i]);

                ships[i].Image = us == null ? tools.helper.OpenImage("res/png/unknown/899.png") : tools.helper.getShipSmallImage(us);
            }
            else
            {
                ships[i].Image = tools.helper.OpenImage("res/png/unknown/899.png");

            }
        }
    }
    private void updateShipList()
    {
        GridPanel panel = selshiplist.PrimaryGrid;
        panel.Rows.Clear();

        PVECampaignLevel lv = PVEConfigs.instance.GetCampaignLevel(cur_lvid);
        if(lv == null)
        {
            return;
        }
        int i = 0;
        int[] campships = GameData.instance.GetCampaignFleetInfo(lv.id);
        if (campships == null)
        {
            campships = new int[6] { 0, 0, 0, 0, 0, 0 };
        }

        foreach (UserShip us in GameData.instance.UserShips.OrderByDescending(o => o.level).ToList())
        {
            int shiptypeallow = lv.fleetRuleRe[cur_shipindex];
            if (shiptypeallow == 0 || (shiptypeallow == (int) us.ship.type))
            {
                if (campships.Contains(us.id)== false)
                {
	                var gr = getonerow(us);
	                panel.Rows.Add(gr);
	                i++;
                }
            }
        }


    }

    public static GridRow getonerow(UserShip us)
    {
        object[] vals = new object[19];

        vals[0] = tools.helper.getstartstring(us.ship.star);
        vals[1] = us.level;
        vals[2] = us.ship.title;
        vals[3] = tools.helper.getshiptype(us.ship.type);
        vals[4] = tools.helper.getShipSmallImage(us);
        vals[5] = 100 * us.battleProps.hp / us.battlePropsMax.hp;
        vals[6] = "" + us.battleProps.hp + "/" + us.battlePropsMax.hp;
        vals[7] = "更换";// + "/" + us.battlePropsMax.atk;
        vals[8] = us.id;

        GridRow gr = new GridRow(vals);
        gr.RowHeight = 30;
        return gr;
    }











    internal class SwapShipButton : GridButtonXEditControl
    {
        private CampForm form;
        public SwapShipButton(CampForm par)
        {
            form = par;

            Click += new EventHandler(CommandButtonClick);
        }

        #region CommandButtonClick

        void CommandButtonClick(object sender, EventArgs e)
        {
            int shipid = (int)EditorCell.GridRow.Cells[8].Value;

            UserShip us = GameData.instance.GetShipById(shipid);

            if (us == null)
            {
                MessageBox.Show("错误的船只信息");
                return;
            }
            var lv = form.get_curlv();
            if(lv == null)
            {
                MessageBox.Show("错误的关卡信息");
                return;
            }
            ServerRequestManager.instance.UpdateCampaignFleet(lv,shipid,form.get_cur_index());
            form.updateCampFleet(lv.id);
            form.updateShipList();
        }

        #endregion
    }

    private void camplist_RowClick(object sender, GridRowClickEventArgs e)
    {
        updateCampFleet((int)e.GridPanel.GetCell(e.GridRow.RowIndex,0).Value);
    }

    internal PVECampaignLevel get_curlv()
    {
        return PVEConfigs.instance.GetCampaignLevel(cur_lvid);
    }

    internal int get_cur_index()
    {
        return cur_shipindex;
    }

    private void button1_Click(object sender, EventArgs e)
    {

        PVECampaignLevel lv = PVEConfigs.instance.GetCampaignLevel(cur_lvid);
        if (lv == null)
        {
            MessageBox.Show("先选择关卡");
            return;
        }
        int i = 0;
        int[] campships = GameData.instance.GetCampaignFleetInfo(lv.id);
        if (campships == null|| campships.Sum() ==0)
        {
            MessageBox.Show("请选择舰队...");
            return;
        }

        var camp = GameData.instance.GetCampaignInfo(lv.campaignId);
        var totalcamp = GameData.instance.TotalCampainInfo;

        if (camp.remainNum == 0)
        {
            MessageBox.Show("本日次数已经耗光");
            return;

        }

        CurrentWarParameters.selectedCampaignLevel = lv;

        List<UserShip> shiptosupply = new List<UserShip>();
        foreach(int stoi in campships)
        {
            UserShip us = GameData.instance.GetShipById(stoi);
            if(us != null)
            {
                shiptosupply.Add(us);
            }
        }

        ServerRequestManager.instance.SupplyMulti(shiptosupply);
        MessageBox.Show("战役少女吃喝完毕，稍等后点击确认继续....");
                var rs = ServerRequestManager.instance.SearchCampaign(lv.id);
                if (rs != null && rs.responseData != null && rs.responseData.eid == 0)
                {
                    MessageBox.Show("索敌中，稍等继续....");
                    var r = ServerRequestManager.instance.StartCampaignWar(lv.id, formationcombo.SelectedIndex + 1);
                    if (r != null && r.responseData != null && r.responseData.eid == 0)
                    {
                        MessageBox.Show("请等待足够战斗时间再点确认....");
                        var ret = ServerRequestManager.instance.GetCampaignWarResult(checkBoxX1.Checked);
                        if (r != null && r.responseData != null && r.responseData.eid == 0)
                        {
                            MessageBox.Show(tools.helper.getCampwarresultstring(ret.responseData as GetCampaignWarResultResponse));
                        }
                        updateCampFleet(lv.id);
                    }
                }
    }
}