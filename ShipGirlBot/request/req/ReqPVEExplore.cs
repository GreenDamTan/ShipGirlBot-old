using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
////using UnityEngine;

public class ReqPVEExplore : BaseWWWRequest
{
    private FinishExploreResponse finishResponse;

    public event EventHandler<EventArgs> CancelExploreFail;

    public event EventHandler<EventArgs> CancelExploreSuccess;

    public event EventHandler<EventArgs> FinishExploreFail;

    public event EventHandler<EventArgs> FinishExploreSuccess;

    public event EventHandler<EventArgs> StartExploreFail;

    public event EventHandler<EventArgs> StartExploreSuccess;

    public void CancelExplore(int exploreId)
    {
        base.path = "explore/cancel/" + exploreId;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqCancelSuccess), new BaseWWWRequest.OnFail(this.reqCancelFail), true, ServerType.ChoosedServer, false);
    }

    private void CheckUserNewExpInfo()
    {
        if (this.finishResponse.userLevelVo != null)
        {
            GameData.instance.UserInfo.ApplyNewExpInfo(this.finishResponse.userLevelVo);
        }
    }

    public void FinishExplore(int exploreId)
    {

        base.path = "explore/getResult/" + exploreId;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqFinishSuccess), new BaseWWWRequest.OnFail(this.reqFinishFail), true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnCancelExploreFail(EventArgs e)
    {
        if (this.CancelExploreFail != null)
        {
            this.CancelExploreFail(this, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    protected virtual void OnCancelExploreSuccess(EventArgs e)
    {
        if (this.CancelExploreSuccess != null)
        {
            this.CancelExploreSuccess(this, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    protected virtual void OnFinishExploreFail(EventArgs e)
    {
        if (this.FinishExploreFail != null)
        {
            this.FinishExploreFail(this, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    protected virtual void OnFinishExploreSuccess(EventArgs e)
    {
        if (this.FinishExploreSuccess != null)
        {
            this.FinishExploreSuccess(this.finishResponse, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    protected virtual void OnStartExploreFail(EventArgs e)
    {
        if (this.StartExploreFail != null)
        {
            this.StartExploreFail(this, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    protected virtual void OnStartExploreSuccess(EventArgs e)
    {
        if (this.StartExploreSuccess != null)
        {
            this.StartExploreSuccess(this, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    private void reqCancelFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        z.log("[远征取消失败]..." );
        this.OnCancelExploreFail(EventArgs.Empty);
    }

    private void reqCancelSuccess(BaseWWWRequest obj)
    {
        try
        {
            CancelExploreResponse response = new JsonFx.Json.JsonReader().Read<CancelExploreResponse>(this.UTF8String);
            base.responseData = response;
            if (response.eid != 0)
            {
                this.reqCancelFail(obj);
            }
            else
            {
                GameData.instance.UpdatePVEExplore(response.pveExploreVo);
                this.OnCancelExploreSuccess(EventArgs.Empty);
                z.log("[远征已经取消]..." );
            }
        }
        catch (Exception exception)
        {
            //Form1.log(exception);
            this.reqCancelFail(obj);
        }
    }

    private void reqFinishFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnFinishExploreFail(EventArgs.Empty);
        z.log("[远征完成失败]...");
        ServerRequestManager.instance.refreashUIData();
    }

    private void reqFinishSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.finishResponse = new JsonFx.Json.JsonReader().Read<FinishExploreResponse>(this.UTF8String);
            base.responseData = this.finishResponse;
            if (this.finishResponse.eid != 0)
            {
                this.reqFinishFail(obj);
            }
            else
            {
                if (this.finishResponse.detailInfo != null)
                {
                    GameData.instance.UserInfo.UpdateDetailInfo(this.finishResponse.detailInfo);
                }
                GameData.instance.UpdatePVEExplore(this.finishResponse.pveExploreVo);
                this.finishResponse.userResChange = GameData.instance.UserInfo.GetResourceChange(this.finishResponse.userResVo);
                GameData.instance.UserInfo.UpdateResource(this.finishResponse.userResVo);
                GameData.instance.UpdateUserItems(this.finishResponse.packageVo);
                this.CheckUserNewExpInfo();
                z.log("[远征完成成功]..." + (finishResponse.bigSuccess == 0?"完成":"大成功"));
                this.OnFinishExploreSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            //Form1.log(exception);
            this.reqFinishFail(obj);
        }

        ServerRequestManager.instance.refreashUIData();
    }

    private void reqStartSuccess(BaseWWWRequest obj)
    {
        try
        {
            StartExploreResponse response = new JsonFx.Json.JsonReader().Read<StartExploreResponse>(this.UTF8String);
            base.responseData = response;
            if (response.eid != 0)
            {
                this.resStartFail(obj);
            }
            else
            {
                GameData.instance.UpdatePVEExplore(response.pveExploreVo);
                if (response.userResVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(response.userResVo);
                }
                z.log("[远征开始]...");
                this.OnStartExploreSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            //Form1.log(exception);
            this.resStartFail(obj);
        }
    }

    private void resStartFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        z.log("[远征开始失败]...");
        this.OnStartExploreFail(EventArgs.Empty);
    }

    private void Start()
    {
    }

    public void StartExplore(int fleetId, int exploreId)
    {
        object[] objArray1 = new object[] { "explore/start/", fleetId, "/", exploreId };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqStartSuccess), new BaseWWWRequest.OnFail(this.resStartFail), true, ServerType.ChoosedServer, false);
    }
}

