using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace tools
{
    class helper
    {
        public static string count_server_addr = "http://127.0.0.1";

        public static Image OpenImage(string previewFile)
        {
            if(System.IO.File.Exists(previewFile))
            {
                FileStream fs = new FileStream(previewFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var ret = Image.FromStream(fs);
                fs.Dispose();
                return ret;
            }
            else
            {
                FileStream fs = new FileStream("res/png/unknown/899.png", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var ret = Image.FromStream(fs);
                fs.Dispose();
                return ret;
            }

        }
        public static string gettypeofuser(int ulnum)
        {
            int cardnum = ulnum;
            if(cardnum <= 50)
            {
                return "南极提督";
            }
            if (cardnum <= 70)
            {
                return "非洲提督";
            }
            if (cardnum <= 90)
            {
                return "亚洲提督";
            }
            if(cardnum <= 95)
            {
                return "欧洲提督";
            }
            return "蓝星提督";
        }

        public static string getshiptype(ShipType st)
        {
            switch(st)
            {
                case ShipType.AircraftBattleship:
                 return "航战";
                case ShipType.AircraftCarrier:
                 return "轻母";
                case ShipType.AircraftCruiser:
                 return "航巡";
                case ShipType.All:
                 return "ALL";
                case ShipType.BattleCruiser:
                 return "战巡";
                case ShipType.Battleship:
                 return "战列";
                case ShipType.Carrier:
                 return "航母";
                case ShipType.Destroyer:
                 return "驱逐";
                case ShipType.FlagShip:
                    return "旗舰";
                case ShipType.HeavyCruiser:
                 return "重巡";
                case ShipType.LightCruiser:
                 return "轻巡";
                case ShipType.LowHeavyBattleShip:
                 return "重炮";
                case ShipType.Others:
                 return "其他";
                case ShipType.SeaplaneTender:
                 return "水母";
                case ShipType.Submarine:
                 return "潜艇";
                case ShipType.SubmarineCarrier:
                 return "潜航";
                case ShipType.SubmarineHeavyBattle:
                 return "炮潜";
                case ShipType.Supplier:
                    return "补给";
                case ShipType.TorpedoCruiser:
                    return "雷巡";
                case ShipType.Airport:
                    return "机场";
                case ShipType.ShipPort:
                    return "港口";
                case ShipType.Fort:
                    return "要塞";
                case ShipType.MerchantShip:
                    return "商船";
                case ShipType.LandShip:
                    return "登陆";
                case ShipType.Pirate:
                    return "海盗";
                case ShipType.Unknow:
                default:
                    return "未知";
            }
        }

        public static string getresourcetype(string st)
        {
            try
            {
                return getresourcetype((ResourceTypes)int.Parse(st));
            }catch(Exception )
            {
                
            }
            return "未知";
        }
        public static string getresourcetype(ResourceTypes st)
        {
            switch (st)
            {
               case ResourceTypes.Aluminium:
                 return "铝材";
                case ResourceTypes.Ammo:
                 return "弹药";
                case ResourceTypes.BigShip:
                 return "大舰";
                case ResourceTypes.BuildEquipItem:
                 return "开发材料";
                case ResourceTypes.BuildMaterial:
                 return "建材";
                case ResourceTypes.BuildShipItem:
                 return "建造材料";
                case ResourceTypes.Equipment:
                 return "装备";
                case ResourceTypes.Exp:
                 return "经验";
                case ResourceTypes.FastBuild:
                 return "快速建造";
                case ResourceTypes.FastRepair:
                 return "快速修理";
                case ResourceTypes.Gold:
                 return "钻石";
                case ResourceTypes.LoveRing:
                 return "婚戒";
                case ResourceTypes.MainQuest:
                 return "主线任务";
                case ResourceTypes.MediumShip:
                 return "中型舰艇";
                case ResourceTypes.NormalQuest:
                 return "普通任务";
                case ResourceTypes.Oil:
                 return "燃油";
                case ResourceTypes.ShipEnemy:
                 return "船体力";
                case ResourceTypes.ShipExp:
                 return "船经验";
                case ResourceTypes.SmallShip:
                 return "小型舰艇";
                case ResourceTypes.Steel:
                 return "钢铁";
                case ResourceTypes.Submarine:
                 return "潜艇";
                case ResourceTypes.WeekQuest:
                 return "每周任务";
                default:
                 return "未知";
            }
        }

        public static string getformationstring(FleetFormation st)
        {
            switch (st)
            {
                case FleetFormation.OneRow:
                    return "单纵";
                case FleetFormation.TwoRow:
                    return "复纵";
                case FleetFormation.Cicle:
                    return "圆形";
                case FleetFormation.OneColume:
                    return "单横";
                case FleetFormation.TStyle:
                    return "T/梯形";
                default:
                    return "未知";
            }
        }

        public static FleetFormation getformationenum(string st)
        {
            switch (st)
            {
                
                case "单纵":
                    return FleetFormation.OneRow;
                case "复纵":
                    return FleetFormation.TwoRow;
                case "圆形":
                    return FleetFormation.Cicle;
                case "单横":
                    return FleetFormation.OneColume;
                case "T/梯形":
                    return FleetFormation.TStyle;
                    
                default:
                    return FleetFormation.TwoRow;
            }
        }
        public static Image getScaleAndCropImage(string path, Rectangle crop_rect, Rectangle newsize  )
        {

            Bitmap src = tools.helper.OpenImage(path) as Bitmap;
            Bitmap target = new Bitmap(newsize.Width, newsize.Height);

            using(Graphics g = Graphics.FromImage(target))
            {
               g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                crop_rect,                        
                                GraphicsUnit.Pixel);
            }
            return target as Image;
        }

        public static string getShipBarString(ShipBrokenType sbt)
        {
            switch(sbt)
            {

                case ShipBrokenType.middleBroken:
                    return "res/png/bigbg/yellowbar.png";
                case ShipBrokenType.bigBorken:
                    return "res/png/bigbg/redbar.png";
                case ShipBrokenType.noBroken:
                default:
                    return "res/png/bigbg/greenbar.png";
            }
        }
        public static string getShipBarStringv(ShipBrokenType sbt)
        {
            switch (sbt)
            {

                case ShipBrokenType.middleBroken:
                    return "res/png/bigbg/yellowbarv.png";
                case ShipBrokenType.bigBorken:
                    return "res/png/bigbg/redbarv.png";
                case ShipBrokenType.noBroken:
                default:
                    return "res/png/bigbg/greenbarv.png";
            }
        }
        public static Image getShipBigImage( UserShip us, Rectangle newsize)
        {
            int borderid = 1;
            if(us.ship.evoClass>0)
            {
                borderid = 2;
            }
            if(us.married ==1)
            {
                borderid = 3;
            }
            Bitmap border = tools.helper.OpenImage("res/png/bigbg/border" + borderid + ".png") as Bitmap;
            Bitmap bgsrc = tools.helper.OpenImage("res/png/bigbg/fullColor" + us.ship.star + ".png") as Bitmap;
            Bitmap bigsrc = tools.helper.OpenImage("res/png/" + 
                (us.BrokenType == ShipBrokenType.noBroken ? "big_n/ship1024_normal_" : "big_po/ship1024_broken_")
                 + us.ship.picId + ".png") as Bitmap;
            Bitmap target = new Bitmap(newsize.Width, newsize.Height);
            Bitmap barbg = tools.helper.OpenImage("res/png/bigbg/barbg.png") as Bitmap;
            Bitmap bar = tools.helper.OpenImage(getShipBarString(us.BrokenType)) as Bitmap;

            Font titleFont = new Font("微软雅黑", 16);
            SolidBrush titleBrush = new SolidBrush(Color.Black);

            Rectangle crop = new Rectangle(128 + 48, 128, 1024 - 256 - 96, 1024 - 128);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(bgsrc, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(128, 0, bgsrc.Width-256, bgsrc.Height), GraphicsUnit.Pixel);
                g.DrawImage(border, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, border.Width, border.Height), GraphicsUnit.Pixel);
                
                g.DrawImage(bigsrc, new Rectangle(0, 0, target.Width, target.Height), crop, GraphicsUnit.Pixel);

                g.DrawString("Lv." + us.level + " " + us.ship.title, titleFont, titleBrush, new PointF(5.0f, 5.0f));

                int hp = 100 * newsize.Width * us.battleProps.hp / us.battlePropsMax.hp;
                g.DrawImage(barbg, new Rectangle(0, newsize.Height - 6, newsize.Width, 12), new Rectangle(0, 0, barbg.Width, barbg.Height), GraphicsUnit.Pixel);
                g.DrawImage(bar, new Rectangle(0, newsize.Height - 6, newsize.Width * hp /100 , 12), new Rectangle(0, 0, bar.Width, bar.Height), GraphicsUnit.Pixel);
            }
            return target as Image;
        }

        public static Image getShipSmallImage(UserShip us)
        {
            
            Bitmap bgsrc = tools.helper.OpenImage("res/png/bigbg/s" + us.ship.star + ".png") as Bitmap;
            Bitmap bigsrc = tools.helper.OpenImage("res/png/"
            + (us.BrokenType == ShipBrokenType.noBroken ? "head_n/ship64_normal_" : "head_po/ship64_broken_")
            + us.ship.picId + ".png") as Bitmap;

            Rectangle newsize = new Rectangle(0, 0, bigsrc.Width, bigsrc.Height);

            Bitmap shiptype = tools.helper.OpenImage("res/png/shiptype/" + (int)us.ship.type + ".png") as Bitmap;

            Bitmap target = new Bitmap(newsize.Width, newsize.Height);
            Bitmap barbg = tools.helper.OpenImage("res/png/bigbg/barbg.png") as Bitmap;
            Bitmap bar = tools.helper.OpenImage(getShipBarString(us.BrokenType)) as Bitmap;

            Font titleFont = new Font("微软雅黑", 16);
            SolidBrush titleBrush = new SolidBrush(Color.Black);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(bgsrc, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, bgsrc.Width, bgsrc.Height), GraphicsUnit.Pixel);
                g.DrawImage(bigsrc, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0,0, bigsrc.Width,bigsrc.Height), GraphicsUnit.Pixel);

                g.DrawImage(shiptype, new Rectangle(6, 6, shiptype.Width, shiptype.Height), new Rectangle(0, 0, shiptype.Width, shiptype.Height), GraphicsUnit.Pixel);

                //g.DrawString("Lv." + us.level + " " + us.ship.title, titleFont, titleBrush, new PointF(5.0f, 5.0f));

                int hp = 100 * us.battleProps.hp / us.battlePropsMax.hp;
                int pos = hp * newsize.Height / 100;
                g.DrawImage(barbg, new Rectangle(newsize.Width - 10, 0, barbg.Width, newsize.Height), new Rectangle(0, 0, barbg.Width, barbg.Height), GraphicsUnit.Pixel);
                g.DrawImage(bar, new Rectangle(newsize.Width - 10, newsize.Height - pos, barbg.Width, pos), new Rectangle(0, 0, bar.Width, bar.Height), GraphicsUnit.Pixel);
            }
            bgsrc.Dispose(); bgsrc = null;
            bigsrc.Dispose(); bigsrc = null;
            shiptype.Dispose(); shiptype = null;

            barbg.Dispose(); barbg = null;
            bar.Dispose(); bar = null;
            titleFont.Dispose();
            titleBrush.Dispose();

            return target as Image;
        }

        public static Image getShipEquipmentImage(EquipmentConfig us)
        {

            Bitmap bgsrc = tools.helper.OpenImage("res/png/bigbg/weapon_cellbg" + us.star + ".png") as Bitmap;
            Bitmap bigsrc = tools.helper.OpenImage("res/png/weapon_icon/" + us.picId + ".png") as Bitmap;

            Bitmap change_tools = tools.helper.OpenImage("res/png/bigbg/weapon_changer_green.png") as Bitmap;

            Rectangle newsize = new Rectangle(0, 0, 100, 100);

            Bitmap target = new Bitmap(newsize.Width, newsize.Height);

            Font titleFont = new Font("微软雅黑", 16);
            SolidBrush titleBrush = new SolidBrush(Color.Black);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(bgsrc, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, bgsrc.Width, bgsrc.Height), GraphicsUnit.Pixel);
                g.DrawImage(bigsrc, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, bigsrc.Width, bigsrc.Height), GraphicsUnit.Pixel);
                g.DrawImage(change_tools, new Rectangle(69, 0, target.Width - 69, change_tools.Height), new Rectangle(0, 0, change_tools.Width, change_tools.Height), GraphicsUnit.Pixel);

                //g.DrawString(us.title, titleFont, titleBrush, new PointF(5.0f, 5.0f));


            }
            return target as Image;
        }

        public static Image getEquipmentImage(EquipmentConfig us)
        {

            Bitmap bgsrc = tools.helper.OpenImage("res/png/bigbg/fullColor" + us.star + ".png") as Bitmap;
            Bitmap bigsrc = tools.helper.OpenImage("res/png/weapon_icon/" + us.picId + ".png") as Bitmap;

            Rectangle newsize = new Rectangle(0, 0, 256, 256);

            Bitmap target = new Bitmap(newsize.Width, newsize.Height);

            Font titleFont = new Font("微软雅黑", 16);
            SolidBrush titleBrush = new SolidBrush(Color.Black);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(bgsrc, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, bgsrc.Width, bgsrc.Height), GraphicsUnit.Pixel);
                g.DrawImage(bigsrc, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, bigsrc.Width, bigsrc.Height), GraphicsUnit.Pixel);

                g.DrawString(us.title , titleFont, titleBrush, new PointF(5.0f, 5.0f));


            }
            return target as Image;
        }
        internal static string getwarresultstring(GetBattleResultResponse battleResult, int fleetid)
        {
            UserFleet uf = GameData.instance.GetFleetOfId(fleetid);
            string r = "";

            if(battleResult.warResult == null)
            {
                return ("这里的海面静悄悄，什么都没有发生... 大概？");
            }
            r += (WarResultLevel)battleResult.warResult.resultLevel + "胜 ，";

            if (battleResult.newShipVO != null)
            {
	            foreach(var ns in battleResult.newShipVO)
	            {
	                
	                r+=" 发现新少女 -- "+ ns.ship.title + " " + ns.ship.titleClass;
	            }
            }
            int enemycount = 0;
            int i = 0;
            r += "\r\n";
            if (battleResult.warResult.selfShipResults != null && battleResult.warResult.selfShipResults.Length > 0
                && battleResult.warResult.enemyShipResults != null && battleResult.warResult.enemyShipResults.Length > 0
                && CurrentWarParameters.shipsbeforebattle.Length >0 )
            {
                for (int index = 0; index < battleResult.warResult.selfShipResults.Length ;index++ )
                {
                    if(battleResult.warResult.selfShipResults.Length > index)
                    {
                        var v = battleResult.warResult.selfShipResults[index];
                        UserShip us = GameData.instance.GetShipById(uf.ships[index]);
                        UserShip bus = CurrentWarParameters.shipsbeforebattle[index];
                        r += (v.isMvp == 1 ? "MVP " : "    ") + us.ship.title + "\t\t Lv." + us.level + (v.isLevelUp == 1 ? "↑" : "")
                          + "\t(+" + v.expAdd + ")/" + v.nextLevelExpNeed
                          + "\tHP: " + us.battleProps.hp + "/" + us.battlePropsMax.hp
                          + "\t 油:" + us.battleProps.oil + "(" + (us.battleProps.oil - bus.battleProps.oil) + "）"
                        + "\t 弹:" + us.battleProps.ammo + "(" + (us.battleProps.ammo - bus.battleProps.ammo) + "）"
                          + " \r\n";
                    }

                }
            }

            return r;
        }

        static public bool isFleetCanBattle(UserFleet uf, int changetype)
        {
            if(GameData.instance.IsFleetInExplore(uf.id))
            {
                return false;
            }

            foreach (int i in uf.ships)
            {
                var us = GameData.instance.GetShipById(i);
                if(us == null 
                    ||  us.IsBigBroken 
                    || us.IsInExplore 
                    || us.IsInRepair 
                    || us.battleProps.oil <=0 
                    || us.battleProps.ammo <=0
                    || ((int)us.BrokenType)  >= changetype + 2
                    )
                {
                    return false;
                }
            }

            if(GameData.instance.UserShips.Count  >= GameData.instance.UserInfo.detailInfo.shipNumTop)
            {
                z.log("[后宫已满]无法出征，请bi~~~~掉一些少女再说....");
                return false;
            }
            return true;
        }

        public static string getstartstring(int s)
        {
            string r = "";
            for(int i =1;i<8;i++)
            {
                r += i > s ? " " : "★";
            }
            return r;
        }
        internal static string getEquipmentDesc(int cid)
        {
            var e = GameConfigs.instance.GetEquipmentByCid(cid);
            if(e!= null)
            {
                string r = "";
                r += e.title + " " + getstartstring(e.star)+"\r\n";
                r += e.desc + "\r\n";
                r += getEquipmentTypeString(e.type) + "\r\n";
                r += e.aircraftAtk > 0 ? ("轰炸 " + e.aircraftAtk + "\r\n") : "";
                r += e.def > 0 ? ("装甲 " + e.def + "\r\n") : "";
                r += e.atk > 0 ? ("火力 " + e.atk + "\r\n") : "";
                r += e.torpedo > 0 ? ("雷装 " + e.torpedo + "\r\n") : "";
                r += e.airDef > 0 ? ("对空 " + e.airDef + "\r\n") : "";
                r += e.antisub > 0 ? ("对潜 " + e.antisub + "\r\n") : "";
                r += e.range > 0 ? ("射程 " + e.range + "\r\n") : "";
                r += e.radar > 0 ? ("索敌 " + e.radar + "\r\n") : "";
                r += e.luck > 0 ? ("幸运 " + e.luck + "\r\n") : "";
                r += (e.specialEffect != null && e.specialEffect != "") ? ("特效 " + e.specialEffect + "\r\n") : "";
                r += e.aluminiumUse > 0 ? ("耗铝 " + e.aluminiumUse + "\r\n") : "";
                r += e.miss > 0 ? ("miss " + e.miss + "\r\n") : "";
                r += (e.correction != null && e.correction != "") ? ("补正 " + e.correction + "\r\n") : "";

                return r;
            }
            else
            {
                return "无此装备";
            }
        }

        public static string getEquipmentTypeString(EquipmentType equipmentType)
        {
            switch(equipmentType)
            {
                case EquipmentType.All:
	                 return "装备";
                case EquipmentType.MainGun:
	                 return "主炮";
                case EquipmentType.ViceGun:
	                 return "副炮";
                case EquipmentType.Torpedo:
	                 return "鱼雷";
                case EquipmentType.AttackPlane:
	                 return "攻击机";
                case EquipmentType.FightPlane:
	                 return "战斗机";
                case EquipmentType.XAttackPlane:
                     return "轰炸机";
                case EquipmentType.SpyPlane:
                     return "侦察机";
                case EquipmentType.ElectricDetec:
                     return "雷达";
                case EquipmentType.StrengthenComp:
                     return "强化部件";
                case EquipmentType.Bullet:
                     return "炮弹";
                case EquipmentType.AirGun:
                     return "防空炮";
                case EquipmentType.SpecialSubmarin:
                     return "特殊潜艇";
                case EquipmentType.RepairMan:
                     return "修理员";
                case EquipmentType.AntiSubmarinComp:
                     return "反潜装备";
                default:
                     return "全部";

            }
        }

        public static string getItemtypestr(int type)
        {
            switch(type)
            {
                case 141:
                    return "快速建造";
                case 241:
                    return "建造蓝图 ";
                case 741:
                    return "装备蓝图";
                case 541:
                    return "快速修理";
                case 10141:
                    return "航母改造核心";
                case 10241:
                    return "战列改造核心";
                case 10341:
                    return "巡洋改造核心";
                case 10441:
                    return "驱逐改造核心";
                case 88841:
                    return "誓约之戒";

                default:
                    return "未知" + type;
            }
        }

        internal static string getShipDesc(int shipcid)
        {
            var e = AllShipConfigs.instance.getShip(shipcid);
            if (e != null)
            {
                string r = "";
                r += e.title + " " + getstartstring(e.star) + "\r\n";
                r += e.desc;
                r += getshiptype(e.type);
                r += e.classNo + "\r\n";
                r += e.def > 0 ? ("装甲 " + e.def + "\r\n") : "";
                r += e.atk > 0 ? ("火力 " + e.atk + "\r\n") : "";
                r += e.torpedo > 0 ? ("雷装 " + e.torpedo + "\r\n") : "";
                r += e.airDef > 0 ? ("对空 " + e.airDef + "\r\n") : "";
                r += e.antisub > 0 ? ("对潜 " + e.antisub + "\r\n") : "";
                r += e.range > 0 ? ("射程 " + e.range + "\r\n") : "";
                r += e.radar > 0 ? ("索敌 " + e.radar + "\r\n") : "";
                r += e.luck > 0 ? ("幸运 " + e.luck + "\r\n") : "";
                r += e.miss > 0 ? ("miss " + e.miss + "\r\n") : "";
                r += (e.canEvo == 1 ) ? ("可进化! " + "\r\n") : "";

                return r;
            }
            else
            {
                return "无此船只";
            }
        }

        internal static string getpvpwarresultstring(GetPVPWarResultResponse battleResult, int fleetid)
        {
            UserFleet uf = GameData.instance.GetFleetOfId(fleetid);
            string r = "";

            if (battleResult.warResult == null)
            {
                return ("这里的海面静悄悄，什么都没有发生...可能会丢/多了点油弹什么的？ 大概？");
            }
            r += (WarResultLevel)battleResult.warResult.resultLevel + "胜 ，";

            if (battleResult.newShipVO != null)
            {
                foreach (var ns in battleResult.newShipVO)
                {

                    r += " 发现新少女 -- " + ns.ship.title + " " + ns.ship.titleClass;
                }
            }
            int enemycount = 0;
            int i = 0;
            r += "\r\n";
            if (battleResult.warResult.selfShipResults != null && battleResult.warResult.selfShipResults.Length >0)
            {
	            foreach (var v in battleResult.warResult.selfShipResults)
	            {
	                UserShip us = GameData.instance.GetShipById(uf.ships[i]);
	                r += (v.isMvp == 1 ? "MVP " : "    ") + us.ship.title + " Lv." + us.level + (v.isLevelUp == 1 ? "↑" : "") + "(+" + v.expAdd + ")/" + v.nextLevelExpNeed + " \r\n";
	                i++;
	            }
            }
            r += "\r\n敌方:\r\n";
            if (battleResult.warResult.enemyShipResults != null && battleResult.warResult.enemyShipResults.Length > 0
                && CurrentWarParameters.selectedOpponent!=null
                )
            {
                var p = CurrentWarParameters.selectedOpponent;
                i = 0;
	            foreach (var v in battleResult.warResult.enemyShipResults)
	            {
                    UserShip us = p.ships[i];
	                r+= getshiptype(us.ship.type) + " "+ us.ship.title + " Lv." + us.level 
                        + " HP:"+ v.hp + "/" + us.ship.hp + " \r\n";
	                i++;
	            }
            }
            return r;
        }

        public static  string getShipProptypestring(ShipPropsType type)
        {

            switch(type)
            {
                case ShipPropsType.AircraftAtk:
	                return "轰炸";
                case ShipPropsType.AirDef:
	                return "防空";
                case ShipPropsType.Antisub:
	                return "反潜";
                case ShipPropsType.Atk:
	                return "火力";
                case ShipPropsType.Capacity:
	                return "搭载";
                case ShipPropsType.Def:
	                return "装甲";
                case ShipPropsType.Hit:
	                return "命中";
                case ShipPropsType.Hp:
	                return "耐久";
                case ShipPropsType.Luck:
	                return "幸运";
                case ShipPropsType.Miss:
	                return "回避";
                case ShipPropsType.Radar:
	                return "索敌";
                case ShipPropsType.Range:
	                return "射程";
                case ShipPropsType.Speed:
	                return "速度";
                case ShipPropsType.Torpedo:
                    return "雷装";
                                default:
                    return "未知";
            }

        }

        internal static string getShipProptypeval(ShipPropsType shipPropsType, int val)
        {
           if(shipPropsType == ShipPropsType.Range)
           {
               switch(val)
               {
                   default:
                   case 0:
                       return "无";
                   case 1:
                       return "短";
                   case 2:
                       return "中";
                   case 3:
                       return "长";
                   case 4:
                       return "超长";
               }
           }
           {
               return val.ToString();
           }

        }

        internal static string getCampwarresultstring(GetCampaignWarResultResponse battleResult)
        {
            PVECampaignLevel lv = CurrentWarParameters.selectedCampaignLevel;
            int[] campships = GameData.instance.GetCampaignFleetInfo(lv.id);
            List<UserShip> battleships = new List<UserShip>();
            foreach (int stoi in campships)
            {
                UserShip us = GameData.instance.GetShipById(stoi);
                if (us != null)
                {
                    battleships.Add(us);
                }
            }

            string r = "";

            if (battleResult.warResult == null)
            {
                return ("这里的海面静悄悄，什么都没有发生...可能会丢/多了点油弹什么的？ 大概？");
            }
            r += (WarResultLevel)battleResult.warResult.resultLevel + "胜 ，";

            if (battleResult.newShipVO != null)
            {
                foreach (var ns in battleResult.newShipVO)
                {

                    r += " 发现新少女 -- " + ns.ship.title + " " + ns.ship.titleClass;
                }
            }
            int enemycount = 0;
            int i = 0;
            r += "\r\n";
            if (battleResult.warResult.selfShipResults != null && battleResult.warResult.selfShipResults.Length > 0)
            {
                foreach (var v in battleResult.warResult.selfShipResults)
                {
                    UserShip us = battleships[i];
                    r += (v.isMvp == 1 ? "MVP " : "    ") + us.ship.title + " Lv." + us.level + (v.isLevelUp == 1 ? "↑" : "") + "(+" + v.expAdd + ")/" + v.nextLevelExpNeed 
                        + "  HP:" + us.battleProps.hp + "/" + us.battlePropsMax.hp + "  \r\n";
                    i++;
                }
            }
            r += "\r\n敌方:\r\n";
            if (battleResult.warResult.enemyShipResults != null && battleResult.warResult.enemyShipResults.Length > 0
                && CurrentWarParameters.selectedOpponent != null
                )
            {
                var p = CurrentWarParameters.selectedOpponent;
                i = 0;
                foreach (var v in battleResult.warResult.enemyShipResults)
                {
                    UserShip us = p.ships[i];
                    r += getshiptype(us.ship.type) + " " + us.ship.title + " Lv." + us.level
                        + " HP:" + v.hp + "/" + us.ship.hp + " \r\n";
                    i++;
                }
            }
            return r;
        }

        internal static object getshipEattenString(UserShip cl)
        {
            string r = "";
            r += " 火力 +" + cl.ship.strengthenSupplyExp.atk;
            r += " 雷装 +" + cl.ship.strengthenSupplyExp.torpedo;
            r += " 装甲 +" + cl.ship.strengthenSupplyExp.def;
            r += " 防空 +" + cl.ship.strengthenSupplyExp.air_def;
                return r;
        }

        public static string getPVEEVENTAwardString(PVEEventLevel level)
        {
            string ret = "";
            if(level!=null &&level.award2!=null)
            {
                foreach(var aw in level.award2)
                {

                    if(aw.cid>0)
                    {
                        if(AllShipConfigs.instance.getShip(aw.cid) != null)
                        {
                            ret += "" + AllShipConfigs.instance.getShip(aw.cid).title + " x " + aw.amount + "\r\n";
                        }else if(GameConfigs.instance.GetEquipmentByCid(aw.cid)!= null)
                        {
                            ret += "" + GameConfigs.instance.GetEquipmentByCid(aw.cid).title + " x " + aw.amount + "\r\n";
                        }else
                        {
                            ret += "" + getItemtypestr(aw.cid) + " x " + aw.amount + "\r\n";
                        }
                    }
                    else
                    {
                        ret += aw.icon + "\r\n";
                    }
                }
            }


            return ret;
        }

        internal static string getDetailWarBattleParam(AttackParam[] ap, UserFleet uf, ShipInWar[] enemy)
        {
            string r = "";
            foreach (var opa in ap)
            {
                if(opa.attackSide == AttackSide.Self)
                {
                    UserShip ata = GameData.instance.GetShipById(uf.ships[opa.fromIndex]);
                    r += "\t " + getshiptype(ata.ship.type) + " " + getFormatedtitle(ata.ship.title) + " Lv." + ata.level + (opa.attackSide == AttackSide.Self ? " => " : " <= ");
                    for (int i = 0; i < opa.targetIndex.Length && i < opa.damage.Length; i++)
                    {
                        ShipInWar siw = enemy[opa.targetIndex[i]];
                        siw.hp -= opa.damage[i];
                        if(siw.hp <0 )
                        {
                            siw.hp = 0;
                        }
                        var damp = opa.damages[i];
                        var eqatk = GameConfigs.instance.GetEquipmentByCid(opa.equipmentCid);
                        var skilldesc = GameConfigs.instance.GetSkillConfig(opa.skillId);

                        r += (opa.damage[i] == 0 ? "MISS" : (" -" + opa.damage[i]))
                            + "\t " + (damp.isCritical == 1 ? " 暴击! " : "       ")
                            + "\t"+ (skilldesc != null ? skilldesc.title : (eqatk != null? eqatk.title:"\t") )
                            + "\t " + getShipAttackTypeString(opa.attackType) 
                            +"\t" + siw.title + " Lv." + siw.level + " " 
                            + siw.hp + "/" + siw.hpMax;
                    }
                    r += "\r\n";
                }else{
                    ShipInWar ata =enemy[opa.fromIndex];
                    
                    for (int i = 0; i < opa.targetIndex.Length && i < opa.damage.Length; i++)
                    {
                        UserShip siw = GameData.instance.GetShipById(uf.ships[opa.targetIndex[i]]);
                        var damp = opa.damages[i];
                        var eqatk = GameConfigs.instance.GetEquipmentByCid(opa.equipmentCid);

                        r += "\t " + getshiptype(siw.ship.type) + " "+ getFormatedtitle(siw.ship.title) + " Lv." + siw.level + " <= ";
                        r += (opa.damage[i] == 0 ? "MISS" : (" -" + opa.damage[i]))
                            + "\t " + (damp.isCritical == 1 ? " 暴击! " : "       ")
                            + "\t " + (eqatk == null ? "\t " : eqatk.title)
                            + "\t " + getShipAttackTypeString(opa.attackType)
                            + "\t"  + ata.title + " Lv." + ata.level + " " 
                            + ata.hp + "/" + ata.hp;
                    }
                    r += "\r\n";
                }

            }
            return r;
        }
        internal static string getDetailDayWarresultstring(GetDealNodeResponse battleResult, int fleetid)
        {
            UserFleet uf = GameData.instance.GetFleetOfId(fleetid);
            string r = "";

            if (battleResult.warReport != null)
            {
                float tspeedus = 0.0f;
                int radarus = 0;
                int radarenemy = 0;
                double player_aircontrolval = 0.0f;
                double enemy_aircontrolval = 0.0f;
               
                var ep = battleResult.warReport;

                foreach (int i in uf.ships)
                {
                    var zus = GameData.instance.GetShipById(i);
                    tspeedus += zus.battleProps.speed;
                    radarus += zus.battleProps.radar;

                    for (int index = 0; index < zus.equipmentArr.Length; index++)
                    {
                        if (zus.capacitySlotMax[index] <= 0)
                        {
                            continue;
                        }
                        var eq = zus.equipmentArr[index].config;
                        if (eq != null && eq.type == EquipmentType.FightPlane)
                        {
                            player_aircontrolval += Math.Sqrt((double)zus.capacitySlotMax[index]) * (double)eq.airDef;
                        }
                    }
                }
                if(ep != null && ep.enemyShips!=null)
                {
                    foreach(var ess in ep.enemyShips)
                    {
                        radarenemy += ess.radar;
                        if(ess.equipment==null )
                        {
                            continue;
                        }
                        for (int index = 0; index < ess.equipment.Length; index++)
                        {
                            if(ess.capacitySlotMax[index] <=0)
                            {
                                continue;
                            }
                            var eq = ess.equipment[index].config;
                            if(eq != null && eq.type == EquipmentType.FightPlane)
                            {
                                enemy_aircontrolval += Math.Sqrt((double)ess.capacitySlotMax[index]) * (double)eq.airDef;
                            }
                        }
                    }
                }

                r += "\t 索敌 " + (ep.isExploreSuccess == 1 ? "成功，发现敌军舰队" : "失败。未能发现敌军")
                    + "  我方索敌值：" + radarus + "     敌军索敌值:" + radarenemy
                    +"\r\n";
                r += "\t " + uf.title + " 对战 敌 - " + ep.enemyName + "\r\n";
                r += "\t 战斗阵型： " + getWarTypeString(ep.warType) + "\r\n";
                r += "\t 制空权结果： " + getAirControlType(ep.airControlType)
                    + "  我方制空值：" + (int)player_aircontrolval + "     敌军制空值:" + (int)enemy_aircontrolval
                    + "\r\n";

                if(ep.openAirAttack != null && ep.openAirAttack.Length >0)
                {
                    r += " 开幕空袭：\r\n";
                    r += getDetailWarBattleParam(ep.openAirAttack, uf, ep.enemyShips);

                }

                if (ep.openAntiSubAttack != null && ep.openAntiSubAttack.Length > 0)
                {
                    r += " 开幕反潜：\r\n";
                    r += getDetailWarBattleParam(ep.openAntiSubAttack, uf, ep.enemyShips);

                }


                if (ep.openTorpedoAttack != null && ep.openTorpedoAttack.Length > 0)
                {
                    r += " 开幕雷击：\r\n";
                    r += getDetailWarBattleParam(ep.openTorpedoAttack, uf, ep.enemyShips);

                }

                if (ep.normalAttacks != null && ep.normalAttacks.Length > 0)
                {
                    r += " 炮击战：\r\n";
                    r += getDetailWarBattleParam(ep.normalAttacks, uf, ep.enemyShips);

                }

                if (ep.closeTorpedoAttack != null && ep.closeTorpedoAttack.Length > 0)
                {
                    r += " 闭幕雷击战：\r\n";
                    r += getDetailWarBattleParam(ep.closeTorpedoAttack, uf, ep.enemyShips);

                }

                if(ep.bossHp > 0)
                {
                    r += " Boss HP: " + ep.bossHpLeft + "/" + ep.bossHp + "\r\n";
                }
            }

            return r;
        }


        internal static string getDetailNightWarresultstring(GetBattleResultResponse battleResult, int fleetid)
        {
            UserFleet uf = GameData.instance.GetFleetOfId(fleetid);
            string r = "";

            if (battleResult.extraProgress != null && CurrentWarParameters.daybattleresult != null)
            {
                var ep = battleResult.extraProgress;
                var dep = CurrentWarParameters.daybattleresult.warReport;
                r += "夜战 - " + uf.title + " 对战 敌 - " + ep.enemyName + "\r\n";

                if (ep.nightAttacks != null)
                {
                    r += " 夜战：\r\n";
                    r += getDetailWarBattleParam(ep.nightAttacks, uf, dep.enemyShips);
                }

                if (ep.bossHp > 0)
                {
                    r += " Boss HP: " + ep.bossHpLeft + "/" + ep.bossHp + "\r\n";
                }
            }

            return r;

        }

        internal static string getPVPDetailDayWarresultstring(StartPVPWarResponse battleResult, int fleetid)
        {
            UserFleet uf = GameData.instance.GetFleetOfId(fleetid);
            string r = "";

            if (battleResult.warReport != null)
            {
                float tspeedus = 0.0f;
                int radarus = 0;
                int radarenemy = 0;
                double player_aircontrolval = 0.0f;
                double enemy_aircontrolval = 0.0f;

                var ep = battleResult.warReport;

                foreach (int i in uf.ships)
                {
                    var zus = GameData.instance.GetShipById(i);
                    tspeedus += zus.battleProps.speed;
                    radarus += zus.battleProps.radar;

                    for (int index = 0; index < zus.equipmentArr.Length; index++)
                    {
                        if (zus.capacitySlotMax[index] <= 0)
                        {
                            continue;
                        }
                        var eq = zus.equipmentArr[index].config;
                        if (eq != null && eq.type == EquipmentType.FightPlane)
                        {
                            player_aircontrolval += Math.Sqrt((double)zus.capacitySlotMax[index]) * (double)eq.airDef;
                        }
                    }
                }
                if (ep != null && ep.enemyShips != null)
                {
                    foreach (var ess in ep.enemyShips)
                    {
                        radarenemy += ess.radar;
                        if (ess.equipment == null)
                        {
                            continue;
                        }
                        for (int index = 0; index < ess.equipment.Length; index++)
                        {
                            if (ess.capacitySlotMax[index] <= 0)
                            {
                                continue;
                            }
                            var eq = ess.equipment[index].config;
                            if (eq != null && eq.type == EquipmentType.FightPlane)
                            {
                                enemy_aircontrolval += Math.Sqrt((double)ess.capacitySlotMax[index]) * (double)eq.airDef;
                            }
                        }
                    }
                }

                r += "\t 索敌 " + (ep.isExploreSuccess == 1 ? "成功，发现敌军舰队" : "失败。未能发现敌军")
                    + "  我方索敌值：" + radarus + "     敌军索敌值:" + radarenemy
                    + "\r\n";
                r += "\t " + uf.title + " 对战 敌 - " + ep.enemyName + "\r\n";
                r += "\t 战斗阵型： " + getWarTypeString(ep.warType) + "\r\n";
                r += "\t 制空权结果： " + getAirControlType(ep.airControlType)
                    + "  我方制空值：" + (int)player_aircontrolval + "     敌军制空值:" + (int)enemy_aircontrolval
                    + "\r\n";

                if (ep.openAirAttack != null && ep.openAirAttack.Length > 0)
                {
                    r += " 开幕空袭：\r\n";
                    r += getDetailWarBattleParam(ep.openAirAttack, uf, ep.enemyShips);

                }

                if (ep.openAntiSubAttack != null && ep.openAntiSubAttack.Length > 0)
                {
                    r += " 开幕反潜：\r\n";
                    r += getDetailWarBattleParam(ep.openAntiSubAttack, uf, ep.enemyShips);

                }


                if (ep.openTorpedoAttack != null && ep.openTorpedoAttack.Length > 0)
                {
                    r += " 开幕雷击：\r\n";
                    r += getDetailWarBattleParam(ep.openTorpedoAttack, uf, ep.enemyShips);

                }

                if (ep.normalAttacks != null && ep.normalAttacks.Length > 0)
                {
                    r += " 炮击战：\r\n";
                    r += getDetailWarBattleParam(ep.normalAttacks, uf, ep.enemyShips);

                }

                if (ep.closeTorpedoAttack != null && ep.closeTorpedoAttack.Length > 0)
                {
                    r += " 闭幕雷击战：\r\n";
                    r += getDetailWarBattleParam(ep.closeTorpedoAttack, uf, ep.enemyShips);

                }

                int enemycount = 0;
                int j = 0;
                r += "\r\n";
                if (battleResult.warReport.selfShips != null && battleResult.warReport.selfShips.Length > 0)
                {
                    foreach (var v in battleResult.warReport.selfShips)
                    {
                        UserShip us = GameData.instance.GetShipById(uf.ships[j]);
                        r += us.ship.title + " Lv." + us.level 
                          +"\t" + v.hp + "/" + us.battlePropsMax.hp
                            
                            + " \r\n";
                        j++;
                    }
                }
                r += "\r\n敌方:\r\n";
                if (battleResult.warReport.enemyShips != null && battleResult.warReport.enemyShips.Length > 0
                    && CurrentWarParameters.selectedOpponent != null
                    )
                {
                    var p = CurrentWarParameters.selectedOpponent;
                    j = 0;
                    foreach (var v in battleResult.warReport.enemyShips)
                    {
                        UserShip us = p.ships[j];
                        r += getshiptype(us.ship.type) + " " + us.ship.title + " Lv." + us.level
                            + " HP:" + v.hp + "/" + us.ship.hp + " \r\n";
                        j++;
                    }
                }
            }

            return r;
        }

        public static string getFormatedtitle(string title)
        {
            int l = title.Length;
            int c = l / 4;
            string r = title;
            for(int i =c;i<1;i++)
            {
                r += "\t";
            }
            return r;
        }
        public static string getAirControlType(AirControlType at)
        {
            
            switch(at)
            {
                case AirControlType.FullControl:
                    return "占据制空权";
                case AirControlType.Advance:
                    return "制空权优势";
                case AirControlType.Disadvace:
                    return "制空权劣势";
                case AirControlType.LoseControl:
                    return "丧失制空权";
                default:
                case AirControlType.Equal:
                    return "势均力敌	";


            }

        }

         public static string  getWarTypeString(WarTypes warType)
        {
            switch (warType)
            {
                case WarTypes.SameWay:
                    return "同航战";
                case WarTypes.OppositeWay:
                    return "反航战";
                case WarTypes.TAdvance:
                    return "T字战 优势";
                default:
                case WarTypes.TDisadvance:
                    return "T字战 劣势";
            }
        }

         public static string getShipAttackTypeString(ShipAttackTypes warType)
         {
             switch (warType)
             {
                 case ShipAttackTypes.Gun:
                     return "炮击";
                 case ShipAttackTypes.AirGun:
                     return "航弹";
                 case ShipAttackTypes.AirTorpedo:
                     return "航空鱼雷";
                 case ShipAttackTypes.Torpedo:
                     return "鱼雷";
                 case ShipAttackTypes.AntiSubmarine:
                     return "反潜";
                 default:
                 case ShipAttackTypes.None:
                     return "无";
             }
         }


         public static string getQuestRewardStr(UserQuest q)
         {
             string r = "";
             ResourceTypes[] typesArray = new ResourceTypes[] { ResourceTypes.Oil, ResourceTypes.Ammo, ResourceTypes.Steel, ResourceTypes.Aluminium };
             for (int i = 0; i < 4 && i < q.award.Count; i++)
             {
                 string key = ((int)typesArray[i]) + string.Empty;
                 if (q.award.ContainsKey(key))
                 {
                     r += tools.helper.getresourcetype(typesArray[i]) + ":" + q.award[key] + " ";
                 }
             }
             r += "\r\n";
             if (q.AwardShipId > 0)
             {
                 ShipConfig config = AllShipConfigs.instance.getShip(q.AwardShipId);
                 if (config != null)
                 {
                     r += tools.helper.getshiptype(config.type) + ":" + config.title;
                 }
             }
             else
             {
                 if (q.AwardItemId > 0)
                 {
                     r += tools.helper.getItemtypestr(q.AwardItemId) + " X" + q.GetItemAmount(q.AwardItemId);


                 }
                 else
                 {
                     if (q.AwardEquipId > 0)
                     {
                         EquipmentConfig ec = GameConfigs.instance.GetEquipmentByCid(q.AwardEquipId);
                         if (ec != null)
                         {
                             r += tools.helper.getEquipmentTypeString(ec.type) + ":" + ec.title;
                         }
                     }
                 }
             }
             return r;
         }

         public static string getQuestConStr(UserQuest q)
         {
             string r = "";
             int num1 = 0;
             int num2 = 0;
             foreach (var c in q.condition)
             {
                 num1 += c.totalAmount;
                 num2 += c.finishedAmount;
             }
             r += "" + num2 + "/" + num1;
             return r;
         }

         public static string getQuestTypeStr(QuestType questType)
         {
             switch (questType)
             {
                 case QuestType.Daily:
                     return "每日";
                 case QuestType.Weekly:
                     return "每周";
                 case QuestType.Main:
                     return "主线";
                 case QuestType.All:
                 default:
                     return "普通";
             }
         }

    }
}
