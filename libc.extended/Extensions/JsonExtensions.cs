using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
namespace libc.extended.Extensions {
    public static class JsonExtensions {
        private static readonly CamelCasePropertyNamesContractResolver resolver =
            new CamelCasePropertyNamesContractResolver();
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings {
            ContractResolver = resolver
        };
        private static readonly JsonSerializerSettings safeSettings = new JsonSerializerSettings {
            Error = (sender, args) => {
                args.ErrorContext.Handled = true;
            }
        };
        public static string S(this object o, bool indent = false) {
            return JsonConvert.SerializeObject(o, indent ? Formatting.Indented : Formatting.None);
        }
        public static string S(this object o, JsonSerializerSettings settings) {
            return JsonConvert.SerializeObject(o, settings);
        }
        public static T D<T>(this string json, JsonSerializerSettings settings = null) {
            return settings == null
                ? JsonConvert.DeserializeObject<T>(json)
                : JsonConvert.DeserializeObject<T>(json, settings);
        }
        public static object D(this string json, Type type, JsonSerializerSettings settings = null) {
            return settings == null
                ? JsonConvert.DeserializeObject(json, type)
                : JsonConvert.DeserializeObject(json, type, settings);
        }
        public static object D(this string json, JsonSerializerSettings settings = null) {
            return settings == null
                ? JsonConvert.DeserializeObject(json)
                : JsonConvert.DeserializeObject(json, settings);
        }
        public static T DSafe<T>(this string json) {
            return JsonConvert.DeserializeObject<T>(json, safeSettings);
        }
        public static object DSafe(this string json, Type type) {
            return JsonConvert.DeserializeObject(json, type, safeSettings);
        }
        public static object DSafe(this string json) {
            return JsonConvert.DeserializeObject(json, safeSettings);
        }
        public static string S_camelCase(this object s) {
            return s.S(settings);
        }
        public static JToken FromJson(this string s) {
            return string.IsNullOrWhiteSpace(s) ? new JObject() : JToken.Parse(s);
        }
        public static string GetString(this JToken j, string key) {
            return j[key]?.ToString();
        }
        public static List<string> GetStringArray(this JToken j, string key) {
            return j.GetString(key).D<List<string>>();
        }
        public static Guid? GetGuidNullable(this JToken j, string key) {
            return Guid.TryParse(j.GetString(key), out var res) ? res : (Guid?) null;
        }
        public static Guid GetGuid(this JToken j, string key, Guid defaultValue = default) {
            return Guid.TryParse(j.GetString(key), out var res) ? res : defaultValue;
        }
        public static int? GetIntNullable(this JToken j, string key) {
            return int.TryParse(j.GetString(key), out var res) ? res : (int?) null;
        }
        public static int GetInt(this JToken j, string key, int defaultValue = default) {
            return int.TryParse(j.GetString(key), out var res) ? res : defaultValue;
        }
        public static double? GetDoubleNullable(this JToken j, string key) {
            return double.TryParse(j.GetString(key), out var res) ? res : (double?) null;
        }
        public static double GetDouble(this JToken j, string key, double defaultValue = default) {
            return double.TryParse(j.GetString(key), out var res) ? res : defaultValue;
        }
        public static long? GetLongNullable(this JToken j, string key) {
            return long.TryParse(j.GetString(key), out var res) ? res : (long?) null;
        }
        public static long GetInt(this JToken j, string key, long defaultValue = default) {
            return long.TryParse(j.GetString(key), out var res) ? res : defaultValue;
        }
    }
}