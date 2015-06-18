using JsonFx.Json;
using System;
using System.Threading;
using System.Collections.Generic;

public class ReqSignin : BaseWWWRequest
{
    private CheckInviteValidResponse checkInviteValidResponse;
    private CheckNewVersoinResponse checkNewVersoinResponse;
    private LoginPlatformResponse loginPlatformUserResponse;
    public LoginResponse loginResponse;

    public event EventHandler<EventArgs> CheckInviteValidFailEvent;

    public event EventHandler<EventArgs> CheckInviteValidSuccessEvent;

    public event EventHandler<EventArgs> CheckNeedInviteFailEvent;

    public event EventHandler<EventArgs> CheckNeedInviteSuccessEvent;

    public event EventHandler<EventArgs> CheckNewVersionFailEvent;

    public event EventHandler<EventArgs> CheckNewVersionSuccessEvent;

    public event EventHandler<EventArgs> LoginFailEvent;

    public event EventHandler<EventArgs> LoginPlatformUserFailEvent;

    public event EventHandler<EventArgs> LoginPlatformUserSuccessEvent;

    public event EventHandler<EventArgs> LoginSuccessEvent;

    public event EventHandler<EventArgs> SendEmailFailEvent;

    public event EventHandler<EventArgs> SendEmailSuccessEvent;

    public void CheckIfHaveNewVerison()
    {
        int usingPlatform = (int) AndroidPlatformManager.instance.usingPlatform;
        string channelId = AndroidPlatformManager.instance.ChannelId;
        object[] objArray = new object[] { "index/checkVer/", GameInfo.instance.version, "/", usingPlatform, "/", channelId };
        base.path = string.Concat(objArray);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqCheckNewVersionSuccess), new BaseWWWRequest.OnFail(this.onReqCheckNewVersionFail), true, ServerType.MainServer, false);
    }

    public void CheckIfNeedInviteCode()
    {
        base.path = "index/useRegCode";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqCheckInviteSuccess), new BaseWWWRequest.OnFail(this.onReqCheckInviteFail), true, ServerType.LoginServer, false);
    }

    public void CheckInviteValid(string code)
    {
        base.path = "index/getRegCode/" + code;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqCheckInviteValidSuccess), new BaseWWWRequest.OnFail(this.onReqCheckInviteValidFail), true, ServerType.LoginServer, false);
    }

    protected virtual void OnCheckInviteValidFailEvent()
    {
        if (this.CheckInviteValidFailEvent != null)
        {
            if (this.checkInviteValidResponse != null)
            {
                this.CheckInviteValidFailEvent(this.checkInviteValidResponse.eid, EventArgs.Empty);
            }
            else
            {
                this.CheckInviteValidFailEvent(0, EventArgs.Empty);
            }
        }
    }

    protected virtual void OnCheckInviteValidSuccessEvent()
    {
        if (this.CheckInviteValidSuccessEvent != null)
        {
            this.CheckInviteValidSuccessEvent(this.checkInviteValidResponse, EventArgs.Empty);
        }
    }

    protected virtual void OnCheckNeedInviteFailEvent()
    {
        if (this.CheckNeedInviteFailEvent != null)
        {
            this.CheckNeedInviteFailEvent(this, EventArgs.Empty);
        }
    }

    protected virtual void OnCheckNeedInviteSuccessEvent()
    {
        if (this.CheckNeedInviteSuccessEvent != null)
        {
            this.CheckNeedInviteSuccessEvent(this, EventArgs.Empty);
        }
    }

    protected virtual void OnCheckNewVersionFailEvent()
    {
        int sender = 0;
        if (this.checkNewVersoinResponse != null)
        {
            sender = this.checkNewVersoinResponse.eid;
        }
        if (this.CheckNewVersionFailEvent != null)
        {
            this.CheckNewVersionFailEvent(sender, EventArgs.Empty);
        }
    }

    protected virtual void OnCheckNewVersionSuccessEvent()
    {
        if (this.CheckNewVersionSuccessEvent != null)
        {
            this.CheckNewVersionSuccessEvent(this.checkNewVersoinResponse.version, EventArgs.Empty);
        }
    }

    protected virtual void OnLoginFailEvent()
    {
        if (this.LoginFailEvent != null)
        {
            if (this.loginResponse != null)
            {
                this.LoginFailEvent(this.loginResponse.eid, EventArgs.Empty);
            }
            else
            {
                this.LoginFailEvent(0, EventArgs.Empty);
            }
        }
    }

    protected virtual void OnLoginPlatformUserFailEvent()
    {
        if (this.LoginPlatformUserFailEvent != null)
        {
            if (this.loginPlatformUserResponse != null)
            {
                this.LoginPlatformUserFailEvent(this.loginPlatformUserResponse.eid, EventArgs.Empty);
            }
            else
            {
                this.LoginPlatformUserFailEvent(0, EventArgs.Empty);
            }
        }
    }

    protected virtual void OnLoginPlatformUserSuccessEvent()
    {
        if (this.LoginPlatformUserSuccessEvent != null)
        {
            this.LoginPlatformUserSuccessEvent(this, EventArgs.Empty);
        }
    }

    protected virtual void OnLoginSuccessEvent()
    {
        if (this.LoginSuccessEvent != null)
        {
            this.LoginSuccessEvent(this, EventArgs.Empty);
        }
    }

    private void onReqCheckInviteFail(BaseWWWRequest obj)
    {
        this.OnCheckNeedInviteFailEvent();
    }

    private void onReqCheckInviteSuccess(BaseWWWRequest obj)
    {
        try
        {
            CheckNeedInviteResponse response = new JsonFx.Json.JsonReader().Read<CheckNeedInviteResponse>(this.UTF8String);
            if (response.eid != 0)
            {
                this.onReqCheckInviteFail(obj);
            }
            else
            {
                if (response.regCode == 1)
                {
                    GameData.instance.NeedInviteToRegister = true;
                }
                this.OnCheckNeedInviteSuccessEvent();
            }
        }
        catch (Exception)
        {
            this.onReqCheckInviteFail(obj);
        }
    }

    private void onReqCheckInviteValidFail(BaseWWWRequest obj)
    {
        this.OnCheckInviteValidFailEvent();
    }

    private void onReqCheckInviteValidSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.checkInviteValidResponse = new JsonFx.Json.JsonReader().Read<CheckInviteValidResponse>(this.UTF8String);
            if (this.checkInviteValidResponse.eid != 0)
            {
                this.onReqCheckInviteValidFail(obj);
            }
            else
            {
                this.OnCheckInviteValidSuccessEvent();
            }
        }
        catch (Exception)
        {
            this.onReqCheckInviteValidFail(obj);
        }
    }

    private void onReqCheckNewVersionFail(BaseWWWRequest obj)
    {
        this.OnCheckNewVersionFailEvent();
    }

    private void onReqCheckNewVersionSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.checkNewVersoinResponse = new JsonFx.Json.JsonReader().Read<CheckNewVersoinResponse>(base.UTF8String);
            if (this.checkNewVersoinResponse.eid != 0)
            {
                this.onReqCheckNewVersionFail(obj);
                z.log("[警告]检查版本失败！");
            }
            else
            {

                if (this.checkNewVersoinResponse.version.newVersionId != GameInfo.instance.version)
                {
                     z.log("[检查版本]版本不匹配，Bot版本: " + GameInfo.instance.version + "  服务器版本:" + this.checkNewVersoinResponse.version.newVersionId + "。 自动停止，请等待更新！" );
                    return;
                }
                z.log("[检查版本]检查版本成功！" + this.checkNewVersoinResponse.version.newVersionId);
                DataServer.instance.LoginServerAddress = this.checkNewVersoinResponse.loginServer;
                ServerRequestManager.instance.LoadConfigs();
                this.OnCheckNewVersionSuccessEvent();
            }
        }
        catch (Exception)
        {
            this.onReqCheckNewVersionFail(obj);
        }
    }

    private void onReqLoginFail(BaseWWWRequest obj)
    {
        this.OnLoginFailEvent();
    }

    private void onReqLoginSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.loginResponse = new JsonFx.Json.JsonReader().Read<LoginResponse>(this.UTF8String);
            if (this.loginResponse.eid != 0)
            {
                this.onReqLoginFail(obj);
            }
            else
            {
                GameData.instance.LoginResponse = this.loginResponse;
                this.SaveLoginInfo();
                this.OnLoginSuccessEvent();
            }
        }
        catch (Exception)
        {
            this.onReqLoginFail(obj);
        }
    }

    private void onReqPlatformUserLoginFail(BaseWWWRequest obj)
    {
        this.OnLoginPlatformUserFailEvent();
        z.log("[登陆失败] 选择服务器继续...." + obj.UTF8String);
        ServerRequestManager.instance.OnLoginFail();
    }

    private void onReqPlatformUserLoginSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.loginPlatformUserResponse = new JsonFx.Json.JsonReader().Read<LoginPlatformResponse>(this.UTF8String);
            if (this.loginPlatformUserResponse.eid != 0)
            {
                this.onReqPlatformUserLoginFail(obj);
            }
            else
            {
                GameData.instance.LoginResponse = this.loginPlatformUserResponse;
                z.log("[登陆成功] 选择服务器继续....");
                this.OnLoginPlatformUserSuccessEvent();
            }
        }
        catch (Exception)
        {
            this.onReqPlatformUserLoginFail(obj);
        }
    }

    private void onReqSendMailFail(BaseWWWRequest obj)
    {
        this.OnSendEmailFailEvent();
    }

    private void onReqSendMailSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.loginResponse = new JsonFx.Json.JsonReader().Read<LoginResponse>(this.UTF8String);
            if (this.loginResponse.eid != 0)
            {
                this.onReqSendMailFail(obj);
            }
            else
            {
                this.OnSendEmailSuccessEvent();
            }
        }
        catch (Exception)
        {
            this.onReqSendMailFail(obj);
        }
    }

    protected virtual void OnSendEmailFailEvent()
    {
        if (this.SendEmailFailEvent != null)
        {
            if (this.loginResponse != null)
            {
                this.SendEmailFailEvent(this.loginResponse.eid, EventArgs.Empty);
            }
            else
            {
                this.SendEmailFailEvent(0, EventArgs.Empty);
            }
        }
    }

    protected virtual void OnSendEmailSuccessEvent()
    {
        if (this.SendEmailSuccessEvent != null)
        {
            this.SendEmailSuccessEvent(this.loginResponse.cellphone, EventArgs.Empty);
        }
    }

    public void QuickRegister(string deviceId, string regCode)
    {
        base.path = "index/passportGuest/" + deviceId + "&regCode=" + regCode;
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["deviceId"] = tools.configmng.deviceUniqueIdentifier;
        data["regCode"] = regCode;
        base.SetupParams(data, new BaseWWWRequest.OnSuccess(this.onReqLoginSuccess), new BaseWWWRequest.OnFail(this.onReqLoginFail), true, ServerType.LoginServer, false);
    }

    public void Register(string userId, string password, string email, string regCode, string inviteCode)
    {
        string[] strArray = new string[] { "index/passportReg/", userId, "/", password, "/", email, "&regCode=", regCode, "&inviteCode=", inviteCode };
        base.path = string.Concat(strArray);
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["deviceId"] = tools.configmng.deviceUniqueIdentifier;
        data["uid"] = userId;
        data["pwd"] = password;
        data["email"] = email;
        data["regCode"] = regCode;
        data["invite"] = inviteCode;
        base.SetupParams(data, new BaseWWWRequest.OnSuccess(this.onReqLoginSuccess), new BaseWWWRequest.OnFail(this.onReqLoginFail), true, ServerType.LoginServer, false);
    }

    private void SaveLoginInfo()
    {
        z.instance.save_usernamepw();
    }

    public void SendPasswordEmail(string email)
    {
        base.path = "index/getPwd/" + email;
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["deviceId"] = tools.configmng.deviceUniqueIdentifier;
        data["email"] = email;

        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqSendMailSuccess), new BaseWWWRequest.OnFail(this.onReqSendMailFail), true, ServerType.LoginServer, false);
    }

    public void SignIn(string userId, string password)
    {
        base.path = "index/passportLogin/" + userId + "/" + password;
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["deviceId"] = tools.configmng.deviceUniqueIdentifier;
        data["uid"] = userId;
        data["pwd"] = password;
        base.SetupParams(data, new BaseWWWRequest.OnSuccess(this.onReqLoginSuccess), new BaseWWWRequest.OnFail(this.onReqLoginFail), true, ServerType.LoginServer, false);
    }

    public void SignInUCUser(string sid)
    {
        base.path = "index/ucLogin/" + sid;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqPlatformUserLoginSuccess), new BaseWWWRequest.OnFail(this.onReqPlatformUserLoginFail), true, ServerType.LoginServer, false);
    }
}

