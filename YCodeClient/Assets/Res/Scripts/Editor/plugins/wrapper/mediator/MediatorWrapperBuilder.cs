﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using game.mvc.view.core;
using pure.mvc.view;
using UnityEditor;
using UnityEngine;

namespace plugins.wrapper.mediator {
    internal class MediatorWrapperBuilder {
        internal const string CLASS_NAME = "MediatorWrap";

        internal static readonly string FILE = string.Format("Assets/Res/Scripts/Game/main/{0}.cs", CLASS_NAME);

        private List<string> _usingList = new List<string>();
        private Dictionary<string, Type> _mediators = new Dictionary<string, Type>();

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
            AppendUsing(typeof (MediatorFactory));
            AppendUsing(typeof (MediatorType));
            AppendUsing(typeof (BaseMediator));
            AppendUsing(typeof (FreeMediator));
            AppendUsing(typeof (CoverMediator));
            AppendUsing(typeof (ChildMediator));
            IEnumerable<Assembly> assembles = AppDomain.CurrentDomain.GetAssemblies();
            assembles = assembles.Where(a => a.FullName.Contains("Assembly"));
            foreach (var a in assembles) {
                Type[] ts = a.GetTypes();
                IEnumerable<Type> types =
                    ts.Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof (MediatorCore)));
                foreach (var t in types) {
                    CustomMediatorAttribute act = GetAttribute<CustomMediatorAttribute>(t);
                    if (act != null) {
                        if (!t.IsSubclassOf(typeof (MediatorCore))) {
                            Debug.LogError(t.Name + " is not MediatorCore");
                            continue;
                        }
                        _mediators.Add(string.IsNullOrEmpty(act.customName) ? t.Name : act.customName, t);
                        AppendUsing(t);
                    }
                }
            }
        }

        private const string STATIC_INIT = @"
        private static bool register;
        public static void Init(){
            if(!register)new MediatorWrap().Register();
            register = true;
        }";

        private void BuildFile() {
            _builder.AppendLine("//this source code was auto-generated, do not modify it");
            foreach (var u in _usingList) {
                _builder.AppendFormat("using {0};\r\n", u);
            }
            _builder.AppendLine("namespace registerWrap {");
            _builder.AppendFormat("\tpublic class {0}{1}\r\n", CLASS_NAME, "{");
            _builder.Append(STATIC_INIT + "\r\n");
            _builder.AppendLine("\t\tprivate void Register(){");
            _builder.AppendFormat("\t\t\tMediatorFactory.Add<{0}>({1}.{2});\r\n", typeof (BaseMediator).Name,
                typeof (MediatorType).Name, MediatorType.BaseMediator);
            _builder.AppendFormat("\t\t\tMediatorFactory.Add<{0}>({1}.{2});\r\n", typeof (FreeMediator).Name,
                typeof (MediatorType).Name, MediatorType.FreeMediator);
            _builder.AppendFormat("\t\t\tMediatorFactory.Add<{0}>({1}.{2});\r\n", typeof (CoverMediator).Name,
                typeof (MediatorType).Name, MediatorType.CoverMediator);
            _builder.AppendFormat("\t\t\tMediatorFactory.Add<{0}>({1}.{2});\r\n", typeof (ChildMediator).Name,
                typeof (MediatorType).Name, MediatorType.ChildMediator);
            foreach (var v in _mediators) {
                _builder.AppendFormat("\t\t\tMediatorFactory.AddCustomFactory<{0}>(\"{1}\");\r\n", v.Value.Name, v.Key);
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