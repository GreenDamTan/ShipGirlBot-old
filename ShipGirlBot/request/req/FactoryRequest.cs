using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;

public class FactoryRequest : BaseWWWRequest
{
    private GetShipData getShipData;
    private BuildLogVO settingFavLog;

    private int dockid = -1;

    public event EventHandler<EventArgs> BuildShipFail;

    public event EventHandler<EventArgs> BuildShipSuccess;

    public event EventHandler<EventArgs> FastBuildShipFail;

    public event EventHandler<EventArgs> FastBuildShipSuccess;

    public event EventHandler<EventArgs> GetLogFail;

    public event EventHandler<EventArgs> GetLogSuccess;

    public event EventHandler<EventArgs> GetShipFail;

    public event EventHandler<EventArgs> GetShipSuccess;

    public event EventHandler<EventArgs> SetFavLogFail;

    public event EventHandler<EventArgs> SetFavLogSuccess;

    public void BuildShip(int dockId, int oil, int steel, int ammo, int alum)
    {
        dockid = dockId;
        object[] objArray1 = new object[] { "dock/buildBoat/", dockId, "/", oil, "/", steel, "/", ammo, "/", alum };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onBuildSuccess), new BaseWWWRequest.OnFail(this.onBuildFail), true, ServerType.ChoosedServer, false);
    }

    public void FastBuild(int dockId)
    {
        base.path = "dock/instantBuild/" + dockId;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onFastBuildSuccess), new BaseWWWRequest.OnFail(this.onFastBuildFail), true, ServerType.ChoosedServer, false);
    }

    public void GetBuildLog()
    {
        base.path = "dock/getBuildBoatLog/";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onGetLogReqSuccess), new BaseWWWRequest.OnFail(this.onGetLogReqFail), true, ServerType.ChoosedServer, false);
    }

    public void GetShip(int dockId)
    {
        dockid = dockId;
        base.path = "dock/getBoat/" + dockId ;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onSuccess), new BaseWWWRequest.OnFail(this.onFail), true, ServerType.ChoosedServer, false);
    }

    private void onBuildFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnBuildShipFail(EventArgs.Empty);
    }

    protected virtual void OnBuildShipFail(EventArgs e)
    {
        if (this.BuildShipFail != null)
        {
            this.BuildShipFail(this, e);
        }
    }

    protected virtual void OnBuildShipSuccess(EventArgs e)
    {
        if (this.BuildShipSuccess != null)
        {
            this.BuildShipSuccess(this, e);
        }
    }

    private void onBuildSuccess(BaseWWWRequest obj)
    {
        try
        {
            BuildShipData data = new JsonFx.Json.JsonReader().Read<BuildShipData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onBuildFail(obj);
            }
            else
            {
                GameData.instance.UserDocks = data.dockVo;
                GameData.instance.UpdateUserItems(data.packageVo);
                if (data.userVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                z.instance.OnbuildShipSuccess(dockid, data);
                this.OnBuildShipSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onBuildFail(obj);
        }
    }

    
    private void onFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetShipFail(EventArgs.Empty);
    }

    private void onFastBuildFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnFastBuildShipFail(EventArgs.Empty);
    }

    protected virtual void OnFastBuildShipFail(EventArgs e)
    {
        if (this.FastBuildShipFail != null)
        {
            this.FastBuildShipFail(this, e);
        }
    }

    protected virtual void OnFastBuildShipSuccess(EventArgs e)
    {
        if (this.FastBuildShipSuccess != null)
        {
            this.FastBuildShipSuccess(this, e);
        }
    }

    private void onFastBuildSuccess(BaseWWWRequest obj)
    {
        try
        {
            FastBuildShipData data = new JsonFx.Json.JsonReader().Read<FastBuildShipData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onFastBuildFail(obj);
            }
            else
            {
                GameData.instance.UserDocks = data.dockVo;
                GameData.instance.UpdateUserItems(data.packageVo);
                if (data.userVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                this.OnFastBuildShipSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onFastBuildFail(obj);
        }
    }

    protected virtual void OnGetLogFail(EventArgs e)
    {
        if (this.GetLogFail != null)
        {
            this.GetLogFail(this, e);
        }
    }

    private void onGetLogReqFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetLogFail(EventArgs.Empty);
    }

    private void onGetLogReqSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetLogData data = new JsonFx.Json.JsonReader().Read<GetLogData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onGetLogReqFail(obj);
            }
            else
            {
                GameData.instance.ShipBuildLogs = data.log;
                this.OnGetLogSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onGetLogReqFail(obj);
        }
    }

    protected virtual void OnGetLogSuccess(EventArgs e)
    {
        if (this.GetLogSuccess != null)
        {
            this.GetLogSuccess(this, e);
        }
    }

    protected virtual void OnGetShipFail(EventArgs e)
    {
        if (this.GetShipFail != null)
        {
            this.GetShipFail(this, e);
        }
    }

    protected virtual void OnGetShipSuccess(EventArgs e)
    {
        if (this.GetShipSuccess != null)
        {
            this.GetShipSuccess(this.getShipData, e);
        }
    }

    protected virtual void OnSetFavLogFail(EventArgs e)
    {
        if (this.SetFavLogFail != null)
        {
            this.SetFavLogFail(this, e);
        }
    }

    private void onSetFavLogReqFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnSetFavLogFail(EventArgs.Empty);
    }

    private void onSetFavLogReqSuccess(BaseWWWRequest obj)
    {
        try
        {
            BasicResponse response = new JsonFx.Json.JsonReader().Read<BasicResponse>(this.UTF8String);
            base.responseData = response;
            if (response.eid != 0)
            {
                this.onSetFavLogReqFail(obj);
            }
            else
            {
                if (this.settingFavLog != null)
                {
                    this.settingFavLog.ToggleFav();
                }
                this.OnSetFavLogSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onSetFavLogReqFail(obj);
        }
    }

    protected virtual void OnSetFavLogSuccess(EventArgs e)
    {
        if (this.SetFavLogSuccess != null)
        {
            this.SetFavLogSuccess(this, e);
        }
    }

    private void onSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.getShipData = new JsonFx.Json.JsonReader().Read<GetShipData>(this.UTF8String);
            base.responseData = this.getShipData;
            if (this.getShipData.eid != 0)
            {
                this.onFail(obj);
            }
            else
            {
                GameData.instance.AddUserShip(this.getShipData.shipVO);
                GameData.instance.UserDocks = this.getShipData.dockVo;
                if (this.getShipData.detailInfo != null)
                {
                    GameData.instance.UserInfo.UpdateDetailInfo(this.getShipData.detailInfo);
                }
                z.instance.OnGetShipSuccess(dockid, getShipData);
                this.OnGetShipSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onFail(obj);
        }
    }

    public void SetFavLog(BuildLogVO log)
    {
        this.settingFavLog = log;
        base.path = "dock/addFov/1/" + log.id;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onSetFavLogReqSuccess), new BaseWWWRequest.OnFail(this.onSetFavLogReqFail), true, ServerType.ChoosedServer, false);
    }

    private void Start()
    {
    }
}

