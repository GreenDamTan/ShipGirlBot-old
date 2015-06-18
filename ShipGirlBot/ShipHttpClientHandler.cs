using ShipGirlBot;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

internal class ShipHttpClientHandler : HttpClientHandler
{
    private string cookie = null;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Cookie", this.cookie);
        request.Headers.Add("X-Unity-Version", "4.6.2f1");
        request.Headers.Add("charset", "UTF-8");
        request.Headers.Add("UserAgent", GameInfo.instance.UA);
        request.Headers.Add("Connection", "Keep-Alive");
        return base.SendAsync(request, cancellationToken);
    }

    internal void setcookie(string scookie)
    {
        this.cookie = scookie;
    }
}

