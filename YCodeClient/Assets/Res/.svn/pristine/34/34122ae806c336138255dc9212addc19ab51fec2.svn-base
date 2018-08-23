using System.Linq;
using edit.pure.etui.utils;
using mono.ui.elements;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace inspectors.ui {
    [CustomEditor(typeof (PImage))]
    [CanEditMultipleObjects]
    public class Insp_Image : GraphicEditor {
        SerializedProperty _fillMethod;
        SerializedProperty _fillOrigin;
        SerializedProperty _fillAmount;
        SerializedProperty _fillClockwise;
        SerializedProperty _type;
        SerializedProperty _fillCenter;
        SerializedProperty _sprite;
        SerializedProperty _preserveAspect;
        GUIContent _spriteContent;
        GUIContent _spriteTypeContent;
        GUIContent _clockwiseContent;
        AnimBool _showSlicedOrTiled;
        AnimBool _showSliced;
        AnimBool _showTiled;
        AnimBool _showFilled;
        AnimBool _showType;

        protected override void OnEnable() {
            base.OnEnable();
            _spriteContent = new GUIContent("Source Image");
            _spriteTypeContent = new GUIContent("Image Type");
            _clockwiseContent = new GUIContent("Clockwise");
            _sprite = serializedObject.FindProperty("m_Sprite");
            _type = serializedObject.FindProperty("m_Type");
            _fillCenter = serializedObject.FindProperty("m_FillCenter");
            _fillMethod = serializedObject.FindProperty("m_FillMethod");
            _fillOrigin = serializedObject.FindProperty("m_FillOrigin");
            _fillClockwise = serializedObject.FindProperty("m_FillClockwise");
            _fillAmount = serializedObject.FindProperty("m_FillAmount");
            _preserveAspect = serializedObject.FindProperty("m_PreserveAspect");
            _showType = new AnimBool(_sprite.objectReferenceValue != null);
            _showType.valueChanged.AddListener(Repaint);
            var typeEnum = (Image.Type) _type.enumValueIndex;
            _showSlicedOrTiled = new AnimBool(!_type.hasMultipleDifferentValues && typeEnum == Image.Type.Sliced);
            _showSliced = new AnimBool(!_type.hasMultipleDifferentValues && typeEnum == Image.Type.Sliced);
            _showTiled = new AnimBool(!_type.hasMultipleDifferentValues && typeEnum == Image.Type.Tiled);
            _showFilled = new AnimBool(!_type.hasMultipleDifferentValues && typeEnum == Image.Type.Filled);
            _showSlicedOrTiled.valueChanged.AddListener(Repaint);
            _showSliced.valueChanged.AddListener(Repaint);
            _showTiled.valueChanged.AddListener(Repaint);
            _showFilled.valueChanged.AddListener(Repaint);
            SetShowNativeSize(true);
        }

        protected override void OnDisable() {
            _showType.valueChanged.RemoveListener(Repaint);
            _showSlicedOrTiled.valueChanged.RemoveListener(Repaint);
            _showSliced.valueChanged.RemoveListener(Repaint);
            _showTiled.valueChanged.RemoveListener(Repaint);
            _showFilled.valueChanged.RemoveListener(Repaint);
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            SpriteGUI();
            AppearanceControlsGUI();
            RaycastControlsGUI();
            _showType.target = _sprite.objectReferenceValue != null;
            if (EditorGUILayout.BeginFadeGroup(_showType.faded))
                TypeGUI();
            EditorGUILayout.EndFadeGroup();
            SetShowNativeSize(false);
            if (EditorGUILayout.BeginFadeGroup(m_ShowNativeSize.faded)) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_preserveAspect);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFadeGroup();
            NativeSizeButtonGUI();
            serializedObject.ApplyModifiedProperties();
        }

        void SetShowNativeSize(bool instant) {
            Image.Type type = (Image.Type) _type.enumValueIndex;
            bool showNativeSize = (type == Image.Type.Simple || type == Image.Type.Filled) &&
                                  _sprite.objectReferenceValue != null;
            base.SetShowNativeSize(showNativeSize, instant);
        }

        protected void SpriteGUI() {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_sprite, _spriteContent);
            if (EditorGUI.EndChangeCheck()) {
                var newSprite = _sprite.objectReferenceValue as Sprite;
                if (newSprite) {
                    Image.Type oldType = (Image.Type) _type.enumValueIndex;
                    if (newSprite.border.SqrMagnitude() > 0) {
                        _type.enumValueIndex = (int) Image.Type.Sliced;
                    } else if (oldType == Image.Type.Sliced) {
                        _type.enumValueIndex = (int) Image.Type.Simple;
                    }
                }
            }
        }

        protected void TypeGUI() {
            EditorGUILayout.PropertyField(_type, _spriteTypeContent);
            ++EditorGUI.indentLevel;
            {
                Image.Type typeEnum = (Image.Type) _type.enumValueIndex;
                bool showSlicedOrTiled = (!_type.hasMultipleDifferentValues &&
                                          (typeEnum == Image.Type.Sliced || typeEnum == Image.Type.Tiled));
                if (showSlicedOrTiled && targets.Length > 1)
                    showSlicedOrTiled = targets.Select(obj => obj as Image).All(img => img.hasBorder);
                _showSlicedOrTiled.target = showSlicedOrTiled;
                _showSliced.target = (showSlicedOrTiled && !_type.hasMultipleDifferentValues &&
                                      typeEnum == Image.Type.Sliced);
                _showTiled.target = (showSlicedOrTiled && !_type.hasMultipleDifferentValues &&
                                     typeEnum == Image.Type.Tiled);
                _showFilled.target = (!_type.hasMultipleDifferentValues && typeEnum == Image.Type.Filled);
                Image image = target as Image;
                if (EditorGUILayout.BeginFadeGroup(_showSlicedOrTiled.faded) && image) {
                    if (image.hasBorder)
                        EditorGUILayout.PropertyField(_fillCenter);
                }
                EditorGUILayout.EndFadeGroup();
                if (EditorGUILayout.BeginFadeGroup(_showSliced.faded) && image) {
                    if (image.sprite != null && !image.hasBorder)
                        EditorGUILayout.HelpBox("This Image doesn't have a border.", MessageType.Warning);
                }
                EditorGUILayout.EndFadeGroup();
                if (EditorGUILayout.BeginFadeGroup(_showTiled.faded) && image) {
                    if (image.sprite != null && !image.hasBorder &&
                        (image.sprite.texture.wrapMode != TextureWrapMode.Repeat || image.sprite.packed))
                        EditorGUILayout.HelpBox(
                            "It looks like you want to tile a sprite with no border. It would be more efficient to convert the Sprite to an Advanced texture, clear the Packing tag and set the Wrap mode to Repeat.",
                            MessageType.Warning);
                }
                EditorGUILayout.EndFadeGroup();
                if (EditorGUILayout.BeginFadeGroup(_showFilled.faded)) {
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_fillMethod);
                    if (EditorGUI.EndChangeCheck()) {
                        _fillOrigin.intValue = 0;
                    }
                    switch ((Image.FillMethod) _fillMethod.enumValueIndex) {
                        case Image.FillMethod.Horizontal:
                            _fillOrigin.intValue =
                                (int)
                                    (Image.OriginHorizontal)
                                        EditorGUILayout.EnumPopup("Fill Origin",
                                            (Image.OriginHorizontal) _fillOrigin.intValue);
                            break;
                        case Image.FillMethod.Vertical:
                            _fillOrigin.intValue =
                                (int)
                                    (Image.OriginVertical)
                                        EditorGUILayout.EnumPopup("Fill Origin",
                                            (Image.OriginVertical) _fillOrigin.intValue);
                            break;
                        case Image.FillMethod.Radial90:
                            _fillOrigin.intValue =
                                (int)
                                    (Image.Origin90)
                                        EditorGUILayout.EnumPopup("Fill Origin", (Image.Origin90) _fillOrigin.intValue);
                            break;
                        case Image.FillMethod.Radial180:
                            _fillOrigin.intValue =
                                (int)
                                    (Image.Origin180)
                                        EditorGUILayout.EnumPopup("Fill Origin", (Image.Origin180) _fillOrigin.intValue);
                            break;
                        case Image.FillMethod.Radial360:
                            _fillOrigin.intValue =
                                (int)
                                    (Image.Origin360)
                                        EditorGUILayout.EnumPopup("Fill Origin", (Image.Origin360) _fillOrigin.intValue);
                            break;
                    }
                    EditorGUILayout.PropertyField(_fillAmount);
                    if ((Image.FillMethod) _fillMethod.enumValueIndex > Image.FillMethod.Vertical) {
                        EditorGUILayout.PropertyField(_fillClockwise, _clockwiseContent);
                    }
                }
                EditorGUILayout.EndFadeGroup();
            }
            --EditorGUI.indentLevel;
        }

        public override bool HasPreviewGUI() {
            return true;
        }

        public override void OnPreviewGUI(Rect rect, GUIStyle background) {
            Image image = target as Image;
            if (image == null) return;
            Sprite sf = image.sprite;
            if (sf == null) return;
            SpriteDrawUtility.DrawSprite(sf, rect, image.canvasRenderer.GetColor());
        }

        public override string GetInfoString() {
            Image image = (Image) target;
            Sprite sprite = image.sprite;
            int x = (sprite != null) ? Mathf.RoundToInt(sprite.rect.width) : 0;
            int y = (sprite != null) ? Mathf.RoundToInt(sprite.rect.height) : 0;
            return string.Format("Image Size: {0}x{1}", x, y);
        }
    }
}