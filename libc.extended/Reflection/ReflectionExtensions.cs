using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace libc.extended.Reflection {
    public static class ReflectionExtensions {
        public static List<FieldInfo> GetConstants(this Type type) {
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        }
    }
}