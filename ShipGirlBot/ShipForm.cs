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
using cSouza.WinForms.Controls;

public partial class ShipForm : Form
{
    private int mfleetid;
    private int ship_index;
    private int shipid;
    private int ship_slot_index = -1;
    private Image[] anim = new Image[4];
    private int animcounter = 0;


    private BorderLabel[] weaponname = new BorderLabel[4];
    private PictureBox[] weapons = new PictureBox[4];
    private BorderLabel[] weaponcount = new BorderLabel[4];

    private BorderLabel[] weaponprop1 = new BorderLabel[4];
    private BorderLabel[] weaponprop1val = new BorderLabel[4];
    private BorderLabel[] weaponprop2 = new BorderLabel[4];
    private BorderLabel[] weaponprop2val = new BorderLabel[4];
    private BorderLabel[] weaponprop3 = new BorderLabel[4];
    private BorderLabel[] weaponprop3val = new BorderLabel[4];

    private BorderLabel[] attrs = new BorderLabel[12];

    private PictureBox[] stars = new PictureBox[6];

    List<ShipPropsType> type_order_list = new List<ShipPropsType> {
            ShipPropsType.Atk, 
            ShipPropsType.Torpedo, 
            ShipPropsType.AircraftAtk, 
            ShipPropsType.AirDef, 
            ShipPropsType.Antisub,
            ShipPropsType.Radar,
            ShipPropsType.Hit,
            ShipPropsType.Miss,
            ShipPropsType.Range,
            ShipPropsType.Luck,
            ShipPropsType.Def };
    public ShipForm()
    {
        InitializeComponent();

        foreach (Panel p in new List<Panel>() { bg, biglihui, panelbg, chainanim })
        {
            //if (System.Windows.Forms.SystemInformation.TerminalServerSession)
            //    continue;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(p, true, null);




        }
            //EquipmentConfig ec = new EquipmentConfig();
            //ec.star = 5;
            //ec.picId = "131";

            //weapon1.Image = tools.helper.getShipEquipmentImage(ec);

        weapons[0] = weapon1;
        weapons[1] = weapon2;
        weapons[2] = weapon3;
        weapons[3] = weapon4;

        weaponname[0] = weapon1name;
        weaponname[1] = weapon2name;
        weaponname[2] = weapon3name;
        weaponname[3] = weapon4name;

        weaponcount[0] = weapon1count;
        weaponcount[1] = weapon2count;
        weaponcount[2] = weapon3count;
        weaponcount[3] = weapon4count;

        weaponprop1[0] = weapon1prop1;
        weaponprop1[1] = weapon2prop1;
        weaponprop1[2] = weapon3prop1;
        weaponprop1[3] = weapon4prop1;

        weaponprop1val[0] = weapon1prop1val ;
        weaponprop1val[1] = weapon2prop1val;
        weaponprop1val[2] = weapon3prop1val;
        weaponprop1val[3] = weapon4prop1val;

        weaponprop2[0] = weapon1prop2;
        weaponprop2[1] = weapon2prop2;
        weaponprop2[2] = weapon3prop2;
        weaponprop2[3] = weapon4prop2;

        weaponprop2val[0] = weapon1prop2val;
        weaponprop2val[1] = weapon2prop2val;
        weaponprop2val[2] = weapon3prop2val;
        weaponprop2val[3] = weapon4prop2val; 
        
        weaponprop3[0] = weapon1prop3;
        weaponprop3[1] = weapon2prop3;
        weaponprop3[2] = weapon3prop3;
        weaponprop3[3] = weapon4prop3;

        weaponprop3val[0] = weapon1prop3val;
        weaponprop3val[1] = weapon2prop3val;
        weaponprop3val[2] = weapon3prop3val;
        weaponprop3val[3] = weapon4prop3val;

        attrs[0] = attr1;
        attrs[1] = attr2;
        attrs[2] = attr3;
        attrs[3] = attr4;
        attrs[4] = attr5;
        attrs[5] = attr6;
        attrs[6] = attr7;
        attrs[7] = attr8;
        attrs[8] = attr9;
        attrs[9] = attr10;
        attrs[10] = attr11;
        attrs[11] = attr12;

        stars[0] = star1;
        stars[1] = star2;
        stars[2] = star3;
        stars[3] = star4;
        stars[4] = star5;
        stars[5] = star6;
        stars[0] = star1;

        for (int i=0;i<4;i++)
        {
            anim[i] = tools.helper.OpenImage("res/png/bigbg/chainanim_0" + (i+1) + ".png");
        }

        timer1.Enabled = true;

    }
    public int FleetID
    {
        get { return mfleetid; }
        set { mfleetid = value; }
    }

    public void setfleet_id(int id, int slotindex, int nowshipid)
    {
        FleetID = id; 
        ship_index = slotindex;
        shipid = nowshipid;

        
        GridColumn column1 = equiplist.PrimaryGrid.Columns[1];
        if (column1.EditControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        if (column1.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }

        GridColumn column2 = equiplist.PrimaryGrid.Columns[0];
        column2.EditorType = typeof(ChooseWeaponButton);
        column2.EditorParams = new object[] { shipid, this };

        equiplist.PrimaryGrid.Rows.Clear();

        updateCurFleetInfo();
    }

    internal void updateCurFleetInfo()
    {

        UserFleet uf = GameData.instance.GetFleetOfId(FleetID);
        if (uf == null)
        {
            return;
        }

        Text = "查看少女 -- " + uf.title;
        int i = 0;

        if(shipid >0)
        {
            
            UserShip us = GameData.instance.GetShipById(shipid);

            updateshipinfo(us);
            updateprop(us);
            lockallweapon(us);
            updateweapon(us);

        }
        else
        {
            biglihui.BackgroundImage = tools.helper.OpenImage("res/png/unknown/899.png");
            updateshipinfo(null);
            lockallweapon(null);
            updateprop(null);

        }
        
    }

    private void updateweapon(UserShip us)
    {
        for(int i=0;i<4;i++)
        {
            updateoneweapon(us, i);
        }
    }

    private void updateoneweapon(UserShip us,int index)
    {
        bool enableslot = index < us.equipmentArr.Length;

        if(enableslot == false)
        {
            return;
        }
        else
        {
            bool hasweapon = us.equipmentArr[index].id >0;
            if(hasweapon)
            {
                weapons[index].Image = tools.helper.getShipEquipmentImage(us.equipmentArr[index].config);
            }
            else
            {
                weapons[index].Image = tools.helper.OpenImage("res/png/icons/weapon_add.png");
                return;
            }
        }
        EquipInShip eip  = us.equipmentArr[index];

        if(us.capacitySlotExist[index]== 1)
        {
            weaponcount[index].Visible = true;
            weaponcount[index].Text = "" + us.capacitySlot[index] + "/" + us.capacitySlotMax[index];
        }
        weaponname[index].Visible = true;
        weaponname[index].Text = eip.config.title.Length>6? (eip.config.title.Substring(0,6)+".."):(eip.config.title.Length<=3? ("  "+eip.config.title):eip.config.title);



        List<int> vallist = new List<int> {
            eip.config.atk,
            eip.config.torpedo,
            eip.config.aircraftAtk,
            eip.config.airDef,
            eip.config.antisub,
            eip.config.radar,
            eip.config.hit,
            eip.config.miss,
            eip.config.range,
            eip.config.luck,
            eip.config.def
        };
        int count = 0;
        List<BorderLabel> pnames = new List<BorderLabel>(){weaponprop1[index],weaponprop2[index],weaponprop3[index]};
        List<BorderLabel> pvals = new List<BorderLabel>() { weaponprop1val[index], weaponprop2val[index], weaponprop3val[index] };
        SortedDictionary<int, int> propdic = new SortedDictionary<int, int>();
        for (int i = 0; (i < vallist.Count) && (count < 3); i++)
        {
            if (vallist[i] != 0)
            {
                pnames[count].Visible = true;
                pnames[count].Text = tools.helper.getShipProptypestring(type_order_list[i]);

                pvals[count].Visible = true;
                pvals[count].Text = tools.helper.getShipProptypeval(type_order_list[i],vallist[i]);

                count++;
            }
        }

    }

    private void lockallweapon(UserShip us)
    {
        for (int i = 0; i < 4;i++ )
        {
            weapons[i].Image = tools.helper.OpenImage("res/png/icons/weapon_locked.png");

            foreach (var v in new List<Control[]>() { 
            weaponname, weaponcount, 
            weaponprop1, weaponprop1val,
            weaponprop2, weaponprop2val, 
            weaponprop3, weaponprop3val
            })
            {
                v[i].Visible = false;
            }

        }
    }

    private void updateprop(UserShip us)
    {
        if(us == null)
        {
            foreach( var v in attrs)
            {
                v.Visible = false;
            }
            return;
        }
        Dictionary<ShipPropsType, bool> equipEffectedProps = new Dictionary<ShipPropsType, bool>();
        List<ShipPropsType> type_list = new List<ShipPropsType> {
            ShipPropsType.Atk, 
            ShipPropsType.Torpedo, 
            ShipPropsType.AirDef, 
            ShipPropsType.Antisub,
            ShipPropsType.Radar,
            ShipPropsType.Hit,
            ShipPropsType.Miss,
            ShipPropsType.Range};

        for (int i = 0; i < us.equipmentArr.Length; i++)
        {
            if (us.equipmentArr[i] != null)
            {
                EquipmentConfig config = us.equipmentArr[i].config;
                if (config != null)
                {
                    List<int> list2 = new List<int> {
                        config.atk,
                        config.torpedo,
                        config.airDef,
                        config.antisub,
                        config.radar,
                        config.hit,
                        config.miss,
                        config.range
                    };
                    for (int j = 0; j < type_list.Count; j++)
                    {
                        if (list2[j] != 0)
                        {
                            equipEffectedProps[type_list[j]] = true;
                        }
                    }
                }
            }
        }

        Dictionary<ShipPropsType, int> attrlist = new Dictionary<ShipPropsType, int>();
        attrlist.Add(ShipPropsType.Hp, us.battlePropsMax.hp);
        attrlist.Add(ShipPropsType.Atk, us.battleProps.atk);
        attrlist.Add(ShipPropsType.Capacity, us.OwnAirplans);
        attrlist.Add(ShipPropsType.Def, us.battleProps.def);
        attrlist.Add(ShipPropsType.Torpedo, us.battleProps.torpedo);
        attrlist.Add(ShipPropsType.Luck, us.battleProps.luck);
        attrlist.Add(ShipPropsType.Miss, us.battleProps.miss);
        attrlist.Add(ShipPropsType.AirDef, us.battleProps.airDef);
        attrlist.Add(ShipPropsType.Range, us.battleProps.range);
        attrlist.Add(ShipPropsType.Radar, us.battleProps.radar);
        attrlist.Add(ShipPropsType.Antisub, us.battleProps.antisub);
        attrlist.Add(ShipPropsType.Speed, (int)us.battleProps.speed);

        int count = 0;
        foreach(var kv in attrlist)
        {
            attrs[count].Visible = true;
            if (equipEffectedProps.ContainsKey(kv.Key) && equipEffectedProps[kv.Key] == true)
            {
                attrs[count].ForeColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                attrs[count].ForeColor = System.Drawing.Color.White;
            }
            attrs[count].Text = tools.helper.getShipProptypeval(kv.Key,kv.Value);
            count++;
        }
        

    }

    private void updateshipinfo(UserShip us)
    {

        bg.BackgroundImage = tools.helper.OpenImage("res/png/bigbg/fullColor" +(us ==null?1:us.ship.star) + ".png");
        if(us == null)
        {
            biglihui.BackgroundImage = tools.helper.OpenImage("res/png/big_n/ship1024_normal_899.png");
        }
        else
        {
            biglihui.BackgroundImage = tools.helper.OpenImage("res/png/" + (us.BrokenType == ShipBrokenType.noBroken ? "big_n/ship1024_normal_" : "big_po/ship1024_broken_") + us.ship.picId + ".png");
        }
        


        int starlv = us == null ? 1 : us.ship.star;
        for(int i =0;i<6;i++)
        {
            stars[i].Visible = starlv > i;
        }

        shipnumber.Text = us == null ? "" : ("N.O. " + us.ship.picId);
        shiptitle.Text = us == null ? "" : (us.ship.title.Length <3? " ":"") + us.ship.title;
        shipsubtitle.Text = us == null ? "" :  us.ship.classNo;
        shiplevel.Text = (us == null ? "" :( "Lv. "+us.level+" Exp:"+us.exp + "/"+us.nextExp));

        
        if(us!=null)
        {
            string s = us.ship.vow;
            string s2 ="";
            if (s.Length > 32)
            {
                s2 = s.Substring(32);
                s = s.Substring(0, 32);
            }
            shipvow1.Text = s;
            shipvow2.Text = s2;
            modicon.Visible = us.ship.evoClass > 0;
            modicon.Image = tools.helper.OpenImage("res/png/bigbg/mod" + us.ship.evoClass + ".png");
        }else{
            modicon.Visible = false;
            shipvow1.Text = "...";
            shipvow2.Text = "";
        }

        int shiptype = us == null ? 16 : (int)us.ship.type;
        if (shiptype < 1 || shiptype > 16)
        { shiptype = 16; }
        shiptypeicon.Image = tools.helper.OpenImage("res/png/icons/w" + shiptype + ".png");
    }

    private void updateWeaponSelGrid()
    {
        GridPanel panel = equiplist.PrimaryGrid;
        panel.Rows.Clear();

        if(shipid<= 0 )
        {
            return;
        }
        UserShip uss = GameData.instance.GetShipById(shipid);
        if(uss == null|| uss.ship == null)
        {
            return;
        }

        Dictionary<int, KeyValuePair<UserEquipment, int> > canlist = new Dictionary<int, KeyValuePair<UserEquipment, int> >();
        List<UserEquipment> userEquipments = GameData.instance.UserEquipments;
        ShipType type = uss.ship.type;
        int num = uss.capacitySlotMax[ship_slot_index-1];

        foreach (UserEquipment equipment in userEquipments)
        {
            if (equipment.status == 0)
            {
                EquipmentConfig config = equipment.config;
                if (config.CanUsedInShip(type) && uss.ship.CanUseEquip((int)config.type))
                {
                    if ((num == 0) && this.IsAirPlane(equipment.config))
                    {
                    }
                    else
                    {
                        if(canlist.ContainsKey(config.cid))
                        {
                            var kv = canlist[config.cid];
                            canlist[config.cid] = new KeyValuePair<UserEquipment,int>(kv.Key,kv.Value+1);
                        }else{
                            canlist[config.cid] = new KeyValuePair<UserEquipment,int>(equipment,1);
                        }
                    }
                }

            }
        }

        object[] vv = new object[6] { "清空", "", "清空", "", "", ""};
        panel.Rows.Add(new GridRow(vv));
        panel.GetCell(panel.Rows.Count - 1, 0).Tag = 0;

        int i = 0;
        foreach (var us in canlist)
        {


            List<int> vallist = new List<int> {
                us.Value.Key.config.atk,
                us.Value.Key.config.torpedo,
                us.Value.Key.config.aircraftAtk,
                us.Value.Key.config.airDef,
                us.Value.Key.config.antisub,
                us.Value.Key.config.radar,
                us.Value.Key.config.hit,
                us.Value.Key.config.miss,
                us.Value.Key.config.range,
                us.Value.Key.config.luck,
                us.Value.Key.config.def
            };

            string[] props = new string[2]{"",""};
            int count = 0;
            for (int j = 0; (j < vallist.Count) && (count < 2); j++)
            {
                if (vallist[j] != 0)
                {
                    props[count] = tools.helper.getShipProptypestring(type_order_list[j]) +":"+tools.helper.getShipProptypeval(type_order_list[j], vallist[j]);
                    count++;
                }
            }
            
            object[] vals = new object[7];

            vals[0] = "装备";
            vals[1] = tools.helper.getShipEquipmentImage(us.Value.Key.config);
            vals[2] = us.Value.Key.config.title;
            vals[3] = us.Value.Key.config.star;
            vals[4] = props[0];
            vals[5] = props[1];
            vals[6] = us.Value.Value;
            GridRow gr = new GridRow(vals);
            gr.RowHeight = 100;

            panel.Rows.Add(gr);
            panel.GetCell(panel.Rows.Count - 1, 0).Tag = us.Key;
            i++;
        }


    }

    void shipButtonClick(object sender, EventArgs e)
    {


    }

    public int get_cur_index()
    {
        return this.ship_slot_index;
    }


    internal class ChooseWeaponButton : GridButtonXEditControl
    {
        private int shipid;
        private ShipForm form;
        public ChooseWeaponButton(int sid, ShipForm par)
        {
            shipid = sid;
            form = par;

            Click += new EventHandler(CommandButtonClick);
        }

        #region CommandButtonClick

        void CommandButtonClick(object sender, EventArgs e)
        {
            if(form.get_cur_index() <=0 || shipid <=0 )
            {
                MessageBox.Show("错误的船只");
                return;
            }
            int weapon = (int)EditorCell.GridRow.Cells[0].Tag;



            if (weapon > 0)
            {
                
                int weaponid = -1;
                foreach (var w in GameData.instance.UserEquipments)
                {
                    if (w.status == 0 && w.config.cid == weapon)
                    { weaponid = w.id; }
                }

                if(weaponid!= -1)
                {
                    ServerRequestManager.instance.ChangeEquip(GameData.instance.GetShipById(shipid), form.get_cur_index()-1, weaponid);
                }
                else
                {
                    MessageBox.Show("错误的id");
                }
            }
            else
            {
                ServerRequestManager.instance.DeleteEquip(GameData.instance.GetShipById(shipid), form.get_cur_index() - 1);
            }

            


            form.updateCurFleetInfo();
            form.updateWeaponSelGrid();
        }

        #endregion
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        //if(animcounter >3)
        //{
        //    animcounter = 0;
        //}
        //using(Graphics g = Graphics.FromImage( chainanim.BackgroundImage ))
        //{
        //    g.DrawImage(anim[animcounter],0,0);
        //}
        //bg.Refresh();
        ////chainanim.Invalidate(false);
        //animcounter++;
    }

    private void weapon1_Click(object sender, EventArgs e)
    {
        ship_slot_index = 1;
        updateWeaponSelGrid();
    }

    private void weapon2_Click(object sender, EventArgs e)
    {
        ship_slot_index = 2;
        updateWeaponSelGrid();
    }

    private void weapon3_Click(object sender, EventArgs e)
    {
        ship_slot_index = 3;
        updateWeaponSelGrid();
    }

    private void weapon4_Click(object sender, EventArgs e)
    {
        ship_slot_index = 4;
        updateWeaponSelGrid();
    }


    private bool IsAirPlane(EquipmentConfig c)
    {
        return ((((c.type == EquipmentType.AttackPlane) || (c.type == EquipmentType.FightPlane)) || (c.type == EquipmentType.XAttackPlane)) || (c.type == EquipmentType.SpyPlane));
    }
}