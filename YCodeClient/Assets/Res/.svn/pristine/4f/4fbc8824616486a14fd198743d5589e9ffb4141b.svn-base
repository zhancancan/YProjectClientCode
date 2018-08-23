using UnityEngine;

namespace mono.terrain {
    public class TerrainLod : MonoBehaviour {
        //[HideInInspector]
        public Renderer LOD1;
        //[HideInInspector]
        public Renderer LOD2;
        //[HideInInspector]
        public Renderer LOD3;


        [HideInInspector]
        public float Interval = 0.5f;
        [HideInInspector]
        public Transform playerCamera;
        [HideInInspector]
        public int mode;
        private Vector3 oldPlayerPos;
        [HideInInspector]
        public int objLodStatus;
        [HideInInspector]
        public float maxViewDistance = 60.0f;
        [HideInInspector]
        public float lod2Start = 20.0f;
        [HideInInspector]
        public float lod3Start = 40.0f;
    }
}