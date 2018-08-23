using UnityEngine;

namespace mono.screen {
    [ExecuteInEditMode]
    public class ImageEffect_WaterDrop : MonoBehaviour {
        //public Shader currShader;
        //public Material material
        //{
        //    get
        //    {
        //        if (_material == null)
        //        {

        //            _material = new Material(currShader);
        //            _material.hideFlags = HideFlags.HideAndDontSave;
        //        }
        //        return _material;
        //    }
        //}

        public Material material;
        private float _timeX = 1.0f;

        //[Range(5, 64), Tooltip("distorion")]
        //public float distortion = 8.0f;

        //[Range(0, 7), Tooltip("drop size x")]
        //public float sizeX = 1f;

        //[Range(0, 7), Tooltip("drop size y")]
        //public float sizeY = 0.5f;

        //[Range(0, 10), Tooltip("drop speed")]
        //public float dropSpeed = 3.6f;

        //public static float ChangeDistortion;
        //public static float ChangeSizeX;
        //public static float ChangeSizeY;
        //public static float ChangeDropSpeed;


        private void Start() {
            if (SystemInfo.supportsImageEffects == false) {
                enabled = false;
            }
            //if (currShader != null && currShader.isSupported == false) {
            //    enabled = false;
            //    return;
            //}
        }

        private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture) {
            Debug.Log("OnRenderImage" + material);
            if (material != null) {
                _timeX += Time.deltaTime;
                if (_timeX > 100) _timeX = 0;
                material.SetFloat("_CurTime", _timeX);
                //material.SetFloat("_Distortion", distortion);
                //material.SetFloat("_SizeX", sizeX);
                //material.SetFloat("_SizeY", sizeY);
                //material.SetFloat("_DropSpeed", dropSpeed);
                Graphics.Blit(sourceTexture, destTexture, material);
            } else {
                Graphics.Blit(sourceTexture, destTexture);
            }
        }


        private void OnValidate() {
            //ChangeDistortion = distortion;
            //ChangeSizeX = sizeX;
            //ChangeSizeY = sizeY;
            //ChangeDropSpeed = dropSpeed;
        }


        private void Update() {
            //Debug.Log("onupdate");
            //if (Application.isPlaying) {
            //    distortion = ChangeDistortion;
            //    sizeX = ChangeSizeX;
            //    sizeY = ChangeSizeY;
            //    dropSpeed = ChangeDropSpeed;
            //}
        }

        private void OnDisable() {
            //  if(_material)DestroyImmediate(_material);
        }
    }
}