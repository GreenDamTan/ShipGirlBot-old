using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;

public partial class CommandForm : Form
{
    private int mfleetid;

    ButtonX[] ships = new ButtonX[6];

    Font titleFont = new Font("微软雅黑", 16);
    SolidBrush titleBrush = new SolidBrush(Color.Black);

    private int cur_index;

    public CommandForm()
    {
        InitializeComponent();
        ships[0] = buttonX1;
        ships[1] = buttonX2;
        ships[2] = buttonX3;
        ships[3] = buttonX4;
        ships[4] = buttonX5;
        ships[5] = buttonX6;
        int i = 0;
        foreach (ButtonX b in ships)
        {
            b.Tag = i;
            b.Click += this.shipButtonClick;
            b.Location = new Point(b.Location.X, 12);
            i++;
        }
        //UserShip us = new UserShip();
        //us.shipCid = 1;
        //us.battleProps = new ShipBattleProps();
        //us.battlePropsMax = new ShipBattleProps();
        //us.battleProps.hp = 30;
        //us.battlePropsMax.hp = 60;

        //ShipConfig sc = new ShipConfig();

        //sc.cid = 1;
        //sc.title = "sdfsdfsdf";
        //sc.picId = "1";
        //sc.star = 5;
        //sc.evoClass = 1;
        //AllShipConfigs.instance.setShips(new ShipConfig[] { sc });
        //this.pictureBox1.Image = tools.helper.getShipBigImage(us, new Rectangle(0, 0, 192, 256));
    }
    public int FleetID
    {
        get { return mfleetid; }
        set { mfleetid = value; }
    }

    public void setfleet_id(int id)
    {
        FleetID = id;

        GridColumn column1 = selshiplist.PrimaryGrid.Columns[4];
        if (column1.EditControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        if (column1.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        GridColumn column2 = selshiplist.PrimaryGrid.Columns[16];
        column2.EditorType = typeof(ChooseShipButton);
        column2.EditorParams = new object[] { mfleetid, this };

        GridColumn column3 = selshiplist.PrimaryGrid.Columns[17];
        column3.EditorType = typeof(LockShipButton);
        column3.EditorParams = new object[] { mfleetid, this };

        updateCurFleetInfo();
        updateUserShipInfo();
        
    }

    internal void updateCurFleetInfo()
    {
        foreach (ButtonX b in ships)
        {
            b.Image = null;
            b.Location = new Point(b.Location.X, 12);
        }

        UserFleet uf = GameData.instance.GetFleetOfId(FleetID);
        if (uf == null)
        {
            return;
        }
        Text = "编辑舰队 -- " + uf.title;
        int i = 0;
        foreach (int j in uf.ships)
        {
            UserShip us = GameData.instance.GetShipById(j);
            if (us != null)
            {
                ships[i].Image = tools.helper.getShipBigImage(us, new Rectangle(0, 0, 192, 256));
            }
            i++;
        }
        updateTeamProps();
    }

    private void updateUserShipInfo()
    {
        GridPanel panel = selshiplist.PrimaryGrid;
        panel.Rows.Clear();

        //移除船
        object[] vv = new object[19] { "", "清空", "清空", "清空", "", 100, "清空", "清空", "清空", "清空", "清空", "清空", "清空", "清空", "清空", "清空", "清空", "", 0 };
        panel.Rows.Add(new GridRow(vv));


        int i = 0;
        foreach (UserShip us in GameData.instance.UserShips.OrderByDescending(o => o.level).ToList())
        {

            var gr =getonerow(us);
            panel.Rows.Add(gr);
            i++;
        }


    }

    public static GridRow getonerow(UserShip us)
    {
        object[] vals = new object[19];

        vals[0] = us.fleetId > 0 ? (GameData.instance.UserFleets[us.fleetId - 1].title) : "";
        vals[1] = us.level;
        vals[2] = us.ship.title;
        vals[3] = tools.helper.getshiptype(us.ship.type);
        vals[4] = tools.helper.getShipSmallImage(us);
        vals[5] = 100 * us.battleProps.hp / us.battlePropsMax.hp;
        vals[6] = us.battleProps.def;// +"/" + us.battlePropsMax.def;
        vals[7] = us.battleProps.atk;// + "/" + us.battlePropsMax.atk;
        vals[8] = us.battleProps.torpedo;// + "/" + us.battlePropsMax.torpedo;
        vals[9] = us.battleProps.airDef;// + "/" + us.battlePropsMax.airDef;
        vals[10] = us.battleProps.antisub;// + "/" + us.battlePropsMax.antisub;
        vals[11] = us.battleProps.radar;// + "/" + us.battlePropsMax.radar;
        vals[12] = us.battleProps.speed;// + "/" + us.battlePropsMax.def;
        vals[13] = us.battleProps.luck;// + "/" + us.battlePropsMax.def;
        vals[14] = +us.battleProps.miss;// + "/" + us.battlePropsMax.miss;
        vals[15] = us.love + "/" + us.loveMax;

        vals[16] = "选择";
        vals[17] = us.IsLocked ? "解锁" : "锁定";
        vals[18] = us.id;

        GridRow gr = new GridRow(vals);
        gr.RowHeight = 30;
        return gr;
    }

    void shipButtonClick(object sender, EventArgs e)
    {
        foreach (ButtonX b in ships)
        {
            b.Checked = false;
            b.Location = new Point(b.Location.X, 12);
        }
        (sender as ButtonX).Checked = true;
        (sender as ButtonX).Location = new Point((sender as ButtonX).Location.X, 32);
        cur_index = (int)(sender as ButtonX).Tag;

    }

    public int get_cur_index()
    {
        return cur_index;
    }

    public void updateTeamProps()
    {
        UserFleet uf = GameData.instance.GetFleetOfId(FleetID);
        if (uf == null)
        {
            return;
        }
        int[] prop= new int[7]{0,0,0,0,0,0,0};
        double cvairval = 0.0f;
        foreach (int j in uf.ships)
        {
            UserShip us = GameData.instance.GetShipById(j);
            if (us != null)
            {
                prop[0] += us.battleProps.atk;
                prop[1] += us.battleProps.torpedo;
                prop[2] += us.battleProps.def;
                prop[3] += us.battleProps.antisub;
                prop[4] += us.battleProps.airDef;
                prop[5] += us.battleProps.radar;

                for (int index = 0; index < us.equipmentArr.Length; index++)
                {
                    if(us.capacitySlotMax[index] <=0)
                    {
                        continue;
                    }
                    var eq = us.equipmentArr[index].config;
                    if(eq != null && eq.type == EquipmentType.FightPlane)
                    {
                        cvairval += Math.Sqrt((double)us.capacitySlotMax[index]) * (double)eq.airDef;
                    }
                }
            }
            
        }
        label1.Text = "总火力: " + prop[0];
        label2.Text = "总雷装: " + prop[1];
        label3.Text = "总装甲: " + prop[2];
        label4.Text = "总反潜: " + prop[3];
        label5.Text = "总对空: " + prop[4];
        label6.Text = "总索敌: " + prop[5];
        label7.Text = "总制空值: " + cvairval;
    }
    internal class ChooseShipButton : GridButtonXEditControl
    {
        private int fleetid;
        private CommandForm form;
        public ChooseShipButton(int flid, CommandForm par)
        {
            fleetid = flid;
            form = par;

            Click += new EventHandler(CommandButtonClick);
        }

        #region CommandButtonClick

        void CommandButtonClick(object sender, EventArgs e)
        {
            int shipid = (int)EditorCell.GridRow.Cells[18].Value;

            if(GameData.instance.IsFleetInExplore(fleetid))
            {
                MessageBox.Show("目标舰队正在出征");
                return;
            }
            UserFleet uf = GameData.instance.GetFleetOfId(fleetid);
            int selindex = form.get_cur_index();
            UserShip us = GameData.instance.GetShipById(shipid);
            UserShip targetship = selindex>=uf.ships.Length? null:GameData.instance.GetShipById(uf.ships[selindex]);
            if( us!=null &&(us.IsInRepair || us.IsInExplore ))
            {
                MessageBox.Show("请求的少女(们)不存在 或者 正在洗澡..远征...战斗....");
                return;
            }
            ServerRequestManager.instance.ChangeFleetShip(fleetid, shipid, form.get_cur_index());

            form.updateCurFleetInfo();
            //form.updateUserShipInfo();
            var panel = EditorCell.GridPanel;
            int rindex = EditorCell.RowIndex;
            if(rindex >0)
            {
                panel.Rows.Remove(panel.Rows[rindex]);
            }
            
            if (targetship != null)
            {
	            var gr = getonerow(targetship);
	            panel.Rows.Insert(rindex==0?1:rindex,gr);
            }
            ServerRequestManager.instance.refreashUIData();
        }

        #endregion
    }

    internal class LockShipButton : GridButtonXEditControl
    {
        private int fleetid;
        private CommandForm form;
        public LockShipButton(int flid, CommandForm par)
        {
            fleetid = flid;
            form = par;

            Click += new EventHandler(CommandButtonClick);
        }

        #region CommandButtonClick

        void CommandButtonClick(object sender, EventArgs e)
        {
            int shipid = (int)EditorCell.GridRow.Cells[18].Value;
            var us = GameData.instance.GetShipById(shipid);
            if (us == null)
            {
                return;
            }

            var ret = ServerRequestManager.instance.ToggleShipLock(us);
            if (ret != null && ret.responseData != null && ret.responseData.eid == 0)
            {
                var uss = GameData.instance.GetShipById(shipid);
                EditorCell.GridRow.Cells[17].Value = uss.IsLocked ? "解锁" : "锁定";
            }

        }

        #endregion
    }

    private void selshiplist_RowsSorting(object sender, GridCancelEventArgs e)
    {
        GridPanel panel = selshiplist.PrimaryGrid;
        panel.Rows.Remove(panel.Rows[0]);


    }

    private void selshiplist_RowsSorted(object sender, GridEventArgs e)
    {
        GridPanel panel = selshiplist.PrimaryGrid;
        object[] vv = new object[19] { "", "清空", "清空", "清空", "", 100, "清空", "清空", "清空", "清空", "清空", "清空", "清空", "清空", "清空", "清空", "清空", "", 0 };
        panel.Rows.Insert(0,new GridRow(vv));

    }

    private void label3_Click(object sender, EventArgs e)
    {

    }


}