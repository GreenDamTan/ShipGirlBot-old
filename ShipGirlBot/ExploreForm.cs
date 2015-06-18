using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;


public partial class ExploreForm : Form
{
    private int mfleetid;

    private UserFleet uf;
    public ExploreForm(int fleetid)
    {
        mfleetid = fleetid;
        InitializeComponent();

        GridColumn c11 = explorelist.PrimaryGrid.Columns[11];
        if (c11 != null)
        {
            c11.EditorType = typeof(DoExplorerCommandButton);
            c11.EditorParams = new object[] { fleetid, this };
        }
    }

    public void setfleet_id(int id)
    {
        mfleetid = id;


        uf = GameData.instance.GetFleetOfId(mfleetid);
        if (uf == null)
        {
            return;
        }
        Text = "舰队远征 -- " + uf.title;
        int i = 0;
        updateExploreInfo();
    }
    private string getrewardstring(PVEExploreLevelConfig c)
    {
        string r = "";
        try
        {
            foreach (var v in c.award)
            {
                int t = int.Parse(v.Key);
                r += tools.helper.getresourcetype((ResourceTypes)t) + ":" + v.Value + " ";
            }

        }
        catch (Exception)
        {

        }
        return r;
    }
    private string gettyperequiredstring(PVEExploreLevelConfig c)
    {
        string r = "";
        foreach (var v in c.needShipType)
        {
            r += tools.helper.getshiptype((ShipType)v.type) + ":" + v.num + " ";
        }
        return r;
    }

    private string getrandawardstring(PVEExploreLevelConfig c)
    {
        string r = "";
        foreach (var v in c.randAward)
        {
            int cid = v.cid;
            if (cid > 0)
            {
                r += tools.helper.getresourcetype((ResourceTypes)cid) + ":";
                int tr = 0;
                foreach (var rate in v.rate)
                {
                    tr += rate["num"] * rate["rate"];
                    //r += rate["num"] == 0 ? "无" : rate["num"] + "-%" + rate["rate"] + " ";
                }
                r += tr + "%";
            }
            break;
            //r += v.Key + ":" + v.Value + " ";
        }
        if (r == "")
        {
            foreach (int cid in c.pruductGoods)
            {
                string item = tools.helper.getresourcetype((ResourceTypes)cid);
                if (item != null)
                {
                    r += item + " ";
                }
            }
        }
        return r;
    }
    private string getoilammocost(PVEExploreLevelConfig c)
    {
        string r = "";
        r += "油:" + c.needOil + " 弹" + c.needAmmo;
        return r;
    }
    private void updateExploreInfo()
    {
        Dictionary<int, int> explist = new Dictionary<int, int>();
        foreach (var expl in GameData.instance.GetUserPVEExplores())
        {
            explist[expl.exploreId] = expl.fleetId;
        }
        GridPanel panel = explorelist.PrimaryGrid;
        panel.Rows.Clear();
        int i = 0;
        foreach (KeyValuePair<int, PVEExploreLevelConfig> kv in GameConfigs.instance.getallExploreLevels())
        {

            int key = kv.Key;
            PVEExploreLevelConfig conf = kv.Value;

            object[] vals = new object[12];

            vals[0] = conf.id;
            vals[1] = conf.title;
            vals[2] = getrewardstring(conf);
            vals[3] = conf.TimeNeed;
            vals[4] = conf.needShipNum;
            vals[5] = conf.needFlagShipLevel;
            vals[6] = gettyperequiredstring(conf);

            vals[7] = getrandawardstring(conf);
            vals[8] = "%" + conf.bigSuccessRate;
            vals[9] = getoilammocost(conf);
            vals[10] = false;
            vals[11] = "执行";


            GridRow gr = new GridRow(vals);
            gr.RowHeight = 0;

            panel.Rows.Add(gr);

            panel.GetCell(i, 11).Tag = conf.id;

            if (explist.ContainsKey(conf.id))
            {
                panel.GetCell(i, 10).Visible = false;
                panel.GetCell(i, 11).Visible = false;


                int flid = explist[conf.id];
                var uf = GameData.instance.UserFleets[flid - 1];
                panel.GetCell(i, 2).Value = uf.title + " 正在执行中...";
                panel.GetCell(i, 2).CellStyles.Default.Background = new DevComponents.DotNetBar.SuperGrid.Style.Background(Color.LightGreen);
                var r = panel.GetRowFromIndex(i);
                if(r!= null)
                {
                    r.CellStyles.Default.Background = new DevComponents.DotNetBar.SuperGrid.Style.Background(Color.LightGreen);
                }
                

            }


            i++;
        }
    }

    internal class DoExplorerCommandButton : GridButtonXEditControl
    {
        private int fleetid;
        private Form expform;
        public DoExplorerCommandButton(int flid, Form f)
        {
            fleetid = flid;
            expform = f;
            Click += new EventHandler(CommandButtonClick);
        }

        #region CommandButtonClick

        void CommandButtonClick(object sender, EventArgs e)
        {
            int explorerid = (int)EditorCell.Tag;
            bool autostart = (bool)EditorCell.GridRow[10].Value;
            if (z.instance.isFleetInBattle(fleetid))
            {
                MessageBox.Show("舰队正在战斗中");
                expform.Close();
                return;
            }
            if (GameData.instance.IsFleetInRepair(fleetid) == true)
            {
                MessageBox.Show("舰队中有船只在修理");
                expform.Close();
                return;
            }

            tools.configmng.instance.setval(z.instance.get_username() + "_explore_" + fleetid, (autostart == true ? explorerid.ToString() : "no"));
            var exp = GameConfigs.instance.GetPVEExploreLevel(explorerid);

            UserFleet uf = GameData.instance.GetFleetOfId(fleetid);
            bool needsupply = false;
            foreach (int sste in uf.ships)
            {
                var sstus = GameData.instance.GetShipById(sste);
                if (sstus.battleProps.oil < sstus.battlePropsMax.oil || sstus.battleProps.ammo < sstus.battlePropsMax.ammo)
                {
                    needsupply = true;
                }
            }
            if (needsupply == true)
            {
                z.log("[开始远征] 远征前补给 ");
                ServerRequestManager.instance.SupplyFleet(fleetid);
                System.Threading.Thread.Sleep(200);
            }

            z.log("[准备开始远征] " + exp.title + (autostart ? "并开始自动执行此远征" : " 自动执行关闭"));
            ServerRequestManager.instance.StartPVEExplore(fleetid, explorerid);
            expform.Close();
        }

        #endregion
    }

}