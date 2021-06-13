namespace libc.extended.Httping
{
    /// <summary>
    ///     information of a proxy
    /// </summary>
    public class ProxyInfo
    {
        public long Id { get; set; }
        public string Scheme { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }

        public string GetUrl()
        {
            return $"{Scheme.ToLower()}://{Ip}:{Port}";
        }
    }
}