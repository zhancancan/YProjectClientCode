using mono.ui.grid;
using UnityEditor;

namespace inspectors.ui {
    [CustomEditor(typeof (GridFactory_Lua))]
    internal class Insp_GridFactoryLua : Editor {
        public override void OnInspectorGUI() {
            GridFactory_Lua factory = target as GridFactory_Lua;
            if (factory == null) return;
            EditorGUI.BeginChangeCheck();
            DefaultAsset prev = AssetDatabase.LoadAssetAtPath<DefaultAsset>(factory.luaAsset);
            DefaultAsset asset =
                (DefaultAsset) EditorGUILayout.ObjectField("Lua Asset", prev, typeof (DefaultAsset), false);
            if (EditorGUI.EndChangeCheck()) {
                factory.luaAsset = asset ? AssetDatabase.GetAssetPath(asset) : string.Empty;
            }
            if (!prev) {
                EditorGUILayout.HelpBox("not lua file selected", MessageType.Error);
            }
        }
    }
}