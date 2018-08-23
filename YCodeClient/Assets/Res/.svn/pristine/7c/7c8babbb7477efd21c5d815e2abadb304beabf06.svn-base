using edit.pure.ui;
using mono.ui.controls;
using pure.ui.controls;
using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UITabNavigator))]
    internal class Insp_UITabNavigator : Insp_UIStyleCore {
        private PrefabPicker _picker;
        private TextProperty _textEditor;

        private ArrayListDrawer _listDrawer = new ArrayListDrawer("_labels");
        private static bool _showText;

        public override void OnEnable() {
            base.OnEnable();
            _picker = new PrefabPicker(UIType.TabNavigator);
            _textEditor = new TextProperty();
        }

        public override void OnInspectorGUI() {
            UITabNavigator b = style as UITabNavigator;
            _picker.Draw(style);
            EditorGUI.BeginChangeCheck();
            b.alignment = (UITabNavigagor_Dll.TabAlignment) EditorGUILayout.EnumPopup("Aligment", b.alignment);
            b.interval = EditorGUILayout.DelayedFloatField("Interval", b.interval);
            b.unselectMargin = EditorGUILayout.DelayedFloatField("UnSelected Margin", b.unselectMargin);
            b.multiLang = EditorGUILayout.Toggle("Multi Lang", b.multiLang);
            b.langKey = EditorGUILayout.TextField("Lang Key", b.langKey);
            DrawSeperator();
            GUILayout.Space(5);
            _listDrawer.Draw(serializedObject);
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(b);
            }
            _showText = EditorGUILayout.Foldout(_showText, "Text Style", true);
            if (_showText) {
                EditorGUI.BeginChangeCheck();
                if (b.textStyle.owner == null) {
                    b.textStyle.owner = b;
                }
                _textEditor.Draw(b.textStyle);
                if (EditorGUI.EndChangeCheck()) {
                    EditorUtility.SetDirty(b);
                }
            }
            DrawSolovView();
            GUILayout.Space(10);
        }
    }
}