using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

public class WWW
{
    private HttpClient client;
    private ShipHttpClientHandler client_handler;
    private byte[] content;
    private Dictionary<string, string> post_data;
    private string requrl;
    private HttpResponseMessage response;
    private Dictionary<string, string> response_head;
    private string serror;

    public WWW(string url, byte[] postData, Hashtable headers, string cookie)
    {
        this.requrl = "";
        this.post_data = new Dictionary<string, string>();
        this.content = null;
        this.response_head = new Dictionary<string, string>();
        this.serror = null;
        client = null;
        this.InitWWW(url, postData, headers, cookie);
    }

    public void Dispose()
    {
    }

    internal void do_request()
    {
        if (client != null)
        {
            try
            {
                this.response = client.GetAsync(new Uri(this.requrl,true) ).Result;
                this.response.EnsureSuccessStatusCode();
                this.content = this.response.Content.ReadAsByteArrayAsync().Result;
            }
            catch (HttpRequestException exception)
            {
                this.serror = exception.Message;
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

    public string data
    {
        get
        {
            return this.text;
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

    public string text
    {
        get
        {
            return Encoding.UTF8.GetString(this.content);
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

