using mono.ui.controls;
using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UIResourceField))]
    internal class Insp_UIResourceField : Insp_UIStyleCore {
        private PrefabPicker _picker;
        private TextProperty _textStyle;
        private static bool _drawTextSetting;

        public override void OnEnable() {
            base.OnEnable();
            _picker = new PrefabPicker(UIType.Combobox);
            _textStyle = new TextProperty();
        }

        public override void OnInspectorGUI() {
            _picker.Draw(style);
            GUILayout.Space(5);
            UIResourceField b = style as UIResourceField;
            if (b != null) {
                EditorGUI.BeginChangeCheck();
                b.resourceType = EditorGUILayout.DelayedIntField("ResourceType", b.resourceType);
                b.value = EditorGUILayout.DelayedFloatField("Value", b.value);
                EditorGUI.indentLevel++;
                _drawTextSetting = EditorGUILayout.Foldout(_drawTextSetting, "Caption Text", true);
                if (_drawTextSetting) {
                    EditorGUI.indentLevel ++;
                    _textStyle.Draw(b.textStyle);
                    EditorGUI.indentLevel--;
                }

                if (EditorGUI.EndChangeCheck()) {
                    EditorUtility.SetDirty(b);
                }
                EditorGUI.indentLevel--;
            }
            DrawSolovView();
            GUILayout.Space(10);
        }
    }
}