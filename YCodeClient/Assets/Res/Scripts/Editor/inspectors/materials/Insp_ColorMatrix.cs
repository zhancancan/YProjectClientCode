using mono.materials;
using UnityEditor;

namespace inspectors.materials {

    [CustomEditor(typeof(ShaderBeh_ColorMatrix))]
    internal class ColorMatrixInsp : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            ShaderBeh_ColorMatrix c = target as ShaderBeh_ColorMatrix;
            bool changed = false;
            float a = EditorGUILayout.Slider("hue", c.colorMatrix.hue,-1,1);
            if (!a.Equals(c.colorMatrix.hue)) {
                c.colorMatrix.hue = a;
                changed = true;
            }
            a = EditorGUILayout.Slider("brightness", c.colorMatrix.brightness,-1,1);
            if (!a.Equals(c.colorMatrix.brightness)) {
                c.colorMatrix.brightness = a;
                changed = true;
            }
            a = EditorGUILayout.Slider("contrast", c.colorMatrix.contrast,-1,1);
            if (!a.Equals(c.colorMatrix.contrast)) {
                c.colorMatrix.contrast = a;
                changed = true;
            }
            a = EditorGUILayout.Slider("saturation", c.colorMatrix.saturation,-1,1);
            if (!a.Equals(c.colorMatrix.saturation)) {
                c.colorMatrix.saturation = a;
                changed = true;
            }
            if (changed) {
                c.UploadData();
            }
        }
    }
}