using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;

public class ReqMarketing : BaseWWWRequest
{
    private ReqMarkeingDataResponse getAwardResponse;

    public event EventHandler<EventArgs> GetContinueLoginAwardFail;

    public event EventHandler<EventArgs> GetContinueLoginAwardSuccess;

    public event EventHandler<EventArgs> GetFirstPayAwardFail;

    public event EventHandler<EventArgs> GetFirstPayAwardSuccess;

    public event EventHandler<EventArgs> GetReachLevelAwardFail;

    public event EventHandler<EventArgs> GetReachLevelAwardSuccess;

    public event EventHandler<EventArgs> UpdateStatusFail;

    public event EventHandler<EventArgs> UpdateStatusSuccess;

    public void GetContinueLoginAward()
    {
        base.path = "active/getLoginAward/";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqGetContinueLoginAwardSuccess), new BaseWWWRequest.OnFail(this.reqGetContinueLoginAwardFail), true, ServerType.ChoosedServer, false);
    }

    public void GetFirstPayAward()
    {
        base.path = "active/getChargeAward/";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqGetFirstPayAwardSuccess), new BaseWWWRequest.OnFail(this.reqGetFirstPayAwaredFail), true, ServerType.ChoosedServer, false);
    }

    public void GetNewStatus()
    {
        base.path = "active/getUserData";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqUpdateSuccess), new BaseWWWRequest.OnFail(this.reqUpdateFail), true, ServerType.ChoosedServer, false);
    }

    public void GetReachLevelAward(int level)
    {
        base.path = "active/getLevelAward/" + level;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqGetReachLevelAwardSuccess), new BaseWWWRequest.OnFail(this.reqGetReachLevelAwardFail), true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnGetContinueLoginAwardFail(EventArgs e)
    {
        if (this.GetContinueLoginAwardFail != null)
        {
            this.GetContinueLoginAwardFail(this, e);
        }
    }

    protected virtual void OnGetContinueLoginAwardSuccess(EventArgs e)
    {
        if (this.GetContinueLoginAwardSuccess != null)
        {
            this.GetContinueLoginAwardSuccess(this.getAwardResponse, e);
        }
    }

    protected virtual void OnGetFirstPayAwardFail(EventArgs e)
    {
        if (this.GetFirstPayAwardFail != null)
        {
            this.GetFirstPayAwardFail(this, e);
        }
    }

    protected virtual void OnGetFirstPayAwardSuccess(EventArgs e)
    {
        if (this.GetFirstPayAwardSuccess != null)
        {
            this.GetFirstPayAwardSuccess(this.getAwardResponse, e);
        }
    }

    protected virtual void OnGetReachLevelAwardFail(EventArgs e)
    {
        if (this.GetReachLevelAwardFail != null)
        {
            this.GetReachLevelAwardFail(this, e);
        }
    }

    protected virtual void OnGetReachLevelAwardSuccess(EventArgs e)
    {
        if (this.GetReachLevelAwardSuccess != null)
        {
            this.GetReachLevelAwardSuccess(this.getAwardResponse, e);
        }
    }

    protected virtual void OnUpdateStatusFail(EventArgs e)
    {
        if (this.UpdateStatusFail != null)
        {
            this.UpdateStatusFail(this, e);
        }
    }

    protected virtual void OnUpdateStatusSuccess(EventArgs e)
    {
        if (this.UpdateStatusSuccess != null)
        {
            this.UpdateStatusSuccess(this.getAwardResponse, e);
        }
    }

    private void reqGetContinueLoginAwardFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetContinueLoginAwardFail(EventArgs.Empty);
    }

    private void reqGetContinueLoginAwardSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.getAwardResponse = new JsonFx.Json.JsonReader().Read<ReqMarkeingDataResponse>(this.UTF8String);
            base.responseData = this.getAwardResponse;
            if (this.getAwardResponse.eid != 0)
            {
                this.reqGetContinueLoginAwardFail(obj);
            }
            else
            {
                this.UpdateCommonResult(this.getAwardResponse);
                GameData.instance.MarketingDatas.continueLoginAward = this.getAwardResponse.marketingData.continueLoginAward;
                this.OnGetContinueLoginAwardSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            //Form1.log(exception);
            this.reqGetContinueLoginAwardFail(obj);
        }
    }

    private void reqGetFirstPayAwardSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.getAwardResponse = new JsonFx.Json.JsonReader().Read<ReqMarkeingDataResponse>(this.UTF8String);
            base.responseData = this.getAwardResponse;
            if (this.getAwardResponse.eid != 0)
            {
                this.reqGetFirstPayAwaredFail(obj);
            }
            else
            {
                this.UpdateCommonResult(this.getAwardResponse);
                GameData.instance.MarketingDatas.firstPayAward = this.getAwardResponse.marketingData.firstPayAward;
                this.OnGetFirstPayAwardSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            //Form1.log(exception);
            this.reqGetFirstPayAwaredFail(obj);
        }
    }

    private void reqGetFirstPayAwaredFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetFirstPayAwardFail(EventArgs.Empty);
    }

    private void reqGetReachLevelAwardFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetReachLevelAwardFail(EventArgs.Empty);
    }

    private void reqGetReachLevelAwardSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.getAwardResponse = new JsonFx.Json.JsonReader().Read<ReqMarkeingDataResponse>(this.UTF8String);
            base.responseData = this.getAwardResponse;
            if (this.getAwardResponse.eid != 0)
            {
                this.reqGetReachLevelAwardFail(obj);
            }
            else
            {
                this.UpdateCommonResult(this.getAwardResponse);
                GameData.instance.MarketingDatas.reachLevelAward = this.getAwardResponse.marketingData.reachLevelAward;
                this.OnGetReachLevelAwardSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            //Form1.log(exception);
            this.reqGetReachLevelAwardFail(obj);
        }
    }

    private void reqUpdateFail(BaseWWWRequest obj)
    {
        this.OnUpdateStatusFail(EventArgs.Empty);
    }

    private void reqUpdateSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.getAwardResponse = new JsonFx.Json.JsonReader().Read<ReqMarkeingDataResponse>(this.UTF8String);
            base.responseData = this.getAwardResponse;
            if (this.getAwardResponse.eid != 0)
            {
                this.reqUpdateFail(obj);
            }
            else
            {
                GameData.instance.MarketingDatas = this.getAwardResponse.marketingData;
                this.OnUpdateStatusSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            //Form1.log(exception);
            this.reqUpdateFail(obj);
        }
    }

    private void Start()
    {
    }

    private void UpdateCommonResult(ReqMarkeingDataResponse response)
    {
        if (response.userResVo != null)
        {
            GameData.instance.UserInfo.UpdateResource(response.userResVo);
        }
        if (response.shipVO != null)
        {
            foreach (UserShip ship in response.shipVO)
            {
                GameData.instance.AddUserShip(ship);
            }
        }
        if (response.equipmentVo != null)
        {
            foreach (UserEquipment equipment in response.equipmentVo)
            {
                GameData.instance.AddUserEquipmenet(equipment);
            }
        }
        if (response.packageVo != null)
        {
            GameData.instance.UpdateUserItems(response.packageVo);
        }
    }
}

