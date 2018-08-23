using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    internal abstract partial class Insp_UIStyleCore {
        protected class TextProperty {
            private TextStyle _textStyle;

            internal void Draw(TextStyle style) {
                _textStyle = style;

                EditorGUI.BeginChangeCheck();
                _textStyle.fontSize = EditorGUILayout.IntField("Font Size:", _textStyle.fontSize);
                _textStyle.richText = EditorGUILayout.Toggle("Rich Text", _textStyle.richText);
                _textStyle.horizontalWrap =
                    (HorizontalWrapMode) EditorGUILayout.EnumPopup("horizontal wrap", _textStyle.horizontalWrap);
                _textStyle.verticalWarp =
                    (VerticalWrapMode) EditorGUILayout.EnumPopup("vertical wrap", _textStyle.verticalWarp);
                _textStyle.alignment = (TextAnchor) EditorGUILayout.EnumPopup("Alignment", _textStyle.alignment);
                _textStyle.bestFit = EditorGUILayout.Toggle("Best Fit", _textStyle.bestFit);
                _textStyle.minSize = EditorGUILayout.IntField("Min Size", _textStyle.minSize);
                _textStyle.maxSize = EditorGUILayout.IntField("Max Size:", _textStyle.maxSize);
                if (EditorGUI.EndChangeCheck()) {
                 //   if (_textStyle.owner != null) _textStyle.owner.Update();
                    EditorUtility.SetDirty(_textStyle.owner);
                }
            }
        }
    }
}