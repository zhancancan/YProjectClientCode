using pure.ui.data;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace inspectors.ui {
    [CustomPropertyDrawer(typeof (ProvideDataList), true)]
    internal class PDropdownOptionListDrawer : PropertyDrawer {
        private ReorderableList _reorderableList;

        private void Init(SerializedProperty property) {
            if (_reorderableList != null)
                return;

            SerializedProperty array = property.FindPropertyRelative("_options");
            _reorderableList = new ReorderableList(property.serializedObject, array) {
                drawElementCallback = DrawOptionData,
                drawHeaderCallback = DrawHeader
            };
            _reorderableList.elementHeight += 16;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            Init(property);
            _reorderableList.DoList(position);
        }

        private void DrawHeader(Rect rect) {
            GUI.Label(rect, "Options");
        }

        private void DrawOptionData(Rect rect, int index, bool isActive, bool isFocused) {
            SerializedProperty itemData = _reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty itemText = itemData.FindPropertyRelative("_text");
            SerializedProperty itemImage = itemData.FindPropertyRelative("_image");

            RectOffset offset = new RectOffset(0, 0, -1, -3);
            rect = offset.Add(rect);
            rect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(rect, itemText, GUIContent.none);
            rect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, itemImage, GUIContent.none);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            Init(property);
            return _reorderableList != null ? _reorderableList.GetHeight() : 10;
        }
    }
}