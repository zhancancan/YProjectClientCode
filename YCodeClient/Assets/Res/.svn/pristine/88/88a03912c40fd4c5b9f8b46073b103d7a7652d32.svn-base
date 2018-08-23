using mono.ui.elements;
using pure.ui.element;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace inspectors.ui {
    [CustomEditor(typeof (RichText)), CanEditMultipleObjects]
    public class Insp_PRichText : GraphicEditor {
        private static GUIStyle _style;
        private RichText _field;
        private SerializedProperty _fontData;

        protected override void OnEnable() {
            base.OnEnable();
            _field = target as RichText;
            _fontData = serializedObject.FindProperty("m_FontData");
        }

        public override void OnInspectorGUI() {
            if (_style == null) {
                _style = new GUIStyle(GUI.skin.textArea) {richText = false};
            }
            EditorGUI.BeginChangeCheck();
            _field.spriteGraphic =
                EditorGUILayout.ObjectField("Graphic", _field.spriteGraphic, typeof (SpriteGraphic), true) as
                    PSpriteGraphic_Dll;
            _field.talkIcon =
                EditorGUILayout.ObjectField("Movie Icon", _field.talkIcon, typeof (GameObject), false) as GameObject;
            if (_field.talkIcon == null) {
                EditorGUILayout.HelpBox("no talk icon", MessageType.Error);
            } else if (!_field.talkIcon.GetComponent<MovieClip>()) {
                EditorGUILayout.HelpBox("no movie clip component", MessageType.Error);
            }
            if (!_field.GetComponent<InputField>()) {
                float h = GUI.skin.textArea.CalcSize(new GUIContent(_field.text)).y;
                if (h < 50) h = 50;
                string temp = EditorGUILayout.TextArea(_field.text, _style, GUILayout.Height(h));
                if (EditorGUI.EndChangeCheck()) {
                    EditorUtility.SetDirty(_field);
                    _field.text = temp;
                }
            }
            _field.multiLang = EditorGUILayout.Toggle("Multi Lang", _field.multiLang);
            serializedObject.Update();
            EditorGUILayout.PropertyField(_fontData);
            AppearanceControlsGUI();
            RaycastControlsGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}