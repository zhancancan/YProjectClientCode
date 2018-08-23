using mono.ui.controls;
using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UICombobox))]
    internal class Insp_UICombobox : Insp_UIStyleCore {
        private PrefabPicker _picker;
        private TextProperty _caption;
        private TextProperty _item;
        private static bool _drawCaptionTex;
        private static bool _drawItemText;

        public override void OnEnable() {
            base.OnEnable();
            _picker = new PrefabPicker(UIType.Combobox);
            _caption = new TextProperty();
            _item = new TextProperty();
        }

        public override void OnInspectorGUI() {
            _picker.Draw(style);
            GUILayout.Space(5);
            UICombobox b = style as UICombobox;
            if (b != null) {
                EditorGUI.BeginChangeCheck();
                b.rowCount = EditorGUILayout.IntSlider("Row Count", b.rowCount, 1, 20);

                EditorGUI.indentLevel++;
                _drawCaptionTex = EditorGUILayout.Foldout(_drawCaptionTex, "Caption Text", true);
                if (_drawCaptionTex) {
                    EditorGUI.indentLevel ++;
                    _caption.Draw(b.captionText);
                    EditorGUI.indentLevel--;
                }
                _drawItemText = EditorGUILayout.Foldout(_drawItemText, "Item Text", true);
                if (_drawItemText) {
                    EditorGUI.indentLevel++;
                    _item.Draw(b.itemText);
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