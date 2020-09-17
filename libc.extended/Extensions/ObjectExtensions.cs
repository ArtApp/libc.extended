using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
namespace libc.extended.Extensions {
    public static class ObjectToDictionaryHelper {
        public static IDictionary<string, object> ToDictionary(this object source) {
            if (source == null)
                throw new NullReferenceException(
                    "Unable to convert anonymous object to a dictionary. The source anonymous object is null.");
            var props = source.GetType().GetRuntimeProperties();
            var res = props.ToDictionary(item => item.Name, item => item.GetValue(source));
            return res;
        }
        public static void AssertNotNull(this object item, string msg = "شناسه اشتباه است") {
            if (item == null) throw new Exception(msg);
        }
        public static dynamic RemoveProperties(this object source, params string[] properties) {
            if (properties == null || properties.Length == 0) return source;
            if (source == null) return null;
            var dic = source.ToDictionary();
            foreach (var property in properties)
                if (dic.ContainsKey(property))
                    dic.Remove(property);
            dynamic res = new ExpandoObject();
            var k = (ICollection<KeyValuePair<string, object>>) res;
            foreach (var kv in dic) k.Add(kv);
            return res;
        }
    }
}