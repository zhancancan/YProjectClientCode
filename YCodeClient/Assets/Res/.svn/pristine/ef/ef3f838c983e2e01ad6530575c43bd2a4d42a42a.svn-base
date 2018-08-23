using mono.ui.controls;
using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UIButton))]
    internal class Insp_UIButton : Insp_UIStyleCore {
        private PrefabPicker _picker;
        private TextProperty _textEditor;

        public override void OnEnable() {
            base.OnEnable();
            _picker = new PrefabPicker(UIType.Button);
            _textEditor = new TextProperty();
        }

        public override void OnInspectorGUI() {
            _picker.Draw(style);
            GUILayout.Space(5);
            UIButton b = style as UIButton;
            if (b != null) {
                EditorGUI.BeginChangeCheck();
                b.text = EditorGUILayout.TextField("Label", b.text);
                if (b.textStyle.owner == null) {
                    b.textStyle.owner = b;
                }
                _textEditor.Draw(b.textStyle);
                b.multiLang = EditorGUILayout.Toggle("Multi Lang", b.multiLang);
                b.langKey = EditorGUILayout.TextField("Lang Key", b.langKey);
                if (EditorGUI.EndChangeCheck()) {
                    EditorUtility.SetDirty(b);
                }
            }
            GUILayout.Space(10);
        }
    }
}