using edit.pure.resource;
using mono.ui.elements;
using pure.ui.element;
using pure.ui.so;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (NumberField))]
    internal class Insp_PNumbeField : GraphicEditor {
        private NumberField _field;
        private SerializedProperty _event;

        protected override void OnEnable() {
            base.OnEnable();
            _field = target as NumberField;
            _event = serializedObject.FindProperty("_rollEvent");
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();
            _field.supportChars = EditorGUILayout.DelayedTextField("Support Chars", _field.supportChars);
            _field.asset =
                EditorGUILayout.ObjectField("Sprite Atlas", _field.asset, typeof (SpriteCollection_Dll), false) as
                    SpriteCollection_Dll;
            _field.numFigures = EditorGUILayout.DelayedIntField("Number Figures", _field.numFigures);
            DrawSeperator();
            _field.value = EditorGUILayout.DelayedIntField("Value", _field.value);
            _field.changeMode =
                (PNumberField_Dll.NumberChangeMode) EditorGUILayout.EnumPopup("Change Mode", _field.changeMode);
            switch (_field.changeMode) {
                case PNumberField_Dll.NumberChangeMode.Roll:
                    _field.rollSpeed = EditorGUILayout.DelayedFloatField("Speed", _field.rollSpeed);
                    _field.rollInterval = EditorGUILayout.DelayedFloatField("Interval", _field.rollInterval);
                    break;
                case PNumberField_Dll.NumberChangeMode.CharByChar:
                    _field.charUpdataDuration = EditorGUILayout.DelayedFloatField("Char Update Duration",
                        _field.charUpdataDuration);
                    break;
            }
            DrawSeperator();
            _field.margin = EditorGUILayout.Vector2Field("Margin", _field.margin);
            _field.alignment = (TextAlignment) EditorGUILayout.EnumPopup("Alignment", _field.alignment);
            _field.letterSpace = EditorGUILayout.DelayedFloatField("Letter Space", _field.letterSpace);
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(_field);
            }
            serializedObject.Update();
            EditorGUILayout.PropertyField(_event);
            AppearanceControlsGUI();
            RaycastControlsGUI();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSeperator() {
            GUILayout.Space(5);
            GUILayout.Box("", EditStyles.Border1, GUILayout.Height(1));
            GUILayout.Space(2);
        }
    }
}