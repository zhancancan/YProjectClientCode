using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using edit.pure.tools.reflection;
using pure.utils.dlltools;
using UnityEditor;
using UnityEngine;

namespace mono {
    public static class MonoDllWrap {
        [InitializeOnLoadMethod]
        public static void Register() {
            IEnumerable<Assembly> assembles = UnityEditorReflectionTool.GetCustomAssembles().ToArray();
            foreach (var a in assembles) {
                Type[] ts = a.GetTypes();
                IEnumerable<Type> types =
                    ts.Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof (MonoBehaviour)));
                foreach (var t in types) {
                    object[] attributes = t.GetCustomAttributes(typeof (MonoDllAttribute), false);
                    if (attributes.Length == 0) continue;
                    MonoDllAttribute att = attributes[0] as MonoDllAttribute;
                    if (att != null) {
                        MonoDllFactory.Register(att.name, t);
                    }
                }
            }
        }
    }
}