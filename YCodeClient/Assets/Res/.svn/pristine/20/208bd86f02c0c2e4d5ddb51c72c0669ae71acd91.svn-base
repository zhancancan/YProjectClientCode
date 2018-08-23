using mono.ui.elements;
using UnityEditor;

namespace inspectors.ui {
    [CustomEditor(typeof (Tab))]
    internal class Insp_PTabButton : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            Tab o = target as Tab;
            EditorGUI.BeginChangeCheck();
            o.isOn = EditorGUILayout.Toggle("Is On", o.isOn);
            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(o);
        }
    }
}