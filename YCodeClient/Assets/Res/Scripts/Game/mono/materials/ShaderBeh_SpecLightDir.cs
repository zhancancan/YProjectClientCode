using UnityEngine;

namespace mono.materials { 

    /// <summary>
    /// the class is find the all the renderer has property of _SpecDir and store 
    /// the gameobject.dir to the dir as secpular light direction;
    /// </summary>
    [ExecuteInEditMode]
    public class ShaderBeh_SpecLightDir : MonoBehaviour {
        private void Start() {
            FindAndResetObject();
        }


        public void FindAndResetObject() {
            GameObject[] objs = gameObject.scene.GetRootGameObjects();
            Vector4 dir = -transform.forward;
            foreach (var o in objs) {
                Renderer[] renders = o.GetComponentsInChildren<Renderer>();
                foreach (var rnd in renders) {
                    if (rnd.sharedMaterial != null && rnd.sharedMaterial.HasProperty("_SpecDir")) {
                        rnd.sharedMaterial.SetVector("_SpecDir", dir);
                    }
                }
            }
        }
    }
}