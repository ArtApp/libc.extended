using System.Collections.Generic;
using System.Net.Http;
namespace libc.extended.Httping {
    public class FlurlResponse {
        public HttpResponseMessage Response { get; set; }
        public List<ProxyInfo> BadProxies { get; set; } = new List<ProxyInfo>();
    }
}