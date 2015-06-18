using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;

public class GetPVEConfigs : BaseWWWRequest
{
    private static bool haveGetPVEConfig;
    private static bool haveGetPVEEventConfig;
    public bool needZip = true;

    public event EventHandler<EventArgs> GetConfigFail;

    public event EventHandler<EventArgs> GetConfigSuccess;

    public event EventHandler<EventArgs> GetEventConfigFail;

    public event EventHandler<EventArgs> GetEventConfigSuccess;

    public void GetConfigs()
    {
        if (haveGetPVEConfig)
        {
            //base.Invoke("MakeConfigSuccess", 0.1f);
        }
        else
        {
            base.path = "pve/getPveData/";
            base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqGetConfigSuccess), new BaseWWWRequest.OnFail(this.onReqGetConfigFail), true, ServerType.ChoosedServer, false);
        }
    }

    public void GetEventConfigs()
    {
        if (haveGetPVEEventConfig)
        {
            //base.Invoke("MakeEventConfigSuccess", 0.1f);
        }
        else
        {
            base.path = "pevent/getPveData";
            base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqGetEventConfigSuccess), new BaseWWWRequest.OnFail(this.onReqGetEventConfigFail), true, ServerType.ChoosedServer, false);
        }
    }

    private void MakeConfigSuccess()
    {
        this.OnGetConfigSuccess(EventArgs.Empty);
    }

    private void MakeEventConfigSuccess()
    {
        this.OnGetEventConfigSuccess(EventArgs.Empty);
    }

    protected virtual void OnGetConfigFail(EventArgs e)
    {
        if (this.GetConfigFail != null)
        {
            this.GetConfigFail(this, e);
        }
    }

    protected virtual void OnGetConfigSuccess(EventArgs e)
    {
        if (this.GetConfigSuccess != null)
        {
            this.GetConfigSuccess(this, e);
        }
    }

    protected virtual void OnGetEventConfigFail(EventArgs e)
    {
        if (this.GetEventConfigFail != null)
        {
            this.GetEventConfigFail(this, e);
        }
    }

    protected virtual void OnGetEventConfigSuccess(EventArgs e)
    {
        if (this.GetEventConfigSuccess != null)
        {
            this.GetEventConfigSuccess(this, e);
        }
    }

    private void onReqGetConfigFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetConfigFail(EventArgs.Empty);
    }

    private void onReqGetConfigSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetPVEConfigData data;
            if (this.needZip)
            {
                data = new JsonFx.Json.JsonReader().Read<GetPVEConfigData>(base.UTF8String);
            }
            else
            {
                data = new JsonFx.Json.JsonReader().Read<GetPVEConfigData>(this.UTF8String);
            }
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onReqGetConfigFail(obj);
            }
            else
            {
                PVEConfigs instance = PVEConfigs.instance;
                instance.SetChapters(data.pve);
                instance.SetLevels(data.pveLevel);
                instance.SetNodes(data.pveNode);
                //MonoBehaviour.print("onReqGetConfigSuccess");
                this.OnGetConfigSuccess(EventArgs.Empty);
                haveGetPVEConfig = true;
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqGetConfigFail(obj);
        }
    }

    private void onReqGetEventConfigFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetEventConfigFail(EventArgs.Empty);
    }

    private void onReqGetEventConfigSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetPVEEventConfigData data;
            if (this.needZip)
            {
                data = new JsonFx.Json.JsonReader().Read<GetPVEEventConfigData>(base.UTF8String);
            }
            else
            {
                data = new JsonFx.Json.JsonReader().Read<GetPVEEventConfigData>(this.UTF8String);
            }
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onReqGetEventConfigFail(obj);
            }
            else
            {
                PVEConfigs instance = PVEConfigs.instance;
                instance.SetEventLevels(data.pveEventLevel);
                instance.SetNodes(data.pveNode);
                //MonoBehaviour.print("onReqGetConfigSuccess");
                this.OnGetEventConfigSuccess(EventArgs.Empty);
                haveGetPVEEventConfig = true;
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqGetEventConfigFail(obj);
        }
    }
}

