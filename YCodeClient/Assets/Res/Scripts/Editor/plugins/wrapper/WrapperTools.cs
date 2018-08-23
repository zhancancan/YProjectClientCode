using System;
using System.Collections.Generic;
using UnityEngine;

namespace plugins.wrapper {
    internal static class WrapperTools {
        internal static void AppendUsing(Type runtime, List<string> usingList) {
            string full = runtime.FullName;
            if (string.IsNullOrEmpty(full)) return;
            int index = full.LastIndexOf(".", StringComparison.Ordinal);
            if (index == -1) {
                return;
            }
            string package = full.Substring(0, index);
            if (!usingList.Contains(package)) {
                usingList.Add(package);
            }
        }

        internal static string GetTypeName(Type type, List<string> usingList) {
            if (string.IsNullOrEmpty(type.FullName)) return string.Empty;
            if (type == typeof (string)) return "string";
            if (type == typeof (int)) return "int";
            if (type == typeof (uint)) return "uint";
            if (type == typeof (long)) return "long";
            if (type == typeof (ulong)) return "ulong";
            if (type == typeof (short)) return "short";
            if (type == typeof (ushort)) return "ushort";
            if (type == typeof (float)) return "float";
            if (type == typeof (byte)) return "byte";
            if (type == typeof (double)) return "double";
            if (type == typeof (bool)) return "bool";
            if (type.IsArray) {
                string eleType = GetTypeName(type.GetElementType(), usingList);
                return eleType + "[]";
            }
            if (type.FullName.Contains("System.Collections.Generic.List")) {
                Type[] args = type.GetGenericArguments();
                if (args.Length == 1) return "List<" + GetTypeName(args[0], usingList) + ">";
            } else if (type.IsNested) {
                int index = type.FullName.LastIndexOf(".", StringComparison.Ordinal)+1;
                string p = type.FullName.Substring(index, type.FullName.Length - index);
                p = p.Replace("+", ".");
                AppendUsing(type, usingList);
                return p;
            } else if (type.FullName.Contains(".")) {
                AppendUsing(type, usingList);
                return type.Name;
            }
            throw new UnityException(" not support typeOf Data " + type.FullName);
        }
    }
}