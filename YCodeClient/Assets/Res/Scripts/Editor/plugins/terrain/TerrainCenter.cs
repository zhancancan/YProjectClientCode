using edit.pure.resource;
using edit.pure.system;
using edit.pure.ui;
using edit.pure.undo;
using mono.terrain;
using pure.scene.misc;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    public class TerrainCenter {
        public const string PREVIEWER_NAME = "PureTerrainPreviewer";
        public static readonly int LAYER_MASK = 1 << LayerType.TERRAIN;

        public const string DEFAULT_SHADER = "Terrain/Texture4_Diffuse";


        internal static bool initiatedGui;
        internal static bool InitiationError;


        public static int menuToolBar;
        public static bool Activated = true;

        public static TerrainEditData editData = new TerrainEditData();
        public static UndoQueue undo = new UndoQueue(20);
        public static EditInput input = new EditInput();
        public static readonly EditProgressBar progressBar = new EditProgressBar();
        public static readonly EditorCoroutineManager coroutine = new EditorCoroutineManager();

        public static bool SetupGUI() {
            if (!initiatedGui) {
                if (!PGUIStyle.Init()) {
                    InitiationError = true;
                    return false;
                }
                GUI.color = Color.white;
                PPaintCenter.RepaintClients();
                initiatedGui = true;
            }
            return true;
        }


        public static void Start() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            editData = new TerrainEditData();
        }


        public static void OnGUI() {
            if (InitiationError) {
                GUILayout.Label("Icon Source Error");
                return;
            }
            editData.currentSelect = Selection.activeTransform;
            editData.nbrTerrainObject = 0;
            TerrainObject[] ts = Object.FindObjectsOfType<TerrainObject>();
            foreach (var t in ts) {
                if (t.master == 1) {
                    editData.nbrTerrainObject ++;
                }
            }
            TerrainGUI.Draw();
            coroutine.Update();
            progressBar.UpdateProgress();
        }

        public static void Stop() {
            menuToolBar = 0;
            editData = null;
            Projector[] projects = Object.FindObjectsOfType<Projector>();
            foreach (var p in projects) {
                if (p.hideFlags == HideFlags.HideInHierarchy || p.name == "previewTerrain") {
                    p.hideFlags = HideFlags.None;
                    Object.DestroyImmediate(p);
                }
            }
        }
    }
}