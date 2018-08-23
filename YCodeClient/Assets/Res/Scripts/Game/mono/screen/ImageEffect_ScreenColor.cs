using pure.utils.color;
using UnityEngine;

namespace mono.screen {
    [ExecuteInEditMode]
    public class ImageEffect_ScreenColor : MonoBehaviour {
        public Shader currShader;

        public Material material { get { return _material ?? (_material = new Material(currShader)); } }

        private Material _material;

        public ColorMatrix color;

        protected void Start() {
            currShader = Shader.Find("Pure/Screen/ColorMatrix");
            if (SystemInfo.supportsImageEffects == false) {
                enabled = false;
            }
            if (currShader != null && currShader.isSupported == false) {
                enabled = false;
            }
        }

        protected void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture) {
            if (material != null) {
                material.SetMatrix("_Matrix", color.GetMatrix());
                material.SetVector("_Offset", color.GetOffset());
                Graphics.Blit(sourceTexture, destTexture, material);
            } else {
                Graphics.Blit(sourceTexture, destTexture);
            }
        }

        protected void OnDisable() {
            if (_material) DestroyImmediate(_material);
            _material = null;
        }
    }
}