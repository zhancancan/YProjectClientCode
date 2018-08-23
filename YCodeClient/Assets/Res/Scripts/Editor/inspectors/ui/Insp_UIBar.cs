using mono.ui.controls;
using pure.ui.core;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace inspectors.ui {
    [CustomEditor(typeof (UIBar))]
    internal class Insp_UIBar : Insp_UIStyleCore {
        private PrefabPicker _picker;
        private UIBar _bar;

        public override void OnEnable() {
            base.OnEnable();
            _picker = new PrefabPicker(UIType.Bar);
            _bar = target as UIBar;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            GUILayout.Space(5);
            _picker.Draw(style);
            GUILayout.Space(5);
            _bar.wholeNumber = EditorGUILayout.Toggle("Whole Number", _bar.wholeNumber);
            if (_bar.wholeNumber) {
                _bar.ratio = EditorGUILayout.IntSlider("Ratio", (int) _bar.ratio, (int) _bar.minValue,
                    (int) _bar.maxValue);
                _bar.minValue = EditorGUILayout.IntField("Min", (int) _bar.minValue);
                _bar.maxValue = EditorGUILayout.IntField("Max", (int) _bar.maxValue);
            } else {
                _bar.ratio = EditorGUILayout.Slider("Ratio", _bar.ratio, _bar.minValue, _bar.maxValue);
                _bar.minValue = EditorGUILayout.FloatField("Min", _bar.minValue);
                _bar.maxValue = EditorGUILayout.FloatField("Max", _bar.maxValue);
            }
            _bar.direction = (Slider.Direction) EditorGUILayout.EnumPopup("Direction", _bar.direction);
            _bar.useTween = EditorGUILayout.Toggle("Use Tween", _bar.useTween);
            if (_bar.useTween) _bar.tweenDuration = EditorGUILayout.Slider("Tween Duration", _bar.tweenDuration, 0, 5);
        }
    }
}