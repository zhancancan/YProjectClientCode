using UnityEngine;

namespace mono.scene {
    [RequireComponent(typeof (Camera))]
    public class CameraDepthRender : MonoBehaviour {
        private Material _depth;

        protected void OnRenderImage(RenderTexture src, RenderTexture dest) {
            if (!_depth) {
                Shader s = Shader.Find("Pure/Water/DepthSplit");
                _depth = new Material(s);
            }
            Graphics.Blit(src, dest, _depth);
        }
    }
}