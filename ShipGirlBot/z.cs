using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;

using Microsoft.Win32;
using JsonConfig;

using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;


    public enum BATTLE_STATUS
    {
        NONE =0,
        PREPARE,
        SUPPLY,
        STARTED,
        VISIT_NODE,
        SPY_NODE,
        NODE_BATTLE,
        BATTLE_RESULT,
        BACKING_HOME,
        FINISHED,

        ERR
    }

    public struct BSLOG
    {
        public int oil ;
        public int ammo;
        public int steel ;
        public int al;
        public long timetick;
        public int fleetcommanderid ;
        public int buildreturntype;
    }
    [GuidAttribute("550d188a-adaf-3c4f-9fd3-90cd0a538483")] 
    public partial class z : Form
    {

        public static z instance = null;

        private bool login_successed = false;

        private int exp_counter = 10;
        private int battle_counter = 10;
        private int repair_counter = 10;
        private int dis_counter = 10;

        private bool dis_ship = false;
        private bool dis_equip = false;

        private bool dis_sel_blocker = false;

        private int refresh_counter = 20;


        private int refresh_wait = 20;

        private int delay_counter = 0;

        private BATTLE_STATUS battel_status = BATTLE_STATUS.NONE;
        private int bcounter = 5;
        private int battle_fleetid = 0;
        private int battle_levelid = 0;
        private int battle_nodeid = 0;
        private int battle_do_nightwar = 0;
        private int battle_node_counter = 0;

        private bool dealnode_enemyclearornot = false;

        private FleetFormation battle_formation = FleetFormation.TwoRow;
        private bool initing = true;

        private bool autostart_battle = false;

        private List<int> all_backupships = new List<int>();
        private List<int> all_battle_ships = new List<int>();

        PVELevel currentlevel;
        PVENode currentNode;
        private Dictionary<string, int> currentNodeStatus;
        List<PVENode> passedNodes;

        bool pveLevelEnd = false;
        int battle_retry_counter = 3;


        bool can_relogin = false;
        int relogin_counter = 3600;

        private string servername = "";

        private int nodropcounter = 0;

        private int autocraft_ship = 0;
        private int autocraft_equip = 0;

        private int autoquest_counter = 0;
        private int autodev_counter = 0;

        private long expire_time = 0;

        private string Version = "v65";

        private bool canUpdateGridUI = true;

        public Dictionary<int, Dictionary<int, int>> fleetcfgs = new Dictionary<int, Dictionary<int, int> >();

        public Dictionary<int, BSLOG> build_data = new Dictionary<int, BSLOG>();
        public Dictionary<int, BSLOG> weapon_build_data = new Dictionary<int, BSLOG>();
        
        public z()
        {
            instance = this;
            InitializeComponent();

            this.update_bot_info();
            this.Text = this.Version;

            repairytpecombo.SelectedIndex = 2;
            battlechangetypecombo.SelectedIndex = 1;
            foreach (var p in new List<ExpandablePanel>() { expandablePanel1, expandablePanel2, expandablePanel3, expandablePanel4 })
            {
                p.Tag = p.Height;
            }
            initphonecombo();
            inituiparams();

            initdefaultvals();
#if DEBUG || RELEASE_TEST
            battledelayinput.MinValue = 0;
            battleresultdelayinput.MinValue = 0;
            timer1.Interval = 50;

#endif
            secondnodedelaymulti.Visible = true;
            //timer1.Interval = 100;
            //battledelayinput.MinValue = 2;
            //battleresultdelayinput.MinValue = 1;

            initing = false;

            string changelog = null;

            if(System.IO.File.Exists("changelog.txt"))
            {
                changelog = System.IO.File.ReadAllText("changelog.txt");
            }
            if (changelog != null)
            {
                //A a = new A(changelog);
                //a.Show();
                log("[初始化完成]...ShipGirlBot Ver " + Version);
                log(changelog.Replace("{br}", "\r\n"));
            }
        }

        private void showtt()
        {
            var a = new A(System.IO.File.ReadAllText("tt.txt"));
            a.Show();
        }
        private void initdefaultvals()
        {
            object val = tools.configmng.instance.getval(get_username() + "_do_nightwar");
            if (val != null)
            {
                battle_do_nightwar = (int)val;
            }
            nightwarcheck.Checked = battle_do_nightwar == 1;


            object val2 = tools.configmng.instance.getval(get_username() + "_formation");
            if (val2 != null)
            {
                battle_formation = (FleetFormation)val2;
            }
            formationcombo.SelectedIndex = (int)battle_formation - 1;

            object val3 = tools.configmng.instance.getval(get_username() + "_auto_repair");
            if (val3 != null)
            {
                autorepaircheck.Checked = (val3 as string).Equals("yes");
            }

        }

        private void inituiparams()
        {
            string s_uname = (string)tools.configmng.instance.getval("username");
            if(s_uname != null){username.Text = s_uname;}
            string s_pw = (string)tools.configmng.instance.getval("passwd");
            if (s_pw != null) { password.Text = s_pw; }


            formationcombo.Items.Clear();
            formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.OneRow));
            formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.TwoRow));
            formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.Cicle));
            formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.TStyle));
            formationcombo.Items.Add(tools.helper.getformationstring(FleetFormation.OneColume));

            formationcombo.SelectedIndex = 1;

            foreach(GridColumn column1 in shiplist.PrimaryGrid.Columns)
            {
                var ec = column1.EditControl;
                if (column1.EditControl.GetType().Equals( typeof(GridImageEditControl)) )
                {
                    (column1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
                }
                if (column1.RenderControl.GetType().Equals( typeof(GridImageEditControl)) )
                {
                    (column1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
                }


            }
            GridColumn c9 = shiplist.PrimaryGrid.Columns[8];
            if (c9 != null)
            {
                c9.EditorType = typeof(ExplorerCommandButton);
            }
            GridColumn c10 = shiplist.PrimaryGrid.Columns[9];
            if(c10 != null)
            {
                c10.EditorType = typeof(GroupCommandButton);
            }
            GridColumn c11 = shiplist.PrimaryGrid.Columns[10];
            if (c11 != null)
            {
                c11.EditorType = typeof(BattleCommandButton);
            }

            List<RadialMenu> lrm = new List<RadialMenu>();
            lrm.Add(radialMenu1);
            lrm.Add(radialMenu2);
            lrm.Add(radialMenu3);
            lrm.Add(radialMenu4);
            string[] ss = { "读取1", "读取2", "读取3", "读取4", 
                                "读取5", "读取6", "读取7", "读取8", 
                                "保存1", "保存2", "保存3", "保存4",
                                "保存5", "保存6", "保存7", "保存8"};
            int jjj = 1;
            foreach (var llrm in lrm)
            {
                llrm.Symbol = "\uf018";
                llrm.Diameter = 300;

                llrm.Tag = jjj;
                llrm.ItemClick += new EventHandler(llrm_ItemClick);
                int i = 0;
                foreach (string s in ss)
                {
                    if (llrm != null)
                    {
                        var i1 = new RadialMenuItem();
                        i1.Text = s;
                        i1.Symbol = "\uf018";
                        i1.Tag = jjj;
                        llrm.Items.Add(i1);

                    }

                    i++;
                }
                jjj++;
            }



            GridColumn r1 = repairlist.PrimaryGrid.Columns[3];
            if (r1 != null)
            {
                r1.EditorType = typeof(RepairCommandButton);
            }

            GridColumn cr1 = repairlist.PrimaryGrid.Columns[1];
            if (cr1.EditControl.GetType().Equals(typeof(GridImageEditControl)))
            {
                (cr1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
            }
            if (cr1.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
            {
                (cr1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
            }


            GridColumn b1 = buildlist.PrimaryGrid.Columns[3];
            if (b1 != null)
            {
                b1.EditorType = typeof(BuildCommandButton);
            }

            GridColumn cb1 = buildlist.PrimaryGrid.Columns[1];
            if (cb1.EditControl.GetType().Equals(typeof(GridImageEditControl)))
            {
                (cb1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
            }
            if (cb1.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
            {
                (cb1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
            }

            //buttonX1.Image = tools.helper.getScaleAndCropImage("res/png/big_n/1.png"
            //                    , new Rectangle(128+48, 128, 1024 - 256-96, 1024 -128), new Rectangle(0, 0, 192, 256));

            //init shiplist

            GridPanel panel = shiplist.PrimaryGrid;
            panel.Rows.Clear();


            for(int i=0;i<4;i++)
            {
                object[] vals = new object[12];

                vals[0] = i;
                for (int j = 1; j < 7; j++)
                {
                        vals[j] = "";
                }
                vals[7] = "无";
                vals[8] = "远征";
                vals[9] = "命令";
                vals[10] = "战斗";
                vals[11] = "";
                GridRow gr = new GridRow(vals);
                gr.RowHeight = 35;

                panel.Rows.Add(gr);
            }
        }


        private void loginbtn_Click(object sender, EventArgs e)
        {
            loginbtn.Enabled = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;

            GameInfo.instance.UA = getphone_choose();
            phonetypecombo.Enabled = false;
            ServerRequestManager srm = ServerRequestManager.instance;
            ReqSignin rs = srm.CheckIfHaveNewVerison();
            rs.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(login_successed == false)
            {
                relogin_counter--;
                if(can_relogin== true  && relogin_counter <=0)
                {
                    do_relogin_logic();
                }

                return;
            }

            update_PVEexplore();

            update_Battle();

            update_repair();
            update_build();
           
            refresh_counter--;
            if (refresh_counter <= 0)
            {
                refresh_counter = refresh_wait;
                refreshUIData();
            }
        
            if(delay_counter >=0)
            {
                delay_counter--;
                return;
            }

            battle_counter--;
            exp_counter--;
            repair_counter--;
            dis_counter--;
            autoquest_counter--;
            autodev_counter--;


            if (dis_sel_blocker)
            {
                return;
            }


            if (battle_counter <= 0)
            {
                battle_counter = battledelayinput.Value;
                if (auto_battle())
                {
                    delay_counter++;
                    return;
                }
            }

            //战斗中不能收菜...
            if(isInBattle())
            {
                return;
            }

            
            if(exp_counter <=0)
            {
                exp_counter = 10;
                if (auto_PVEExpllore())
                {
                    delay_counter += 1;
                    return;
                }
            }


            if (repair_counter <= 0)
            {
                repair_counter = 4;

                if(auto_repair())
                {
                    delay_counter += 1;
                    return;
                }
            }


            if (dis_counter <= 0)
            {
                dis_counter = 10;

                if (auto_dis())
                {
                    delay_counter += 2;
                    return;
                }
            }

            if (autoquest_counter <= 0)
            {


                if (auto_quest())
                {
                    autoquest_counter = 3600;
                    return;
                }
                delay_counter += 2;
            }


            if (autodev_counter <= 0)
            {


                if (auto_dev())
                {
                    autodev_counter = 120;
                    return;
                }
            }



        }

        public bool isInBattle()
        {
            return battel_status != BATTLE_STATUS.NONE && battel_status != BATTLE_STATUS.FINISHED;
        }
        internal void tryStartNewBattle(int fleetid, int levelid, bool autostart, List<int> backshiplist)
        {
            if (isInBattle())
            {
                MessageBox.Show("已有舰队在战斗中");
                return;
            }
            battel_status = BATTLE_STATUS.PREPARE;
            battle_fleetid = fleetid;
            battle_levelid = levelid;
            battle_node_counter = 0;
            bcounter = 5;

            this.all_battle_ships.Clear();
            this.all_backupships.Clear();
            UserFleet uf = GameData.instance.GetFleetOfId(fleetid);
            if(uf == null)
            {
                MessageBox.Show("战斗开启参数错误，错误的舰队");
                return;
            }
            foreach(int sidbattle in uf.ships)
            {
                all_battle_ships.Add(sidbattle);
            }

            foreach (int i in backshiplist)
            {
                all_backupships.Add(i);
            }
            initing = true;
            autostart_battle = autostart;
            autobattlecheck.Checked = autostart_battle;
            initing = false;
            z.log("[准备开始战斗] " + (autostart ? "并开始自动执行此战斗" : ""));

        }

        public void setBattleStatus(BATTLE_STATUS s)
        {
            battel_status = s;
        }
        public void setBattleNodeId(int p)
        {
            battle_nodeid = p;
        }

        public bool isFleetInBattle(int flid)
        {
            if(flid == battle_fleetid)
            {
                if (battel_status != BATTLE_STATUS.NONE && battel_status!= BATTLE_STATUS.FINISHED)
                {
                    return true;
                }
            }
            return false;
        }

        private bool tryFillBackupShips(UserFleet uf)
        {
            if(uf == null)
            {
                log("[自动检查舰队成员失败] 请先在战斗界面设定战斗关卡");
                return false;
            }

            //首先尝试复活主力
            for(int mc =0;mc<all_battle_ships.Count;mc++)
            {
                //不在舰队，且修理好，且不在远征
                UserShip mus = GameData.instance.GetShipById(all_battle_ships[mc]);
                if(mus == null)
                {
                    continue;
                }
                if( ((int)mus.BrokenType) < battlechangetypecombo.SelectedIndex + 2
                    && !uf.ships.Contains(mus.id)
                    && !mus.IsInExplore
                    && !mus.IsInRepair
                    )
                {
                    //复活修理后主力
                    int tobereplace_shipindex = 0;
                    int tobereplace_shipid = -1;
                    int index = 0;
                    foreach(int cursid in uf.ships)
                    {
                        UserShip tbus = GameData.instance.GetShipById(cursid);
                        if(tbus.ship.cid == mus.ship.cid
                            || tbus.ship.evoCid == mus.ship.cid 
                            || tbus.ship.cid == mus.ship.evoCid)
                        {
                            tobereplace_shipid = tbus.id;
                            tobereplace_shipindex = index;
                            break;
                        }
                        if(!all_battle_ships.Contains(cursid))
                        {
                            tobereplace_shipid = tbus.id;
                            tobereplace_shipindex = index;
                        }
                        index++;
                    }
                    if(tobereplace_shipid >0)
                    {
                        UserShip us = GameData.instance.GetShipById(tobereplace_shipid);

                        var p = ServerRequestManager.instance.ChangeFleetShip(uf.id, mus.id, tobereplace_shipindex);
                        if (p != null && p.responseData != null && p.responseData.eid == 0)
                        {
                            log("[自动战斗]主力少女复活！ " + us.ship.title + " Lv." + us.level + "hp:" + us.battleProps.hp + "/" + us.battlePropsMax.hp
                                + " => 主力少女 " + mus.ship.title + " Lv." + mus.level + "hp:" + mus.battleProps.hp + "/" + mus.battlePropsMax.hp);

                        }
                        else
                        {
                            log("[自动战斗]主力少女复活失败... 主力少女:" + mus.ship.title + " Lv." + mus.level + "hp:" + mus.battleProps.hp + "/" + mus.battlePropsMax.hp);

                        }

                        return true;
                    }

                }
            }

            //原来的旗舰要是旗舰
            if (all_battle_ships.Count > 0 
                && uf.ships.Length >0
                && uf.ships.Contains(all_battle_ships[0]) 
                && all_battle_ships[0] != uf.ships[0])
            {
                UserShip oldflag = GameData.instance.GetShipById(uf.ships[0]);
                UserShip newflag = GameData.instance.GetShipById(all_battle_ships[0]);

                var p = ServerRequestManager.instance.ChangeFleetShip(uf.id, newflag.id, 0);
                if (p != null && p.responseData != null && p.responseData.eid == 0)
                {
                    log("[自动战斗] 主力旗舰少女回归旗舰位置！ " + oldflag.ship.title + " Lv." + oldflag.level + "hp:" + oldflag.battleProps.hp + "/" + oldflag.battlePropsMax.hp
                        + " => 新旗舰 " + newflag.ship.title + " Lv." + newflag.level + "hp:" + newflag.battleProps.hp + "/" + newflag.battlePropsMax.hp);
                }else{
                    log("[自动战斗] 主力旗舰少女回归旗舰位置失败... ");
                }
                return true;
            }

           
            for (int i=0 ;i<uf.ships.Length;i++)
            {
                UserShip us = GameData.instance.GetShipById(uf.ships[i]);
                int sbt = (int)us.BrokenType;
                if (sbt > battlechangetypecombo.SelectedIndex + 1 
                    || us.IsInRepair 
                    /*|| us.battleProps.oil<=0 
                    || us.battleProps.ammo <=0*/)
                {
                    int backupshipid = tryGetBackupShipForBattle(us.id);
                    if (backupshipid == -1)
                    {
                        log("[自动战斗] 暂时没有更多少女可用了....稍微休息10秒吧..");
                        battle_counter += 20;
                        return false;
                    }
                    UserShip bus = GameData.instance.GetShipById(backupshipid);
                    if(bus!= null && bus.IsBigBroken == false)
                    {
                        log("[自动战斗]更换"+ ((us.battleProps.oil<=0 || us.battleProps.ammo <=0)?"药渣":"爆衣" )+"少女 (" + battlechangetypecombo.SelectedItem.ToString() + ") " 
                            + us.ship.title + " Lv." + us.level + "hp:" + us.battleProps.hp + "/" + us.battlePropsMax.hp
                                + " => " + bus.ship.title + " Lv." + bus.level + "hp:" + bus.battleProps.hp + "/" + bus.battlePropsMax.hp);

                        var p =ServerRequestManager.instance.ChangeFleetShip(uf.id, bus.id, i);
                        if(p !=null && p.responseData!=null && p.responseData.eid ==0)
                        {

                        }
                        return true;
                    }
                }
            }
            return false;
        }
        private int tryGetBackupShipForBattle()
        {
            return tryGetBackupShipForBattle(-1);
        }
        private int tryGetBackupShipForBattle(int tobe_replace_sid)
        {
            int r = -1;
            if(this.battle_fleetid <= 0 || this.battle_fleetid >4)
            {
                return r;
            }
            UserFleet uf = GameData.instance.UserFleets[this.battle_fleetid -1];
            UserShip tttus = GameData.instance.GetShipById(tobe_replace_sid);
            if (uf == null)
            {
                return r ;
            }
            foreach(var s in all_backupships)
            {
                UserShip us = GameData.instance.GetShipById(s);

                if(!us.IsBigBroken
                    && ((int)us.BrokenType) < battlechangetypecombo.SelectedIndex + 2
                    && !us.IsInRepair
                    && !us.IsInExplore
                    && us.fleetId <=0

                    )
                {

                    bool can_replace = true;
                    foreach(int ssid in uf.ships)
                    {
                        UserShip bus = GameData.instance.GetShipById(ssid);
                        if(bus == null)
                        {
                            continue;
                        }
                        if(us.shipCid == bus.shipCid 
                            || us.ship.cid == bus.ship.evoCid
                            || us.ship.evoCid == bus.ship.cid
                            )
                        {
                            if(tttus!= null && tttus.id != bus.id)
                            {
                                can_replace = false;
                                break;
                            }
                        }
                    }
                    if(can_replace == true)
                    {
                        r = s;
                        break;
                    }

                }

            }
            return r;
        }

        private int tryGetBackupShipForRepair()
        {
            int r = -1;
            foreach (var s in all_backupships)
            {
                UserShip us = GameData.instance.GetShipById(s);

                if ((us.IsBigBroken || ((int)us.BrokenType) > this.repairytpecombo.SelectedIndex)
                    && us.battleProps.hp < us.battlePropsMax.hp
                    && !us.IsInRepair
                    && !us.IsInExplore
                    && us.fleetId <= 0
                    )
                {
                    r = s;
                    break;
                }

            }
            return r;
        }
        private bool auto_battle()
        {
            if(!isInBattle())
            {
                if (autostart_battle == true)
                {
                    
                    try
                    {

                        //if (this.bigbrokenhaiyaosuozi.Checked == true)
                        //{
                        //    log("[警告-警告-警告] 大破进击开关已经打开，请立即停止作死....请立即停止作死....",Color.Red);
                        //}


                        var uinfo = GameData.instance.UserInfo;

                        //黑暗炼钢
                        if(darkforge.Checked == true )
                        {
                            UserFleet ff = GameData.instance.GetFleetOfId(1);
                            if(ff == null 
                                || GameData.instance.IsFleetInExplore(ff.id)
                                || GameData.instance.IsFleetInRepair(ff.id)
                                 )
                            {
                                initing = true;
                                darkforge.Checked = false;
                                autostart_battle = false;
                                autobattlecheck.Checked = false;
                                initing = false;
                                log("[黑暗炼钢不支持非1号舰队] 请让1号舰队舰娘待机...");
                                return false;
                            }
                            if(GameData.instance.UserInfo.detailInfo.shipNum > darkforgetotalshipnum.Value)
                            {
                                initing = true;
                                darkforge.Checked = false;
                                autostart_battle = false;
                                autobattlecheck.Checked = false;
                                initing = false;
                                log("[黑暗炼钢] 已经炼制太多少女，超过提督设定负荷极限 " +darkforgetotalshipnum.Value + "停止炼钢...");
                                return false;
                            }
                        }

                        
                        if(darkforge.Checked == true)
                        {

                            List<UserShip> forgelist = new List<UserShip>();
                            bool need_new_forger = false;
                            //检查舰队
                            UserFleet forgefleet = GameData.instance.GetFleetOfId(1);
                            UserShip flagship = forgefleet.GetUserShips().First();
                            
                            if(flagship.ship.type != ShipType.Destroyer
                                || flagship.level >5
                                || flagship.ship.star >2
                                || forgefleet.ships.Length >1
                                || flagship.battleProps.oil ==0
                                || flagship.battleProps.ammo == 0
                                || flagship.BrokenType == ShipBrokenType.bigBorken
                                || flagship.IsLocked == true
                                )
                            {
                                need_new_forger = true;
                            }
                            //更新预备队
                            if (need_new_forger == true)
                            {
	                            foreach (var dfs in GameData.instance.UserShips)
	                            {
	                                if (dfs.IsLocked == false
	                                    && dfs.level == 1
                                        && dfs.ship.star < 3
	                                    && dfs.ship.type == ShipType.Destroyer
	                                    && dfs.BrokenType == ShipBrokenType.noBroken
	                                    && dfs.battleProps.oil == dfs.battlePropsMax.oil
	                                    && dfs.battleProps.ammo == dfs.battlePropsMax.ammo
	                                    && dfs.fleetId == 0
	                                    && dfs.IsInRepair == false
	                                    )
	                                {
	                                    forgelist.Add(dfs);
	                                }
	                            }
                                if(forgelist.Count == 0)
                                {
                                    initing = true;
                                    darkforge.Checked = false;
                                    autostart_battle = false;
                                    autobattlecheck.Checked = false;
                                    initing = false;
                                    
                                    log("[黑暗炼钢] 缺少祭品，无法炼钢 =>" + forgelist.Count + "枚");
                                    return false;
                                }
                                log("[黑暗炼钢] 预备熔炼少女 =>" + forgelist.Count + "枚");

                                if (forgefleet.ships.Length >1)
                                {
                                    for (int i = 0; i < forgefleet.ships.Length - 1;i++ )
                                    {
                                        log("[黑暗炼钢]" + "移出其他舰队成员");
                                        ServerRequestManager.instance.ChangeFleetShip(forgefleet.id, 0, 0);
                                    }
                                }
                                UserShip forger = forgelist.First();
                                log("[黑暗炼钢]抛弃钢渣少女 => " + tools.helper.getstartstring(flagship.ship.star) + tools.helper.getshiptype(flagship.ship.type) + " "
                                        + flagship.ship.title + " Lv." + flagship.level
                                        + " HP: " + flagship.battleProps.hp + "/" + flagship.battlePropsMax.hp
                                        + " 油:" + flagship.battleProps.oil + " 弹:" + flagship.battleProps.ammo);

                                var ret =ServerRequestManager.instance.ChangeFleetShip(forgefleet.id, forger.id, 0);

                                if(ret!= null && ret.responseData != null && ret.responseData.eid == 0)
                                {
                                    log("[黑暗炼钢] 计划熔炼少女 => " + tools.helper.getstartstring(forger.ship.star) + tools.helper.getshiptype(forger.ship.type) + " " 
                                        + forger.ship.title + " Lv." + forger.level
                                        + "HP: " + forger.battleProps.hp + "/" +forger.battlePropsMax.hp
                                        + " 油:" + forger.battleProps.oil + " 弹:" + forger.battleProps.ammo);
                                }
                                else
                                {
                                    initing = true;
                                    darkforge.Checked = false;
                                    autostart_battle = false;
                                    autobattlecheck.Checked = false;
                                    initing = false;

                                    log("[黑暗炼钢] 更换祭品失败，无法炼钢 " );
                                    return false;
                                }
                            }

                            //开始炼钢
                            PVELevel lv = PVEConfigs.instance.GetLevel(101);
                            UserFleet uf = GameData.instance.GetFleetOfId(1);
                            if (lv != null && uf != null && tools.helper.isFleetCanBattle(uf, battlechangetypecombo.SelectedIndex))
                            {
                                battle_fleetid = uf.id;
                                battle_levelid = lv.id;
                                battle_node_counter = 0;
                                battel_status = BATTLE_STATUS.PREPARE;
                                bcounter = 5;
                                var forger = uf.GetUserShips().First();
                                log("[黑暗炼钢] 开始熔炼少女 => " +tools.helper.getstartstring(forger.ship.star)+ tools.helper.getshiptype(forger.ship.type) + " "
                                    + forger.ship.title + " Lv." + forger.level
                                    + " HP: " + forger.battleProps.hp + "/" + forger.battlePropsMax.hp
                                    + " 油:"+ forger.battleProps.oil + " 弹:"+ forger.battleProps.ammo);
                            }
                            else
                            {
                                initing = true;
                                darkforge.Checked = false;
                                autostart_battle = false;
                                autobattlecheck.Checked = false;
                                initing = false;

                                log("[黑暗炼钢] 炼钢参数错误，无法炼钢 ");
                                return false;
                            }
                        }
                        else
                        {
                            //普通
                            if (uinfo.oil < 1000 || uinfo.ammo < 1000 || uinfo.steel < 1000 || uinfo.aluminium < 200)
                            {
                                log("[自动战斗] 油弹钢铝资源过低，休息30分钟...");
                                battle_counter += 3600;
                                return false;
                            }
                            PVELevel lv = PVEConfigs.instance.GetLevel(battle_levelid);
                            UserFleet uf = GameData.instance.GetFleetOfId(battle_fleetid);
                            if(tryFillBackupShips(uf) == true)
                            {
                                return true;
                            }
                            if (lv != null && uf != null && tools.helper.isFleetCanBattle(uf , battlechangetypecombo.SelectedIndex))
                            {
                                battle_fleetid = uf.id;
                                battle_levelid = lv.id;
                                battle_node_counter = 0;
                                battel_status = BATTLE_STATUS.PREPARE;
                                bcounter = 5;
                            }
                        }


                    }
                    catch (System.Exception ex)
                    {
                        return true;
                    }
                }
                
                return false;
            }
            switch(battel_status)
            {
                case BATTLE_STATUS.PREPARE:
                    if(darkforge.Checked == true)
                    {
                        setBattleStatus(BATTLE_STATUS.SUPPLY);
                        log("[黑暗炼钢] 炼钢少女不许吃喝....");
                    }
                    else if( supplyfleetbeforebattel_check.Checked == false)
                    {
                        setBattleStatus(BATTLE_STATUS.SUPPLY);
                        log("[自动战斗] 黑心提督不许软萌少女吃喝... ≡≡[。。]≡");
                    }
                    else
                    {
                        ServerRequestManager.instance.trySupplyFleet(battle_fleetid);
                    }

                    break;
                case BATTLE_STATUS.SUPPLY:
                    var startret = ServerRequestManager.instance.StartPVELevel(battle_levelid, battle_fleetid, battle_formation);
                    if (startret != null && startret.responseData != null && startret.responseData.eid == 0)
                    {
                        var blevel =PVEConfigs.instance.GetLevel(battle_levelid);
                        this.currentlevel = blevel;
                        this.currentNode = PVEConfigs.instance.GetNode(this.currentlevel.initNodeId);
                        this.currentNodeStatus = new Dictionary<string,int>();
                        this.passedNodes = new List<PVENode>();
                        this.passedNodes.Add(this.currentNode);
                        this.pveLevelEnd = false;
                        this.battle_retry_counter = 10;
                    }

                    break;
                case BATTLE_STATUS.STARTED:
                    if(CanBattle())
                    {
                        var ret =ServerRequestManager.instance.GetNextPVELevelNode();
                        if(ret != null && ret.responseData != null && ret.responseData.eid ==0)
                        {
                            GetNextNodeResponse r = (ret.responseData as GetNextNodeResponse);
                            if(r.node == 0)
                            {
                                log("[自动战斗] 关卡结束，返回港口....");
                                setBattleStatus(BATTLE_STATUS.FINISHED);
                                refreshUIData();
                                return true;
                            }

                            this.currentNode = PVEConfigs.instance.GetNode(r.node);
                            this.currentNodeStatus = r.nodeStatus;
                            this.passedNodes.Add(currentNode);


                            //战斗路点控制
                            if(battle_route.Text!=null && battle_route.Text != "")
                            {
                                char[] shcedule_route = battle_route.Text.ToArray();
                                if(this.passedNodes.Count <= shcedule_route.Length +1)
                                {
                                    string cur_schedule_route_point = shcedule_route[this.passedNodes.Count - 2].ToString().ToUpper();

                                    if (currentNode.flag.ToUpper() != cur_schedule_route_point)
                                    {
                                        string sroute = "";
                                        foreach(var nnn in this.passedNodes)
                                        {
                                            sroute += nnn.flag + " => ";
                                        }
                                        log("[自动战斗-路径控制] 前进路径 " + sroute + "不符合设定路径" + battle_route.Text + "回港重来....");
                                        ServerRequestManager.instance.NotifyPVEBackHome();
                                        break;
                                    }
                                }
                            }


                            if (this.passedNodes.Count > 2)
                            {
                                this.delay_counter += this.secondnodedelaymulti.Value;
                            }
                            else
                            {
                                this.delay_counter += this.battleresultdelayinput.Value;
                            }

                        }
                        else if ( battle_retry_counter >0)
                        {

                            battle_retry_counter--;

                                if (this.passedNodes.Count > 2)
                                {
                                    this.delay_counter += this.secondnodedelaymulti.Value/2;
                                }
                                else
                                {
                                    this.delay_counter += this.battleresultdelayinput.Value;
                                }

                            log("[自动战斗] 提督你肝少女们太快了...服务器响应失败...重试ing... 剩余次数 x " + battle_retry_counter);
                        }
                        else
                        {
                            ServerRequestManager.instance.NotifyPVEBackHome();
                            log("[自动战斗] 服务器娘已经肝倒....回港重来... ");
                        }

                    }else{
                        ServerRequestManager.instance.NotifyPVEBackHome();
                        setBattleStatus(BATTLE_STATUS.FINISHED);
                        log("[舰队有大破需要修理或者修理中] 停止自动战斗....");
                    }
                    break;
                case BATTLE_STATUS.VISIT_NODE:
                    if (currentNode.IsBattleNode == true)
                    {
                        var spyret = ServerRequestManager.instance.SearchPVELevelNode(currentNode.id);
                        if (spyret != null && spyret.responseData != null && spyret.responseData.eid == 0)
                        {
                            SearchEnemyResponse r = (spyret.responseData as SearchEnemyResponse);


                            //避战控制
                            if (avoidbattletext.Text != null && avoidbattletext.Text != "")
                            {
                                char[] shcedule_avoid = avoidbattletext.Text.ToArray();
                                if (this.passedNodes.Count <= shcedule_avoid.Length + 1)
                                {
                                    string cur_schedule_avoid_point = shcedule_avoid[this.passedNodes.Count - 2].ToString();

                                    if (cur_schedule_avoid_point == "1" && r.enemyVO.canSkip == 1)
                                    {
                                        log("[自动战斗-自动回避] 尝试回避 @ " + currentlevel.title + "#" + currentNode.flag);
                                        var skipret = ServerRequestManager.instance.SkipPVELevelNode(currentNode.id);
                                        if (skipret != null && skipret.responseData != null && skipret.responseData.eid == 0)
                                        {
                                            var sret = skipret.responseData as SkipWarResponse;
                                            if(sret.isSuccess == 1)
                                            {
                                                //避战成功，下一个点
                                                log("[自动战斗-自动回避] 回避成功 @ " + currentlevel.title + "#" + currentNode.flag);
                                      
                                                setBattleStatus(BATTLE_STATUS.STARTED);
                                                return true;
                                            }
                                            else
                                            {
                                                log("[自动战斗-自动回避] 回避失败 @ " + currentlevel.title + "#" + currentNode.flag);
                                      
                                            }
                                        }

                                    }

                                }
                            }
                            //战斗 敌人 舰队组成 控制
                            if (searchenemycase.Text != null && searchenemycase.Text != "")
                            {
                                char[] enemycase = searchenemycase.Text.ToArray();
                                if (this.passedNodes.Count <= enemycase.Length + 1)
                                {
                                    string cur_enemycase_point = enemycase[this.passedNodes.Count - 2].ToString().ToUpper();

                                    if (judge_enemy_case(r.enemyVO.enemyShips, cur_enemycase_point) == dofightinstedofback.Checked)
                                    {
                                        string sroute = "";
                                        foreach (var nnn in this.passedNodes)
                                        {
                                            sroute += nnn.flag + " => ";
                                        }
                                        log("[自动战斗-索敌控制] " + (dofightinstedofback.Checked? "不符合设定索敌条件， 回港重来....":"符合设定 避开 索敌条件， 回港重来....") );
                                        ServerRequestManager.instance.NotifyPVEBackHome();
                                    }
                                }
                            }
                        }
                        else
                        {
                            ServerRequestManager.instance.NotifyPVEBackHome();
                            log("[自动战斗]索敌错误，回港重来...");
                        }
                    }
                    else
                    {
                        log("[自动战斗-索敌控制] 资源点不索敌....");
                        setBattleStatus(BATTLE_STATUS.SPY_NODE);
                    }
                    break;

                case BATTLE_STATUS.SPY_NODE:
                    UserFleet buf = GameData.instance.GetFleetOfId(battle_fleetid);
                    UserShip[] nowshipstatus = new UserShip[buf.ships.Length];
                    for(int iii =0;iii < buf.ships.Length;iii++)
                    {
                        nowshipstatus[iii] = GameData.instance.GetShipById(buf.ships[iii]);
                    }
                    CurrentWarParameters.shipsbeforebattle = nowshipstatus;

                    int nodeformation = -1;
                    if (formation_control.Text != null && formation_control.Text != "")
                    {
                        char[] formation_controlpoint = formation_control.Text.ToArray();
                        if (this.passedNodes.Count <= formation_controlpoint.Length + 1)
                        {
                            string formation_control_point = formation_controlpoint[this.passedNodes.Count - 2].ToString();

                            if (formation_control_point != "")
                            {
                                nodeformation = int.Parse(formation_control_point);
                                if (nodeformation < 1 || nodeformation > 5)
                                {
                                    nodeformation = -1;
                                }
                            }
                        }
                    }
                    log("[自动战斗] 舰队阵型:" + tools.helper.getformationstring(((nodeformation == -1 ? battle_formation : (FleetFormation)nodeformation))));
                    var firstbattl = ServerRequestManager.instance.DealPVELevelNode(currentNode.id,battle_fleetid, nodeformation == -1 ? battle_formation:((FleetFormation)nodeformation));
                    if (firstbattl!= null && firstbattl.responseData!= null && firstbattl.responseData.eid==0)
                    {
                        var respone = (firstbattl.responseData as GetDealNodeResponse);

                        if (respone.pveLevelEnd != null && respone.pveLevelEnd == 1)
                        {
                            this.pveLevelEnd = true;
                        }

                        if (currentNode.IsBattleNode == true)
                        {
                            if (respone.warReport != null)
                            {
                                dealnode_enemyclearornot = respone.warReport.canDoNightWar == 0;
                            }
                            bool extranightwarswitch =false;
                            if(night_war_control.Text!=null && night_war_control.Text != "")
                            {
                                char[] night_war_controlpoint = night_war_control.Text.ToArray();
                                if (this.passedNodes.Count <= night_war_controlpoint.Length + 1)
                                {
                                    string night_war_control_point = night_war_controlpoint[this.passedNodes.Count - 2].ToString();

                                    if (night_war_control_point == "1")
                                    {
                                        extranightwarswitch = true;
                                    }
                                }
                            }

                            CurrentWarParameters.daybattleresult = respone;
                            if (detailbattlelog.Checked == true)
                            {
                                z.log( tools.helper.getDetailDayWarresultstring(respone, battle_fleetid));
                            }

                            if (dealnode_enemyclearornot)
                            {
                                log("[日战胜利!!]少女战利品回收中.. ");
                            }
                            else
                            {
                                log((this.nightwarcheck.Checked||extranightwarswitch) == true ? "[日战失利!!]提督判定大群少女继续夜战..... " : "[日战失利] 提督决定不参与夜战活动..");
                            }
                        }
                        else
                        {
                            if (this.currentNode.nodeType == PVENodeType.addItem)
                            {
                                if(currentNode.gain != null)
                                foreach (var gkv in currentNode.gain)
                                {
                                	log("[自动战斗] 在关卡"+currentlevel.title + " "+currentNode.flag + "点 遇到好心人，获得 " + tools.helper.getresourcetype(gkv.Key)+ " x " + gkv.Value);
                                }
                            }
                            else if (this.currentNode.nodeType == PVENodeType.loseItem)
                            {
                                if(currentNode.loss != null)
                                foreach (var gkv in currentNode.loss)
                                {
                                	log("[自动战斗] 在关卡"+currentlevel.title + " "+currentNode.flag + "点 遇到坏蛋，丢失 " + tools.helper.getresourcetype(gkv.Key)+ " x " + gkv.Value);
                                }
                            }
                            else if (this.currentNode.nodeType == PVENodeType.empty)
                            {
                                log("[自动战斗] 在关卡"+currentlevel.title + " "+currentNode.flag + "点 什么也没有发生............真的");
                            }
                            setBattleStatus(BATTLE_STATUS.STARTED);
                        }

                    }
                    else if (battle_retry_counter >0)
                    {

                        battle_retry_counter--;

                            if (this.passedNodes.Count > 2)
                            {
                                this.delay_counter += this.secondnodedelaymulti.Value;
                            }
                            else
                            {
                                this.delay_counter += this.battleresultdelayinput.Value;
                            }

                        log("[自动战斗] 提督你肝少女们太快了...服务器响应失败...重试ing... 剩余次数 x " + battle_retry_counter);
                    }
                    else
                    {
                        ServerRequestManager.instance.NotifyPVEBackHome();
                        log("[自动战斗] 服务器娘已经肝倒....回港重来... ");
                    }
                    this.delay_counter += this.battleresultdelayinput.Value;
                    break;
                case BATTLE_STATUS.NODE_BATTLE:

                        //夜战额外控制
                            bool dobattleextranightwarswitch =false;
                            if(night_war_control.Text!=null && night_war_control.Text != "")
                            {
                                char[] dobattlenight_war_controlpoint = night_war_control.Text.ToArray();
                                if (this.passedNodes.Count <= dobattlenight_war_controlpoint.Length + 1)
                                {
                                    string dobattlenight_war_control_point = dobattlenight_war_controlpoint[this.passedNodes.Count - 2].ToString();

                                    if (dobattlenight_war_control_point == "1")
                                    {
                                        dobattleextranightwarswitch = true;
                                    }
                                }
                            }

                    var battleret = ServerRequestManager.instance.GetPVEBattleResult( (!dealnode_enemyclearornot) && (battle_do_nightwar==1 ||dobattleextranightwarswitch) , battle_fleetid);
                    if (battleret != null && battleret.responseData != null && battleret.responseData.eid == 0)
                    {
                        z.instance.setBattleStatus(BATTLE_STATUS.BATTLE_RESULT);
                        var battleResult = battleret.responseData as GetBattleResultResponse;

                        if (detailbattlelog.Checked == true)
                        {
                            z.log(tools.helper.getDetailNightWarresultstring(battleResult, battle_fleetid));

                        }
                        z.log("[少女卖肉报酬入手]..." + tools.helper.getwarresultstring(battleResult, battle_fleetid));
                        tools.reporter.reportGotShip(battleResult, battle_fleetid, currentlevel.title, currentNode.flag);

                        //开关开了，5场 s ss 不掉船就停止捞船


                        if(stopbattlewhennodrop_check.Checked == true)
                        {
                            WarResultLevel wrl = (WarResultLevel)battleResult.warResult.resultLevel;
                            if(battleResult.newShipVO!= null && battleResult.newShipVO.Length >0)
                            {
                                nodropcounter = 0;
                            }
                            else if (wrl == WarResultLevel.s || wrl == WarResultLevel.ss)
                            {
                                nodropcounter++;
                                log("[自动战斗] S/SS胜无掉落...计数: " + nodropcounter + "  , 5次后停止自动战斗");
                                
                            }

                            if(nodropcounter >=5 && autobattlecheck.Checked == true)
                            {

                                initing = true;
                                autostart_battle = false;
                                autobattlecheck.Checked = autostart_battle;
                                initing = false;
                            }
                        }

                    }
                    else if (battle_retry_counter >0)
                    {

                        battle_retry_counter--;
                        log("[自动战斗] 提督你肝少女们太快了...服务器响应失败...重试ing... 剩余次数 x " + battle_retry_counter);
                        this.delay_counter += this.battleresultdelayinput.Value;
                    }
                    else
                    {
                        ServerRequestManager.instance.NotifyPVEBackHome();
                        log("[自动战斗] 服务器娘已经肝倒....回港重来... ");
                    }
                    if ((!dealnode_enemyclearornot) && (battle_do_nightwar == 1 || dobattleextranightwarswitch))
                    {
                    	this.delay_counter += this.battleresultdelayinput.Value;
                    }
                    break;
                case BATTLE_STATUS.BATTLE_RESULT:
                    //todo think logic
                    battle_node_counter++;
                    if(this.pveLevelEnd == true)
                    {
                        log("[自动战斗] 关卡结束，返回港口....");
                        setBattleStatus(BATTLE_STATUS.FINISHED);
                        refreshUIData();
                        return true;
                    }
                    if (battle_node_counter >= this.battlenodenuminput.Value)
                    {
                        ServerRequestManager.instance.NotifyPVEBackHome();
                    }
                    else
                    {
                        setBattleStatus(BATTLE_STATUS.STARTED);
                    }

                    refreshUIData();
                    break;

                default:
                    return false;
                    break;
            }
            return true;
        }

        private bool CanBattle()
        {
            foreach (var s in GameData.instance.GetFleetOfId(battle_fleetid).ships )
            {

                //if (this.bigbrokenhaiyaosuozi.Checked == false)
                {
                    UserShip us = GameData.instance.GetShipById(s);
                    if (us.damageLevel >= 2)
                    {
                        return false;
                    }
                    if(us.IsInRepair)
                    {
                        log("[警告] 舰队中有少女 "+us.ship.title + " Lv."+us.level+" 正在洗澡，取消战斗", Color.Red);
                        return false;
                    }
                }
                /*else{
                    log("[警告-警告-警告] 大破进击开关已经打开，请立即停止作死....请立即停止作死....", Color.Red);
                }*/

            }
            return true;
        }

        private bool auto_PVEExpllore()
        {
            int[] fl = { 0, 0, 0, 0, 0 };
            Dictionary<int, int> explist = new Dictionary<int, int>();
            foreach (UserPVEExplore explore in GameData.instance.GetUserPVEExplores())
            {
                fl[explore.fleetId] = 1;
                explist[explore.exploreId] = explore.fleetId;
                long eta = ServerTimer.GetTimeLeftTo(explore.startTime, explore.endTime);
                if (eta < 1)
                {
                    ServerRequestManager.instance.FinishPVEExplore(explore.exploreId);
                    return true;
                }
            }
            if (autoexplorecheck.Checked == true)
            {
                return false;
            }
            int i = 1;
            foreach (UserFleet uf in GameData.instance.UserFleets)
            {
                if (fl[uf.id] == 1)
                {
                    i++;
                    continue;

                }
                string can = (string)tools.configmng.instance.getval(username.Text + "_explore_" + i);
                if (can != null && can.Equals("no") == false)
                {
                    try
                    {
                        int expid = int.Parse(can);
                        PVEExploreLevelConfig pe = GameConfigs.instance.GetPVEExploreLevel(expid);
                        if (pe != null && !explist.ContainsKey(pe.id) 
                            && !explist.ContainsValue(uf.id)
                            && isFleetInBattle(uf.id) == false
                            && GameData.instance.IsFleetInExplore(uf.id) == false)
                        {
                            bool needsupply = false;
                            foreach(int sste in uf.ships)
                            {
                                var sstus = GameData.instance.GetShipById(sste);
                                if(sstus.battleProps.oil < sstus.battlePropsMax.oil || sstus.battleProps.ammo < sstus.battlePropsMax.ammo)
                                {
                                    needsupply = true;
                                }
                            }
                            if(needsupply == true)
                            {
                                log("[开始自动远征] 远征前补给 " + uf.title + " -- " + pe.title );
                                ServerRequestManager.instance.SupplyFleet(uf.id);
                                System.Threading.Thread.Sleep(200);
                            }


                            log("[开始自动远征] " + uf.title + " -- " + pe.title + " " + pe.TimeNeed + " 后回港");
                            ServerRequestManager.instance.StartPVEExplore(uf.id, pe.id);
                            return true;
                        }

                    }
                    catch (Exception)
                    {
                        return true;
                    }
                }
                i++;

            }
            return false;
        }

        private bool auto_repair()
        {
            Queue<int> useable_dock = new Queue<int>();

            List<RepairDockInfo> rdi = new List<RepairDockInfo>();
            //修好的和自动吃桶
            foreach (var dock in GameData.instance.UserRepairDock)
            {
                if(dock.locked == 0)
                {
                    if( dock.shipId !=0 )
                    {
                        if (dock.IsFinished == true)
                        {
                            int shipid = dock.shipId;
                            var r =ServerRequestManager.instance.FinishRepair(shipid, dock.id);
                            if(r.responseData!= null && r.responseData.eid ==0)
                            {

                                if(all_backupships.Contains(shipid))
                                {
                                    //log("[自动修理] 后备队)
                                }
                            }
                            return true;
                        }
                        else
                        {
                            rdi.Add(dock);
                        }
                    }else{
                        useable_dock.Enqueue(dock.id);
                    }
                }
            }

            //自动吃桶，优先吃战斗队伍中的，然后吃时间最长的
            if (this.autofastrepaircheck.Checked == true && GameData.instance.GetItemAmount(ResourceTypes.FastRepair)>0)
            {
                if(this.usetongwhennobackup.Checked == true)
                {
                    if (this.battle_fleetid > 0 && this.autobattlecheck.Checked == true)
                    {
                        //在战斗舰队中的优先吃桶
                        UserFleet uf = GameData.instance.GetFleetOfId(battle_fleetid);
                        if (uf != null && uf.ships != null)
                        {
                            foreach (var dc in rdi)
                            {
                                if (uf.ships.Contains(dc.shipId) == true)
                                {
                                    log("[自动吃桶][优先修理战斗舰队中船只] 少女你该起床了.....");
                                    var r = ServerRequestManager.instance.InstantRepair(dc.id, 1);
                                    return true;
                                }
                            }
                        }
                    }
                    //检测时要传入大破船只
                    int tobereplacesid = -1;
                    UserFleet ruf = GameData.instance.GetFleetOfId(this.battle_fleetid);
                    if(ruf != null&& ruf.ships!= null)
                    {
                        foreach(int rsid in ruf.ships)
                        {
                            UserShip us = GameData.instance.GetShipById(rsid);
                            if(us == null)
                            {
                                continue;
                            }
                            int sbt = (int)us.BrokenType;
                            if(sbt > repairytpecombo.SelectedIndex)
                            {
                                tobereplacesid = us.id;
                                break;
                            }
                        }
                    }
                    //如果没有后备队了...
                    if (tryGetBackupShipForBattle(tobereplacesid) <= 0 && rdi.Count >0)
                    {
                        var longgestdock = rdi.Where(o => o.IsFinished == false).OrderByDescending(o => o.TimeLeft).Take(1);
                        log("[自动吃桶][优先修理泡澡时间最长船只] 少女你该起床了.....");
                        var r = ServerRequestManager.instance.InstantRepair(longgestdock.First().id, 1);
                        return true;
                    }
                }
                else if(this.usetongwhenfull.Checked == true)
                {
                    if (this.battle_fleetid > 0 && this.autobattlecheck.Checked == true)
                    {
                        //在战斗舰队中的优先吃桶
                        UserFleet uf = GameData.instance.GetFleetOfId(battle_fleetid);
                        if (uf != null && uf.ships != null)
                        {
                            foreach (var dc in rdi)
                            {
                                if (uf.ships.Contains(dc.shipId) == true)
                                {
                                    log("[自动吃桶][优先修理战斗舰队中船只] 少女你该起床了.....");
                                    var r = ServerRequestManager.instance.InstantRepair(dc.id, 1);
                                    return true;
                                }
                            }
                        }
                    }

                    //如果满了，就给最长的吃桶
                    if (useable_dock.Count == 0 && rdi.Count > 0)
                    {
                        var longgestdock = rdi.Where(o => o.IsFinished == false).OrderByDescending(o => o.TimeLeft).Take(1);
                        log("[自动吃桶][优先修理泡澡时间最长船只] 少女你该起床了.....");
                        var r = ServerRequestManager.instance.InstantRepair(longgestdock.First().id, 1);
                        return true;
                    }
                }
                else
                {
                    if(rdi.Count >0)
                    {
                        var longgestdock = rdi.Where(o => o.IsFinished == false).OrderByDescending(o => o.TimeLeft).Take(1);
                        log("[自动吃桶]少女你该起床了.....");
                        var r = ServerRequestManager.instance.InstantRepair(longgestdock.First().id, 1);
                        return true;
                    }
                }
                


            }



            if(autorepaircheck.Checked == false)
            {
                return false;
            }
            if(useable_dock.Count == 0)
            {
                return false;
            }

            //舰队中少女
            List<int> usedships = new List<int>();
            Dictionary<int, UserShip> slist = new Dictionary<int, UserShip>();
            
            foreach (UserFleet us in GameData.instance.UserFleets)
            {
                foreach (int s in us.ships)
                {
                    usedships.Add(s);
                }
            }

            //先修后备队中破了的
            int bbrsid = tryGetBackupShipForRepair();
            while (bbrsid != -1)
            {
                if (useable_dock.Count == 0)
                {
                    break;
                }

                UserShip us = GameData.instance.GetShipById(bbrsid);
                if (us == null)
                {
                    continue;
                }
                if(doAutoRepair(ref useable_dock, ref usedships, us) == true)
                {
                    return true;
                }
                bbrsid = tryGetBackupShipForRepair();
            }

            //没后备队可用优先修舰队
            if ((all_backupships.Count ==0 || tryGetBackupShipForBattle() == -1) && autostart_battle == true)
            {
	            foreach(UserFleet muf in GameData.instance.UserFleets)
	            {
	                if(GameData.instance.IsFleetInExplore(muf.id))
	                {
	                    continue;
	                }
	                foreach(int msid in muf.ships)
	                {
	                    if (useable_dock.Count == 0)
	                    {
	                        break;
	                    }
                        UserShip mus = GameData.instance.GetShipById(msid);
	                    if (doAutoRepair(ref useable_dock, ref usedships, mus,true))
	                    {
                            log("[自动战斗中强制修理] 后备队已经没有可用的少女， 先强制修理当前舰队队员吧...");
	                        return true;
	                    }
	                }
	            }
            }

            //修其他闲置船只
            foreach (UserShip us in GameData.instance.UserShips)
            {
                if (useable_dock.Count == 0)
                {
                    break;
                }
                if(doAutoRepair(ref useable_dock,ref usedships, us))
                {
                    return true;
                }
            }


            return false;
        }

        private bool doAutoRepair(ref Queue<int> useable_dock, ref List<int> usedships, UserShip us ,bool repairfleet = false)
        {
            if (useable_dock.Count == 0)
            {
                return false;
            }
            if (us.IsInExplore || us.IsInRepair)
            {
                return false;
            }
            if( repairfleet == false &&  usedships.Contains(us.id) )
            {
                return false;
            }
            int sbt = (int)us.BrokenType;
            if(sbt < repairytpecombo.SelectedIndex+1)
            {
                return false;
            }
            if(us.battleProps.hp >= us.battlePropsMax.hp)
            {
                return false;
            }
            //string can = (string)tools.configmng.instance.getval(username.Text + "_auto_repair");
            if (autorepaircheck.Checked == true)
            {
                int dockid = useable_dock.Dequeue();
                try
                {
                    log("[开始自动修理] 槽位：" + dockid + " 少女 " + us.ship.title + " Lv." + us.level
                        + " HP:" + us.battleProps.hp + "/" + us.battlePropsMax.hp + "\r\n    消耗 油-"
                        + us.RepairOilNeeds + " 钢:" + us.RepairSteelNeeds);
                    ServerRequestManager.instance.StartRepair(us.id, dockid);

                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }


        private bool auto_dis()
        {
            if(dis_sel_blocker == true)
            { 
                return false;
            }
            var uinfo = GameData.instance.UserInfo;
            if(dis_ship == false &&　doautodis_check.Checked == true && GameData.instance.UserShips.Count >= uinfo.detailInfo.shipNumTop)
            {
                log("[警告！警告！警告！][自动拆船开始...]");
                dis_ship = true;
            }
            if(dis_ship)
            {

                List<UserShip> dislist = new List<UserShip>();
                var allowdislist = tools.configmng.instance.getDisShipList();
                foreach (UserShip us in GameData.instance.UserShips)
                {
                    if(us.fleetId > 0
                        ||us.IsInRepair 
                        || us.IsInExplore
                        || us.IsLocked
                        || !allowdislist.ContainsKey(us.shipCid.ToString())
                        || us.level >5
                        || us.ship.star >4
                        )
                    {
                        continue;
                    }
                    dislist.Add(us);
                    //if (dislist.Count >= 10)
                    //{
                    //    break;
                    //}
                }
                if(dislist.Count >0)
                {
                    if (auto_feed_fleet_with_disship_check.Checked == true)
                    {
                        //查找当前舰队 可强化/升级技能对象

                        if (auto_feed_fleet_upgradeskill_check.Checked == true)
                        {
                        	tryUpgradeshipskill();
                        }

                        if(tryFeedFleet(dislist))
                        {
                            return true;
                        }
                        log("[自动强化]找不到可以强化的方法，手动吃吧...");
                    }
                    string r = "准备拆解:\r\n";
                    int i = 0;
                    List<UserShip> todislist = new List<UserShip>();
                    foreach (var u in dislist)
                    {
                        i++;
                        todislist.Add(u);
                        if (i >= 10)
                        {
                            break;
                        }
                    }
                    foreach (var u in todislist)
                    {
                        r += tools.helper.getshiptype(u.ship.type) + " " + u.ship.title + " Lv." + u.level + " " + tools.helper.getstartstring(u.ship.star) + "\r\n";
                    }
                    dis_sel_blocker = true;
                    DialogResult result = DialogResult.No;
                    if (this.disswitch_button.Value == true)
                    {
                        result = MessageBox.Show(r, "确定要拆解下列船只？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    dis_sel_blocker = false;
                    if (result == DialogResult.Yes)
                    {

                        var ret = ServerRequestManager.instance.DestroyShips(todislist, 1);
                        if (ret != null && ret.responseData != null && ret.responseData.eid == 0)
                        {
                            log("[拆解成功]:\r\n" + r);
                        }

                        return true;

                    }
                    else
                    {
                        initing = true;
                        log("[选择不拆，取消自动拆船]");
                        doautodis_check.Checked = false;
                        initing = false;
                    }
                }
               
            }

            if (dis_equip)
            {

                List<UserEquipment> dislist = new List<UserEquipment>();
                var allowdisequiplist = tools.configmng.instance.getDisEquipList();
                foreach (UserEquipment us in GameData.instance.UserEquipments)
                {
                    if (us.status > 0
                        || !allowdisequiplist.ContainsKey(us.config.cid.ToString())
                        || us.config.star >4
                        )
                    {
                        continue;
                    }
                    dislist.Add(us);
                    if (dislist.Count >= 40)
                    {
                        break;
                    }
                }
                if (dislist.Count > 0)
                {
                    string r = "准备拆解:\r\n";
                    foreach (var u in dislist)
                    {
                        r += tools.helper.getEquipmentTypeString(u.config.type) + " " + u.config.title + " " + tools.helper.getstartstring(u.config.star) + "\r\n";
                    }
                    dis_sel_blocker = true;
                    DialogResult result = DialogResult.No;
                    if (this.disswitch_button.Value == true)
                    {
                        result = MessageBox.Show(r, "确定要拆解下列装备？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    dis_sel_blocker = false;
                    if (result == DialogResult.Yes)
                    {

                        var ret = ServerRequestManager.instance.DestroyEquips(dislist);
                        if (ret != null && ret.responseData != null && ret.responseData.eid == 0)
                        {
                            log("[拆解成功]:\r\n" + r);
                        }

                        return true;

                    }
                }

            }

            dis_ship = false;
            dis_equip = false;
            switchdisui(true);
            return false;
        }

        public bool auto_quest()
        {
            if(this.autofinishquest.Checked == false)
            {
                return true;
            }
            log("[自动任务]更新任务信息");
            ServerRequestManager.instance.GetQuests();

            foreach(var qu in GameData.instance.AllQuests)
            {
                if(qu.IsFinished == true)
                {
                    log("[自动任务]尝试完成任务: "+ qu.title);
                    ServerRequestManager.instance.FinishQuest(qu);
                    log("[自动任务]完成任务: " + qu.title + " ,奖励" + tools.helper.getQuestRewardStr(qu));
                    return false;
                }
            }


            return true;
        }

        public bool auto_dev()
        {
            int rc = 0;
            if(autodevnum.Text == null || autodevnum.Text == "")
            {
                return false;
            }
            try
            {
                rc = int.Parse(autodevnum.Text);
            }catch(Exception e)
            {

            }
            if (rc <= 0)
            {
                return false;
            }

            if (this.autodevcheck.Checked == true)
            {
                foreach (var ued in GameData.instance.UserEquipDocks)
                {
                    if (ued!=null && ued.locked ==0 )
                    {
                        if(ued.IsFinished == true && ued.equipmentCid >0)
                        {
                            log("[自动建造/开发]领取物品: " + tools.helper.getEquipmentDesc(ued.equipmentCid));
                            var r= ServerRequestManager.instance.GetEquipFromDock(ued.id);
                            if (r != null && r.responseData != null && r.responseData.eid == 0)
                            {
                                GetEquipData bed = r.responseData as GetEquipData;

                                if (bed != null && bed.equipmentVo != null)
                                {
                                    var v = bed.equipmentVo;
                                    {
                                        var es = tools.helper.getEquipmentDesc(v.equipmentCid);
                                        //MessageBox.Show(es);
                                        log("[自动建造/开发]获得装备:\r\n" + es);
                                        return false;
                                    }
                                }
                            }
                        }else if(ued.equipmentCid ==0)
                        {
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
                            var uinfo = GameData.instance.UserInfo;
                            if (uinfo.oil > res[0] 
                                && uinfo.ammo > res[1] 
                                && uinfo.steel > res[2] 
                                && uinfo.aluminium > res[3]
                                && GameData.instance.GetItemAmount(ResourceTypes.BuildEquipItem)>0
                                )
                            {
                                log("[自动建造/开发]开发物品: " + res[0] + "/" + res[1] + "/" + res[2] + "/" + res[3]);
                                ServerRequestManager.instance.BuildEquipInDock(ued.id, res[0], res[2], res[1], res[3]);
                                rc--;
                                autodevnum.Text = rc.ToString();
                                return true;
                            }
                        }
                    }
                }

            }
            
            if (this.autobuildcheck.Checked == true)
            {
                foreach (var ued in GameData.instance.UserDocks)
                {
                    if (ued!=null && ued.locked ==0) 
                    {
                        if(ued.IsFinished == true && ued.shipType >0)
                        {
                            log("[自动建造/开发]领取船只: " );
                            var r=ServerRequestManager.instance.GetShipFromDock(ued.id);
                            if (r != null && r.responseData != null && r.responseData.eid == 0)
                            {
                                GetShipData bed = r.responseData as GetShipData;

                                if (bed != null && bed.dockVo != null)
                                {
                                    var v = bed.shipVO;
                                    {
                                        var es = tools.helper.getShipDesc(v.ship.cid);
                                        //MessageBox.Show(es);
                                        log("[自动建造/开发]获得船只:\r\n" + es);
                                        return false;
                                    }
                                }
                            }
                        }else if(ued.shipType ==0)
                        {
                            int[] res = { 10, 10, 10, 10 };
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
                            var uinfo = GameData.instance.UserInfo;
                            if(uinfo.oil > res[0] 
                                && uinfo.ammo > res[1] 
                                && uinfo.steel > res[2] 
                                && uinfo.aluminium > res[3]
                                && GameData.instance.GetItemAmount(ResourceTypes.BuildShipItem) > 0
                                )
                            {
                                log("[自动建造/开发]建造船只: " + res[0] + "/" + res[1] + "/" + res[2] + "/" + res[3]);
                                ServerRequestManager.instance.BuildShipInDock(ued.id, res[0], res[2], res[1], res[3]);
                                rc--;
                                autodevnum.Text = rc.ToString();
                                return true;
                            }

                        }
                    }
                }

            }

            return false;
        }
        public static void log(string l)
        {
            log(l, Color.Black);
        }
        public static void log(string l, Color color)
        {
            if (l == null)
                return;
            instance._log(l, color);
        }
        public void _log(string l, Color color)
        {
            string timestap = "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            string fname = "log_" +DateTime.Now.ToString("yyyy-MM-dd") +"_.txt";
            string log = timestap + l + "\r\n";
            System.IO.File.AppendAllText(fname, log, Encoding.UTF8);
            if(logger.Text.Length > 100000)
            {
                logger.Text = "日志滚动清除，请去 " + fname + " 查看之前日志";
            }
            logger.AppendText(log);

            logger.Select(logger.Text.Length, 0);
            logger.ScrollToCaret();
        }
        public string getphone_choose()
        {
            return  phonetypecombo.SelectedItem as string;
        }

        public string get_username()
        {
            return username.Text;
        }
        public string get_password()
        {
            return password.Text;
        }

        public void save_usernamepw()
        {
            tools.configmng.instance.setval("username", username.Text);
            tools.configmng.instance.setval("passwd", password.Text);
            tools.configmng.instance.savecfg();
            int i = 0;
            foreach (ForChooseServer fcs in GameData.instance.LoginResponse.serverList)
            {
                serverList.Items.Add(fcs.name);
                if (fcs.id == GameData.instance.LoginResponse.defaultServer)
                {
                    serverList.SelectedIndex = i;
                }
                i++;
            }

            this.choose_server.Enabled = true;
        }
        private void initphonecombo()
        {
            phonetypecombo.Items.Clear();
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_GT-I9300_Build/JSS15J)");

            phonetypecombo.Items.Add("Dalvik/1.4.0_(Linux;_U;_Android_2.3.5;_SHW-M250K_Build/GINGERBREAD)");
            phonetypecombo.Items.Add("Dalvik/1.4.0_(Linux;_U;_Android_2.3.6;_SHV-E120S_Build/GINGERBREAD)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.0.3;_Full_AOSP_on_Tcl_Amber3_Build/IML74K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.0.4;_SHV-E160S_Build/IMM76D)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.1;_HTL21_Build/JRO03C)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.1;_SHV-E210S_Build/JRO03C)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_GT-I8552_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_GT-N7105_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_GT-S7390_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_IM-A800S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_IM-A830L_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_IM-A830S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_IM-A850L_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_IM-A870K_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_IM-A870S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SC-06D_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E110S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E120K_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E120S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E160K_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E160L_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E160S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E170K_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E170S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E210K_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E210S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E220S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E250K_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E250L_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E250S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E270K_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E270L_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E270S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHV-E275S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHW-M250K_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHW-M250L_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHW-M250S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHW-M440S_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SHW-M585D_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_SM-T210L_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.1.2;_YP-GI2_Build/JZO54K)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.1;_Micromax_A116_Build/JOP40D)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_6043D_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_AMLOGIC8726MX_Build/V440R270C089B0530CD)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_C6903_Build/14.1.G.1.534)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_DREAM_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_GT-I9060_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_GT-I9505_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_GT-P5220_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_IM-A880S_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_IM-A890K_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_IM-A890S_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_IM-A900K_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_IM-A900S_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_Lenovo_A369i_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_M8_SDK12_DDR1G_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_Micromax_A94_Build/A94)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_O__8.36z_Android_Build/OplusO__8.36z_Android)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_POLYTRON_R1500_Build/POLYTRON_R1500-V001-2014.07.01)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_R1001_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SHV-E300S_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SHV-E310K_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SHV-E310L_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SHV-E310S_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SHV-E330K_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SHV-E330L_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SHV-E330S_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SHV-E370K_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SHV-E470S_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SM-C105S_Build/JDQ39)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.2.2;_SOL22_Build/10.3.1.D.0.220)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_??T6_Build/1447.01.1QLTE)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_H700_Build/JLS36C)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_HM_1SW_MIUI/JHCCNBL50.0)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_LT29i_Build/9.2.A.1.205)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_Q10_Build/10.3.1.191)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SC-04E_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SCL22_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E210K_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E210L_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E210S_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E250K_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E250L_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E250S_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E300K_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E300S_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E330K_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E330L_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHV-E330S_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SHW-M440S_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SM-G710K_Build/JLS36C)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SM-G710S_Build/JLS36C)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SM-G910S_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SM-N750K_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SM-N750S_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SM-N900K_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SM-N900S_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.3;_SM-N900T_Build/JSS15J)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_GT-N7100_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_GT-N7105_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_HM_NOTE_1LTETD_MIUI/KHICNBH14.0)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_IM-A870K_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_IM-A890L_Build/KVT49L)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SCH-I605_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SHV-E250K_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SHV-E250L_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SHV-E250S_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SHV-E300K_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SHV-E300L_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SHV-E300S_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SHV-E310S_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SHV-E330K_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SHV-E330L_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SHV-E330S_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SM-G906L_Build/KVT49L)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SM-G906S_Build/KVT49L)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SM-N750K_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SM-N750_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SM-N9005_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SM-N900L_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_SM-N900S_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.2;_gboard_7.9_Build/KOT49H)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.3;_HTC_One_Build/KTU84L)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_C6602_Build/10.5.1.A.0.283)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_D6503_Build/23.0.1.A.0.167)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_D6653_Build/23.0.1.A.1.49)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_SHV-E210K_Build/KTU84P)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_SHV-E210S_Build/KTU84P)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_SHV-E370K_Build/KTU84P)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_SM-G5309W_Build/KTU84P)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_SM-G720N0_Build/KTU84P)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_SM-N910K_Build/KTU84P)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_SM-N910L_Build/KTU84P)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4.4;_SM-N910V_Build/KTU84P)");
            phonetypecombo.Items.Add("Dalvik/1.6.0_(Linux;_U;_Android_4.4;_Nexus_5_Build/KRT16M)");
            phonetypecombo.Items.Add("Dalvik/2.1.0_(Linux;_U;_Android_5.0.1;_Nexus_5_Build/LRX22C)");
            phonetypecombo.Items.Add("Dalvik/2.1.0_(Linux;_U;_Android_5.0.1;_SM-N916K_Build/LRX22C)");
            phonetypecombo.Items.Add("Dalvik/2.1.0_(Linux;_U;_Android_5.0;_SM-G900L_Build/LRX21T)");
            phonetypecombo.Items.Add("Dalvik/2.1.0_(Linux;_U;_Android_5.0;_SM-G900S_Build/LRX21T)");

            phonetypecombo.SelectedIndex = 0;

        }

        public string getServerName()
        {
            return servername;
        }
        private void choose_server_Click(object sender, EventArgs e)
        {
            ForChooseServer host = null;
            foreach (ForChooseServer fcs in GameData.instance.LoginResponse.serverList)
            {
                if (fcs.name == serverList.SelectedItem.ToString())
                {
                    host = fcs;
                }
            }

            if (host.host.EndsWith("/"))
            {
                DataServer.instance.ChoosedServerAddress = host.host;
            }
            else
            {
                DataServer.instance.ChoosedServerAddress = host.host + "/";
            }

            servername = serverList.SelectedItem.ToString();
            ServerRequestManager instance = ServerRequestManager.instance;
            ServerRequestManager.instance.Login(GameData.instance.LoginResponse.userId);
        }


        internal void refreshUIData()
        {
            GameData gd = GameData.instance;

            this.namelvlabel.Text = "Lv." + gd.UserInfo.level + " " + tools.helper.gettypeofuser(int.Parse(gd.UserInfo.detailInfo.collection.Replace("%",""))) + "  " + gd.UserInfo.username;

            this.collect_info.Text = "收集: " + gd.UnlockedCards.Count + " 图鉴，收集率:" + gd.UserInfo.detailInfo.collection;
            this.chuzhenglabel.Text = "出征: " + gd.UserInfo.detailInfo.pveNum + "  胜:" + gd.UserInfo.detailInfo.pveWin;
            this.chuzhenglabel2.Text =  "      败:" + gd.UserInfo.detailInfo.pveLost + " 胜率:" + (gd.UserInfo.detailInfo.pveNum == 0?0:(gd.UserInfo.detailInfo.pveWin * 100 / gd.UserInfo.detailInfo.pveNum)) + "%";

            this.yanxilabel.Text = "演习: " + gd.UserInfo.detailInfo.pvpNum + "  胜:" + gd.UserInfo.detailInfo.pvpWin;
            this.yanxilabel2.Text = "       败:" + gd.UserInfo.detailInfo.pvpLost + " 胜率:" +
    (gd.UserInfo.detailInfo.pvpNum==0?0:(gd.UserInfo.detailInfo.pvpWin * 100 / gd.UserInfo.detailInfo.pvpNum)) + "%";

            this.yuanzhenglabel.Text = "远征: " + gd.UserInfo.detailInfo.exploreNum + "  大成功:" + gd.UserInfo.detailInfo.exploreBigSuccessNum;
            this.yuanzhenglabel2.Text = "        大成功率:" + 
                (gd.UserInfo.detailInfo.exploreNum==0? 0:(gd.UserInfo.detailInfo.exploreBigSuccessNum * 100 / gd.UserInfo.detailInfo.exploreNum)) + "%";

            this.explabel.Text = "经验 " + gd.UserInfo.exp + " / " + gd.UserInfo.nextExp;
            this.shipnum.Text = "少女 " + gd.UserShips.Count + " / " + gd.UserInfo.shipNumTop;
            this.equipmentnum.Text = "装备 " + gd.UserEquipments.Count + " / " + gd.UserInfo.equipmentNumTop;

            this.res1label.Text = "油 " + gd.UserInfo.oil + " / " + gd.UserInfo.resourcesTops[0];
            this.res2label.Text = "弹 " + gd.UserInfo.ammo + " / " + gd.UserInfo.resourcesTops[1];
            this.res3label.Text = "钢 " + gd.UserInfo.steel + " / " + gd.UserInfo.resourcesTops[2];
            this.res4label.Text = "铝 " + gd.UserInfo.aluminium + " / " + gd.UserInfo.resourcesTops[3];

            this.buildres.Text = "建造材料 " + gd.GetItemAmount(ResourceTypes.BuildShipItem) + " 快速建造 " + gd.GetItemAmount(ResourceTypes.FastBuild);
            this.devres.Text = "开发材料 " + gd.GetItemAmount(ResourceTypes.BuildEquipItem) + " 快速开发 " + gd.GetItemAmount(ResourceTypes.FastBuild);

            this.fastrepairnum.Text = "快速修理 " + gd.GetItemAmount(ResourceTypes.FastRepair);

            updateFleetInfo(gd.UserFleets);
       
            updateRepairInfo();
            updateBuildInfo();

            update_PVEexplore();
            update_Battle();
            update_repair();
            update_build();
        }
        public void OnInitDataSuccess()
        {
            login_successed = true;
            timer1.Enabled = true;
            choose_server.Enabled = false;
            this.expandablePanel1.Expanded = false;

            string regtime_s = TimeFormatter.GetDate(expire_time / 1000);

            this.Text = this.Version + " - " + GameData.instance.UserInfo.username + " - " + serverList.SelectedItem.ToString()
                + " -- 过期时间" + regtime_s; 
            refreshUIData();
        }

        private void updateFleetInfo(UserFleet[] fleets)
        {
            if(canUpdateGridUI == false)
            {
                return;
            }
            GridPanel panel = shiplist.PrimaryGrid;

            int i = 0;
            foreach(UserFleet f in fleets)
            {
                object[] vals = new object[12];

                panel.GetCell(i,0).Value = i;
                
                for( int j =1; j<7;j++)
                {
                    if (f.ships.Length >= j)
                    {
                        UserShip us = GameData.instance.GetShipById(f.ships[j - 1]);
                        if (us != null)
                        {
                            panel.GetCell(i, j).Value = tools.helper.getShipSmallImage(us);//"res/png/head_n/" + us.ship.picId + ".png";
                        }
                        else
                        {
                            panel.GetCell(i, j).Value = "";
                        }
                    }else{
                        panel.GetCell(i, j).Value = "";
                    }
                }

                panel.GetCell(i, 7).Value = "无";
                panel.GetCell(i, 8).Value = "远征";
                i++;
            }
            
        }

        private void updateRepairInfo()
        {
            GridPanel panel = repairlist.PrimaryGrid;
            panel.Rows.Clear();
            int i = 0;
            foreach (var repairs in GameData.instance.UserRepairDock)
            {
                if (repairs.locked == 1)
                {
                    continue;
                }

                
                object[] vals = new object[4];

                vals[0] = repairs.id;


                        UserShip us = GameData.instance.GetShipById(repairs.shipId);
                        if (us != null)
                        {
                            vals[1] = tools.helper.getShipSmallImage(us);//"res/png/head_n/" + us.ship.picId + ".png";
                        }
                        else
                        {
                            vals[1] = "";
                        }

                vals[2] = "修理:"+ repairs.TimeLeft;
                vals[3] = "加速";

                GridRow gr = new GridRow(vals);
                gr.RowHeight = 32;

                panel.Rows.Add(gr);
                i++;
            }
        }

        private void updateBuildInfo()
        {
            GridPanel panel = buildlist.PrimaryGrid;
            panel.Rows.Clear();
            int i = 0;
            foreach (var sbs in GameData.instance.UserDocks)
            {
                if (sbs.locked == 1)
                {
                    continue;
                }


                object[] vals = new object[5];

                vals[0] = sbs.id;


                ShipConfig us = AllShipConfigs.instance.getShip(sbs.shipType);
                if (us != null)
                {
                    UserShip uss = new UserShip();
                    uss.shipCid = us.cid;
                    uss.battleProps = new ShipBattleProps();
                    uss.battlePropsMax = new ShipBattleProps();
                    uss.battleProps.hp = us.hp;
                    uss.battlePropsMax.hp = us.hpMax;
                    uss.level = 1;

                    vals[1] = tools.helper.getShipSmallImage(uss);//"res/png/head_n/" + us.ship.picId + ".png";
                }
                else
                {
                    vals[1] = "";
                }
                long eta = sbs.secondsLeft;
                if (eta == 0)
                {
                    if (sbs.shipType <= 0)
                    {
                        vals[2] = "";
                        vals[3] = "建造";
                    }
                    else
                    {
                        vals[2] = "建造: 完毕";
                        vals[3] = "完成";
                    }
                }
                else
                {
                    vals[2] = "建造:" + TimeFormatter.GetETAString(eta);
                    vals[3] = "加速";
                }
                vals[4] = "船体";

                GridRow gr = new GridRow(vals);
                gr.RowHeight = 20;

                panel.Rows.Add(gr);
                i++;
            }

            foreach (var sbs in GameData.instance.UserEquipDocks)
            {
                if (sbs.locked == 1)
                {
                    continue;
                }


                object[] vals = new object[5];

                vals[0] = sbs.id;


                var us = GameData.instance.GetEquipmentById(sbs.equipmentCid);
                if (us != null)
                {
                    vals[1] = tools.helper.getEquipmentImage(us.config);
                }
                else
                {
                    vals[1] = "";
                }

                long eta = sbs.secondsLeft;
                if (eta == 0)
                {
                    if (sbs.equipmentCid <= 0)
                    {
                        vals[2] = "";
                        vals[3] = "建造";
                    }
                    else
                    {
                        vals[2] = "建造: 完毕";
                        vals[3] = "完成";
                    }
                }
                else
                {
                    vals[2] = "建造:" + TimeFormatter.GetETAString(eta);
                    vals[3] = "加速";
                }
                vals[4] = "装备";

                GridRow gr = new GridRow(vals);
                gr.RowHeight = 20;

                panel.Rows.Add(gr);
                i++;
            }
        }


        private void update_PVEexplore()
        {
            foreach (UserPVEExplore explore in GameData.instance.GetUserPVEExplores())
            {
                if (explore.fleetId >=0)
                {
                    //GridContainer row = shiplist.PrimaryGrid.GetRowFromIndex(explore.fleetId -1);
                    var cell = shiplist.PrimaryGrid.GetCell(explore.fleetId - 1, 7);
                    var cell2 = shiplist.PrimaryGrid.GetCell(explore.fleetId - 1, 8);
                    long eta = ServerTimer.GetTimeLeftTo(explore.startTime, explore.endTime);

                    if (cell != null)
                    {
                        string val = "远征:" + TimeFormatter.GetETAString(eta);
                        PVEExploreLevelConfig ff = GameConfigs.instance.GetPVEExploreLevel(explore.exploreId);
                        if(ff != null)
                        {
                            val += "\r\n " + ff.title;
                        }
                        cell.Value = val;
                    }
                    if(cell2 != null)
                    {
                        if (eta == 0)
                        {
                            cell2.Value = "完成";
                        }
                        else
                        {
                            cell2.Value = "取消";
                        }
                        
                    }
                    
                }
            }
        }

        private void update_Battle()
        {
            var cell = shiplist.PrimaryGrid.GetCell(battle_fleetid - 1, 7);
            var cell2 = shiplist.PrimaryGrid.GetCell(battle_fleetid - 1, 10);
            this.battelstatus.Text = battel_status.ToString();

            int bfb = 0;
            int bfr = 0;
            int bfb_inteam = 0;


            
            foreach(var bbsid in all_backupships)
            {
                UserFleet uf = GameData.instance.UserFleets[this.battle_fleetid - 1];
                UserShip us = GameData.instance.GetShipById(bbsid);
                if (!us.IsBigBroken
                    && ((int)us.BrokenType) < battlechangetypecombo.SelectedIndex + 2
                    && !us.IsInRepair
                    && !us.IsInExplore)
                {
                    if(uf.ships.Contains(us.id))
                    {
                        bfb_inteam++;
                    }
                    else
                    {
                        bfb++;
                    }
                    
                }else{
                    bfr++;
                }
            }

            this.waitforrepairelabel.Text = "战斗后备队修复中:" + bfr;
            this.battlewaitlistlabel.Text = "战斗后备队可用:" + bfb + "  使用中:" + bfb_inteam;;
            
            if (this.isInBattle())
            {
                if (battle_fleetid >= 0)
                {
                    PVELevel level = PVEConfigs.instance.GetLevel(battle_levelid);
                    UserFleet uf = GameData.instance.GetFleetOfId(battle_fleetid);

                    this.battle_position.Text = "关卡:" + level.title + " - " + level.subTitle;

                    if (cell != null)
                    {
                        string val = (darkforge.Checked == true?"炼钢中":"战斗中:") + uf.title + "\r\n =>"+level.subTitle ;
                        cell.Value = val;
                    }
                    if (cell2 != null)
                    {
                        cell2.Value = "取消";
                    }
                    return;

                }
            }
            if (cell2 != null)
            {
                cell2.Value = "战斗";
            }
        }

        private void update_repair()
        {
            foreach (var repairs in GameData.instance.UserRepairDock)
            {
                if (repairs.locked != 1)
                {
                    int dockid = repairs.id;
                    //GridContainer row = shiplist.PrimaryGrid.GetRowFromIndex(explore.fleetId -1);
                    var cell = repairlist.PrimaryGrid.GetCell(dockid - 1, 2);
                    var cell2 = repairlist.PrimaryGrid.GetCell(dockid - 1, 3);
                    long eta = ServerTimer.GetTimeLeftTo(repairs.startTime,repairs.endTime);
                    if(cell != null && cell2 != null)
                    {
                        if (eta == 0)
                        {
                            if (repairs.shipId <= 0)
                            {
                                cell.Value = "";
                                cell2.Value = "修理";
                            }
                            else
                            {
                                cell.Value = "修理: 等待完成";
                                cell2.Value = "完成";
                            }

                        }
                        else
                        {
                            cell.Value = "修理:" + TimeFormatter.GetETAString(eta);
                            cell2.Value = "加速";
                        }
                    }

                }
            }
        }

        private void update_build()
        {
            int delta =0;
            foreach (var bi in GameData.instance.UserDocks)
            {
                if (bi.locked ==0 )
                {
                    delta ++;
                    //GridContainer row = shiplist.PrimaryGrid.GetRowFromIndex(explore.fleetId -1);
                    var cell = buildlist.PrimaryGrid.GetCell(bi.id -1, 2);
                    var cell2 = buildlist.PrimaryGrid.GetCell(bi.id - 1, 3);
                    var cell3 = buildlist.PrimaryGrid.GetCell(bi.id - 1, 4);
                    long eta = ServerTimer.GetTimeLeftTo(bi.startTime, bi.endTime);

                    if(cell == null || cell2 == null || cell3==null )
                    {
                        return;
                    }

                    if (eta == 0)
                    {
                        if (bi.shipType <= 0)
                        {
                            cell.Value= "";
                            cell2.Value = "建造";
                        }
                        else
                        {
                            cell.Value = "建造: 完毕";
                            cell2.Value = "完成";
                        }
                    }
                    else
                    {
                        cell.Value = "建造:" + TimeFormatter.GetETAString(eta);
                        cell2.Value = "加速";
                    }
                    cell3.Value = "船体";
                }
            }


            foreach (var bi in GameData.instance.UserEquipDocks)
            {
                if (bi.locked == 0)
                {
                    var cell = buildlist.PrimaryGrid.GetCell(bi.id - 1 + delta, 2);
                    var cell2 = buildlist.PrimaryGrid.GetCell(bi.id - 1 + delta, 3);
                    var cell3 = buildlist.PrimaryGrid.GetCell(bi.id - 1 + delta, 4);
                    long eta = ServerTimer.GetTimeLeftTo(bi.startTime, bi.endTime);

                    if (cell == null || cell2 == null || cell3 == null)
                    {
                        return;
                    }

                    if (eta == 0)
                    {
                        if (bi.equipmentCid <= 0)
                        {
                            cell.Value = "";
                            cell2.Value = "建造";
                        }
                        else
                        {
                            cell.Value = "建造: 完毕";
                            cell2.Value = "完成";
                        }
                    }
                    else
                    {
                        cell.Value = "建造:" + TimeFormatter.GetETAString(eta);
                        cell2.Value = "加速";
                    }
                    cell3.Value = "装备";
                }
            }
        }

        internal class GroupCommandButton : GridButtonXEditControl
        {

            public GroupCommandButton()
            {
                Click += new EventHandler(this.GroupCommandButtonXClick);
            }

            #region CommandButtonClick

            private void GroupCommandButtonXClick(object sender, EventArgs e)
            {
                    int fleetid = EditorCell.RowIndex + 1;

                    if (z.instance.isFleetInBattle(fleetid))
                    {
	                    MessageBox.Show("舰队正在战斗中");
	                    return;
                    }

                    if (GameData.instance.IsFleetInExplore(fleetid))
                    {
                        MessageBox.Show("舰队正在远征中");
                        return;
                    }
                    Type targetform = Type.GetType("CommandForm");
                    if (targetform == null)
                    {
                        return;
                    }
                    var f= Activator.CreateInstance(targetform);
                    if (f == null)
                    {
                        return;
                    }

                    MethodInfo m = targetform.GetMethod("setfleet_id");
                    if (m != null)
                    {
                        m.Invoke(f, new object[] { fleetid });
                    }
                    MethodInfo m2 = targetform.GetMethod("ShowDialog",Type.EmptyTypes);
                    if (m2 != null)
                    {
                        m2.Invoke(f, new object[] {  });
                    }

                    
                    //form.Text = "配置舰队任务";
                    //form.setfleet_id(fleetid);
                    //form.ShowDialog();
            }

            #endregion
        }

        internal class ExplorerCommandButton : GridButtonXEditControl
        {

            public ExplorerCommandButton()
            {
                Click += new EventHandler(CommandButtonClick);
            }

            #region CommandButtonClick

            void CommandButtonClick(object sender, EventArgs e)
            {
                int fleetid = EditorCell.RowIndex + 1;
                int exploerid = 0;


                if (z.instance.isFleetInBattle(fleetid))
                {
                    MessageBox.Show("舰队正在战斗中");
                    return;
                }

                //判定舰队状态
                long eta = 0;
                bool hasexplorer = false;
                foreach (UserPVEExplore explore in GameData.instance.GetUserPVEExplores())
                {
                    if (explore.fleetId == fleetid)
                    {
                        eta = ServerTimer.GetTimeLeftTo(explore.startTime, explore.endTime);
                        hasexplorer = true;
                        exploerid = explore.exploreId;
                    }
                }
                if(hasexplorer)
                {
                    if(eta > 0 )
                    {
                        DialogResult result = MessageBox.Show("确定要取消远征么？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            ServerRequestManager.instance.CancelPVEExplore(exploerid);
                            tools.configmng.instance.setval(z.instance.get_username() + "_explore_" + fleetid,  "no");
                        }
                    }
                    else
                    {
                        ServerRequestManager.instance.FinishPVEExplore(exploerid);
                    }
                }
                else
                {
                    ExploreForm form = new ExploreForm(fleetid);

                    form.setfleet_id(fleetid);
                    form.ShowDialog();
                }

            }

            #endregion
        }

        internal class BattleCommandButton : GridButtonXEditControl
        {

            public BattleCommandButton()
            {
                Click += new EventHandler(CommandButtonClick);
            }

            #region CommandButtonClick

            void CommandButtonClick(object sender, EventArgs e)
            {
                int fleetid = EditorCell.RowIndex + 1;


                if(z.instance.isInBattle())
                {
                    DialogResult result = MessageBox.Show("确定要取消战斗么？ 取消后将会完成本节点后停止", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        tools.configmng.instance.setval(z.instance.get_username() + "_autofight_" + fleetid, "no");
                        z.instance.stopbattle();
                        return;
                    }
                }

                Type targetform = Type.GetType("BattleForm");
                if (targetform == null)
                {
                    return;
                }

                dynamic f = Activator.CreateInstance(targetform);
                if(f == null)
                {
                    return;
                }

                ServerRequestManager.instance.GetPVEConfigs();
                ServerRequestManager.instance.GetPVEData();
                ServerRequestManager.instance.GetPVEEventConfigs();
                ServerRequestManager.instance.GetPVEEventData();


                MethodInfo m = targetform.GetMethod("setFleetId");
                if (m != null)
                {
                    m.Invoke(f, new object[] { fleetid });
                }
                else
                {
                    f.setFleetId(fleetid);
                }
                m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
                if (m != null)
                {
                    m.Invoke(f, new object[] { });
                }

                //form.setfleetid(fleetid);

                //form.ShowDialog();
            }

            #endregion
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if ((sender as CheckBox).Checked == true)
            {
	           switch_to_android();
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked == true)
            {
                switch_to_ios();
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked == true)
            {
                switch_to_ios_APP();
            }
        }

        private void switch_to_android()
        {
            checkBox1.Checked = true;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            AndroidPlatformManager.instance.usingPlatform = PlatformType.Self;
            AndroidPlatformManager.instance.usingChannel = "1";
            DataServer.secretKey = "Mb7x98rShwWRoCXQRHQb";
            GameInfo.instance.UV = "4.6.3p3";
            initphonecombo();
        }

        private void switch_to_ios()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = true;
            checkBox3.Checked = false;
            AndroidPlatformManager.instance.usingPlatform = PlatformType.Ios;
            AndroidPlatformManager.instance.usingChannel = "1";
            DataServer.secretKey = "Mb7x98rShwWRoCXQRHQb";
            GameInfo.instance.UV = "4.6.3p3";

            initphonecomboios();
        }
        private void switch_to_ios_APP()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = true;
            AndroidPlatformManager.instance.usingPlatform = PlatformType.Ios;
            AndroidPlatformManager.instance.usingChannel = "2";
            DataServer.secretKey = "Mb7x98rShwWRoCXQRHQb";
            GameInfo.instance.UV = "4.6.3p3";
            initphonecomboios();
        }

        private void initphonecomboios()
        {
            phonetypecombo.Items.Clear();

            phonetypecombo.Items.Add("shipwar/" + GameInfo.instance.version + " CFNetwork/672.1.14 Darwin/14.0.0");
            phonetypecombo.Items.Add("shipwar/" + GameInfo.instance.version + " CFNetwork/672.1.15 Darwin/14.0.0");
            phonetypecombo.Items.Add("shipwar/" + GameInfo.instance.version + " CFNetwork/711.0.6 Darwin/14.0.0");
            phonetypecombo.Items.Add("shipwar/" + GameInfo.instance.version + " CFNetwork/711.1.12 Darwin/14.0.0");
            phonetypecombo.Items.Add("shipwar/" + GameInfo.instance.version + " CFNetwork/711.1.16 Darwin/14.0.0");
            phonetypecombo.Items.Add("shipwar/" + GameInfo.instance.version + " CFNetwork/711.2.23 Darwin/14.0.0");
            phonetypecombo.Items.Add("shipwar/" + GameInfo.instance.version + " CFNetwork/609.1.4 Darwin/13.0.0");
            phonetypecombo.Items.Add("shipwar/" + GameInfo.instance.version + " CFNetwork/672.0.2 Darwin/14.0.0");
            phonetypecombo.Items.Add("shipwar/" + GameInfo.instance.version + " CFNetwork/672.0.8 Darwin/14.0.0");
            phonetypecombo.Items.Add("shipwar/" + GameInfo.instance.version + " CFNetwork/672.1.13 Darwin/14.0.0");

            phonetypecombo.SelectedIndex = 0;

        }



        internal void loginfailed(string err)
        {
            log("[登入失败]... " +err );
            loginbtn.Enabled = true;
        }

        private void nightwarcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (initing)
            {
                return;
            }
            CheckBox cb = sender as CheckBox;
            battle_do_nightwar = cb.Checked ? 1:0;
            tools.configmng.instance.setval(get_username() + "_do_nightwar", battle_do_nightwar);
        }

        private void formationcombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if(initing)
            {
                return;
            }
            battle_formation = (FleetFormation)formationcombo.SelectedIndex +1;
            tools.configmng.instance.setval(get_username() + "_formation", (int)battle_formation);
        }

        private void autorepaircheck_CheckedChanged(object sender, EventArgs e)
        {
            if (initing)
            {
                return;
            }
            bool check = (sender as CheckBox).Checked;
            if(check== true && darkforge.Checked == true)
            {
                darkforge.Checked = false;
                log("[黑暗炼钢] 修理不炼钢");
            }
            string ck = check? "yes" : "no";
            tools.configmng.instance.setval(get_username() + "_auto_repair", ck);

        }

        private void autobattlecheck_CheckedChanged(object sender, EventArgs e)
        {
            if (initing)
            {
                return;
            }
            autostart_battle = (sender as CheckBox).Checked;
        }





        internal class RepairCommandButton : GridButtonXEditControl
        {

            public RepairCommandButton()
            {
                Click += new EventHandler(CommandButtonClick);
            }

            #region CommandButtonClick

            void CommandButtonClick(object sender, EventArgs e)
            {
                int dockid = (int)EditorCell.GridRow.Cells[0].Value-1;

                var dock = GameData.instance.UserRepairDock[dockid];
                if(dock == null || dock.locked == 1)
                {
                    return;
                }
                if(dock.shipId == 0)
                {
                    Type targetform = Type.GetType("RepairForm1");
                    if (targetform == null)
                    {
                        return;
                    }
                    var f = Activator.CreateInstance(targetform, new object[] { dock.id });
                    if (f == null)
                    {
                        return;
                    }

                    MethodInfo m = targetform.GetMethod("setdock_id");
                    if (m != null)
                    {
                        m.Invoke(f, new object[] { dock.id });
                    }
                    m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
                    if (m != null)
                    {
                        m.Invoke(f, new object[] { });
                    }
                    return;
                }
                int shipid = dock.shipId;
                DialogResult result = MessageBox.Show("确定要使用快速修理么？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    ServerRequestManager.instance.InstantRepair(dock.id,1);
                    log("[快速修理]....");
                }
            }

            #endregion
        }
        
        internal class BuildCommandButton : GridButtonXEditControl
        {

            public BuildCommandButton()
            {
                Click += new EventHandler(CommandButtonClick);
            }

            #region CommandButtonClick

            void CommandButtonClick(object sender, EventArgs e)
            {
                string docktype = (string)EditorCell.GridRow.Cells[4].Value;
                int dockid = (int)EditorCell.GridRow.Cells[0].Value;

                if (docktype.Equals("装备"))
                {
                    var dock = GameData.instance.UserEquipDocks[dockid-1];
                    if (dock == null || dock.locked == 1 )
                    {
                        return;
                    }
                    if( dock.equipmentCid ==0 && dock.secondsLeft ==0 )
                    {
                        //BuildForm b = new BuildForm(false, dock.id);
                        //b.initdata(dock.id);
                        //b.ShowDialog();

                        Type targetform = Type.GetType("BuildForm");
                        if (targetform == null)
                        {
                            return;
                        }
                        var f = Activator.CreateInstance(targetform, new object[] { false, dock.id });
                        if (f == null)
                        {
                            return;
                        }

                        MethodInfo m = targetform.GetMethod("initdata");
                        if (m != null)
                        {
                            m.Invoke(f, new object[] { dock.id });
                        }
                        m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
                        if (m != null)
                        {
                            m.Invoke(f, new object[] { });
                        }

                        return;
                    }
                    if (dock.secondsLeft >0)
                    {
                        DialogResult result = MessageBox.Show("确定要使用快速建造么？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            ServerRequestManager.instance.FastBuildEquipAtDock(dock.id);
                            log("[快速开发装备]....");
                        }
                    } 
                    else
                    {
                        var r = ServerRequestManager.instance.GetEquipFromDock(dock.id);
                        if(r != null &&r.responseData != null && r.responseData.eid ==0)
                        {
                            GetEquipData bed = r.responseData as GetEquipData;

                            if (bed != null && bed.equipmentVo != null)
                            {
	                            var v = bed.equipmentVo;
	                            {
                                    var es = tools.helper.getEquipmentDesc(v.equipmentCid);
                                    MessageBox.Show(es);
                                    log("[获得装备]\r\n"+es);
	                            }
                            }
                        }
                        
                    }

                } 
                else
                {
                    var dock = GameData.instance.UserDocks[dockid-1];
                    if (dock == null || dock.locked == 1 )
                    {
                        return;
                    }
                    if (dock.shipType == 0 && dock.secondsLeft == 0)
                    {
                        //BuildForm b = new BuildForm(true,dock.id);
                        //b.initdata(dock.id);
                        //b.ShowDialog();

                        Type targetform = Type.GetType("BuildForm");
                        if (targetform == null)
                        {
                            return;
                        }
                        if (targetform == null)
                        {
                            return;
                        }
                        var f = Activator.CreateInstance(targetform, new object[] { true, dock.id });


                        MethodInfo m = targetform.GetMethod("initdata");
                        if (m != null)
                        {
                            m.Invoke(f, new object[] { dock.id });
                        }
                        m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
                        if (m != null)
                        {
                            m.Invoke(f, new object[] { });
                        }

                        return;

                        return;
                    }
                    if (dock.secondsLeft > 0)
                    {
                        DialogResult result = MessageBox.Show("确定要使用快速建造么？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            ServerRequestManager.instance.FastBuildShipAtDock(dock.id);
                            log("[快速建造]....");
                        }
                    }
                    else
                    {
                        var r = ServerRequestManager.instance.GetShipFromDock(dock.id);
                        if (r != null && r.responseData != null && r.responseData.eid == 0)
                        {
                            GetShipData bed = r.responseData as GetShipData;

                            if (bed != null && bed.dockVo != null)
                            {
                                var v = bed.shipVO;
                                {
                                    var es = tools.helper.getShipDesc(v.ship.cid);
                                    MessageBox.Show(es);
                                    log("[获得船只]\r\n" + es);
                                }
                            }
                        }

                    }
                }

            }

            #endregion
        }

        private void buildbtn_Click(object sender, EventArgs e)
        {

        }

        private void disshipbtn_Click(object sender, EventArgs e)
        {
            this.dis_ship = true;
            switchdisui(false);
        }

        private void disequipbtn_Click(object sender, EventArgs e)
        {
            this.dis_equip = true;
            switchdisui(false);
        }

        private void switchdisui(bool p)
        {
            initing = true;
            disshipbtn.Enabled = p;
            disshipbtn.Text = p?"拆解少女":"拆解中";

            disequipbtn.Enabled = p;
            disequipbtn.Text = p?"拆解装备":"拆解中";


            initing = false;
        }

        private void questbtn_Click(object sender, EventArgs e)
        {
            //QuestForm qf = new QuestForm();
            //qf.ShowDialog();

            Type targetform = Type.GetType("QuestForm");
            if (targetform == null)
            {
                return;
            }
            var f = Activator.CreateInstance(targetform);
            if (f == null)
            {
                return;
            }

            MethodInfo m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
            if (m != null)
            {
                m.Invoke(f, new object[] { });
            }

        }



        private bool ca()
        {
            return true;
            string c = System.IO.File.ReadAllText(this.GetType().ToString(),Encoding.UTF8);

            string mac = "";

            System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_NetworkAdapterConfiguration");

            System.Management.ManagementObjectCollection moc = mc.GetInstances();

            foreach (System.Management.ManagementObject mo in moc)

                if ((bool)mo["IPEnabled"] == true)
                {

                    mac += mo["MacAddress"].ToString() + " ";

                    break;

                }

            moc = null;

            mc = null;

            mac =mac.Trim();

            return true;
        }

        private void darkforge_CheckedChanged(object sender, EventArgs e)
        {
            initing = true;
            bool check = (sender as CheckBox).Checked;
            if (check == true && this.autorepaircheck.Checked == true)
            {
                autorepaircheck.Checked = false;
                log("[黑暗炼钢] 炼钢不修理");
            }

            if (check == true )
            {
                this.autobattlecheck.Checked = true;
                this.autostart_battle = true;
                log("[黑暗炼钢] 炼钢要自动开始战斗");
            }
            else
            {
                this.autobattlecheck.Checked = false;
                this.autostart_battle = false;
                log("[黑暗炼钢] 停止炼钢要自动停止战斗");
            }
            initing =false;
        }

        private void expandablePanel1_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            int htotal = 35;
            int i = 1;
            int[] ss = new int[] { 0,218, 200, 310, 220 };
            foreach (var p in new List<ExpandablePanel>() { expandablePanel1, expandablePanel2, expandablePanel3, expandablePanel4 })
            {
                int ph = p.Expanded? ss[i]: 20;
                htotal += ph;
                //log("p" + i + " h:" + ph);
                i++;
            }
            this.Height = htotal;
            //log("total:" + this.Height);
        }

        private void expandablePanel1_ExpandedChanging(object sender, ExpandedChangeEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Type targetform = Type.GetType("PVPForm");
            if (targetform == null)
            {
                return;
            }
            var f = Activator.CreateInstance(targetform);
            if (f == null)
            {
                return;
            }

            MethodInfo m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
            if (m != null)
            {
                m.Invoke(f, new object[] { });
            }
        }

        private void shiplist_CellClick(object sender, GridCellClickEventArgs e)
        {
            dynamic obj = sender;
            if(e.GridCell.ColumnIndex<1 || e.GridCell.ColumnIndex >6)
            {
                return;
            }
            int fleetid = e.GridCell.RowIndex + 1;
            UserFleet uf = GameData.instance.GetFleetOfId(fleetid);
            if(uf==null|| uf.ships== null )
            {
                return;
            }
            int clickshippos = e.GridCell.ColumnIndex - 1;
            int shipid = 0;
            if(clickshippos < uf.ships.Length )
            {
                shipid = uf.ships[clickshippos];
            }

            if (z.instance.isFleetInBattle(fleetid))
            {
                MessageBox.Show("舰队正在战斗中");
                return;
            }

            if (GameData.instance.IsFleetInExplore(fleetid))
            {
                MessageBox.Show("舰队正在远征中");
                return;
            }



            var shipdialog = new ShipForm();

            shipdialog.setfleet_id(fleetid, clickshippos, shipid);
            shipdialog.ShowDialog();
            shipdialog.Dispose();
        }

        private void battlechangetypecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initing == true)
                return;

            initing = true;
            int sel = battlechangetypecombo.SelectedIndex;
            if (sel == 0 && repairytpecombo.SelectedIndex > 1)
            {
                log("[自动战斗]中破就换 必须有 中破就修支持，自动更改");
                repairytpecombo.SelectedIndex = 1;
            }

            initing = false;
        }

        private void repairytpecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initing == true)
                return;

            initing = true;
            int sel = repairytpecombo.SelectedIndex;
            if (sel == 2 && battlechangetypecombo.SelectedIndex == 0)
            {
                log("[自动战斗]大破才修 必须有 自动战斗 大破才换船支持 ，自动更改");
                MessageBox.Show("[自动战斗]大破才修 必须有 自动战斗 大破才换船支持 ，自动更改");
                battlechangetypecombo.SelectedIndex = 1;
            }

            initing = false;
        }


        internal void stopbattle()
        {
            initing = true;
            autostart_battle = false;
            autobattlecheck.Checked = false;
            initing = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Type targetform = Type.GetType("CampForm");
            if (targetform == null)
            {
                return;
            }
            var f = Activator.CreateInstance(targetform);
            if (f == null)
            {
                return;
            }

            MethodInfo m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
            if (m != null)
            {
                m.Invoke(f, new object[] { });
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Type targetform = Type.GetType("Coupon");
            if (targetform == null)
            {
                return;
            }
            var f = Activator.CreateInstance(targetform);
            if (f == null)
            {
                return;
            }

            MethodInfo m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
            if (m != null)
            {
                m.Invoke(f, new object[] { });
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Type targetform = Type.GetType("Enchant");
            if (targetform == null)
            {
                return;
            }
            var f = Activator.CreateInstance(targetform);
            if (f == null)
            {
                return;
            }

            MethodInfo m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
            if (m != null)
            {
                m.Invoke(f, new object[] { });
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ret = ServerRequestManager.instance.GetContinueLoginAward();
            if(ret != null && ret.responseData!= null && ret.responseData.eid ==0)
            {
                MessageBox.Show("领取成功");
            }
            else
            {
                MessageBox.Show("领取失败，去客户端领吧");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Type targetform = Type.GetType("DisShipConf");
            if (targetform == null)
            {
                return;
            }
            var f = Activator.CreateInstance(targetform);
            if (f == null)
            {
                return;
            }

            MethodInfo m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
            if (m != null)
            {
                m.Invoke(f, new object[] { });
            }
        }

        private void doautodis_check_CheckedChanged(object sender, EventArgs e)
        {
            if (initing == true)
                return;

            initing = true;

            

            initing = false;
        }

        private void choosedisequiplist_Click(object sender, EventArgs e)
        {
            Type targetform = Type.GetType("DisEquipConf");
            if (targetform == null)
            {
                return;
            }
            var f = Activator.CreateInstance(targetform);
            if (f == null)
            {
                return;
            }

            MethodInfo m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
            if (m != null)
            {
                m.Invoke(f, new object[] { });
            }
        }

        internal void do_relogin()
        {
            login_successed = false;
            can_relogin = true;
            relogin_counter = 3600;
        }

        private void do_relogin_logic()
        {
            can_relogin = false;
            ServerRequestManager.instance.hasSendInitDataRequest = false;
            ServerRequestManager.instance.loadConfigError = false;
            loginbtn_Click(null, null);
            choose_server_Click(null, null);
        }

        private void autoexplorecheck_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void dropanalyze_Click(object sender, EventArgs e)
        {
            Type targetform = Type.GetType("Analyze");
            if (targetform == null)
            {
                return;
            }
            var f = Activator.CreateInstance(targetform);
            if (f == null)
            {
                return;
            }

            MethodInfo m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
            if (m != null)
            {
                m.Invoke(f, new object[] { });
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string fname = "log_" + DateTime.Now.ToString("yyyy-MM-dd") + "_.txt";
            System.Diagnostics.Process.Start("NOTEPAD", System.Environment.CurrentDirectory + "\\\\" + fname); 
        }

        private bool tryUpgradeshipskill()
        {
            foreach(UserShip us in GameData.instance.UserShips)
            {
                if (us.ship.evoClass > 0
                    && us.strengthenAttribute.atk >= us.ship.strengthenTop.atk
                    && us.strengthenAttribute.torpedo >= us.ship.strengthenTop.torpedo
                    && us.strengthenAttribute.def >= us.ship.strengthenTop.def
                    && us.strengthenAttribute.air_def >= us.ship.strengthenTop.air_def
                    && us.skillLevel <3 
                    )
                {
                    log("[自动强化-升级技能] " + tools.helper.getshiptype(us.ship.type) + " " + us.ship.title + " Lv." + us.level + " 技能 lv." + us.skillLevel + " => lv." + (us.skillLevel + 1) + "\r\n");
                    ServerRequestManager.instance.SkillShip(us);
                    return true;
                }
            }
            return false;
        }

        private bool tryFeedFleet(List<UserShip> dislist, List<UserShip> eaterlist)
        {
            Dictionary<int, List<UserShip>> toeat = new Dictionary<int, List<UserShip>>();
            var atklist = eaterlist.Where(o => o.expNeedAtk > 0).OrderByDescending(o => o.expNeedAtk).ToList();
            var torpedolist = eaterlist.Where(o => o.expNeedTorpedo > 0).OrderByDescending(o => o.expNeedTorpedo).ToList();
            var deflist = eaterlist.Where(o => o.expNeedDef > 0).OrderByDescending(o => o.expNeedDef).ToList();
            var airdeflist = eaterlist.Where(o => o.expNeedAirdef > 0).OrderByDescending(o => o.expNeedAirdef).ToList();

            //优先级顺序，火力，防空，装甲，雷装

            foreach(var us in  dislist)
            {
                if(    us.ship.strengthenSupplyExp.atk >= us.ship.strengthenSupplyExp.torpedo
                    && us.ship.strengthenSupplyExp.atk >= us.ship.strengthenSupplyExp.def
                    && us.ship.strengthenSupplyExp.atk >= us.ship.strengthenSupplyExp.air_def
                    )
                {
                    bool eated = false;
                    foreach(var tus in atklist)
                    {
                        if (toeat.ContainsKey(tus.id) == false) { toeat[tus.id] = new List<UserShip>(); }
                        int sn = toeat[tus.id].Sum(o => o.ship.strengthenSupplyExp.atk);
                        if(sn < tus.expNeedAtk )
                        {
                            toeat[tus.id].Add(us);
                            eated = true;
                            break;
                        }
                    }
                    if (eated) { continue; }
                }
                if (us.ship.strengthenSupplyExp.air_def >= us.ship.strengthenSupplyExp.torpedo
                        && us.ship.strengthenSupplyExp.air_def >= us.ship.strengthenSupplyExp.def
                        && us.ship.strengthenSupplyExp.air_def >= us.ship.strengthenSupplyExp.atk
                        )
                {
                    bool eated = false;
                    foreach (var tus in airdeflist)
                    {
                        if (toeat.ContainsKey(tus.id) == false) { toeat[tus.id] = new List<UserShip>(); }
                        int sn = toeat[tus.id].Sum(o => o.ship.strengthenSupplyExp.air_def);
                        if (sn < tus.expNeedAirdef )
                        {
                            toeat[tus.id].Add(us);
                            eated = true;
                            break;
                        }
                    }
                    if (eated) { continue; }
                }
                if (us.ship.strengthenSupplyExp.def >= us.ship.strengthenSupplyExp.torpedo
                    && us.ship.strengthenSupplyExp.def >= us.ship.strengthenSupplyExp.atk
                    && us.ship.strengthenSupplyExp.def >= us.ship.strengthenSupplyExp.air_def
                    )
                {
                    bool eated = false;
                    foreach (var tus in deflist)
                    {
                        if (toeat.ContainsKey(tus.id) == false) { toeat[tus.id] = new List<UserShip>(); }
                        int sn = toeat[tus.id].Sum(o => o.ship.strengthenSupplyExp.def);
                        if (sn < tus.expNeedDef)
                        {
                            toeat[tus.id].Add(us);
                            eated = true;
                            break;
                        }
                    }
                    if (eated) { continue; }
                }

                if (us.ship.strengthenSupplyExp.torpedo >= us.ship.strengthenSupplyExp.atk
                    && us.ship.strengthenSupplyExp.torpedo >= us.ship.strengthenSupplyExp.def
                    && us.ship.strengthenSupplyExp.torpedo >= us.ship.strengthenSupplyExp.air_def
                    )
                {
                    bool eated = false;
                    foreach (var tus in torpedolist)
                    {
                        if (toeat.ContainsKey(tus.id) == false) { toeat[tus.id] = new List<UserShip>(); }
                        int sn = toeat[tus.id].Sum(o => o.ship.strengthenSupplyExp.torpedo);
                        if (sn <= tus.expNeedTorpedo )
                        {
                            toeat[tus.id].Add(us);
                            eated = true;
                            break;
                        }
                    }
                    if (eated) { continue; }
                }

            }

            int i = 0;

            foreach (var eat in toeat)
            {
                if (eat.Value.Count <= 0) { continue; }
                int ii = 0;
                List<UserShip> realeat = new List<UserShip>();
                foreach (var u in eat.Value)
                {
                    ii++;
                    realeat.Add(u);
                    if(ii>= 10)
                    {
                        break;
                    }

                }
                var idlist = from o in realeat select o.id;
                UserShip eatus = GameData.instance.GetShipById(eat.Key);
                string r = "[自动强化]准备强化 " + tools.helper.getshiptype(eatus.ship.type) + " " + eatus.ship.title + " Lv." + eatus.level + " 狗粮:\r\n";
                foreach (var u in realeat)
                {
                    r += tools.helper.getshiptype(u.ship.type) + " " + u.ship.title + " Lv." + u.level + " " + tools.helper.getstartstring(u.ship.star) + "\r\n";
                }
                r += "火力 " + eatus.strengthenAttribute.atk + "/" + eatus.ship.strengthenTop.atk + " => " + (eatus.strengthenAttribute.atk + realeat.Sum(o => o.ship.strengthenSupplyExp.atk)) + "/" + eatus.ship.strengthenTop.atk + "\r\n";
                r += "雷装 " + eatus.strengthenAttribute.torpedo + "/" + eatus.ship.strengthenTop.torpedo + " => " + (eatus.strengthenAttribute.torpedo + realeat.Sum(o => o.ship.strengthenSupplyExp.torpedo)) + "/" + eatus.ship.strengthenTop.torpedo + "\r\n";
                r += "装甲 " + eatus.strengthenAttribute.def + "/" + eatus.ship.strengthenTop.def + " => " + (eatus.strengthenAttribute.def + realeat.Sum(o => o.ship.strengthenSupplyExp.def)) + "/" + eatus.ship.strengthenTop.def + "\r\n";
                r += "防空 " + eatus.strengthenAttribute.air_def + "/" + eatus.ship.strengthenTop.air_def + " => " + (eatus.strengthenAttribute.air_def + realeat.Sum(o => o.ship.strengthenSupplyExp.air_def)) + "/" + eatus.ship.strengthenTop.air_def + "\r\n";
                log(r);
                ServerRequestManager.instance.Strengthen(eat.Key,idlist.ToList() );
                System.Threading.Thread.Sleep(1000);
                i++;
                if(i >3){break;}
                    
            }

            return i >0? true:false;
        }
        private bool tryFeedFleet(List<UserShip> dislist)
        {
            List<UserShip> eaterlist= new List<UserShip>();
            foreach (var fl in GameData.instance.UserFleets)
            {
                if (fl != null && fl.ships != null)
                {
                    foreach (int sid in fl.ships)
                    {
                        UserShip us = GameData.instance.GetShipById(sid);
                        if (us == null ||us.ship.canEvo == 1)
                        {
                            continue;
                        }
                        if ( us.strengthenAttribute.atk < us.ship.strengthenTop.atk
                                || us.strengthenAttribute.torpedo < us.ship.strengthenTop.torpedo
                                || us.strengthenAttribute.def < us.ship.strengthenTop.def
                                || us.strengthenAttribute.air_def < us.ship.strengthenTop.air_def)

                        {
                            eaterlist.Add(us);
                        }

                    }
                }
            }

            if(tryFeedFleet(dislist, eaterlist))
            {
                return true;
            }

            var alleaterlist = new List<UserShip>();
                    foreach (var us in GameData.instance.UserShips)
                    {
                        if (us == null || us.level < 10 || us.ship.canEvo == 1)
                        {
                            continue;
                        }
                        if (us.strengthenAttribute.atk < us.ship.strengthenTop.atk
                                || us.strengthenAttribute.torpedo < us.ship.strengthenTop.torpedo
                                || us.strengthenAttribute.def < us.ship.strengthenTop.def
                                || us.strengthenAttribute.air_def < us.ship.strengthenTop.air_def)
                        {
                            alleaterlist.Add(us);
                        }

                    }
            return tryFeedFleet(dislist, alleaterlist.OrderByDescending(o=>o.level).ToList());
        }

        private bool judge_enemy_case(ShipInWar[] enemyShips, string param2)
        {
            if(param2 != "1" && param2 != "2" && param2 != "3")
            {
                return true;
            }

            int caseship = -1;
            int  casejudge = -1;
            int caseval = -1;

            if(param2 == "1")
            {
                caseship = secase1ship.SelectedIndex;
                casejudge = secase1judge.SelectedIndex;
                caseval = secase1val.Value;
            }else if(param2 == "2")
            {
                caseship = secase2ship.SelectedIndex;
                casejudge = secase2judge.SelectedIndex;
                caseval = secase2val.Value;
            }
            else if (param2 == "3")
            {
                caseship = secase3ship.SelectedIndex;
                casejudge = secase3judge.SelectedIndex;
                caseval = secase3val.Value;
            }

            if(caseship == -1 || casejudge == -1 || caseval == -1)
            {
                log("[自动战斗-索敌控制]条件填写不全....放弃判定");
                return true;
            }

            if(caseship == 0)
            {
                //全部船只，判定条件即可
                switch(casejudge)
                {
                        //大于
                    case 0:
                        if(enemyShips.Length > caseval)
                        {
                            log("[自动战斗-索敌控制] 敌舰 总数 "+ enemyShips.Length + "大于 设定条件 " + caseval+ " 主动归港");
                            return false;
                        }
                        break;
                    //等于
                    case 1:
                        if (enemyShips.Length == caseval)
                        {
                            log("[自动战斗-索敌控制] 敌舰 总数 "+ enemyShips.Length + "等于 设定条件 " + caseval+ " 主动归港");
                            return false;
                        }
                        break;
                    //大于
                    case 2:
                        if (enemyShips.Length < caseval)
                        {
                            log("[自动战斗-索敌控制] 敌舰 总数 "+ enemyShips.Length + "小于 设定条件 " + caseval+ " 主动归港");
                            return false;
                        }
                        break;
                }
                return true;
            }

            //构造船只类型字典
            Dictionary<int, int> shiptypecount = new Dictionary<int, int>();
            for (int i = 0; i < secase1ship.Items.Count;i++ )
            {
                shiptypecount[i] = 0;
            }
            foreach(var s in enemyShips)
            {
                int st = s.type;
                if(shiptypecount.ContainsKey(st) == true)
                {
                    shiptypecount[st] = shiptypecount[st] + 1;
                }
                else
                {
                    shiptypecount[st] = 1;
                }
            }
            switch (casejudge)
            {
                //大于
                case 0:
                    if (shiptypecount.ContainsKey(caseship) == true &&  shiptypecount[caseship] > caseval)
                    {
                        log("[自动战斗-索敌控制] 敌 " + tools.helper.getshiptype((ShipType)caseship)+ "数量 "+ shiptypecount[caseship] + "大于 设定条件 " + caseval+ " 主动归港");
                        return false;
                    }
                    break;
                //等于
                case 1:
                    if (shiptypecount.ContainsKey(caseship) == true && shiptypecount[caseship] == caseval)
                    {
                        log("[自动战斗-索敌控制] 敌 " + tools.helper.getshiptype((ShipType)caseship)+ "数量 "+ shiptypecount[caseship] + "等于 设定条件 " + caseval+ " 主动归港");
                        return false;
                    }
                    break;
                //大于
                case 2:
                    if (shiptypecount.ContainsKey(caseship) == true && shiptypecount[caseship] < caseval)
                    {
                        log("[自动战斗-索敌控制] 敌 " + tools.helper.getshiptype((ShipType)caseship)+ "数量 "+ shiptypecount[caseship] + "小于 设定条件 " + caseval+ " 主动归港");
                        return false;
                    }
                    break;
            }
            return true;

        }


        private void save_fleet_cfg()
        {
            try
            {
                System.IO.File.WriteAllText("fleetconfig.json",new JsonFx.Json.JsonWriter().Write(fleetcfgs));
                log("[舰队配置]保存成功");

            }
            catch (Exception e)
            {
                log("[舰队配置]保存失败..."+ e.Message);
            }
        }

        private string q()
        {
            var uk = Registry.CurrentUser.CreateSubKey("Software");
            if (uk == null)
            {
                return null;
            }
            var rk = uk.CreateSubKey("shipgirlrunner");

            if (rk == null)
            {
                return null;
            }

            string val = rk.GetValue("auth_guid", null) as string;
            if (val == null)
            {
                val = "";
            }
            return val;
        }

        private void update_bot_info()
        {
            try
            {

                string ss = System.IO.File.ReadAllText("fleetconfig.json");
                if (ss.Length > 10)
                {
                    dynamic rrr = new JsonFx.Json.JsonReader().Read(ss);

                    for (int i = 1; i < 9; i++)
                    {
                        fleetcfgs[i] = new Dictionary<int, int>();
                    }

                    foreach (dynamic rc in rrr)
                    {
                        Dictionary<int, int> nn = new Dictionary<int, int>();
                        foreach (dynamic rcc in rc.Value)
                        {
                            try
                            {
                                int k = int.Parse(rcc.Key);
                                int v = rcc.Value;
                                nn[k] = v;
                            }
                            catch (Exception) { }
                        }
                        try
                        {
                            int oid = int.Parse(rc.Key);
                            fleetcfgs[oid] = nn;
                        }
                        catch (Exception) { }

                    }

                }   
  

            }
            catch (Exception)
            {

            }
        }

        void llrm_ItemClick(object sender, EventArgs e)
        {
            var ob = (sender as RadialMenuItem);
            if(ob == null)
            {
                return;
            }
            int fid = (int)ob.Tag;

            string opcode = (sender as BaseItem).Text;
            if (opcode == null || opcode == "" || opcode.Length < 3)
            {
                return;
            }
            string opc = opcode.Substring(0, opcode.Length - 1);
            string opoder = opcode.Substring(opcode.Length - 1, 1);

            //MessageBox.Show(opc + " -- " + opoder);

            var fl = GameData.instance.GetFleetOfId(fid);

            int orderid = 0;
            try
            {
                orderid = int.Parse(opoder);
            }
            catch (Exception)
            {

            }
            if (orderid == 0)
            {
                MessageBox.Show("舰队配置存取出错");
            }

            if (fl != null && opc == "保存")
            {
                Dictionary<int, int> dic = new Dictionary<int, int>();
                int i = 1;
                foreach (int r in fl.ships)
                {
                    dic[i] = r;
                    i++;
                }

                z.instance.fleetcfgs[orderid] = dic;



                z.instance.save_fleet_cfg();
                z.log("舰队配置保存成功 @ " + orderid + "号配置槽" + " <= " + fl.title);
                MessageBox.Show("舰队配置保存成功 @ " + orderid + "号配置槽" + " <= " + fl.title);
                return;
            }

            if (fl != null && opc == "读取")
            {

                Dictionary<int, int> dic = z.instance.fleetcfgs[orderid];

                for (int i = 0; i < 6; i++)
                {
                    int sid = 0;
                    if (i < dic.Count)
                    {
                        sid = dic[i + 1];
                    }
                    ServerRequestManager.instance.ChangeFleetShip(fl.id, sid, i);
                    System.Threading.Thread.Sleep(300);
                }

                z.log("舰队更换成功 @ " + fl.title + " <= " + orderid + "号配置槽");
                MessageBox.Show("舰队更换成功 @ " + fl.title + " <= " + orderid + "号配置槽");
                z.instance.updateFleetInfo(GameData.instance.UserFleets);
                return;
            }
        }

        internal void freezeFleetGrid(bool p)
        {
            this.canUpdateGridUI = !p;
        }

        private void autodevcheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        internal void OnGetShipSuccess(int dockid,GetShipData getShipData)
        {
            if (build_data.ContainsKey(dockid))
            {
                tools.reporter.reportBuildShip(dockid, build_data[dockid], getShipData);
            }
        }

        internal void BeforeBuildShip(int dockid, int oil, int steel, int ammo, int al)
        {
            BSLOG bl = new BSLOG();
            bl.oil = oil;
            bl.ammo = ammo;
            bl.steel = steel;
            bl.al = al;
            bl.fleetcommanderid = GameData.instance.UserFleets[0].ships[0];
            bl.timetick = ServerTimer.GetNowServerTime();
            build_data[dockid] = bl;
        }
        internal void OnbuildShipSuccess(int dockid,BuildShipData data)
        {
            if(build_data.ContainsKey(dockid))
            {
                var r = build_data[dockid];
                r.buildreturntype = data.dockVo[0].shipType;
                build_data[dockid] = r;
            }
        }

        internal void OnBuildWeaponSuccess(int dockid, BuildEquipData data)
        {
            if (weapon_build_data.ContainsKey(dockid))
            {
                var r = weapon_build_data[dockid];
                r.buildreturntype = data.equipmentDockVo[0].equipmentCid;
                weapon_build_data[dockid] = r;
            }
        }

        internal void BeforeBuildWeapon(int dockid, int oil, int steel, int ammo, int al)
        {
            BSLOG bl = new BSLOG();
            bl.oil = oil;
            bl.ammo = ammo;
            bl.steel = steel;
            bl.al = al;
            bl.fleetcommanderid = GameData.instance.UserFleets[0].ships[0];
            bl.timetick = ServerTimer.GetNowServerTime();
            weapon_build_data[dockid] = bl;
        }

        internal void OnGetWeaponSuccess(int dockid, GetEquipData getEquipData)
        {
            if (weapon_build_data.ContainsKey(dockid))
            {
                tools.reporter.reportBuildWeapon(dockid, weapon_build_data[dockid], getEquipData);
            }
        }

        private void buildanalyzebtn_Click(object sender, EventArgs e)
        {
            Type targetform = Type.GetType("DropCount");
            if (targetform == null)
            {
                return;
            }
            var f = Activator.CreateInstance(targetform);
            if (f == null)
            {
                return;
            }

            MethodInfo m = targetform.GetMethod("ShowDialog", Type.EmptyTypes);
            if (m != null)
            {
                m.Invoke(f, new object[] { });
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            showtt();
        }
    }

