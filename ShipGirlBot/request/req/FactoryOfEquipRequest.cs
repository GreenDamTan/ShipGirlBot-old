using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;


public class FactoryOfEquipRequest : BaseWWWRequest
{
    private GetEquipData gettingEquipData;
    private BuildLogVO settingFavLog;
    private int dockid;

    public event EventHandler<EventArgs> BuildEquipFail;

    public event EventHandler<EventArgs> BuildEquipSuccess;

    public event EventHandler<EventArgs> FastBuildEquipFail;

    public event EventHandler<EventArgs> FastBuildEquipSuccess;

    public event EventHandler<EventArgs> GetEquipFail;

    public event EventHandler<EventArgs> GetEquipSuccess;

    public event EventHandler<EventArgs> GetLogFail;

    public event EventHandler<EventArgs> GetLogSuccess;

    public event EventHandler<EventArgs> SetFavLogFail;

    public event EventHandler<EventArgs> SetFavLogSuccess;

    public void BuildEquip(int dockId, int oil, int steel, int ammo, int alum)
    {
        dockid = dockId;
        object[] objArray1 = new object[] { "dock/buildEquipment/", dockId, "/", oil, "/", steel, "/", ammo, "/", alum };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqBuildSuccess), new BaseWWWRequest.OnFail(this.onReqBuildFail), true, ServerType.ChoosedServer, false);
    }
    public void FastBuild(int dockId)
    {
        base.path = "dock/instantEquipmentBuild/" + dockId;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqFastBuildSuccess), new BaseWWWRequest.OnFail(this.onReqFastBuildFail), true, ServerType.ChoosedServer, false);
    }

    public void GetBuildLog()
    {
        base.path = "dock/getBuildEquipmentLog/";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onGetLogReqSuccess), new BaseWWWRequest.OnFail(this.onGetLogReqFail), true, ServerType.ChoosedServer, false);
    }

    public void GetEquip(int dockId)
    {
        dockid = dockId;
        base.path = "dock/getEquipment/" + dockId;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqGetEquipSuccess), new BaseWWWRequest.OnFail(this.onReqGetEquipFail), true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnBuildEquipFail(EventArgs e)
    {
        if (this.BuildEquipFail != null)
        {
            this.BuildEquipFail(this, e);
        }
    }

    protected virtual void OnBuildEquipSuccess(EventArgs e)
    {
        if (this.BuildEquipSuccess != null)
        {
            this.BuildEquipSuccess(this, e);
        }
    }

    protected virtual void OnFastBuildEquipFail(EventArgs e)
    {
        if (this.FastBuildEquipFail != null)
        {
            this.FastBuildEquipFail(this, e);
        }
    }

    protected virtual void OnFastBuildEquipSuccess(EventArgs e)
    {
        if (this.FastBuildEquipSuccess != null)
        {
            this.FastBuildEquipSuccess(this, e);
        }
    }

    protected virtual void OnGetEquipFail(EventArgs e)
    {
        if (this.GetEquipFail != null)
        {
            this.GetEquipFail(this, e);
        }
    }

    protected virtual void OnGetEquipSuccess(EventArgs e)
    {
        if (this.GetEquipSuccess != null)
        {
            this.GetEquipSuccess(this.gettingEquipData.equipmentVo, e);
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
                GameData.instance.EquipBuildLogs = data.log;
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

    private void onReqBuildFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnBuildEquipFail(EventArgs.Empty);
    }

    private void onReqBuildSuccess(BaseWWWRequest obj)
    {
        try
        {
            BuildEquipData data = new JsonFx.Json.JsonReader().Read<BuildEquipData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onReqBuildFail(obj);
            }
            else
            {
                GameData.instance.UserEquipDocks = data.equipmentDockVo;
                GameData.instance.UpdateUserItems(data.packageVo);
                if (data.userVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                z.instance.OnBuildWeaponSuccess(dockid, data);
                this.OnBuildEquipSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqBuildFail(obj);
        }
    }

    private void onReqFastBuildFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnFastBuildEquipFail(EventArgs.Empty);
    }

    private void onReqFastBuildSuccess(BaseWWWRequest obj)
    {
        try
        {
            FastBuildEquipData data = new JsonFx.Json.JsonReader().Read<FastBuildEquipData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onReqFastBuildFail(obj);
            }
            else
            {
                GameData.instance.UserEquipDocks = data.equipmentDockVo;
                GameData.instance.UpdateUserItems(data.packageVo);
                if (data.userVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                this.OnFastBuildEquipSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqFastBuildFail(obj);
        }
    }

    private void onReqGetEquipFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetEquipFail(EventArgs.Empty);
    }

    private void onReqGetEquipSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.gettingEquipData = new JsonFx.Json.JsonReader().Read<GetEquipData>(this.UTF8String);
            base.responseData = this.gettingEquipData;
            if (this.gettingEquipData.eid != 0)
            {
                this.onReqGetEquipFail(obj);
            }
            else
            {
                if (this.gettingEquipData.equipmentVo != null)
                {
                    GameData.instance.AddUserEquipmenet(this.gettingEquipData.equipmentVo);
                }
                if (this.gettingEquipData.detailInfo != null)
                {
                    GameData.instance.UserInfo.UpdateDetailInfo(this.gettingEquipData.detailInfo);
                }
                GameData.instance.UserEquipDocks = this.gettingEquipData.equipmentDockVo;

                z.instance.OnGetWeaponSuccess(dockid, responseData as GetEquipData);
                this.OnGetEquipSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqGetEquipFail(obj);
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

    public void SetFavLog(BuildLogVO log)
    {
        this.settingFavLog = log;
        base.path = "dock/addFov/2/" + log.id;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onSetFavLogReqSuccess), new BaseWWWRequest.OnFail(this.onSetFavLogReqFail), true, ServerType.ChoosedServer, false);
    }

    private void Start()
    {
    }
}

