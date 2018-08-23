using System.Text;
using edit.pure.machine.core;
using edit.pure.tools.command;
using edit.pure.treespace.main;
using plugins.wrapper.machine;
using plugins.wrapper.mediator;
using plugins.wrapper.mono;
using plugins.wrapper.property;
using plugins.wrapper.tree;
using pure.scene.pathFinder.solver;
using UnityEditor;

namespace plugins.wrapper {
    internal static class WrapperMenu {
        [MenuItem("Wrapper/Tree/Builder wrap files", false, 2050)]
        internal static void BuildWrapper() {
            if (EditorApplication.isCompiling) {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
                return;
            }
            new TreeWrapperBuilder().Start();
        }

        [MenuItem("Wrapper/Tree/Clean wrap files", false, 2051)]
        internal static void CleanWrapper() {
            if (EditorApplication.isCompiling) {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
                return;
            }
            new TreeWrapperCleaner().Start();
        }

        [MenuItem("Wrapper/Machine/Build wrap files", false, 2052)]
        internal static void BuildMachineWrap() {
            if (EditorApplication.isCompiling) {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
                return;
            }
            new MachineWrapperBuilder().Start();
        }

        [MenuItem("Wrapper/Machine/Clear wrap files", false, 2053)]
        internal static void CleanMachineWrap() {
            if (EditorApplication.isCompiling) {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
                return;
            }
            new MachineWrapperCleaner().Start();
        }

        [MenuItem("Wrapper/Mediator/Build wrap files", false, 2054)]
        internal static void BuildMediatoryWrap() {
            if (EditorApplication.isCompiling) {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
                return;
            }
            new MediatorWrapperBuilder().Start();
        }

        [MenuItem("Wrapper/Mediator/Clear wrap files", false, 2055)]
        internal static void CleanMediatoryWrap() {
            new MediatorWrapperCleaner().Start();
        }

        [MenuItem("Wrapper/Property/Build wrap files", false, 2506)]
        internal static void BuildPropertyWrap() {
            if (EditorApplication.isCompiling) {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
                return;
            }
            new PropertyWrapperBuilder().Start();
        }

        [MenuItem("Wrapper/Property/Clear wrap files", false, 2507)]
        internal static void CleanPropertyWrap() {
            if (EditorApplication.isCompiling) {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
                return;
            }
            new PropertyWrapCleaner().Start();
        }

        [MenuItem("Wrapper/Mono/Build wrap files", false, 2508)]
        internal static void BuildMonoWrap() {
            if (EditorApplication.isCompiling) {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
                return;
            }
            new MonoWrapBuilder().Start();
        }

        [MenuItem("Wrapper/Mono/Clear wrap files", false, 2509)]
        internal static void CleanMonoWrap() {
            if (EditorApplication.isCompiling) {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
                return;
            }
            new MonoWrapCleaner().Start();
        }

        [MenuItem("Wrapper/Shortcut/Export all data _F11", false, 5000)]
        internal static void ExportAllData() {
            StringBuilder sb = new StringBuilder();
            ProgressCommand c = new ProgressCommand();
            ProgressCommand child = TreeCenter.GetExportAllCommand();
            if (child != null) {
                sb.Append("Tree export \r\n");
                c.Add(child);
            }
            child = MachineCenter.GetExportCommand();
            if (child != null) {
                sb.Append("Machine export \r\n");
                c.Add(child);
            }
            c.notify = sb.ToString();
            c.refresh = true;
            c.StartCommand();
        }

        [MenuItem("Wrapper/Shortcut/Export all data _F11", true)]
        internal static bool ExpportCheck() {
            return TreeCenter.isOpen || MachineCenter.isOpen;
        }

        [MenuItem("Wrapper/Shortcut/Build all wrap files  _F9", false, 5000)]
        internal static void BuildAllWrap() {
            if (EditorApplication.isCompiling) {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
                return;
            }
            new TreeWrapperBuilder().Start();
            new MachineWrapperBuilder().Start();
            new PropertyWrapperBuilder().Start();
            new MediatorWrapperBuilder().Start();
        }




  

        //[MenuItem("Wrapper/Lang/Build UI to server", false, 3000)]
        //internal static void BuildUILange() {
        //    ProgressCommand c = new ProgressCommand("UI Lang Build Complete");
        //    c.Add(new LangTxtBuilder_UI());
        //    c.StartCommand();
        //}

        //[MenuItem("Wrapper/Lang/Export lang to Binary", false, 3001)]
        //internal static void ExportLangToBinary() {
        //    ProgressCommand c = new ProgressCommand("UI Lang Export Complete");
        //    c.Add(new UILangExporter());
        //    c.StartCommand();
        //}

        //[MenuItem("Wrapper/Lang/Build and export", false, 3002)]
        //internal static void ExportLangBoth() {
        //    ProgressCommand c = new ProgressCommand("UI Lang Build And Export Complete");
        //    c.Add(new LangTxtBuilder_UI());
        //    c.Add(new UILangExporter());
        //    c.StartCommand();
        //}

        //[MenuItem("Wrapper/Lang/Build Program", false, 3500)]
        //internal static void BuildProgramLang() {
        //    ProgressCommand c = new ProgressCommand("Program Lang Build Complete");
        //    c.Add(new LangTxtBuilder_Program());
        //    c.StartCommand();
        //}
    }
}