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
using System.Text;
using System.Text.RegularExpressions;

public partial class Enchant : Form
{
    private List<UserShip> sellist = new List<UserShip>();

    private int selshipid = 0;
    public Enchant()
    {
        InitializeComponent();

        GridColumn column1 = selshiplist.PrimaryGrid.Columns[4];
        if (column1.EditControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        if (column1.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }

        GridColumn column3 = feedlist.PrimaryGrid.Columns[0];
        column3.EditorType = typeof(EatShipCheck);
        column3.EditorParams = new object[] { this };

        updateFeedList();

        updateShipList();

    }

    private void updateFeedList()
    {
        sellist.Clear();

        GridPanel panel = feedlist.PrimaryGrid;
        panel.Rows.Clear();

        foreach (var cl in GameData.instance.UserShips.OrderByDescending(o => o.level).ToList())
        {
            if (cl.level < 5 && cl.IsLocked == false && cl.fleetId == 0 && cl.ship.star <= 4)
            {
                object[] vals = new object[6];

                vals[0] = "吃掉";
                vals[1] = cl.level;
                vals[2] = tools.helper.getshiptype(cl.ship.type);
                vals[3] = cl.ship.title;
                vals[4] = tools.helper.getshipEattenString(cl);
                vals[5] = cl.id;

                GridRow gr = new GridRow(vals);
                panel.Rows.Add(gr);
                gr.RowHeight = 20;
            }
        }
    }

    private void updateShipList()
    {
        GridPanel panel = selshiplist.PrimaryGrid;
        panel.Rows.Clear();

        foreach (var cl in GameData.instance.UserShips.OrderByDescending(o => o.level).ToList())
        {
            var gr = getonerow(cl);
            panel.Rows.Add(gr);
        }
    }
    public static GridRow getonerow(UserShip us)
    {
        object[] vals = new object[10];

        vals[0] = tools.helper.getstartstring(us.ship.star);
        vals[1] = us.level;
        vals[2] = us.ship.title;
        vals[3] = tools.helper.getshiptype(us.ship.type);
        vals[4] = tools.helper.getShipSmallImage(us);

        vals[5] = "" + us.strengthenAttribute.atk + "/" + us.ship.strengthenTop.atk;
        vals[6] = "" + us.strengthenAttribute.torpedo + "/" + us.ship.strengthenTop.torpedo;
        vals[7] = "" + us.strengthenAttribute.def + "/" + us.ship.strengthenTop.def;
        vals[8] = "" + us.strengthenAttribute.air_def + "/" + us.ship.strengthenTop.air_def;
        vals[9] = us.id;

        GridRow gr = new GridRow(vals);
        gr.RowHeight = 30;
        return gr;
    }

    private void updateSel(UserShip us)
    {
        if( us == null)
        {
            return;
        }
        this.shiphead.Image = tools.helper.getShipBigImage(us, new Rectangle(0, 0, 192, 256));
        int v1 = 0;
        int v2 = 0;
        int v3 = 0;
        int v4 = 0;
        foreach(var vvs in this.sellist)
        {
            v1 += vvs.ship.strengthenSupplyExp.atk;
            v2 += vvs.ship.strengthenSupplyExp.torpedo;
            v3 += vvs.ship.strengthenSupplyExp.def;
            v4 += vvs.ship.strengthenSupplyExp.air_def;
        }
        int en = us.strengthenAttribute.atk + v1;
        int ens = us.ship.strengthenLevelUpExp;
        int ene = us.ship.strengthenTop.atk ;
        this.label1.Text = "火力: Lv " + en / ens + "(" + en % ens + "/" + ens + ") / " + ene/ens;

        en = us.strengthenAttribute.torpedo + v2;
        ene = us.ship.strengthenTop.torpedo;
        this.label2.Text = "雷装: Lv " + en / ens + "(" + en % ens + "/" + ens + ") / " + ene / ens;

        en = us.strengthenAttribute.def + v3;
        ene = us.ship.strengthenTop.def;
        this.label3.Text = "装甲: Lv " + en / ens + "(" + en % ens + "/" + ens + ") / " + ene / ens;

        en = us.strengthenAttribute.air_def+ v4;
        ene = us.ship.strengthenTop.air_def;
        this.label4.Text = "防空: Lv " + en / ens + "(" + en % ens + "/" + ens + ") / " + ene / ens;

        if(us.ship.canEvo == 1)
        {
            UserInfo userInfo = GameData.instance.UserInfo;
            if (
                us.level >= us.ship.evoLevel 
                && GameData.instance.GetItemAmount(us.ship.evoNeedItemCid) >= GetAmountOf(us, us.ship.evoNeedItemCid)
                && userInfo.oil >= GetAmountOf(us,2)
                && userInfo.ammo >= GetAmountOf(us,3)
                && userInfo.steel >= GetAmountOf(us, 4)
                && userInfo.aluminium >= GetAmountOf(us,9)
                )
            {
                evobtn.Enabled = true;
            }
            else
            {
                evobtn.Enabled = false;
            }
            labelX1.Text = "改造级别: " + us.level + "/" + us.ship.evoLevel + "\r\n"
                + " " + tools.helper.getItemtypestr(us.ship.evoNeedItemCid) + ":" + GameData.instance.GetItemAmount(us.ship.evoNeedItemCid) + "/" + GetAmountOf(us, us.ship.evoNeedItemCid) + "\r\n"
                + " 油:" + userInfo.oil + "/" + GetAmountOf(us, 2)
                + " 弹:" + userInfo.ammo + "/" + GetAmountOf(us, 3)
                + " 钢:" + userInfo.steel + "/" + GetAmountOf(us, 4)
                + " 铝:" + userInfo.aluminium + "/" + GetAmountOf(us, 9);

        }else{
            if(us.ship.evoClass ==0)
            {
                labelX1.Text = "未开放改造";
            }
            else
            {
                labelX1.Text = "改"+ us.ship.evoClass;
            }
            
            evobtn.Enabled = false;
        }


        if(us.ship.evoClass >0 
            && us.strengthenAttribute.atk >= us.ship.strengthenTop.atk
            && us.strengthenAttribute.torpedo >= us.ship.strengthenTop.torpedo
            && us.strengthenAttribute.def >= us.ship.strengthenTop.def
            && us.strengthenAttribute.air_def >= us.ship.strengthenTop.air_def
            )
        {
            Regex reg = new Regex(@"\^\w{9}");
            var sk = GameConfigs.instance.GetSkillConfig(us.skillId);
            label5.Text = sk.title + " Lv." + us.skillLevel;
            labelX2.Text = reg.Replace(sk.desc,"");
            label7.Text = sk.phaseDesc;
            if (us.skillLevel >= 3)
            {
                skilllvupbtn.Enabled = false;
            }
            else
            {
                skilllvupbtn.Enabled = true;
            }
        }
        else
        {
            label5.Text = "";
            labelX2.Text = "";
            label7.Text = "";
            skilllvupbtn.Enabled = false;
        }
       

    }

    private int GetAmountOf(UserShip us,int id)
    {
        if ((us.ship.evoNeedResource != null) && us.ship.evoNeedResource.ContainsKey(id + string.Empty))
        {
            return us.ship.evoNeedResource[id + string.Empty];
        }
        return 0;
    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void eatbutton_Click(object sender, EventArgs e)
    {
        List<int> food = new List<int>();
        UserShip us = GameData.instance.GetShipById(selshipid);
        if (us == null)
        {
            return;
        }
        foreach(var v in sellist)
        {
            food.Add(v.id);
        }
        var rr = ServerRequestManager.instance.Strengthen(selshipid, food);

        if(rr != null && rr.responseData!= null )
        {
            MessageBox.Show("强化成功");
        }
        else
        {
            MessageBox.Show("强化失败，开客户端搞吧...");
            Close();
        }
        UserShip uus = GameData.instance.GetShipById(selshipid);
        sellist.Clear();
        updateSel(uus);
        for (int rc = 0; rc < selshiplist.PrimaryGrid.Rows.Count;rc++ )
        {
            
            if( ((int)selshiplist.PrimaryGrid.GetCell(rc,9).Value ) == uus.id)
            {
                selshiplist.PrimaryGrid.GetCell(rc, 5).Value = "" + uus.strengthenAttribute.atk + "/" + uus.ship.strengthenTop.atk;
                selshiplist.PrimaryGrid.GetCell(rc, 6).Value = "" + uus.strengthenAttribute.torpedo + "/" + uus.ship.strengthenTop.torpedo;
                selshiplist.PrimaryGrid.GetCell(rc, 7).Value = "" + uus.strengthenAttribute.def + "/" + uus.ship.strengthenTop.def;
                selshiplist.PrimaryGrid.GetCell(rc, 8).Value = "" + uus.strengthenAttribute.air_def + "/" + uus.ship.strengthenTop.air_def;

                break;
            }
        }
        foreach (int shipeatten in food)
        {
            for (int rc = 0; rc < feedlist.PrimaryGrid.Rows.Count; rc++)
            {

                if (((int)feedlist.PrimaryGrid.GetCell(rc, 5).Value) == shipeatten)
                {
                    feedlist.PrimaryGrid.Rows.RemoveAt(rc);
                    break;
                }
            }
        }
    }

    internal void updateCheckList(UserShip us)
    {
        if (sellist.Contains(us) )
        {
            sellist.Remove(us);
        }
        else 
        {
            sellist.Add(us);
        }
        UserShip uus = GameData.instance.GetShipById(selshipid);
        updateSel(uus);
    }
    private void selshiplist_RowClick(object sender, GridRowClickEventArgs e)
    {
        int shipid = (int)e.GridPanel.GetCell(e.GridRow.RowIndex,9).Value;
        UserShip us = GameData.instance.GetShipById(shipid);
        if(us == null)
        {
            return;
        }
        selshipid = us.id;
        updateSel(us);
    }
    public int getselcount()
    {
        return sellist.Count;
    }

    internal class EatShipCheck : GridButtonXEditControl
    {
        private Enchant form;
        public EatShipCheck(Enchant par)
        {
            form = par;

            this.Click += CheckStateChangedCallback;
        }

        #region CheckStateChangedCallback

        void CheckStateChangedCallback(object sender, EventArgs e)
        {
            int shipid = (int)EditorCell.GridRow.Cells[5].Value;
            string s = (string)EditorCell.GridRow.Cells[0].Value;
            

            if(s == "吃掉")
            {
                if (form.getselcount() >= 10)
                {
                    MessageBox.Show("一次最多10只少女...");
                    return;
                }

                EditorCell.GridRow.Cells[0].Value = "不吃";
            }
            else
            {
                EditorCell.GridRow.Cells[0].Value = "吃掉";
            }
            UserShip us = GameData.instance.GetShipById(shipid);

            if (us == null)
            {
                MessageBox.Show("错误的船只信息");
                return;
            }


            form.updateCheckList(us);
            EditorCell.GridRow.Cells[1].CellStyles.Default.Background = new DevComponents.DotNetBar.SuperGrid.Style.Background(s == "吃掉"? Color.White:Color.LightGreen);
        }

        #endregion
    }

    private void skilllvupbtn_Click(object sender, EventArgs e)
    {
        UserShip us = GameData.instance.GetShipById(selshipid);
        if (us == null)
        {
            return;
        }
        ServerRequestManager.instance.SkillShip(us);
        us = GameData.instance.GetShipById(selshipid);
        updateSel(us);

    }

    private void evobtn_Click(object sender, EventArgs e)
    {
        UserShip us = GameData.instance.GetShipById(selshipid);
        if (us == null)
        {
            return;
        }
        ServerRequestManager.instance.ModifyShip(us);
        us = GameData.instance.GetShipById(selshipid);
        updateSel(us);
    }




}