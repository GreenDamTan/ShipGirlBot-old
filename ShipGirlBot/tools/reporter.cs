using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tools
{
    class reporter
    {

        public static void reportGotShip(GetBattleResultResponse battleResult, int battle_fleetid, string level, string nodeflag)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                do_reportGotShip(battleResult, battle_fleetid, level, nodeflag);
            }, null);
        }
        private  static void do_reportGotShip(GetBattleResultResponse battleResult, int battle_fleetid, string level, string nodeflag)
        {
            if(battleResult == null || battleResult.newShipVO == null || battleResult.newShipVO.Length ==0)
            {
                return;
            }
            var dic = new Dictionary<string, string>();
            string desc = "";
            UserShip flagship = GameData.instance.GetShipById(GameData.instance.UserFleets[battle_fleetid -1].ships[0]);
            foreach(UserShip us in battleResult.newShipVO)
            {
                desc += level + "|" + nodeflag + "|" + us.ship.cid + "|" + us.ship.title + "|" + us.ship.star
                    + "|" + ServerTimer.GetNowServerTime()
                    + "|" + z.instance.getServerName()
                    + "|" + flagship.level + "|" + flagship.ship.cid + "|" + flagship.ship.luck + "|" + flagship.ship.star + "|" + flagship.ship.title
                    + "|" + (WarResultLevel)battleResult.warResult.resultLevel
                    + "|" + battleResult.bossHpLeft
                    + "|" + GameData.instance.UserInfo.detailInfo.collection
                    + "|" + GameData.instance.UserInfo.level
                    + "\r\n";
            }
            dic["msg"] = desc;
            var c = new System.Net.Http.FormUrlEncodedContent(dic);

            try
            {
                var p = new System.Net.Http.HttpClient();
                var r = p.PostAsync(tools.helper.count_server_addr + "/sssgbsssgb/reportdrop", c).Result;
            }
            catch (Exception)
            {

            }
        }

        internal static void reportBuildShip(int dockid, BSLOG bSLOG, GetShipData getShipData)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                do_reportBuildShip(dockid, bSLOG, getShipData);
            }, null);
        }
        private static void do_reportBuildShip(int dockid, BSLOG bSLOG, GetShipData getShipData)
        {
            if (getShipData == null || getShipData.shipVO == null )
            {
                return;
            }
            var dic = new Dictionary<string, string>();
            string desc = "";
            UserShip flagship = GameData.instance.GetShipById(GameData.instance.UserFleets[0].ships[0]);
            UserShip us = getShipData.shipVO;
            {
                desc += bSLOG.oil.ToString() + "|" +
                    bSLOG.ammo.ToString() + "|" +
                    bSLOG.steel.ToString() + "|" +
                    bSLOG.al.ToString() + "|" +
                    bSLOG.timetick.ToString() + "|" +
                    bSLOG.buildreturntype.ToString()  +
                    
                    "|" + us.ship.cid + "|" + us.ship.title + "|" + us.ship.star
                    + "|" + ServerTimer.GetNowServerTime()
                    + "|" + z.instance.getServerName()
                    + "|" + flagship.level + "|" + flagship.ship.cid + "|" + flagship.ship.luck + "|" + flagship.ship.star + "|" + flagship.ship.title
                    + "|" + GameData.instance.UserInfo.detailInfo.collection
                    + "|" + GameData.instance.UserInfo.level
                    + "\r\n";
            }
            dic["msg"] = desc;
            var c = new System.Net.Http.FormUrlEncodedContent(dic);

            try
            {
                var p = new System.Net.Http.HttpClient();
                var r = p.PostAsync(tools.helper.count_server_addr + "/sssgbsssgb/reportbuild", c).Result;
            }
            catch (Exception)
            {

            }
        }

        internal static void reportBuildWeapon(int dockid, BSLOG bSLOG, GetEquipData getEquipData)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                do_reportBuildWeapon(dockid, bSLOG, getEquipData);
            }, null);
        }
        private static void do_reportBuildWeapon(int dockid, BSLOG bSLOG, GetEquipData getEquipData)
        {
            if (getEquipData == null || getEquipData.equipmentVo == null)
            {
                return;
            }
            var dic = new Dictionary<string, string>();
            string desc = "";
            UserShip flagship = GameData.instance.GetShipById(GameData.instance.UserFleets[0].ships[0]);
            UserEquipment us = getEquipData.equipmentVo;
            {
                desc += bSLOG.oil.ToString() + "|" +
                    bSLOG.ammo.ToString() + "|" +
                    bSLOG.steel.ToString() + "|" +
                    bSLOG.al.ToString() + "|" +
                    bSLOG.timetick.ToString() + "|" +
                    bSLOG.buildreturntype.ToString() +

                    "|" + us.config.cid + "|" + us.config.title + "|" + us.config.star
                    + "|" + ServerTimer.GetNowServerTime()
                    + "|" + z.instance.getServerName()
                    + "|" + flagship.level + "|" + flagship.ship.cid + "|" + flagship.ship.luck + "|" + flagship.ship.star + "|" + flagship.ship.title
                    + "|" + GameData.instance.UserInfo.username
                    + "|" + GameData.instance.UserInfo.level
                    + "\r\n";
            }
            dic["msg"] = desc;
            var c = new System.Net.Http.FormUrlEncodedContent(dic);

            try
            {
                var p = new System.Net.Http.HttpClient();
                var r = p.PostAsync(tools.helper.count_server_addr + "/sssgbsssgb/reportbuildweapon", c).Result;
            }
            catch (Exception)
            {

            }
        }
    }
}
