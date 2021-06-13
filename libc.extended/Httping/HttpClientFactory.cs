using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Flurl;
using Flurl.Http;

namespace libc.extended.Httping
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private static readonly ConcurrentDictionary<string, IFlurlClient> cache =
            new ConcurrentDictionary<string, IFlurlClient>();

        public IFlurlClient GetProxied(Url proxyUrl)
        {
            return cache.AddOrUpdate(proxyUrl, addProxied, updateProxied);
        }

        public IFlurlClient Get(Url url)
        {
            return cache.AddOrUpdate(url, add, update);
        }

        public IFlurlClient CreateProxied(Url proxyUrl)
        {
            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy(proxyUrl),
                UseProxy = true
            };

            var client = new HttpClient(handler);

            return new FlurlClient(client);
        }

        public IFlurlClient Create(Url url)
        {
            return new FlurlClient(url);
        }

        public void Dispose()
        {
            foreach (var item in cache)
                if (!item.Value.IsDisposed)
                    item.Value.Dispose();

            cache.Clear();
        }

        public FlurlResponse GetProxiedRetry(string url, List<ProxyInfo> proxies, int retry,
            Action<IFlurlRequest> configure, out List<Exception> exceptions)
        {
            if (proxies != null && proxies.Count > 0)
            {
                var k = callRetryProxied(url, proxies, configure, out exceptions, HttpMethod.Get, null, retry);

                if (k.Response.IsSuccessStatusCode) return k;
                retry = 2;
            }

            return GetRetry(url, retry, configure, out exceptions);
        }

        public FlurlResponse PostProxiedRetry(string url, List<ProxyInfo> proxies, int retry,
            Action<IFlurlRequest> configure, out List<Exception> exceptions, HttpContent content)
        {
            if (proxies != null && proxies.Count > 0)
            {
                var k = callRetryProxied(url, proxies, configure, out exceptions, HttpMethod.Post, content, retry);

                if (k.Response.IsSuccessStatusCode) return k;
                retry = 2;
            }

            return GetRetry(url, retry, configure, out exceptions);
        }

        public FlurlResponse GetRetry(string url, int retry, Action<IFlurlRequest> configure,
            out List<Exception> exceptions)
        {
            var client = Get(url);
            var k = callRetry(client, url, configure, out exceptions, HttpMethod.Get, null, retry);

            return new FlurlResponse
            {
                Response = k,
                BadProxies = new List<ProxyInfo>()
            };
        }

        public FlurlResponse PostRetry(string url, int retry, Action<IFlurlRequest> configure,
            out List<Exception> exceptions, HttpContent content)
        {
            var client = Get(url);
            var k = callRetry(client, url, configure, out exceptions, HttpMethod.Post, content, retry);

            return new FlurlResponse
            {
                Response = k,
                BadProxies = new List<ProxyInfo>()
            };
        }

        private static void defaultFlUrlRequestConfiguration(IFlurlRequest request)
        {
            request.WithTimeout(7);
        }

        private FlurlResponse callRetryProxied(string url, List<ProxyInfo> proxies, Action<IFlurlRequest> configure,
            out List<Exception> exceptions, HttpMethod method, HttpContent content = null, int retry = 5)
        {
            var res = new FlurlResponse
            {
                Response = new HttpResponseMessage(HttpStatusCode.Unused),
                BadProxies = new List<ProxyInfo>()
            };

            var cnt = -1;
            exceptions = new List<Exception>();

            while (++cnt < proxies.Count)
            {
                var p = proxies[cnt];

                using (var client = GetProxied(p.GetUrl()))
                {
                    try
                    {
                        res.Response = callRetry(client, url, configure, out var exList, method, content, retry);

                        if (res.Response.IsSuccessStatusCode) break;
                        res.BadProxies.Add(p);
                        exceptions.Add(exList.FirstOrDefault());
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                        res.BadProxies.Add(p);
                    }
                }
            }

            return res;
        }

        private HttpResponseMessage callRetry(IFlurlClient client, string url,
            Action<IFlurlRequest> configure, out List<Exception> exceptions, HttpMethod method,
            HttpContent content = null,
            int retry = 5)
        {
            var res = new HttpResponseMessage(HttpStatusCode.Unused);
            var cnt = -1;
            exceptions = new List<Exception>();

            while (++cnt < retry)
            {
                res = call(client, url, configure, out var exception, method, content);

                if (res.IsSuccessStatusCode) break;
                exceptions.Add(exception);
            }

            return res;
        }

        private HttpResponseMessage call(IFlurlClient client, string url,
            Action<IFlurlRequest> configure, out Exception exception, HttpMethod method, HttpContent content = null)
        {
            exception = null;

            try
            {
                var req = client.Request(url);
                var x = configure ?? defaultFlUrlRequestConfiguration;
                x(req);

                if (method == HttpMethod.Post) return req.PostAsync(content).Result.ResponseMessage;
                if (method == HttpMethod.Delete) return req.DeleteAsync().Result.ResponseMessage;
                if (method == HttpMethod.Head) return req.HeadAsync().Result.ResponseMessage;
                if (method == HttpMethod.Options) return req.OptionsAsync().Result.ResponseMessage;
                if (method == HttpMethod.Put) return req.PutAsync(content).Result.ResponseMessage;

                return req.GetAsync().Result.ResponseMessage;
            }
            catch (Exception ex)
            {
                exception = ex;

                return new HttpResponseMessage(HttpStatusCode.Unused)
                {
                    Content = new StringContent(ex.ToString())
                };
            }
        }

        private IFlurlClient updateProxied(string url, IFlurlClient client)
        {
            if (!client.IsDisposed) return client;

            return CreateProxied(url);
        }

        private IFlurlClient addProxied(string url)
        {
            return CreateProxied(url);
        }

        private IFlurlClient update(string url, IFlurlClient client)
        {
            if (!client.IsDisposed) return client;

            return Create(url);
        }

        private IFlurlClient add(string url)
        {
            return Create(url);
        }
    }
}