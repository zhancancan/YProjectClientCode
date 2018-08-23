using UnityEngine;

namespace mono.terrain {
    public class TerrainObject : MonoBehaviour {
        [HideInInspector]
        public string convertType = "";

        [HideInInspector]
        public bool enabledLODSystem = true;

        [HideInInspector]
        public Vector3[] objPostion;

        [HideInInspector]
        public TerrainLod[] objLodScript;

        [HideInInspector]
        public int[] objLodStatus; //0=Occlude/1=lod1/2=lod2/3=lod3

        [HideInInspector]
        public float maxViewDistance = 60.0f;

        [HideInInspector]
        public float lod2Start = 20.0f;

        [HideInInspector]
        public float lod3Start = 40.0f;

        [HideInInspector]
        public float interval = 0.5f;

        [HideInInspector]
        public Transform playerCamera;

        private Vector3 _oldPlayerPos;

        [HideInInspector]
        public int mode = 1;

        [HideInInspector]
        public int master;

        [HideInInspector]
        public bool enabledBillboard = true;

        [HideInInspector]
        public Vector3[] billboardPosition;

        [HideInInspector]
        public float billboardInterval = 0.05f;

        [HideInInspector]
        public int[] billboardStatus; // 0=occlude/1=active

        [HideInInspector]
        public float billmaxViewDistance = 30.0f;

        [HideInInspector]
        public TerrainBillboard[] billboardScript;

        [HideInInspector]
        public bool enabledLayerCul = true;

        [HideInInspector]
        public float backgroundView = 1000f;

        [HideInInspector]
        public float farView = 200.0f;

        [HideInInspector]
        public float normalView = 60.0f;

        [HideInInspector]
        public float closeView = 30.0f;


        [HideInInspector]
        public int axis = 0;

        [HideInInspector]
        public bool lodBasedOnScript = true;

        [HideInInspector]
        public bool billboardBaseOnScript = true;

        public Material material;
        public MeshFilter mesh;

        [HideInInspector]
        public Color TranslucencyColor = new Color(0.73f, 0.85f, 0.3f, 1f);

        [HideInInspector]
        public Vector4 wind = new Vector4(0.85f, 0.075f, 0.4f, 0.5f);

        [HideInInspector]
        public float windFrequency = 0.75f;

        [HideInInspector]
        public float grassWindFrequency = 1.5f;

        public bool windActive = false;
        public bool layerCullPreview = false;
        public bool lodPreview = false;
        public bool billboardPreview = false;


       // private float[] _distances = new float[32];
    }
}