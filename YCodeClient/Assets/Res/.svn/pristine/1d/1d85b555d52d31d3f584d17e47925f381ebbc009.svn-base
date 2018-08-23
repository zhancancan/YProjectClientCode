using mono.materials;
using UnityEditor;
using UnityEngine;

namespace inspectors.materials {
    [CustomEditor(typeof (ShaderBeh_SpecLightDir))]
    internal class SpecLightInspector : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Collect Materials", GUILayout.MaxWidth(150))) {
                (target as ShaderBeh_SpecLightDir).FindAndResetObject();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}