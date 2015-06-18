using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

public class WWW : IDisposable
{
    private bool usezip = false;
    private HttpClient client;
    private ShipHttpClientHandler client_handler;
    private byte[] content;
    private byte[] post_data = null;
    private string requrl;
    private HttpResponseMessage response;
    private Dictionary<string, string> response_head;
    private string serror;

    public void Dispose()
    {
        content = null;
        post_data = null;
        response = null;
        client = null;
        client_handler = null;
        GC.SuppressFinalize(this);
    }

    public WWW(string url, byte[] postData, Hashtable headers, string cookie, bool usezipornot)
    {
        this.usezip = usezipornot;
        this.requrl = "";
        this.content = null;
        this.post_data = null;
        this.response_head = new Dictionary<string, string>();
        this.serror = null;
        client = null;
        this.InitWWW(url, postData, headers, cookie);
    }
    internal void do_request()
    {
        if (client != null)
        {
            try
            {
                if(post_data == null)
                {
                    this.response = client.GetAsync(new Uri(this.requrl, true)).Result;
                }
                else
                {
                    Dictionary<string, string> val = new Dictionary<string, string>();
                    val[Encoding.UTF8.GetString(post_data)] = "";
                    var cc = new FormUrlEncodedContent( val);
                    this.response = client.PostAsync(new Uri(this.requrl, true), cc).Result;
                }
                
                this.response.EnsureSuccessStatusCode();
                this.content = this.response.Content.ReadAsByteArrayAsync().Result;
                this.client = null;
                this.client_handler = null;
            }
            catch (Exception exception)
            {
                this.serror = exception.Message +"\r\n" + exception.StackTrace + "\r\n";
            }
        }
    }

    public static string EscapeURL(string s)
    {
        Encoding e = Encoding.UTF8;
        return EscapeURL(s, e);
    }

    public static string EscapeURL(string s, Encoding e)
    {
        if (s == null)
        {
            return null;
        }
        if (s == string.Empty)
        {
            return string.Empty;
        }
        if (e == null)
        {
            return null;
        }
        return WWWTranscoder.URLEncode(s, e);
    }

    private void InitWWW(string surl, byte[] postData, Hashtable iHeaders, string cookie)
    {
        this.requrl = surl;
        post_data = postData;
        if (client == null)
        {
            client_handler = new ShipHttpClientHandler();
            client_handler.CookieContainer = new System.Net.CookieContainer();
            if (cookie != null && cookie != "")
            {
                string k = cookie.Substring(0, 7);
                string v = cookie.Substring(8);
                client_handler.CookieContainer.Add(new Uri(surl), new System.Net.Cookie(k, v, "/"));
            }
            client_handler.UseCookies = true;

            client = new HttpClient(client_handler);
        }
    }

    public void setcookie(string cookie)
    {
        client_handler.setcookie(cookie);
    }

    public static string UnEscapeURL(string s)
    {
        Encoding e = Encoding.UTF8;
        return UnEscapeURL(s, e);
    }

    public static string UnEscapeURL(string s, Encoding e)
    {
        if (s == null)
        {
            return null;
        }
        if ((s.IndexOf('%') == -1) && (s.IndexOf('+') == -1))
        {
            return s;
        }
        return WWWTranscoder.URLDecode(s, e);
    }

    public byte[] bytes
    {
        get
        {
            return this.content;
        }
    }

    internal static Encoding DefaultEncoding
    {
        get
        {
            return Encoding.UTF8;
        }
    }

    public string error
    {
        get
        {
            return this.serror;
        }
    }

    public HttpResponseHeaders responseHeaders
    {
        get
        {
            return this.response.Headers;
        }
    }

    public string url
    {
        get
        {
            return this.requrl;
        }
    }
}

