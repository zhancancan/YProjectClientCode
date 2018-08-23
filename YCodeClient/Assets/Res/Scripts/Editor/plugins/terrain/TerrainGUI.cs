using edit.pure.resource;
using mono.terrain;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    internal class TerrainGUI {
        private static GUIContent[] menuIcon = new GUIContent[7];

        private static TerrianEditor[] subEditors = {
            new TvConverter(),
            new TvOptmaizer(),
            new TvSetting(),
            new TvPaint(),
            new TvCollapse(),
            null,
            null
        };


        internal static void Draw() {
            PGUIStyle.StartNodeGUI();
            TerrainEditData data = TerrainCenter.editData;
            data.currentSelect = Selection.activeTransform;
            BuildIcons();


            if ((data.currentSelect != null && Selection.activeInstanceID != data.terrainSelectId) ||
                (data.unityTerrain != null && TerrainCenter.menuToolBar != 0) ||
                (TerrainCenter.menuToolBar != 3 && data.previewer != null)) {
                data.InitSelection();
            }


            GUILayout.BeginHorizontal("box");
            TerrainCenter.menuToolBar =
                GUILayout.Toolbar(TerrainCenter.menuToolBar, menuIcon, GUILayout.Width(240), GUILayout.Height(18));
            GUILayout.FlexibleSpace();
            GUILayout.Label("Controls", GUILayout.Width(52));
            if (GUILayout.Button(TerrainCenter.Activated ? "Activated" : "Deactivated")) {
                TerrainCenter.Activated = !TerrainCenter.Activated;
            }
            GUILayout.EndHorizontal();

            if (data.currentSelect != null && TerrainCenter.Activated) {
                TerrainObject to = data.currentSelect.GetComponent<TerrainObject>();
                if (data.currentSelect.GetComponent<TerrainPart>()) {
                    Selection.activeTransform = data.currentSelect.parent;
                }
                Renderer[] rendererPart = data.currentSelect.GetComponentsInChildren<Renderer>();

                if (to != null && (!to.material || !to.mesh)) {
                    if (rendererPart.Length == 0) {
                        to.material = data.currentSelect.GetComponent<Renderer>().sharedMaterial;
                        to.mesh = data.currentSelect.GetComponent<MeshFilter>();
                    } else {
                        to.material = rendererPart[0].sharedMaterial;
                        to.mesh = rendererPart[0].gameObject.GetComponent<MeshFilter>();
                    }
                } else if (to != null && to.material) {
                    if (data.nbrTerrainObject == 1 && to.master != 1) {
                        data.isMaster = false;
                    } else if (data.nbrTerrainObject == 1 && to.master == 1 && data.isMaster == false &&
                               data.initMaster == false) {
                        data.isMaster = true;
                        data.initMaster = true;
                    }

                    Material mat = rendererPart.Length > 0
                        ? rendererPart[0].sharedMaterial
                        : data.currentSelect.GetComponent<Renderer>().sharedMaterial;

                    if (to.material != mat) {
                        to.material = mat;
                        EditorUtility.SetSelectedRenderState(to.GetComponent<Renderer>(),
                            EditorSelectedRenderState.Highlight);
                    }
                }
                if (data.currentSelect.GetComponent<TerrainObject>()) {
                    int countChild = data.currentSelect.transform.childCount;
                    if (countChild > 0) {
                        data.childRenderers = data.currentSelect.GetComponentsInChildren<Renderer>();
                    }
                }
                if (subEditors[TerrainCenter.menuToolBar] != null) {
                    subEditors[TerrainCenter.menuToolBar].OnGUI();
                }
            } else {
                if (data.currentSelect != null &&
                    data.currentSelect.GetComponent<TerrainObject>() != null &&
                    data.currentSelect.GetComponent<TerrainObject>().material != null) {
                    Renderer[] renderPart = data.currentSelect.GetComponentsInChildren<Renderer>();
                    if (renderPart.Length == 0) {
                        EditorUtility.SetSelectedRenderState(data.currentSelect.GetComponent<Renderer>(),
                            EditorSelectedRenderState.Highlight | EditorSelectedRenderState.Wireframe);
                    } else {
                        foreach (var r in renderPart) {
                            EditorUtility.SetSelectedRenderState(r,
                                EditorSelectedRenderState.Highlight | EditorSelectedRenderState.Wireframe);
                        }
                    }
                }
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label(PResourceManager.LoadTexture("Terrain/Waiting.png"));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }


            PGUIStyle.EndNodeGUI();
        }


        private static void BuildIcons() {
            menuIcon[0] = new GUIContent(PResourceManager.LoadTexture("Terrain/conv.png"), "Conveter");
            menuIcon[1] = new GUIContent(PResourceManager.LoadTexture("Terrain/optimize.png"), "Optimize");
            menuIcon[2] = new GUIContent(PResourceManager.LoadTexture("Terrain/myt4m.png"), "Setting");
            menuIcon[3] = new GUIContent(PResourceManager.LoadTexture("Terrain/paint.png"), "Paint");
            menuIcon[4] = new GUIContent(PResourceManager.LoadTexture("Terrain/plant.png"), "Plant");
            menuIcon[5] = new GUIContent(PResourceManager.LoadTexture("Terrain/lod.png"), "LOD");
            menuIcon[6] = new GUIContent(PResourceManager.LoadTexture("Terrain/bill.png"), "Billboard");
        }
    }
}