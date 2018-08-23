using System;
using System.Reflection;
using edit.pure.tools.reflection;
using UnityEditor;

namespace consoler {
    public static class ConsolerHelper {
        private static Type _consoleType;
        private static FieldInfo _consoleField;
        private static FieldInfo _fieldListViewState;
        private static FieldInfo _fieldActiveText;

        public static string GetLog() {
            BuildConsole();
            EditorWindow instance = (EditorWindow) _consoleField.GetValue(null);
            if (EditorWindow.focusedWindow == instance) {
                object valueListView = _fieldListViewState.GetValue(instance);
                if (valueListView == null) {
                    return null;
                }
                return _fieldActiveText.GetValue(instance).ToString();
            }
            return null;
        }

        private static void BuildConsole() {
            if (_consoleType == null) {
                _consoleType = UnityEditorReflectionTool.GetTypeInEditor("ConsoleWindow");
                _consoleField = _consoleType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
                _fieldListViewState = _consoleType.GetField("m_ListView", BindingFlags.Instance | BindingFlags.NonPublic);
                //   _typeListViewState = UnityEditorReflectionTool.GetTypeInEditor("ListViewState");
                _fieldActiveText = _consoleType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
            }
        }

        //private static Type[] editorTypes;

        //private static Type FindTypeInUnityEditor(string type) {
        //    if (editorTypes == null) {
        //        Assembly a = Assembly.GetAssembly(typeof (Editor));
        //        editorTypes = a.GetTypes();
        //    }
        //    return editorTypes.First(t => t.Name == type);
        //}

        public static bool IsActiveFromConsole() {
            BuildConsole();
            EditorWindow instance = (EditorWindow) _consoleField.GetValue(null);
            return EditorWindow.focusedWindow == instance;
        }
    }
}