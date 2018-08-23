﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using edit.pure.tools.reflection;
using pure.refactor.property;
using pure.stateMachine.machine.core;
using pure.treeComp;
using UnityEditor;

namespace plugins.wrapper.property {
    internal partial class PropertyWrapperBuilder {
        internal const string CLASS_NAME = "PropertyWrap";

        internal static readonly string FILE = string.Format("Assets/Res/Scripts/Game/main/{0}.cs", CLASS_NAME);

        private const string STATIC_INIT = @"
        private static bool register;
        public static void Init(){
            if(!register){  
                new PropertyWrap().Register();
            }
            register = true;
        }";
        private const string INITANCE_INIT = @"
        private void Register(){  
            RegProperty();
        }";

        internal static readonly Type[] supportAttributes = {
            typeof (CpxActionAttribute),
            typeof (CpxConditionAttribute),
            typeof (CpxAttribute),
            typeof (TreeAttribute)
        };

        private List<string> _usingList = new List<string>();
        private List<Type> _types = new List<Type>();
        private StringBuilder _builder = new StringBuilder();
        private List<string> _functionList = new List<string>();

        internal void Start() {
            AppendUsing(typeof (EditableProperty));
            AppendUsing(typeof (RuntimePropertyInfo));
            AppendUsing(typeof (CpxState_Action.Order));
            AppendUsing(typeof (Type));
            EditableProperty.ClearPropertyCache();
            CollectTypes();
            BuildFunctions();
            BuildFile();
            SaveFile();
        }

        private void CollectTypes() {
            IEnumerable<Assembly> assembles = UnityEditorReflectionTool.GetCustomAssembles();
            foreach (var a in assembles) {
                Type[] ts = a.GetTypes();
                IEnumerable<Type> types = ts.Where(t => t.IsClass && !t.IsAbstract);
                foreach (var t in types) {
                    if (IsSupportedTypes(t) && !_types.Contains(t)) {
                        _types.Add(t);
                        AppendUsing(t);
                    }
                }
            }
        }

        private void BuildFunctions() {
            foreach (var t in _types) {
                string func, code;
                if (Create(t, out code, out func)) {
                    _functionList.Add(func);
                }
            }
        }

        private void BuildFile() {
            _builder.AppendLine("//this source code was auto-generated, do not modify it");
            foreach (var u in _usingList) {
                _builder.AppendFormat("using {0};\r\n", u);
            }
            _builder.AppendLine("namespace registerWrap {");
            _builder.AppendFormat("\tpublic class {0}{1}\r\n", CLASS_NAME, "{");
            _builder.Append(STATIC_INIT + "\r\n");
            _builder.AppendFormat("\t\tprivate RuntimePropertyInfo _info =new RuntimePropertyInfo();\r\n");
            _builder.Append("\r\n");
            _builder.Append(INITANCE_INIT + "\r\n");
            _builder.AppendLine("\t\tinternal void RegProperty(){");
            _builder.AppendLine("\t\t\tEditableProperty.ClearPropertyCache();");
            foreach (var func in _functionList) {
                _builder.AppendFormat("\t\t\t{0}();\r\n", func);
            }
            _builder.AppendLine("\t\t}");
            _builder.Append(_funcBuilder);
            _builder.AppendLine("\t}");
            _builder.AppendLine("}");
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

        private void AppendUsing(Type runtime) {
            WrapperTools.AppendUsing(runtime, _usingList);
        }

        private bool IsSupportedTypes(Type b) {
            foreach (var att in supportAttributes) {
                object[] attributes = b.GetCustomAttributes(att, false);
                if (attributes.Length > 0) {
                    return EditableProperty.GetProperties(b).Length > 0;
                }
            }
            return false;
        }
    }
}