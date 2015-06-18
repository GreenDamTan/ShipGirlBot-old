using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;

public class GetPVEData : BaseWWWRequest
{
    private int curfleetid = 0;
    private GetBattleResultResponse battleResult;
    private int dealingNodeId;
    private GetDealNodeResponse dealNodeResponse;
    private bool isContinueNightWar;
    private GetNextNodeResponse nextNodeResponse;
    private SearchEnemyResponse searchNodeResponse;
    private SkipWarResponse skipWarResponse;
    private GetStartLevelResponse startLevelResponse;

    public event EventHandler<EventArgs> GetBattleResultFail;

    public event EventHandler<EventArgs> GetBattleResultSuccess;

    public event EventHandler<EventArgs> GetDataFail;

    public event EventHandler<EventArgs> GetDataSuccess;

    public event EventHandler<EventArgs> GetDealNodeFail;

    public event EventHandler<EventArgs> GetDealNodeSuccess;

    public event EventHandler<EventArgs> GetEventDataFail;

    public event EventHandler<EventArgs> GetEventDataSuccess;

    public event EventHandler<EventArgs> GetNextNodeFail;

    public event EventHandler<EventArgs> GetNextNodeSuccess;

    public event EventHandler<EventArgs> GetSearchNodeFail;

    public event EventHandler<EventArgs> GetSearchNodeSuccess;

    public event EventHandler<EventArgs> GetSkipWarResultFail;

    public event EventHandler<EventArgs> GetSkipWarResultSuccess;

    public event EventHandler<EventArgs> GetStartLevelFail;

    public event EventHandler<EventArgs> GetStartLevelSuccess;

    private void CheckUserNewExpInfo(UserExpAndLevelVO levelVo)
    {
        if (levelVo != null)
         {
            GameData.instance.UserInfo.ApplyNewExpInfo(levelVo);
         }
    }

    public void DealNode(int nodeId, int fleetId, FleetFormation formationType)
    {
        this.dealingNodeId = nodeId;
        object[] objArray1 = new object[] { "pve/deal/", nodeId, "/", fleetId, "/", (int) formationType };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqDealNodeSuccess), new BaseWWWRequest.OnFail(this.onReqDealNodeFail), true, ServerType.ChoosedServer, false);
    }

    public void GetBattleResult(bool isContinueNightWar , int fleetid)
    {
        this.curfleetid = fleetid;
        this.isContinueNightWar = isContinueNightWar;
        int num = 0;
        if (isContinueNightWar)
        {
            num = 1;
        }
        base.path = "pve/getWarResult/" + num;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqGetBattleResultSuccess), new BaseWWWRequest.OnFail(this.onReqGetBattleResultFail), true, ServerType.ChoosedServer, false);
    }

    public void GetData()
    {
        base.path = "pve/getUserData/";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqGetDataSuccess), new BaseWWWRequest.OnFail(this.onReqGetDataFail), true, ServerType.ChoosedServer, false);
    }

    public void GetEventData()
    {
        base.path = "pevent/getUserData/";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqGetEventDataSuccess), new BaseWWWRequest.OnFail(this.onReqGetEventDataFail), true, ServerType.ChoosedServer, false);
    }

    public void GetNextNode()
    {
        base.path = "pve/next/";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqNextNodeSuccess), new BaseWWWRequest.OnFail(this.onReqNextNodeFail), true, ServerType.ChoosedServer, false);
    }

    private void HandleNewPveData(GetPVEDataResponse res)
    {
        if (res != null)
        {
            GameData instance = GameData.instance;
            if (res.currentVo != null)
            {
                instance.CurrentPVEChapterId = res.currentVo.pveId;
            }
            if (res.pveLevel != null)
            {
                instance.SetPassedPVELevels(res.pveLevel);
            }
            if (res.currentVo != null)
            {
                instance.SetCurrentNodeStatus(res.currentVo);
            }
            if (res.pveEventLevel != null)
            {
                instance.SetPassedPVEEventLevels(res.pveEventLevel);
                foreach (UserPVEEventLevel level in res.pveEventLevel)
                {
                    if (level.shipVO != null)
                    {
                        foreach (UserShip ship in level.shipVO)
                        {
                            GameData.instance.AddUserShip(ship);
                        }
                    }
                    if (level.equipmentVo != null)
                    {
                        foreach (UserEquipment equipment in level.equipmentVo)
                        {
                            GameData.instance.AddUserEquipmenet(equipment);
                        }
                    }
                }
            }
        }
    }

    public void NotifyPVEBackHome()
    {
        z.instance.setBattleStatus(BATTLE_STATUS.FINISHED);
        z.log("[少女&新少女 归港成功]....");
        base.path = "pve/pveEnd/";
        base.SetupParams(null, null, null, true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnGetBattleResultFail(EventArgs e)
    {
        if (this.GetBattleResultFail != null)
        {
            this.GetBattleResultFail(this.battleResult, e);
        }
    }

    protected virtual void OnGetBattleResultSuccess(EventArgs e)
    {
        if (this.GetBattleResultSuccess != null)
        {
            this.GetBattleResultSuccess(this.battleResult, e);
        }
    }

    protected virtual void OnGetDataFail(EventArgs e)
    {
        if (this.GetDataFail != null)
        {
            this.GetDataFail(this, e);
        }
    }

    protected virtual void OnGetDataSuccess(EventArgs e)
    {
        if (this.GetDataSuccess != null)
        {
            this.GetDataSuccess(this, e);
        }
    }

    protected virtual void OnGetDealNodeFail(EventArgs e)
    {
        if (this.GetDealNodeFail != null)
        {
            this.GetDealNodeFail(this.dealNodeResponse, e);
        }
    }

    protected virtual void OnGetDealNodeSuccess(EventArgs e)
    {
        if (this.GetDealNodeSuccess != null)
        {
            this.GetDealNodeSuccess(this.dealNodeResponse, e);
        }
    }

    protected virtual void OnGetEventDataFail(EventArgs e)
    {
        if (this.GetEventDataFail != null)
        {
            this.GetEventDataFail(this, e);
        }
    }

    protected virtual void OnGetEventDataSuccess(EventArgs e)
    {
        if (this.GetEventDataSuccess != null)
        {
            this.GetEventDataSuccess(this, e);
        }
    }

    protected virtual void OnGetNextNodeFail(EventArgs e)
    {
        if (this.GetNextNodeFail != null)
        {
            this.GetNextNodeFail(this, e);
        }
    }

    protected virtual void OnGetNextNodeSuccess(EventArgs e)
    {
        if (this.GetNextNodeSuccess != null)
        {
            this.GetNextNodeSuccess(this.nextNodeResponse, e);
        }
    }

    protected virtual void OnGetSearchNodeFail(EventArgs e)
    {
        if (this.GetSearchNodeFail != null)
        {
            this.GetSearchNodeFail(this.searchNodeResponse, e);
        }
    }

    protected virtual void OnGetSearchNodeSuccess(EventArgs e)
    {
        if (this.GetSearchNodeSuccess != null)
        {
            this.GetSearchNodeSuccess(this.searchNodeResponse, e);
        }
    }

    protected virtual void OnGetSkipWarResultFail(EventArgs e)
    {
        if (this.GetSkipWarResultFail != null)
        {
            this.GetSkipWarResultFail(this.skipWarResponse, e);
        }
    }

    protected virtual void OnGetSkipWarResultSuccess(EventArgs e)
    {
        if (this.GetSkipWarResultSuccess != null)
        {
            this.GetSkipWarResultSuccess(this.skipWarResponse, e);
        }
    }

    protected virtual void OnGetStartLevelFail(EventArgs e)
    {
        if (this.GetStartLevelFail != null)
        {
            this.GetStartLevelFail(this.startLevelResponse, e);
        }
    }

    protected virtual void OnGetStartLevelSuccess(EventArgs e)
    {
        if (this.GetStartLevelSuccess != null)
        {
            this.GetStartLevelSuccess(this, e);
        }
    }

    private void onReqDealNodeFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetDealNodeFail(EventArgs.Empty);
    }

    private void onReqDealNodeSuccess(BaseWWWRequest obj)
    {
        try
        {
            GameData instance = GameData.instance;
            this.dealNodeResponse = new JsonFx.Json.JsonReader().Read<GetDealNodeResponse>(this.UTF8String);
            base.responseData = this.dealNodeResponse;
            if (this.dealNodeResponse.eid != 0)
            {
                this.onReqDealNodeFail(obj);
            }
            else
            {
                instance.AddPassedNodeId(this.dealingNodeId);
                if ((this.dealNodeResponse.newShipVO != null) && (this.dealNodeResponse.newShipVO.Length > 0))
                {
                    foreach (UserShip ship in this.dealNodeResponse.newShipVO)
                    {
                        instance.AddUserShip(ship);
                    }
                }
                if ((this.dealNodeResponse.equipmentVo != null) && (this.dealNodeResponse.equipmentVo.Length > 0))
                {
                    foreach (UserEquipment equipment in this.dealNodeResponse.equipmentVo)
                    {
                        instance.AddUserEquipmenet(equipment);
                    }
                }
                if ((this.dealNodeResponse.packageVo != null) && (this.dealNodeResponse.packageVo.Length > 0))
                {
                    instance.UpdateUserItems(this.dealNodeResponse.packageVo);
                }
                if (this.dealNodeResponse.userResVO != null)
                {
                    instance.UserInfo.UpdateResource(this.dealNodeResponse.userResVO);
                }
                this.HandleNewPveData(this.dealNodeResponse.newPveData);
                this.OnGetDealNodeSuccess(EventArgs.Empty);
                z.instance.setBattleStatus(BATTLE_STATUS.NODE_BATTLE);
                z.log("[少女卖肉成功].... 等待战(tiao)斗(jiao)过程 ...");
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqDealNodeFail(obj);
        }
    }

    private void onReqGetBattleResultFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetBattleResultFail(EventArgs.Empty);
    }

    private void onReqGetBattleResultSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.battleResult = new JsonFx.Json.JsonReader().Read<GetBattleResultResponse>(this.UTF8String);
            base.responseData = this.battleResult;
            if (this.battleResult.eid != 0)
            {
                this.onReqGetBattleResultFail(obj);
            }
            else
            {
                if (this.battleResult.shipVO != null)
                {
                    foreach (UserShip ship in this.battleResult.shipVO)
                    {
                        GameData.instance.UpdateUserShip(ship);
                    }
                }
                if ((this.battleResult.newShipVO != null) && (this.battleResult.newShipVO.Length > 0))
                {
                    CurrentWarParameters.newShipsGotFromWar = this.battleResult.newShipVO;
                    foreach (UserShip ship2 in this.battleResult.newShipVO)
                    {
                        GameData.instance.AddUserShip(ship2);
                    }
                }
                if (this.battleResult.detailInfo != null)
                {
                    GameData.instance.UserInfo.UpdateDetailInfo(this.battleResult.detailInfo);
                }
                if (this.battleResult.campaignVo != null)
                {
                    if (this.battleResult.campaignVo.canCampaignChallengeLevel != null)
                    {
                        GameData.instance.SetOpenedPVECampaignLevels(this.battleResult.campaignVo.canCampaignChallengeLevel);
                    }
                    if (this.battleResult.campaignVo.campaignChallenge != null)
                    {
                        GameData.instance.SetCampaignChapterTimesInfo(this.battleResult.campaignVo.campaignChallenge);
                    }
                    if (this.battleResult.campaignVo.passInfo != null)
                    {
                        GameData.instance.TotalCampainInfo = this.battleResult.campaignVo.passInfo;
                    }
                }
                if (this.battleResult.warResult != null)
                {
                    this.CheckUserNewExpInfo(this.battleResult.warResult.userLevelVo);
                }
                this.HandleNewPveData(this.battleResult.newPveData);
                this.OnGetBattleResultSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqGetBattleResultFail(obj);
        }
    }

    private void onReqGetDataFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetDataFail(EventArgs.Empty);
        z.log("[刷新PVE关卡数据]失败..."+ obj.UTF8String);
    }

    private void onReqGetDataSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetPVEDataResponse res = new JsonFx.Json.JsonReader().Read<GetPVEDataResponse>(this.UTF8String);
            base.responseData = res;
            if (res.eid != 0)
            {
                this.onReqGetDataFail(obj);
            }
            else
            {
                this.HandleNewPveData(res);
                GameData.instance.SetPassedNodeIds(res.passedNodes);
                this.OnGetDataSuccess(EventArgs.Empty);
                z.log("[刷新PVE关卡数据]成功");
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqGetDataFail(obj);
        }
    }

    private void onReqGetEventDataFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetEventDataFail(EventArgs.Empty);
    }

    private void onReqGetEventDataSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetPVEDataResponse res = new JsonFx.Json.JsonReader().Read<GetPVEDataResponse>(this.UTF8String);
            base.responseData = res;
            if (res.eid != 0)
            {
                this.onReqGetEventDataFail(obj);
            }
            else
            {
                this.HandleNewPveData(res);
                GameData.instance.SetPassedEventNodeIds(res.passedNodes);
                this.OnGetEventDataSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqGetEventDataFail(obj);
        }
    }

    private void onReqNextNodeFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetNextNodeFail(EventArgs.Empty);
        z.log("[PVE - 进入下一节点失败]...." + obj.UTF8String);
    }

    private void onReqNextNodeSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.nextNodeResponse = new JsonFx.Json.JsonReader().Read<GetNextNodeResponse>(this.UTF8String);
            base.responseData = this.nextNodeResponse;
            if (this.nextNodeResponse.eid != 0)
            {
                this.onReqNextNodeFail(obj);
            }
            else
            {
                this.HandleNewPveData(this.nextNodeResponse.newPveData);
                this.OnGetNextNodeSuccess(EventArgs.Empty);
                z.instance.setBattleStatus(BATTLE_STATUS.VISIT_NODE);
                z.instance.setBattleNodeId(nextNodeResponse.node);
                z.log("[PVE - 挺进下一节点]....");
            }
        }
        catch (Exception exception)
        {
            z.log(this.UTF8String + exception);
            this.onReqNextNodeFail(obj);
        }
    }


    private void onReqSearchNodeFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetSearchNodeFail(EventArgs.Empty);
    }

    private void onReqSearchNodeSuccess(BaseWWWRequest obj)
    {
        try
        {
            GameData instance = GameData.instance;
            this.searchNodeResponse = new JsonReader().Read<SearchEnemyResponse>(base.UTF8String);
            base.responseData = this.searchNodeResponse;
            if (this.searchNodeResponse.eid != 0)
            {
                this.onReqSearchNodeFail(obj);
            }
            else
            {
                this.OnGetSearchNodeSuccess(EventArgs.Empty);
                z.instance.setBattleStatus(BATTLE_STATUS.SPY_NODE);
                z.log("[PVE - 索敌进行中]....");
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqSearchNodeFail(obj);
        }
    }

    private void onReqSkipWarFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetSkipWarResultFail(EventArgs.Empty);
    }

    private void onReqSkipWarSuccess(BaseWWWRequest obj)
    {
        try
        {
            GameData instance = GameData.instance;
            this.skipWarResponse = new JsonReader().Read<SkipWarResponse>(base.UTF8String);

            base.responseData = this.skipWarResponse;
            if (this.skipWarResponse.eid != 0)
            {
                this.onReqSkipWarFail(obj);
            }
            else
            {
                instance.AddPassedNodeId(this.dealingNodeId);
                if (this.skipWarResponse.shipVO != null)
                {
                    foreach (UserShip ship in this.skipWarResponse.shipVO)
                    {
                        GameData.instance.UpdateUserShip(ship);
                    }
                }
                if ((this.skipWarResponse.newShipVO != null) && (this.skipWarResponse.newShipVO.Length > 0))
                {
                    CurrentWarParameters.newShipsGotFromWar = this.skipWarResponse.newShipVO;
                    foreach (UserShip ship2 in this.skipWarResponse.newShipVO)
                    {
                        GameData.instance.AddUserShip(ship2);
                    }
                }
                if (this.skipWarResponse.detailInfo != null)
                {
                    GameData.instance.UserInfo.UpdateDetailInfo(this.skipWarResponse.detailInfo);
                }
                if (this.skipWarResponse.campaignVo != null)
                {
                    if (this.skipWarResponse.campaignVo.canCampaignChallengeLevel != null)
                    {
                        GameData.instance.SetOpenedPVECampaignLevels(this.skipWarResponse.campaignVo.canCampaignChallengeLevel);
                    }
                    if (this.skipWarResponse.campaignVo.campaignChallenge != null)
                    {
                        GameData.instance.SetCampaignChapterTimesInfo(this.skipWarResponse.campaignVo.campaignChallenge);
                    }
                    if (this.skipWarResponse.campaignVo.passInfo != null)
                    {
                        GameData.instance.TotalCampainInfo = this.skipWarResponse.campaignVo.passInfo;
                    }
                }
                if (this.skipWarResponse.warResult != null)
                {
                    this.CheckUserNewExpInfo(this.skipWarResponse.warResult.userLevelVo);
                }
                this.HandleNewPveData(this.skipWarResponse.newPveData);
                this.OnGetSkipWarResultSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqSkipWarFail(obj);
        }
    }

    private void onReqStartLevelFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetStartLevelFail(EventArgs.Empty);
        z.instance.setBattleStatus(BATTLE_STATUS.FINISHED);
        z.log("[开始PVE战斗失败]...." + obj.UTF8String);
    }

    private void onReqStartLevelSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.startLevelResponse = new JsonFx.Json.JsonReader().Read<GetStartLevelResponse>(this.UTF8String);
            base.responseData = this.startLevelResponse;
            if (this.startLevelResponse.eid != 0)
            {
                this.onReqStartLevelFail(obj);
            }
            else
            {
                this.OnGetStartLevelSuccess(EventArgs.Empty);
                z.instance.setBattleStatus(BATTLE_STATUS.STARTED);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqStartLevelFail(obj);
        }
    }

    public void StartLevel(int levelId, int fleetId, FleetFormation formationType)
    {
        UserFleet uf = GameData.instance.GetFleetOfId(fleetId);
        PVELevel pl = PVEConfigs.instance.GetLevel(levelId);
        z.log("[开始PVE战斗]...."+ uf.title + " ===> "+ pl.title + " " + pl.subTitle);
        object[] objArray1 = new object[] { "pve/challenge129/", levelId, "/", fleetId, "/", (int) formationType };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqStartLevelSuccess), new BaseWWWRequest.OnFail(this.onReqStartLevelFail), true, ServerType.ChoosedServer, false);
    }

    public void SearchNode(int nodeId)
    {
        base.path = "pve/spy/" + nodeId;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqSearchNodeSuccess), new BaseWWWRequest.OnFail(this.onReqSearchNodeFail), true, ServerType.ChoosedServer, false);
    }

    public void SkipNode(int nodeId)
    {
        this.dealingNodeId = nodeId;
        base.path = "pve/SkipWar/" + nodeId;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqSkipWarSuccess), new BaseWWWRequest.OnFail(this.onReqSkipWarFail), true, ServerType.ChoosedServer, false);
    }
}

