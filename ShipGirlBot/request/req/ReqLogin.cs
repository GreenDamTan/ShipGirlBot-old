using JsonFx.Json;
using System;

public class ReqLogin : BaseWWWRequest
{
    private BasicResponse initResponse;
    private LoginVO loginVO;


    public void ChooseInitShipAndName(string name, string shipId)
    {
        string str = WWW.EscapeURL(name);
        string[] textArray1 = new string[] { "api/regRole/", str, "/", shipId, "/" };
        base.path = string.Concat(textArray1);
        z.log(base.path);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onInitShipSuccess), new BaseWWWRequest.OnFail(this.onInitFail), true, ServerType.ChoosedServer, false);
    }

    public void LoginUseId(string userId)
    {
        base.path = "index/login/" + userId;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onSuccess), new BaseWWWRequest.OnFail(this.onFail), true, ServerType.ChoosedServer, false);
    }

    private void onFail(BaseWWWRequest obj)
    {
        if (this.loginVO != null)
        {
            ServerRequestManager.instance.OnLoginFail(this.loginVO.eid);
        }
        else
        {
            ServerRequestManager.instance.OnLoginFail(0);
        }
        z.log("[登陆失败] " + base.www.error + "\r\n " + this.UTF8String);
    }

    private void onInitFail(BaseWWWRequest obj)
    {
        z.log("Login Fail: " + base.www.error);
    }
    private void onInitShipSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.initResponse = new JsonReader().Read<BasicResponse>(base.UTF8String);
            if (this.initResponse.eid != 0)
            {
                this.onInitFail(obj);
            }
            else
            {
                //ServerRequestManager.instance.OnChooseInitShipSuccess();
                z.log("onInitShipSuccess Success: ");
            }
        }
        catch (Exception)
        {
            this.onInitFail(obj);
        }
    }
    private void onSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.loginVO = new JsonFx.Json.JsonReader().Read<LoginVO>(this.UTF8String);
            if (this.loginVO.eid != 0)
            {
                this.onFail(obj);
            }
            else
            {
                GameData.instance.LoginInfo = this.loginVO;
                ServerRequestManager.instance.OnLoginSuccess();
            }
        }
        catch (Exception)
        {
            this.onFail(obj);
        }
    }
}

