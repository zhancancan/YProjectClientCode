using mono.fbx;
using UnityEditor;
using UnityEngine;

namespace inspectors.fbx {
    [CustomEditor(typeof (PortraitAct_Skill))]
    internal class Insp_PortraitAct_Skill : Editor {
        private PortraitAct_Skill _act;

        internal void OnEnable() {
            _act = target as PortraitAct_Skill;
        }

        public override void OnInspectorGUI() {
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(_act.firefx);
            EditorGUI.BeginChangeCheck();
            obj = EditorGUILayout.ObjectField("Fire Fx", obj, typeof (DefaultAsset), false);
            if (EditorGUI.EndChangeCheck()) {
                _act.firefx = obj ? AssetDatabase.GetAssetPath(obj) : string.Empty;
                EditorUtility.SetDirty(_act);
            }
            _act.useEditorMode = EditorGUILayout.Toggle("Use Edit Mode", _act.useEditorMode);
        }
    }
}