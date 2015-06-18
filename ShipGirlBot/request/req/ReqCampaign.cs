using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;
//using UnityEngine;

public class ReqCampaign : BaseWWWRequest
{
    private GetCampaignWarResultResponse battleResult;
    private PVECampaignLevel requesttingLevel;
    private StartCampaignWarResponse warResponse;
    private SearchEnemyResponse searchNodeResponse;

    public event EventHandler<EventArgs> GetCampaignDataFail;

    public event EventHandler<EventArgs> GetCampaignDataSuccess;

    public event EventHandler<EventArgs> GetCampaignFleetFail;

    public event EventHandler<EventArgs> GetCampaignFleetSuccess;

    public event EventHandler<EventArgs> GetCampaignWarResultFail;

    public event EventHandler<EventArgs> GetCampaignWarResultSuccess;

    public event EventHandler<EventArgs> StartCampaignWarFail;

    public event EventHandler<EventArgs> StartCampaignWarSuccess;

    public event EventHandler<EventArgs> UpdateCampaignFleetFail;

    public event EventHandler<EventArgs> UpdateCampaignFleetSuccess;

    public event EventHandler<EventArgs> GetSearchFail;

    public event EventHandler<EventArgs> GetSearchSuccess;

    private void CheckUserNewExpInfo()
    {
        if ((this.battleResult.warResult != null) && (this.battleResult.warResult.userLevelVo != null))
        {
            GameData.instance.UserInfo.ApplyNewExpInfo(this.battleResult.warResult.userLevelVo);
        }
    }

    public void GetData()
    {
        base.path = "campaign/getUserData";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqGetUserDataSuccess), new BaseWWWRequest.OnFail(this.reqGetUserDataFail), true, ServerType.ChoosedServer, false);
    }

    public void GetFleetData(PVECampaignLevel level)
    {
        this.requesttingLevel = level;
        base.path = "campaign/getFleet/" + level.id;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqGetFleetSuccess), new BaseWWWRequest.OnFail(this.reqGetFleetFail), true, ServerType.ChoosedServer, false);
    }

    public void GetWarResult(bool continueWar)
    {
        int num = 0;
        if (continueWar)
        {
            num = 1;
        }
        base.path = "campaign/getWarResult/" + num;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqGetResultSuccess), new BaseWWWRequest.OnFail(this.reqGetResultFail), true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnGetCampaignDataFail(EventArgs e)
    {
        if (this.GetCampaignDataFail != null)
        {
            this.GetCampaignDataFail(this, e);
        }
    }

    protected virtual void OnGetCampaignDataSuccess(EventArgs e)
    {
        if (this.GetCampaignDataSuccess != null)
        {
            this.GetCampaignDataSuccess(this, e);
        }
    }

    protected virtual void OnGetCampaignFleetFail(EventArgs e)
    {
        if (this.GetCampaignFleetFail != null)
        {
            this.GetCampaignFleetFail(this, e);
        }
    }

    protected virtual void OnGetCampaignFleetSuccess(EventArgs e)
    {
        if (this.GetCampaignFleetSuccess != null)
        {
            this.GetCampaignFleetSuccess(this, e);
        }
    }

    protected virtual void OnGetCampaignWarResultFail(EventArgs e)
    {
        if (this.GetCampaignWarResultFail != null)
        {
            this.GetCampaignWarResultFail(this, e);
        }
    }

    protected virtual void OnGetCampaignWarResultSuccess(EventArgs e)
    {
        if (this.GetCampaignWarResultSuccess != null)
        {
            this.GetCampaignWarResultSuccess(this.battleResult, e);
        }
    }

    protected virtual void OnGetSearchFail(EventArgs e)
    {
        if (this.GetSearchFail != null)
        {
            this.GetSearchFail(this.searchNodeResponse, e);
        }
    }

    protected virtual void OnGetSearchSuccess(EventArgs e)
    {
        if (this.GetSearchSuccess != null)
        {
            this.GetSearchSuccess(this.searchNodeResponse, e);
        }
    }

    private void onReqSearchFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetSearchFail(EventArgs.Empty);
    }

    private void onReqSearchSuccess(BaseWWWRequest obj)
    {
        try
        {
            GameData instance = GameData.instance;
           
            this.searchNodeResponse = new JsonReader().Read<SearchEnemyResponse>(base.UTF8String);
           
            base.responseData = this.searchNodeResponse;
            if (this.searchNodeResponse.eid != 0)
            {
                this.onReqSearchFail(obj);
            }
            else
            {
                this.OnGetSearchSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqSearchFail(obj);
        }
    }

    protected virtual void OnStartCampaignWarFail(EventArgs e)
    {
        if (this.StartCampaignWarFail != null)
        {
            this.StartCampaignWarFail(this, e);
        }
    }

    protected virtual void OnStartCampaignWarSuccess(EventArgs e)
    {
        if (this.StartCampaignWarSuccess != null)
        {
            this.StartCampaignWarSuccess(this.warResponse.warReport, e);
        }
    }

    protected virtual void OnUpdateCampaignFleetFail(EventArgs e)
    {
        if (this.UpdateCampaignFleetFail != null)
        {
            this.UpdateCampaignFleetFail(this, e);
        }
    }

    protected virtual void OnUpdateCampaignFleetSuccess(EventArgs e)
    {
        if (this.UpdateCampaignFleetSuccess != null)
        {
            this.UpdateCampaignFleetSuccess(this, e);
        }
    }

    private void reqGetFleetFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetCampaignFleetFail(EventArgs.Empty);
    }

    private void reqGetFleetSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetCampaignFleetResponse response = new JsonFx.Json.JsonReader().Read<GetCampaignFleetResponse>(this.UTF8String);
            base.responseData = response;
            if (response.eid != 0)
            {
                this.reqGetFleetFail(obj);
            }
            else
            {
                GameData.instance.UpdateCampaignFleetInfo(this.requesttingLevel.id, response.campaignLevelFleet);
                this.OnGetCampaignFleetSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.reqGetFleetFail(obj);
        }
    }

    private void reqGetResultFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetCampaignWarResultFail(EventArgs.Empty);
    }

    private void reqGetResultSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.battleResult = new JsonFx.Json.JsonReader().Read<GetCampaignWarResultResponse>(this.UTF8String);
            base.responseData = this.battleResult;
            if (this.battleResult.eid != 0)
            {
                this.reqGetResultFail(obj);
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
                if (this.battleResult.userResVo != null)
                {
                    this.battleResult.userResChange = GameData.instance.UserInfo.GetResourceChange(this.battleResult.userResVo);
                    GameData.instance.UserInfo.UpdateResource(this.battleResult.userResVo);
                }
                if (this.battleResult.packageVo != null)
                {
                    GameData.instance.UpdateUserItems(this.battleResult.packageVo);
                }
                this.CheckUserNewExpInfo();
                this.OnGetCampaignWarResultSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.reqGetResultFail(obj);
        }
    }

    private void reqGetUserDataFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetCampaignDataFail(EventArgs.Empty);
    }

    private void reqGetUserDataSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetCampaignDataResponse response = new JsonFx.Json.JsonReader().Read<GetCampaignDataResponse>(this.UTF8String);
            base.responseData = response;
            if (response.eid != 0)
            {
                this.reqGetUserDataFail(obj);
            }
            else
            {
                GameData.instance.SetOpenedPVECampaignLevels(response.canCampaignChallengeLevel);
                GameData.instance.SetCampaignChapterTimesInfo(response.campaignChallenge);
                if (response.passInfo != null)
                {
                    GameData.instance.TotalCampainInfo = response.passInfo;
                }
                this.OnGetCampaignDataSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.reqGetUserDataFail(obj);
        }
    }

    private void reqStartWarFail(BaseWWWRequest obj)
    {
        this.OnStartCampaignWarFail(EventArgs.Empty);
    }

    private void reqStartWarSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.warResponse = new JsonFx.Json.JsonReader().Read<StartCampaignWarResponse>(this.UTF8String);
            base.responseData = this.warResponse;
            if (this.warResponse.eid != 0)
            {
                this.reqStartWarFail(obj);
            }
            else
            {
                this.OnStartCampaignWarSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.reqStartWarFail(obj);
        }
    }

    private void reqUpdateFleetFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnUpdateCampaignFleetFail(EventArgs.Empty);
    }

    private void reqUpdateFleetSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetCampaignFleetResponse response = new JsonFx.Json.JsonReader().Read<GetCampaignFleetResponse>(this.UTF8String);
            base.responseData = response;
            if (response.eid != 0)
            {
                this.reqUpdateFleetFail(obj);
            }
            else
            {
                GameData.instance.UpdateCampaignFleetInfo(this.requesttingLevel.id, response.campaignLevelFleet);
                this.OnUpdateCampaignFleetSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.reqUpdateFleetFail(obj);
        }
    }

    public void SearchCampaign(int level)
    {
        base.path = "campaign/spy/" + level;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqSearchSuccess), new BaseWWWRequest.OnFail(this.onReqSearchFail), true, ServerType.ChoosedServer, false);
    }

    private void Start()
    {
    }

    public void StartWar(int levelId, int formationId)
    {
        object[] objArray1 = new object[] { "campaign/challenge/", levelId, "/", formationId };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqStartWarSuccess), new BaseWWWRequest.OnFail(this.reqStartWarFail), true, ServerType.ChoosedServer, false);
    }

    public void UpdateFleetData(PVECampaignLevel level, int shipId, int index)
    {
        this.requesttingLevel = level;
        object[] objArray1 = new object[] { "campaign/changeFleet/", level.id, "/", shipId, "/", index };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqUpdateFleetSuccess), new BaseWWWRequest.OnFail(this.reqUpdateFleetFail), true, ServerType.ChoosedServer, false);
    }
}

