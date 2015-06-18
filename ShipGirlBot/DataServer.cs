using JsonFx.Json;
using ShipGirlBot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

public class DataServer : MonoBehaviour
{
    private string choosedServerAddress = "http://jtest.jianniang.com/";
    private string cookie = null;
    private long lastCallTime;
    private string loginServer;
    public string mainServerAddress = "http://jtest.jianniang.com/";
    private static DataServer mInstance;
    public bool removeSessionForTest;
    private static string secretKey = "FHQTuRFQMQgqdJLydl0m";
    public string[] servers = new string[] { "http://jtest.jianniang.com/", "http://login.version.p7game.com/" };
    public int sessionExpireTime = 900;
    public PublicServers usingPublicServer = PublicServers.p7game;

    public DataServer()
    {
        this.mainServerAddress = this.servers[(int) this.usingPublicServer];
        this.loginServer = this.mainServerAddress;
        this.choosedServerAddress = this.mainServerAddress;
    }

    private byte[] GetBytes(string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }

    public WWW GetWWWOf(string path, Dictionary<string, string> ddd = null, ServerType useServerType = 0, bool useZip = false, string extraInfo = "")
    {
        this.lastCallTime = DateTime.Now.Ticks;
        Hashtable headers = new Hashtable();
        string choosedServerAddress = this.choosedServerAddress;
        if (useServerType == ServerType.MainServer)
        {
            choosedServerAddress = this.mainServerAddress;
        }
        else if (useServerType == ServerType.LoginServer)
        {
            choosedServerAddress = this.loginServer;
        }
        string str2 = "&t=" + DateTime.Now.Ticks;
        string str3 = "&e=" + Md5(path + str2);
        string str4 = string.Empty;
        if (useZip)
        {
            str4 = "&gz=1";
        }
        WWW www = null;
        if (ddd != null)
        {
            string str = JsonWriter.Serialize(ddd);
            www = new WWW(choosedServerAddress + path + str2 + str3 + str4 + extraInfo, this.GetBytes(str), headers, cookie);
        }
        else
        {
            www = new WWW(choosedServerAddress + path + str2 + str3 + str4 + extraInfo, null, headers, cookie);
        }

        return www;
    }

    public static string Md5(string strToEncrypt)
    {
        byte[] bytes = new UTF8Encoding().GetBytes(strToEncrypt + secretKey);
        byte[] buffer2 = new MD5CryptoServiceProvider().ComputeHash(bytes);
        string str = string.Empty;
        for (int i = 0; i < buffer2.Length; i++)
        {
            str = str + Convert.ToString(buffer2[i], 0x10).PadLeft(2, '0');
        }
        return str.PadLeft(0x20, '0');
    }

    public void TryUpdateSettionCookie(WWW www)
    {
        if (www.responseHeaders.Contains("SET-COOKIE"))
        {
            char[] separator = new char[] { ';' };
            foreach (string str in www.responseHeaders.GetValues("SET-COOKIE"))
            {
                //Form1.log(str);
                foreach (string str2 in str.Split(separator))
                {
                    if (!(string.IsNullOrEmpty(str2) || !str2.Substring(0, 7).ToLower().Equals("hf_skey")))
                    {
                        this.cookie = str2;
                        break;
                    }
                }
            }
        }
    }

    public string ChoosedServerAddress
    {
        get
        {
            return this.choosedServerAddress;
        }
        set
        {
            this.choosedServerAddress = value;
        }
    }

    public static DataServer instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new DataServer();
            }
            return mInstance;
        }
    }

    public bool IsSessionExpired
    {
        get
        {
            if (this.lastCallTime <= 0L)
            {
                return false;
            }
            TimeSpan span = new TimeSpan(DateTime.Now.Ticks - this.lastCallTime);
            return (span.Seconds >= (this.sessionExpireTime - 1));
        }
    }

    public string LoginServerAddress
    {
        get
        {
            return this.loginServer;
        }
        set
        {
            this.loginServer = value;
        }
    }

    public string MainServerAddress
    {
        get
        {
            return this.mainServerAddress;
        }
        set
        {
            this.mainServerAddress = value;
        }
    }
}

