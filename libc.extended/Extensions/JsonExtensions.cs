using System;
using System.Text.Json;

namespace libc.extended.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null,
            DictionaryKeyPolicy = null
        };

        private static readonly JsonSerializerOptions optionsIndented = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null,
            DictionaryKeyPolicy = null
        };

        private static readonly JsonSerializerOptions optionsCamelCase = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        };

        private static readonly JsonSerializerOptions optionsCamelCaseIndented = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        };

        public static string S(this object o, bool indent = false)
        {
            return JsonSerializer.Serialize(o, indent ? optionsIndented : options);
        }

        public static string S(this object o, JsonSerializerOptions serializerOptions)
        {
            return JsonSerializer.Serialize(o, serializerOptions);
        }

        public static T D<T>(this string json, JsonSerializerOptions serializerOptions = null)
        {
            return serializerOptions == null
                ? JsonSerializer.Deserialize<T>(json, options)
                : JsonSerializer.Deserialize<T>(json, serializerOptions);
        }

        public static object D(this string json, Type type, JsonSerializerOptions serializerOptions = null)
        {
            return serializerOptions == null
                ? JsonSerializer.Deserialize(json, type, options)
                : JsonSerializer.Deserialize(json, type, serializerOptions);
        }

        public static object D(this string json, JsonSerializerOptions serializerOptions = null)
        {
            return serializerOptions == null
                ? JsonSerializer.Deserialize<object>(json, options)
                : JsonSerializer.Deserialize<object>(json, serializerOptions);
        }

        public static string S_camelCase(this object s, bool indented = false)
        {
            return s.S(indented ? optionsCamelCase : optionsCamelCaseIndented);
        }
    }
}