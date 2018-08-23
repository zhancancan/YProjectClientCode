using mono.ui.elements;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (PText)), CanEditMultipleObjects]
    public class Insp_Text : GraphicEditor {
        private PText _field;

        private SerializedProperty _fontData;

        protected override void OnEnable() {
            base.OnEnable();
            _field = target as PText;
            _fontData = serializedObject.FindProperty("m_FontData");
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();
            GUILayout.Label("Text");
            float h = GUI.skin.textArea.CalcSize(new GUIContent(_field.text)).y;
            if (h < 50) h = 50;
            _field.text = EditorGUILayout.TextArea(_field.text, GUI.skin.textArea, GUILayout.Height(h));
            _field.multiLang = EditorGUILayout.Toggle("Multi Lang", _field.multiLang);
            _field.langKey = EditorGUILayout.TextField("Lang Key", _field.langKey);
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(_field);
            }
            serializedObject.Update();
            EditorGUILayout.PropertyField(_fontData);
            AppearanceControlsGUI();
            RaycastControlsGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}