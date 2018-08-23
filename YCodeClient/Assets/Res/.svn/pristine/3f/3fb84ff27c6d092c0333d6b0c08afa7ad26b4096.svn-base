using mono.ui.controls;
using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UITextArea))]
    internal class Insp_TextArea : Insp_UIStyleCore {
        private UITextArea _field;
        private TextProperty _textEditor;
        private PrefabPicker _picker;
        private Drawer_ScrollView _scrollDrawer;

        public override void OnEnable() {
            base.OnEnable();
            _field = target as UITextArea;
            _picker = new PrefabPicker(UIType.Button);
            _scrollDrawer = new Drawer_ScrollView();
            _textEditor = new TextProperty();
        }

        public override void OnInspectorGUI() {
            _picker.Draw(style);
            GUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            GUILayout.Label("Text");
            float h = GUI.skin.textArea.CalcSize(new GUIContent(_field.text)).y;
            if (h < 50) h = 50;
            _field.text = EditorGUILayout.TextArea(_field.text, GUI.skin.textArea, GUILayout.Height(h));
            if (_field.textStyle.owner == null) {
                _field.textStyle.owner = _field;
            }
            _field.multiLang = EditorGUILayout.Toggle("Multi Lang", _field.multiLang);
            _field.langKey = EditorGUILayout.TextField("Lang Key", _field.langKey);
            DrawSeperator();
            _textEditor.Draw(_field.textStyle);
            DrawSeperator();
            EditorGUILayout.LabelField("Border (Left, Top, Right, Bottom)");
            _field.border = EditorGUILayout.Vector4Field("", _field.border);
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(_field);
            }
            _scrollDrawer.Draw(_field.scrollSetting, _field);
            DrawSolovView();
            GUILayout.Space(10);
        }
    }
}