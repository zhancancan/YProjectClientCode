using pure.utils.color;
using UnityEngine;

namespace mono.materials {
    [RequireComponent(typeof (Renderer))]
    public class ShaderBeh_ColorMatrix : MonoBehaviour {
        [SerializeField]
        public ColorMatrix colorMatrix = new ColorMatrix();

        private Renderer _renderer;


        public void Start() {
            _renderer = GetComponent<Renderer>();
        }


        public void UploadData() {
            _renderer = gameObject.GetComponent<Renderer>();
            if (_renderer != null && _renderer.sharedMaterial != null) {
                _renderer.sharedMaterial.SetMatrix("_ColorMatrix", colorMatrix.GetMatrix());
                _renderer.sharedMaterial.SetVector("_ColorOffset", colorMatrix.GetOffset());
            }
        }
    }
}