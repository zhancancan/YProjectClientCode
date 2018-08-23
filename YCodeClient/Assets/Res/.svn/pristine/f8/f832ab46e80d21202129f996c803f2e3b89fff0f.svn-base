using mono.ui.controls;
using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UIStar))]
    internal class Insp_UIStar : Insp_UIStyleCore {
        private PrefabPicker _picker;

        public override void OnEnable() {
            base.OnEnable();
            _picker = new PrefabPicker(UIType.Star);
        }

        public override void OnInspectorGUI() {
            _picker.Draw(style);

            GUILayout.Space(5);
            UIStar b = style as UIStar;
            if (b != null) {
                EditorGUI.BeginChangeCheck();

                b.numStars = EditorGUILayout.DelayedIntField("Number Stars", b.numStars);
                b.maxColumn = EditorGUILayout.DelayedIntField("Max Column ", b.maxColumn);
                b.activeValue = EditorGUILayout.DelayedIntField("Active Value", b.activeValue);

                if (EditorGUI.EndChangeCheck()) {
                    EditorUtility.SetDirty(b);
                }
            }

            GUILayout.Space(10);
        }
    }
}