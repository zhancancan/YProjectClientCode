﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using pure.utils.dlltools;
using UnityEditor;
using UnityEngine;

namespace plugins.wrapper.mono {
    public class MonoWrapBuilder {
        private const string CLASS_NAME = "MonoWrap";

        internal static readonly string FILE = string.Format("Assets/Res/Scripts/Game/main/{0}.cs",
            CLASS_NAME);

        private List<string> _usingList = new List<string>();
        private Dictionary<string, Type> _treeTypes = new Dictionary<string, Type>();
        private StringBuilder _builder = new StringBuilder();

        private static T GetAttribute<T>(Type runtime) where T : Attribute {
            object[] attributes = runtime.GetCustomAttributes(typeof (T), false);
            if (attributes.Length > 0) return attributes[0] as T;
            return null;
        }

        internal void Start() {
            CollectTypes();
            BuildFile();
            SaveFile();
        }

        private void CollectTypes() {
            AppendUsing(typeof (MonoDllFactory));
            IEnumerable<Assembly> assembles = AppDomain.CurrentDomain.GetAssemblies();
            assembles = assembles.Where(a => a.FullName.Contains("Assembly"));
            foreach (var a in assembles) {
                Type[] ts = a.GetTypes();
                IEnumerable<Type> types =
                    ts.Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof (MonoBehaviour)));
                foreach (var t in types) {
                    MonoDllAttribute act = GetAttribute<MonoDllAttribute>(t);
                    if (act != null) {
                        _treeTypes.Add(act.name, t);
                        AppendUsing(t);
                    }
                }
            }
        }

        private const string STATIC_INIT = @"
        private static bool register;
        public static void Init(){
            if(!register){  
                new MonoWrap().Register();
            }
            register = true;
        }";
        private const string INITANCE_INIT = @"
        private void Register(){  
            RegMono();
        }";

        private void BuildFile() {
            _builder.AppendLine("//this source code was auto-generated, do not modify it");
            foreach (var u in _usingList) {
                _builder.AppendFormat("using {0};\r\n", u);
            }
            _builder.AppendLine("namespace registerWrap {");
            _builder.AppendFormat("\tpublic class {0}{1}\r\n", CLASS_NAME, "{");
            _builder.Append(STATIC_INIT + "\r\n");
            _builder.Append(INITANCE_INIT + "\r\n");
            _builder.AppendLine("\t\tprivate void RegMono(){");
            foreach (var v in _treeTypes) {
                _builder.AppendFormat("\t\t\tMonoDllFactory.Register<{0}>(\"{1}\");\r\n", v.Value.Name, v.Key);
            }
            _builder.AppendLine("\t\t}");
            _builder.AppendLine("\t}");
            _builder.AppendLine("}");
        }

        private void AppendUsing(Type runtime) {
            WrapperTools.AppendUsing(runtime, _usingList);
        }

        private void SaveFile() {
            string dir = Path.GetDirectoryName(FILE);
            if (string.IsNullOrEmpty(dir)) return;
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
            using (StreamWriter w = new StreamWriter(FILE, false, Encoding.UTF8)) {
                w.Write(_builder.ToString());
                w.Flush();
                w.Close();
            }
            AssetDatabase.Refresh();
        }
    }
}