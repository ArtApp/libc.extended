using System;
using System.Collections.Generic;
using System.Net.Http;
using Flurl;
using Flurl.Http;

namespace libc.extended.Httping
{
    public interface IHttpClientFactory : IDisposable
    {
        IFlurlClient GetProxied(Url proxyUrl);

        IFlurlClient Get(Url url);

        IFlurlClient CreateProxied(Url proxyUrl);

        IFlurlClient Create(Url url);

        FlurlResponse GetProxiedRetry(string url, List<ProxyInfo> proxies, int retry, Action<IFlurlRequest> configure,
            out List<Exception> exceptions);

        FlurlResponse PostProxiedRetry(string url, List<ProxyInfo> proxies, int retry, Action<IFlurlRequest> configure,
            out List<Exception> exceptions, HttpContent content);

        FlurlResponse GetRetry(string url, int retry, Action<IFlurlRequest> configure, out List<Exception> exceptions);

        FlurlResponse PostRetry(string url, int retry, Action<IFlurlRequest> configure, out List<Exception> exceptions,
            HttpContent content);
    }
}