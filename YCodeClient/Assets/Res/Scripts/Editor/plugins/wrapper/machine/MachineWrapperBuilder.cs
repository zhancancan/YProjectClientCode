using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using edit.pure.tools.reflection;
using pure.stateMachine.machine.core;
using pure.stateMachine.share;
using UnityEditor;
using UnityEngine;

namespace plugins.wrapper.machine {
    internal class MachineWrapperBuilder {
        internal const string CLASS_NAME = "MachineWrap";

        internal static readonly string FILE = string.Format("Assets/Res/Scripts/Game/main/{0}.cs",CLASS_NAME);

        private List<string> _usingList = new List<string>();
        private Dictionary<string, Type> _actions = new Dictionary<string, Type>();
        private Dictionary<string, Type> _conditions = new Dictionary<string, Type>();

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
            AppendUsing(typeof (CpxMachineFactory));
            AppendUsing(typeof (MachineType));
            IEnumerable<Assembly> assembles = UnityEditorReflectionTool.GetCustomAssembles(); 
            foreach (Assembly a in assembles) {
                Type[] ts = a.GetTypes();
                IEnumerable<Type> types = ts.Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof (CpxCore)));
                foreach (var t in types) {
                    CpxActionAttribute act = GetAttribute<CpxActionAttribute>(t);
                    if (act != null) {
                        if (!t.IsSubclassOf(typeof (CpxAction))) {
                            Debug.LogError(t.Name + " is not for CpxAction");
                            continue;
                        }
                        if (_actions.ContainsKey(act.actionName)) {
                            throw new Exception(string.Format("action: key = {0} occupied by {1}", act.actionName,
                                _actions[act.actionName]));
                        }
                        _actions.Add(act.actionName, t);
                        AppendUsing(t);
                    }
                    CpxConditionAttribute con = GetAttribute<CpxConditionAttribute>(t);
                    if (con != null) {
                        if (!t.IsSubclassOf(typeof (CpxCondition))) {
                            Debug.LogError(t.Name + " is not for CpxCondition");
                            continue;
                        }
                        if (_conditions.ContainsKey(con.conditionName)) {
                            throw new Exception(string.Format("condition: key = {0} occupied by {1}", con.conditionName,
                                _conditions[con.conditionName]));
                        }
                        _conditions.Add(con.conditionName, t);
                        AppendUsing(t);
                    }
                }
            }
        }

        private const string STATIC_INIT = @"
        private static bool register;
        public static void Init(){
            if(!register)new MachineWrap().Register();
            register = true;
        }";
        private const string INITANCE_INIT = @"
        private void Register(){
            MachineType.Init();
            RegAction();
            RegCondition();
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


            _builder.AppendLine("\t\tinternal void RegAction(){");
            foreach (var v in _actions) {
                _builder.AppendFormat("\t\t\tCpxMachineFactory.AddAction<{0}>(\"{1}\");\r\n", v.Value.Name, v.Key);
            }
            _builder.AppendLine("\t\t}");


            _builder.AppendLine("\t\tinternal void RegCondition(){");
            foreach (var v in _conditions) {
                _builder.AppendFormat("\t\t\tCpxMachineFactory.AddCondition<{0}>(\"{1}\");\r\n", v.Value.Name, v.Key);
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