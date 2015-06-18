using JsonFx.Json;
using tools;
using System;

public class GetInitData : BaseWWWRequest
{
    private InitDataVO initData;
    public bool needZip;

    private void onFail(BaseWWWRequest obj)
    {
        if (this.initData != null)
        {
            ServerRequestManager.instance.OnLoginFail(this.initData.eid);
        }
        else
        {
            ServerRequestManager.instance.OnLoginFail(0);
        }
        base.ShowServerError();
        z.log("[获取初始配置失败]: " + base.www.error + "\r\n" + this.UTF8String);
    }

    private void onSuccess(BaseWWWRequest obj)
    {
        try
        {
            if (this.needZip)
            {
                this.initData = new JsonFx.Json.JsonReader().Read<InitDataVO>(base.UTF8String);
            }
            else
            {
                this.initData = new JsonFx.Json.JsonReader().Read<InitDataVO>(this.UTF8String);
            }
            if (this.initData.eid != 0)
            {
                this.onFail(obj);
            }
            else
            {
                GameData instance = GameData.instance;
                ServerTimer.SetSystemInitTime(this.initData.systime);
                instance.UserInfo = this.initData.userVo;
                instance.UserFleets = this.initData.fleetVo;
                instance.SetUserShips(this.initData.userShipVO);
                instance.SetUserEquipments(this.initData.equipmentVo);
                instance.UserDocks = this.initData.dockVo;
                instance.UserEquipDocks = this.initData.equipmentDockVo;
                instance.UpdateUserItems(this.initData.packageVo);
                instance.SetRepairDocks(this.initData.repairDockVo);
                instance.UpdatePVEExplore(this.initData.pveExploreVo);
                instance.AddUserQuests(this.initData.taskVo);
                instance.SetUnlockedCards(this.initData.unlockShip);
                instance.MarketingDatas = this.initData.marketingData;
                instance.SetLastUpdateQuestTime();
                instance.NewMailNum = this.initData.newMailNum;
                ServerRequestManager.instance.OnGetInitDataSuccess();
                z.log("[登陆成功] 获取基本信息成功");
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onFail(obj);
        }
    }

    public override void Start()
    {
        string deviceUniqueIdentifier = configmng.deviceUniqueIdentifier;
        if (string.IsNullOrEmpty(base.path))
        {
            base.path = "api/initData/" + deviceUniqueIdentifier + "/";
        }
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onSuccess), new BaseWWWRequest.OnFail(this.onFail), true, ServerType.ChoosedServer, this.needZip);
    }
}

