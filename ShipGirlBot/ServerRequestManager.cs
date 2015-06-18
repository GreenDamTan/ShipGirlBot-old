using ShipGirlBot;
using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

public class ServerRequestManager : MonoBehaviour
{
    private bool hasSendInitDataRequest = false;
    private bool loadConfigComplete = false;
    private bool loadConfigError = false;
    private bool loginSuccess = false;
    private static ServerRequestManager mInstance;
    public Action<int> OnChooseInitShipFailAction;
    public Action<int> OnInitFailAction;
    public Action OnInitSuccessAction;
    public Action OnStartChooseInitShipAction;

    public ReqSignin CheckIfHaveNewVerison()
    {
        ReqSignin signin = new ReqSignin();
        signin.CheckIfHaveNewVerison();
        return signin;
    }

    private void CheckLoginAndConfig()
    {
        if ((!this.hasSendInitDataRequest && this.loginSuccess) && this.loadConfigComplete)
        {
            if (this.loadConfigError)
            {
                this.OnLoginFail(0);
            }
            else
            {
                this.hasSendInitDataRequest = true;
                this.GetInitData();
            }
        }
    }

    public void GetInitData()
    {
        new GetInitData().Start();
    }

    public void LoadConfigs()
    {
        this.loadConfigError = false;
        new LoadConfigs().Start();
    }

    public void Login(string userId)
    {
        new ReqLogin().LoginUseId(userId);
    }

    public void OnGetInitDataSuccess()
    {
        z.instance.OnInitDataSuccess();
        if (this.OnInitSuccessAction != null)
        {
            this.OnInitSuccessAction.Invoke();
        }
        GetNewMarketingStatus();
        ServerRequestManager.instance.GetPVEConfigs();
        ServerRequestManager.instance.GetPVEData();
    }

    public void OnLoadConfigsComplete()
    {
        this.loadConfigComplete = true;
        ReqSignin signin = instance.SignIn(z.instance.get_username(), z.instance.get_password());
    }

    public void OnLoadConfigsError(string err)
    {
        this.loadConfigComplete = false;
        //this.loadConfigError = true;
        //this.CheckLoginAndConfig();
        z.instance.loginfailed(err);
    }

    public void OnLoginFail(int eid = 0)
    {
        z.instance.loginfailed("");
    }

    public void OnLoginSuccess()
    {
        if (GameData.instance.LoginInfo.hadRole == 0)
        {
            z.log("没有角色...先用客户端创建人物");
        }
        else
        {
            string s = "";
            foreach (var ss in GameData.instance.LoginInfo.noticeList)
            {
                s += ss+"\r\n";
            }
            z.log("[登陆成功]\r\n"+ s );
            this.loginSuccess = true;
            this.CheckLoginAndConfig();
        }
    }

    public ReqSignin SignIn(string userId, string password)
    {
        ReqSignin signin = new ReqSignin();
        signin.SignIn(userId, password);
        return signin;
    }

    public static ServerRequestManager instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new ServerRequestManager();
            }
            return mInstance;
        }
    }



#region  PVEExplorer
    public ReqPVEExplore StartPVEExplore(int fleetId, int exploreId)
    {
        ReqPVEExplore component = new ReqPVEExplore();
        component.StartExplore(fleetId, exploreId);
        return component;
    }

    public ReqPVEExplore CancelPVEExplore(int exploreId)
    {
        ReqPVEExplore component = new ReqPVEExplore();
        component.CancelExplore(exploreId);
        return component;
    }

    public ReqPVEExplore FinishPVEExplore(int exploreId)
    {
        ReqPVEExplore component = new ReqPVEExplore();
        component.FinishExplore(exploreId);
        return component;
    }



#endregion PVEExplorer

    #region CHANGEBOAT
    public FleetShipChangeRequest ChangeFleetName(int fleetId, string name)
    {
        FleetShipChangeRequest component = new FleetShipChangeRequest();
        component.ChangeFleetName(fleetId, name);
        return component;
    }

    public FleetShipChangeRequest ChangeFleetShip(int fleetId, int shipId, int index)
    {
        FleetShipChangeRequest component = new FleetShipChangeRequest();
        component.ChangeShip(fleetId, shipId, index);
        return component;
    }

    public FleetShipChangeRequest ToggleShipLock(UserShip userShip)
    {
        FleetShipChangeRequest component = new FleetShipChangeRequest();
        component.ToggleLockStatus(userShip);
        return component;
    }

    #endregion CHANGEBOAT

    #region PVE

    public GetPVEData StartPVELevel(int levelId, int fleetId, FleetFormation formationType)
    {
        GetPVEData component = new GetPVEData();
        component.StartLevel(levelId, fleetId, formationType);
        return component;
    }

    public GetPVEData GetNextPVELevelNode()
    {
        GetPVEData component = new GetPVEData();
        component.GetNextNode();
        return component;
    }

    public GetPVEData DealPVELevelNode(int nodeId, int fleetId, FleetFormation formationType)
    {
        UserFleet uf = GameData.instance.GetFleetOfId(fleetId);
        PVENode pl = PVEConfigs.instance.GetNode(nodeId);
        ShipGirlBot.z.log("[少女卖肉开始]...." + uf.title + " @ " + pl.flag + "点 ");

        GetPVEData component = new GetPVEData();
        component.DealNode(nodeId, fleetId, formationType);
        return component;
    }

    public GetPVEData GetPVEBattleResult(bool isContinueNightWar, int fleetid)
    {
        GetPVEData component = new GetPVEData();
        if (isContinueNightWar)
        {
            ShipGirlBot.z.log("[少女夜晚继续卖肉开始]....");
        }
        component.GetBattleResult(isContinueNightWar, fleetid);
        return component;
    }

    public GetPVEData NotifyPVEBackHome()
    {
        GetPVEData component = new GetPVEData();
        component.NotifyPVEBackHome();
        return component;
    }


    public GetPVEConfigs GetPVEConfigs()
    {
        GetPVEConfigs component = new GetPVEConfigs();
        component.GetConfigs();
        return component;
    }

    public GetPVEData GetPVEData()
    {
        GetPVEData component = new GetPVEData();
        component.GetData();
        return component;
    }
    public GetPVEConfigs GetPVEEventConfigs()
    {
        GetPVEConfigs component = new GetPVEConfigs();
        component.GetEventConfigs();
        return component;
    }

    public GetPVEData GetPVEEventData()
    {
        GetPVEData component = new GetPVEData();
        component.GetEventData();
        return component;
    }
    #endregion PVE


#region MARKETING
    public ReqMarketing GetNewMarketingStatus()
    {
        ReqMarketing component = new ReqMarketing();
        component.GetNewStatus();
        return component;
    }
#endregion
    internal void refreashUIData()
    {
        z.instance.refreshUIData();
    }

#region SUPPLY
    public ReqSupply SupplyFleet(int fleetId)
    {
        ReqSupply component = new ReqSupply();
        component.SupplyFleet(fleetId, 0);
        return component;
    }
#endregion
    internal void trySupplyFleet(int battle_fleetid)
    {
        UserFleet uf = GameData.instance.GetFleetOfId(battle_fleetid);
        bool needsupply = false;
        if (uf != null)
        {
            foreach(var s in uf.ships)
            {
                UserShip us = GameData.instance.GetShipById(s);
                if(us.NeedSupplyAmount > 1)
                {
                    needsupply = true;
                }
            }
        }

        if(needsupply == true)
        {
            ReqSupply component = new ReqSupply();
            component.SupplyOneFleetSuccess += new EventHandler<EventArgs>(this.ontrySupplyFleetSuccess);
            component.SupplyOneFleetFail += new EventHandler<EventArgs>(this.ontrySupplyFleetFail);
            component.SupplyFleet(battle_fleetid, 0);
        }
        else
        {
            z.instance.setBattleStatus(BATTLE_STATUS.SUPPLY);
        }
    }

    public void ontrySupplyFleetFail(object o ,EventArgs args)
    {
        ShipGirlBot.z.instance.setBattleStatus(ShipGirlBot.BATTLE_STATUS.NONE);
        ShipGirlBot.z.log("[战斗前补给失败]....");
    }

    public void ontrySupplyFleetSuccess(object o, EventArgs args)
    {
        ShipGirlBot.z.instance.setBattleStatus(ShipGirlBot.BATTLE_STATUS.SUPPLY);
        ShipGirlBot.z.log("[战斗前补给成功] 少女吃喝完毕....");
    }

#region REPAIR
    public RepairRequest InstantRepair(int shipid, int useGold)
    {
        RepairRequest component = new RepairRequest();
        component.InstantRepairShip(shipid, useGold);
        return component;
    }

    public RepairRequest StartRepair(int shipid, int dockId)
    {
        RepairRequest component = new RepairRequest();
        component.StartRepairShip(shipid, dockId);
        return component;
    }

    public RepairRequest FinishRepair(int shipid, int dockId)
    {
        RepairRequest component = new RepairRequest();
        component.GetRepairedShip(shipid, dockId);
        return component;
    }

#endregion

#region BUILD
    public FactoryRequest GetShipBuildLog()
    {
        FactoryRequest component = new FactoryRequest();
        component.GetBuildLog();
        return component;
    }

    public FactoryRequest BuildShipInDock(int dockId, int oil, int steel, int ammo, int alum)
    {
        FactoryRequest component = new FactoryRequest();
        component.BuildShip(dockId, oil, steel, ammo, alum);
        return component;
    }

    public FactoryRequest FastBuildShipAtDock(int dockId)
    {
        FactoryRequest component = new FactoryRequest();
        component.FastBuild(dockId);
        return component;
    }

    public FactoryRequest GetShipFromDock(int dockId)
    {
        FactoryRequest component = new FactoryRequest();
        component.GetShip(dockId);
        return component;
    }
    public FactoryRequest ToggleShipBuildLogFav(BuildLogVO log)
    {
        FactoryRequest component = new FactoryRequest();
        component.SetFavLog(log);
        return component;
    }

    public FactoryOfEquipRequest GetEquipBuildLog()
    {
        FactoryOfEquipRequest component = new FactoryOfEquipRequest();
        component.GetBuildLog();
        return component;
    }
    public FactoryOfEquipRequest BuildEquipInDock(int dockId, int oil, int steel, int ammo, int alum)
    {
        FactoryOfEquipRequest component = new FactoryOfEquipRequest();
        component.BuildEquip(dockId, oil, steel, ammo, alum);
        return component;
    }
    public FactoryOfEquipRequest FastBuildEquipAtDock(int dockId)
    {
        FactoryOfEquipRequest component = new FactoryOfEquipRequest();
        component.FastBuild(dockId);
        return component;
    }

    public FactoryOfEquipRequest GetEquipFromDock(int dockId)
    {
        FactoryOfEquipRequest component = new FactoryOfEquipRequest();
        component.GetEquip(dockId);
        return component;
    }

    public FactoryOfEquipRequest ToggleEquipBuildLogFav(BuildLogVO log)
    {
        FactoryOfEquipRequest component = new FactoryOfEquipRequest();
        component.SetFavLog(log);
        return component;
    }
#endregion


#region DESTROY
    public ReqDestroyEquip DestroyEquips(List<UserEquipment> equips)
    {
        ReqDestroyEquip component = new ReqDestroyEquip();
        component.DestroyeEquip(equips);
        return component;
    }

    public ReqDestroyShip DestroyShips(List<UserShip> ships, int saveEqups)
    {
        ReqDestroyShip component = new ReqDestroyShip();
        component.DestroyeShip(ships, saveEqups);
        return component;
    }
#endregion

#region QUEST

    public ReqQuest GetQuests()
    {
        ReqQuest component = new ReqQuest();
        component.GetQuest();
        return component;
    }

    public ReqQuest FinishQuest(UserQuest quest)
    {
        ReqQuest component = new ReqQuest();
        component.FinishQuest(quest);
        return component;
    }
#endregion
}

