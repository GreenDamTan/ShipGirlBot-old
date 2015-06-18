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



public partial class BuildForm : Form
{
    bool initing = false;
    private int dockid = 0;
    public BuildForm(bool is_ship, int dock_id)
    {
        initing = true;
        InitializeComponent();

        GridColumn column1 = recipelist.PrimaryGrid.Columns[2];
        if (column1.EditControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        if (column1.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        GridColumn column2 = recipelist.PrimaryGrid.Columns[8];
        column2.EditorType = typeof(ResBuildCommandButton);
        column2.EditorParams = new object[] { this };


        dockid = dock_id;
        if (is_ship)
        {
            switch_to_ship();
        }
        else
        {
            switch_to_equip();
        }


        initing = false;
    }

    internal void initdata(int dock_id)
    {
        dockid = dock_id;
        initing = true;
        var gd = GameData.instance;
        this.v1.Text = "建造材料 " + gd.GetItemAmount(ResourceTypes.BuildShipItem) + " 快速建造 " + gd.GetItemAmount(ResourceTypes.FastBuild);
        this.v2.Text = "开发材料 " + gd.GetItemAmount(ResourceTypes.BuildEquipItem) + " 快速建造 " + gd.GetItemAmount(ResourceTypes.FastBuild);
        this.v3.Text = "快速修理 " + gd.GetItemAmount(ResourceTypes.FastRepair);

        update_log();
        initing = false;
    }

    private void update_log()
    {
        List<BuildLogVO> log = null;
        if (shipcheck.Checked == true)
        {
            var r = ServerRequestManager.instance.GetShipBuildLog();
            log = GameData.instance.GetShipBuildLogs();
        }
        else
        {
            var r = ServerRequestManager.instance.GetEquipBuildLog();
            log = GameData.instance.GetEquipBuildLogs();
        }

        GridPanel panel = recipelist.PrimaryGrid;
        panel.Rows.Clear();

        if (log == null)
        {
            return;
        }
        int i = 0;
        foreach (var l in log)
        {
            object[] vals = new object[9];

            vals[0] = getbuildtitle(l);
            vals[1] = getbuildstar(l);
            vals[2] = getbuildicon(l);
            vals[3] = l.res.oil;
            vals[4] = l.res.ammo;
            vals[5] = l.res.steel;
            vals[6] = l.res.aluminium;
            vals[7] = TimeFormatter.Get6DTimeFormat(l.createTime);
            vals[8] = "套用";

            GridRow gr = new GridRow(vals);
            gr.RowHeight = 64;

            panel.Rows.Add(gr);
            i++;
        }

    }

    private string getbuildstar(BuildLogVO l)
    {
        if (shipcheck.Checked == true)
        {
            return tools.helper.getstartstring(AllShipConfigs.instance.getShip(l.cid).star);
        }
        else
        {
            return tools.helper.getstartstring(GameConfigs.instance.GetEquipmentByCid(l.cid).star);
        }
    }

    private string getbuildtitle(BuildLogVO l)
    {
        if (shipcheck.Checked == true)
        {
            return AllShipConfigs.instance.getShip(l.cid).title;
        }
        else
        {
            return GameConfigs.instance.GetEquipmentByCid(l.cid).title;
        }

    }

    private Image getbuildicon(BuildLogVO l)
    {
        if (shipcheck.Checked == true)
        {
            var sc = AllShipConfigs.instance.getShip(l.cid);
            UserShip us = new UserShip();
            us.shipCid = sc.cid;
            us.level = 1;
            us.battleProps = new ShipBattleProps();
            us.battlePropsMax = new ShipBattleProps();
            us.battleProps.hp = sc.hp;
            us.battlePropsMax.hp = sc.hpMax;

            return tools.helper.getShipSmallImage(us);

        }
        else
        {
            var e = GameConfigs.instance.GetEquipmentByCid(l.cid);
            return tools.helper.getEquipmentImage(e);
        }
    }

    private void switch_to_ship()
    {
        initing = true;
        shipcheck.Checked = true;
        equipcheck.Checked = false;
        int[] res = { 30, 30, 30, 30 };
        for (int i = 0; i < 4; i++)
        {

            string k = z.instance.get_username() + "_build_res_val_" + i;
            object v = tools.configmng.instance.getval(k);
            if (v != null)
            {
                try
                {
                    res[i] = (int)v;
                }
                catch (System.Exception ex)
                {

                }
            }
        }
        r1.Value = res[0];
        r2.Value = res[1];
        r3.Value = res[2];
        r4.Value = res[3];

        update_log();
        initing = false;
    }

    private void switch_to_equip()
    {
        initing = true;
        shipcheck.Checked = false;
        equipcheck.Checked = true;
        int[] res = { 10, 10, 10, 10 };
        for (int i = 0; i < 4; i++)
        {

            string k = z.instance.get_username() + "_dev_res_val_" + i;
            object v = tools.configmng.instance.getval(k);
            if (v != null)
            {
                try
                {
                    res[i] = (int)v;
                }
                catch (System.Exception ex)
                {

                }
            }
        }
        r1.Value = res[0];
        r2.Value = res[1];
        r3.Value = res[2];
        r4.Value = res[3];

        update_log();
        initing = false;
    }

    private void shipcheck_CheckedChanged(object sender, EventArgs e)
    {
        if (initing == true)
        {
            return;
        }
        if ((sender as CheckBox).Checked)
        {
            switch_to_ship();
        }
        else
        {
            switch_to_equip();
        }

    }

    private void equipcheck_CheckedChanged(object sender, EventArgs e)
    {
        if (initing == true)
        {
            return;
        }
        if ((sender as CheckBox).Checked)
        {
            switch_to_equip();
        }
        else
        {
            switch_to_ship();
        }
    }

    internal void setrecipe(int res1, int res2, int res3, int res4)
    {
        r1.Value = res1;
        r2.Value = res2;
        r3.Value = res3;
        r4.Value = res4;

        string k = z.instance.get_username() + (shipcheck.Checked ? "_build_res_val_" : "_dev_res_val_");
        tools.configmng.instance.setval(k + "0", res1);
        tools.configmng.instance.setval(k + "1", res2);
        tools.configmng.instance.setval(k + "2", res3);
        tools.configmng.instance.setval(k + "3", res4);
    }
    internal class ResBuildCommandButton : GridButtonXEditControl
    {

        private BuildForm par;
        public ResBuildCommandButton(BuildForm f)
        {
            par = f;
            Click += new EventHandler(CommandButtonClick);
        }

        #region CommandButtonClick

        void CommandButtonClick(object sender, EventArgs e)
        {
            int r1 = (int)EditorCell.GridRow.Cells[3].Value;
            int r2 = (int)EditorCell.GridRow.Cells[4].Value;
            int r3 = (int)EditorCell.GridRow.Cells[5].Value;
            int r4 = (int)EditorCell.GridRow.Cells[6].Value;

            par.setrecipe(r1, r2, r3, r4);

        }

        #endregion
    }

    private void buttonX1_Click(object sender, EventArgs e)
    {

        if (shipcheck.Checked)
        {

            var dock = GameData.instance.UserDocks[dockid - 1];
            if (dock == null || dock.locked == 1 || dock.secondsLeft > 0 || dock.shipType > 0)
            {
                return;
            }
            ServerRequestManager.instance.BuildShipInDock(dockid, r1.Value, r3.Value, r2.Value, r4.Value);
        }
        else
        {

            var dock = GameData.instance.UserEquipDocks[dockid - 1];
            if (dock == null || dock.locked == 1 || dock.secondsLeft > 0 || dock.equipmentCid > 0)
            {
                return;
            }
            ServerRequestManager.instance.BuildEquipInDock(dockid, r1.Value, r3.Value, r2.Value, r4.Value);
        }

        setrecipe(r1.Value, r2.Value, r3.Value, r4.Value);

        z.log("[建造请求] " + (shipcheck.Checked ? "少女 " : "装备 ") + r1.Value + " " + r2.Value + " " + r3.Value + " " + r4.Value);
        Close();

    }


}