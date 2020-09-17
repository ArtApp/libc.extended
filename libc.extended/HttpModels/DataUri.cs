using System;
using System.Linq;
namespace libc.extended.HttpModels {
    public class DataUri {
        public string Extension { get; set; }
        public byte[] Data { get; set; }
        public static bool TryParse(string dataUri, out DataUri res) {
            try {
                var match = System.Text.RegularExpressions.Regex.Match(dataUri, @"data:(?<type>.+?);base64,(?<data>.+)");
                var base64Data = match.Groups["data"].Value;
                var contentType = match.Groups["type"].Value;
                var binData = Convert.FromBase64String(base64Data);
                var ext = "";
                if (has(contentType, "png"))
                    ext = ".png";
                else if (has(contentType, "jpg", "jpeg"))
                    ext = ".jpg";
                else if (has(contentType, "gif"))
                    ext = ".gif";
                else if (has(contentType, "bmp"))
                    ext = ".bmp";
                else if (has(contentType, "svg"))
                    ext = ".svg";
                else
                    res = null;
                res = new DataUri {
                    Data = binData,
                    Extension = ext
                };
                return true;
            } catch {
                res = null;
                return false;
            }
        }
        private static bool has(string c, params string[] k) {
            var kk = c.ToLower();
            return k.Any(kk.Contains);
        }
    }
}