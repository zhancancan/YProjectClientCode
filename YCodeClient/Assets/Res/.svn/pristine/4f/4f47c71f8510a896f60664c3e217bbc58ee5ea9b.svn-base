using mono.ui.elements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace inspectors.ui {
    [CustomEditor(typeof (Bar))]
    internal class Insp_PBar : Editor {
        private Bar _bar;

        internal void OnEnable() {
            _bar = target as Bar;
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();
            _bar.value = _bar.wholeNumber
                ? EditorGUILayout.IntSlider("Ratio", (int) _bar.value, (int) _bar.minValue, (int) _bar.maxValue)
                : EditorGUILayout.Slider("Ratio", _bar.value, _bar.minValue, _bar.maxValue);
            _bar.baseRect =
                (RectTransform) EditorGUILayout.ObjectField("Base Bar", _bar.baseRect, typeof (RectTransform), true);
            _bar.overRect =
                (RectTransform) EditorGUILayout.ObjectField("Over Bar", _bar.overRect, typeof (RectTransform), true);
            _bar.textfield =
                (Text) EditorGUILayout.ObjectField("Text Field", _bar.textfield, typeof (Text), true);
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(_bar);
            }
        }
    }
}