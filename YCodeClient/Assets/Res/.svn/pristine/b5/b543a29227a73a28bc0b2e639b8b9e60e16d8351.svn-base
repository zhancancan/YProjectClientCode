using mono.ui.controls;
using pure.ui.core;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace inspectors.ui {
    [CustomEditor(typeof (UISlider))]
    internal class Insp_UISlider : Insp_UIStyleCore {
        private PrefabPicker _picker;

        public override void OnEnable() {
            base.OnEnable();
            _picker = new PrefabPicker(UIType.Slider);
        }

        public override void OnInspectorGUI() {
            _picker.Draw(style);
            GUILayout.Space(5);
            UISlider b = style as UISlider;
            if (b != null) {
                EditorGUI.BeginChangeCheck();
                b.direction = (Slider.Direction) EditorGUILayout.EnumPopup("Direction", b.direction);
                b.minValue = b.wholeNumbers
                    ? EditorGUILayout.DelayedIntField("Min Value", (int) b.minValue)
                    : EditorGUILayout.DelayedFloatField("Min Value", b.minValue);
                b.maxValue = b.wholeNumbers
                    ? EditorGUILayout.DelayedIntField("Max Value", (int) b.maxValue)
                    : EditorGUILayout.DelayedFloatField("Max Value", b.maxValue);
                b.wholeNumbers = EditorGUILayout.Toggle("Whole Numbers", b.wholeNumbers);
                b.value = EditorGUILayout.Slider("Value", b.value, b.minValue, b.maxValue);
                if (EditorGUI.EndChangeCheck()) {
                    EditorUtility.SetDirty(b);
                }
            }

            GUILayout.Space(10);
        }
    }
}