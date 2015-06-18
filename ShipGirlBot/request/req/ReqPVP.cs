using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;
public class ReqPVP : BaseWWWRequest
{
    private GetPVPWarResultResponse battleResult;
    private StartPVPWarResponse warResponse;
    private SearchEnemyResponse searchNodeResponse;

    public event EventHandler<EventArgs> GetPVPListFail;

    public event EventHandler<EventArgs> GetPVPListSuccess;

    public event EventHandler<EventArgs> GetPVPWarResultFail;

    public event EventHandler<EventArgs> GetPVPWarResultSuccess;

    public event EventHandler<EventArgs> GetSearchPVPFail;

    public event EventHandler<EventArgs> GetSearchPVPSuccess;

    public event EventHandler<EventArgs> StartPVPWarFail;

    public event EventHandler<EventArgs> StartPVPWarSuccess;

    private void CheckUserNewExpInfo()
    {
        if ((this.battleResult.warResult != null) && (this.battleResult.warResult.userLevelVo != null))
        {
            GameData.instance.UserInfo.ApplyNewExpInfo(this.battleResult.warResult.userLevelVo);
        }
    }

    public void GetList()
    {
        base.path = "pvp/getChallengeList/";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqGetListSuccess), new BaseWWWRequest.OnFail(this.reqGetListFail), true, ServerType.ChoosedServer, false);
    }

    public void StartWar(int targetId, int fleetId, int formationId)
    {
        object[] objArray1 = new object[] { "pvp/challenge/", targetId, "/", fleetId, "/", formationId };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqStartWarSuccess), new BaseWWWRequest.OnFail(this.reqStartWarFail), true, ServerType.ChoosedServer, false);
    }

    public void GetWarResult(bool continueWar)
    {
        int num = 0;
        if (continueWar)
        {
            num = 1;
        }
        base.path = "pvp/getWarResult/" + num;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqGetResultSuccess), new BaseWWWRequest.OnFail(this.reqGetResultFail), true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnGetPVPListFail(EventArgs e)
    {
        if (this.GetPVPListFail != null)
        {
            this.GetPVPListFail(this, e);
        }
    }

    protected virtual void OnGetPVPListSuccess(EventArgs e)
    {
        if (this.GetPVPListSuccess != null)
        {
            this.GetPVPListSuccess(this, e);
        }
    }

    protected virtual void OnGetPVPWarResultFail(EventArgs e)
    {
        if (this.GetPVPWarResultFail != null)
        {
            this.GetPVPWarResultFail(this, e);
        }
    }

    protected virtual void OnGetPVPWarResultSuccess(EventArgs e)
    {
        if (this.GetPVPWarResultSuccess != null)
        {
            this.GetPVPWarResultSuccess(this.battleResult, e);
        }
    }

    protected virtual void OnGetSearchPVPFail(EventArgs e)
    {
        if (this.GetSearchPVPFail != null)
        {
            this.GetSearchPVPFail(this.searchNodeResponse, e);
        }
    }

    protected virtual void OnGetSearchPVPSuccess(EventArgs e)
    {
        if (this.GetSearchPVPSuccess != null)
        {
            this.GetSearchPVPSuccess(this.searchNodeResponse, e);
        }
    }

    private void onReqSearchPvpFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetSearchPVPFail(EventArgs.Empty);
    }

    private void onReqSearchPvpSuccess(BaseWWWRequest obj)
    {
        try
        {
            GameData instance = GameData.instance;
            this.searchNodeResponse = new JsonReader().Read<SearchEnemyResponse>(base.UTF8String);
            base.responseData = this.searchNodeResponse;
            if (this.searchNodeResponse.eid != 0)
            {
                this.onReqSearchPvpFail(obj);
            }
            else
            {
                this.OnGetSearchPVPSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqSearchPvpFail(obj);
        }
    }

    protected virtual void OnStartPVPWarFail(EventArgs e)
    {
        if (this.StartPVPWarFail != null)
        {
            this.StartPVPWarFail(this, e);
        }
    }

    protected virtual void OnStartPVPWarSuccess(EventArgs e)
    {
        if (this.StartPVPWarSuccess != null)
        {
            this.StartPVPWarSuccess(this.warResponse.warReport, e);
        }
    }

    private void reqGetListFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetPVPListFail(EventArgs.Empty);
    }

    private void reqGetListSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetPVPListResponse response = new JsonReader().Read<GetPVPListResponse>(base.UTF8String);
            base.responseData = response;
            if (response.eid != 0)
            {
                this.reqGetListFail(obj);
            }
            else
            {
                GameData.instance.SetPVPOpponents(response.list);
                this.OnGetPVPListSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message + " " + exception.StackTrace);
            this.reqGetListFail(obj);
        }
    }

    private void reqGetResultFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetPVPWarResultFail(EventArgs.Empty);
    }

    private void reqGetResultSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.battleResult = new JsonReader().Read<GetPVPWarResultResponse>(base.UTF8String);
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
                this.CheckUserNewExpInfo();
                    CurrentWarParameters.selectedOpponent.resultLevel = (WarResultLevel) this.battleResult.warResult.resultLevel;
                this.OnGetPVPWarResultSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message + " " + exception.StackTrace);
            this.reqGetResultFail(obj);
        }
    }

    private void reqStartWarFail(BaseWWWRequest obj)
    {
        this.OnStartPVPWarFail(EventArgs.Empty);
    }

    private void reqStartWarSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.warResponse = new JsonReader().Read<StartPVPWarResponse>(base.UTF8String);
            base.responseData = this.warResponse;
            if (this.warResponse.eid != 0)
            {
                this.reqStartWarFail(obj);
            }
            else
            {
                this.OnStartPVPWarSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message + " " + exception.StackTrace);
            this.reqStartWarFail(obj);
        }
    }

    public void SearchPVPEnemy(int targetId, int fleetId)
    {
        object[] objArray1 = new object[] { "pvp/spy/", targetId, "/", fleetId };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqSearchPvpSuccess), new BaseWWWRequest.OnFail(this.onReqSearchPvpFail), true, ServerType.ChoosedServer, false);
    }
}

