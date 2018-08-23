using edit.pure.etui.utils;
using edit.pure.resource;
using mono.ui.elements;
using pure.ui.imageFiller;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (PolyImage))]
    [CanEditMultipleObjects]
    internal class Insp_PolyImage : GraphicEditor {
        public override void OnInspectorGUI() {
            PolyImage field = target as PolyImage;
            if (!field) return;
            if (serializedObject == null) return;
            SerializedProperty sprite = serializedObject.FindProperty("_sprite");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(sprite);
            if (EditorGUI.EndChangeCheck()) {
                field.sprite = sprite.objectReferenceValue as Sprite;
                EditorUtility.SetDirty(field);
            }
            EditorGUI.BeginChangeCheck();
            field.keepAspect = EditorGUILayout.Toggle("Keep Aspect", field.keepAspect);
            field.shape = (ImageShape) EditorGUILayout.EnumPopup("Shape", field.shape);
            switch (field.shape) {
                case ImageShape.Rect:
                    field.thickness = EditorGUILayout.Slider("Thickness", field.thickness, 0.1f, 100);
                    field.fillCenter = EditorGUILayout.Toggle("Fill Center", field.fillCenter);
                    break;
                case ImageShape.Round:
                    field.numEdge = EditorGUILayout.IntSlider("Number Edge:", field.numEdge, 3, 60);
                    field.startAngle = EditorGUILayout.Slider("Start Angle", field.startAngle, 0, 360);
                    field.thickness = EditorGUILayout.Slider("Thickness", field.thickness, 0.1f, 100);
                    field.fillAmount = EditorGUILayout.Slider("Fill Amount", field.fillAmount, 0.0f, 1.0f);
                    field.fillCenter = EditorGUILayout.Toggle("Fill Center", field.fillCenter);
                    field.clockWise = EditorGUILayout.Toggle("Clock Wise", field.clockWise);
                    break;
                case ImageShape.RoundRect:
                    field.ellipse = EditorGUILayout.Vector2Field("Ellipse", field.ellipse);
                    break;
            }
            field.skew = EditorGUILayout.Vector2Field("Skew", field.skew);
            DrawSeperator();
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(field);
            }
            serializedObject.Update();
            AppearanceControlsGUI();
            RaycastControlsGUI();
            DrawSeperator();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSeperator() {
            GUILayout.Space(5);
            GUILayout.Box("", EditStyles.Border1, GUILayout.Height(1));
            GUILayout.Space(2);
        }

        public override bool HasPreviewGUI() {
            return true;
        }

        public override void OnPreviewGUI(Rect rect, GUIStyle background) {
            PolyImage image = target as PolyImage;
            if (image == null) return;
            Sprite sf = image.sprite;
            if (sf == null) return;
            SpriteDrawUtility.DrawSprite(sf, rect, image.canvasRenderer.GetColor());
        }

        public override string GetInfoString() {
            PolyImage image = (PolyImage) target;
            Sprite sprite = image.sprite;
            int x = (sprite != null) ? Mathf.RoundToInt(sprite.rect.width) : 0;
            int y = (sprite != null) ? Mathf.RoundToInt(sprite.rect.height) : 0;
            return string.Format("Image Size: {0}x{1}", x, y);
        }
    }
}