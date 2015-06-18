using Ionic.Zlib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

public class BaseWWWRequest : MonoBehaviour
{
    protected bool ignoreAllError;
    public string path;
    public BasicResponse responseData;
    public Dictionary<string, string> sendData;
    private DataServer server;
    private ServerType useServerType;
    public bool useZip =false;
    public WWW www;

    public event OnFail FailEvent;

    public event OnSuccess SuccessEvent;


    public void Dispose()
    {
        responseData = null;
        www = null;
    }

    private void CheckNewMailInfo(BasicResponse res)
    {
        if ((res != null) && (res.newMailInfo != null))
        {
            GameData.instance.NewMailNum = res.newMailInfo.newMailNum;
        }
    }

    private void CheckQuestUpdater(BasicResponse res)
    {
        if ((res != null) && (res.updateTaskVo != null))
        {
            GameData.instance.UpdateQuest(res.updateTaskVo);
        }
    }

    public void CheckSystemDataUpdater(BasicResponse res)
    {
        this.CheckQuestUpdater(res);
        this.CheckNewMailInfo(res);
    }

    private static byte[] Decompress(byte[] data)
    {
        byte[] buffer2;
        using (ZlibStream stream = new ZlibStream(new MemoryStream(data), CompressionMode.Decompress))
        {
            byte[] buffer = new byte[0x1000];
            using (MemoryStream stream2 = new MemoryStream())
            {
                int count = 0;
                do
                {
                    count = stream.Read(buffer, 0, 0x1000);
                    if (count > 0)
                    {
                        stream2.Write(buffer, 0, count);
                    }
                }
                while (count > 0);
                buffer2 = stream2.ToArray();
            }
        }
        return buffer2;
    }

    private void OnDestroy()
    {
        this.SuccessEvent = null;
        this.FailEvent = null;
    }

    public void SetupParams(Dictionary<string, string> data, OnSuccess successInvoke = null, OnFail failInvoke = null, bool autoStart = true, ServerType useServerType = 0, bool isuseZip = false)
    {
        this.server = DataServer.instance;
        this.sendData = data;
        this.useServerType = useServerType;
        //默认全部打开压缩
       	this.useZip = true;

        if (successInvoke != null)
        {
            this.SuccessEvent = (OnSuccess) Delegate.Combine(this.SuccessEvent, successInvoke);
        }
        if (failInvoke != null)
        {
            this.FailEvent = (OnFail) Delegate.Combine(this.FailEvent, failInvoke);
        }
        if (autoStart)
        {
            this.StartRequest();
        }
    }

    public void ShowNetWorkError(string error)
    {
        z.log("[错误]" + ((this.responseData != null) ? this.responseData.eidstring : "未知错误....大概是网络问题") + "\r\n" + error);
    }

    public void ShowServerError()
    {

        z.log("[错误]" + ((this.responseData != null) ? this.responseData.eidstring : "未知错误....大概是网络问题") );
    }

    public void StartRequest()
    {
        this.waitforWWW();
    }

    private void waitforWWW()
    {
        waitWWW twww = new waitWWW {
            baserequest = this
        };
        int num = 0;
        while (twww.MoveNext())
        {
            num++;
            if (num >= 100)
            {
                break;
            }
        }
    }

    public string UTF8String
    {
        get
        {
            byte[] bytes;
            if(this.www.bytes == null || this.www.error != null)
            {
                return "";
            }
            if (this.useZip)
            {
                bytes = Decompress(this.www.bytes);
                return Encoding.UTF8.GetString(bytes);
            }
            bytes = this.www.bytes;
            int length = bytes.Length;
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] != 0)
                {
                    break;
                }
                length--;
            }
            byte[] buffer2 = new byte[length];
            int num3 = bytes.Length - buffer2.Length;
            for (int j = 0; j < buffer2.Length; j++)
            {
                buffer2[j] = bytes[j + num3];
            }
            return Encoding.UTF8.GetString(buffer2);
        }
    }

    public delegate void OnFail(BaseWWWRequest obj);

    public delegate void OnSuccess(BaseWWWRequest obj);

    [CompilerGenerated]
    private sealed class waitWWW : IEnumerator<object>, IDisposable, IEnumerator
    {
        internal WWW _current;
        internal int _PC = 0;
        internal BaseWWWRequest baserequest;
        internal string c1;
        internal int p0;
        internal string platformInfo_3;
        internal string version_2;

        [DebuggerHidden]
        public void Dispose()
        {
            this._PC = -1;
            _current = null;
            baserequest = null;
        }

        public bool MoveNext()
        {
            this.p0 = (int) AndroidPlatformManager.instance.usingPlatform;
            this.c1 = AndroidPlatformManager.instance.ChannelId;
            this.version_2 = GameInfo.instance.version;
            object[] objArray = new object[] { "&market=", this.p0, "&channel=", this.c1, "&version=", this.version_2 };
            this.platformInfo_3 = string.Concat(objArray);
            this.baserequest.www = this.baserequest.server.GetWWWOf(this.baserequest.path, this.baserequest.sendData, this.baserequest.useServerType, this.baserequest.useZip, this.platformInfo_3);
            this._current = this.baserequest.www;
            this._PC = 1;
            this._current.do_request();
            if (string.IsNullOrEmpty(this.baserequest.www.error))
            {
                this.baserequest.server.TryUpdateSettionCookie(this.baserequest.www);
                if (this.baserequest.SuccessEvent != null)
                {
                    this.baserequest.SuccessEvent(this.baserequest);
                }
                this.baserequest.tryFectchErrorCode(this.baserequest);
                return false;
            }
            if (!this.baserequest.ignoreAllError)
            {
                this.baserequest.ShowNetWorkError(this.baserequest.www.error);
            }
            if (this.baserequest.FailEvent != null)
            {
                this.baserequest.FailEvent(this.baserequest);
            }
            this.baserequest.CheckSystemDataUpdater(this.baserequest.responseData);
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return this._current;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return this._current;
            }
        }
    }

    internal void tryFectchErrorCode(BaseWWWRequest baseWWWRequest)
    {
        try
        {
            BasicResponse br = new JsonFx.Json.JsonReader().Read<BasicResponse>(this.UTF8String);
            if (br.eid != 0)
            {
                z.log("[错误]"+ br.eidstring);
                if(br.eid== -411)
                {
                    z.log("[强制修复 ]" +br.eidstring + "战斗状态出错，主动回港。。。");
                    ServerRequestManager.instance.NotifyPVEBackHome();
                }
                if (br.eid == -9994 || br.eid == -9995 ||br.eid == -9997)
                {
                    z.log("[登陆失效或已在别处登陆 ]" + br.eidstring + " 半小时内尝试重新登陆。。。");
                    ServerRequestManager.instance.NotifyRelogin();
                }
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
        }
    }
}

