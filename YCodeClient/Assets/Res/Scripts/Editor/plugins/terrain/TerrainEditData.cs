using System.IO;
using mono.terrain;
using pure.scene.misc;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    public class TerrainEditData {
        internal bool initialized;


        internal TerrainShaderType shaderType = TerrainShaderType.Texture4_Diffuse;


        internal Shader customShader;


        internal bool isMaster;
        internal Transform playerCamera;

        internal GameObject unityTerrain;
        internal GameObject defaultChild;

        internal int vertexInfo, trisInfo, partsOfTerrain, nbrTerrainObject = 0;
        internal Renderer[] childRenderers;
        internal bool activateLOD;
        internal bool activateBillboard;
        internal bool activateLayerCul;
        internal float closeView;
        internal float normalView;
        internal float farView;
        internal float backgroundView;


        internal Transform currentSelect;

        internal bool initMaster;

        internal Vector4 upsideTile;
        internal float upsideF;
        internal float blendFac;
        internal bool joinTiles = true;


        internal float maxViewDist;
        internal float lod2Start;
        internal float lod3Start;
        internal float updateInterval;
        internal float billInterval;
        internal float billDist;

        internal LodMode lodController;
        internal BillBoardAxis billboardAxis;
        internal OcclusionMode lodOcclusion;
        internal OcclusionMode billboardOcclusion;

        internal bool billActivate;
        internal bool lodActivate;

        internal Texture2D maskTexture;
        internal float maskTextureUV;

        internal Texture2D maskTexture2;

        internal TerrainData terrainDat;

        internal Texture[] textures;

        internal int terrainSelectId;
        internal Color shinessColor = Color.white;
        internal Color targetColor = new Color();


        internal Projector previewer;

        internal PaintHandler paintPreview = PaintHandler.Classic;


        internal int brushSize = 16;
        internal int brushSizeInPercenter;
        internal float[] brushAlpha;
        internal int selectPaintTexture = 0;
        internal float brushStronger = 0.5f;


        internal TerrainLayer[] layers = {
            new TerrainLayer {name = "_Splat0", bumpName = "_BumpSplat0"},
            new TerrainLayer {name = "_Splat1", bumpName = "_BumpSplat1"},
            new TerrainLayer {name = "_Splat2", bumpName = "_BumpSplat2"},
            new TerrainLayer {name = "_Splat3", bumpName = "_BumpSplat3"}
        };


        internal void InitSelection() {
            if (unityTerrain != null) {
                Object.DestroyImmediate(unityTerrain);
                unityTerrain = null;
                if (defaultChild != null) {
                    Selection.activeTransform = defaultChild.transform;
                    vertexInfo = trisInfo = partsOfTerrain = 0;
                    if (nbrTerrainObject == 0) {
                        TerrainObject t = defaultChild.gameObject.GetComponent<TerrainObject>();
                        t.enabledLODSystem = activateLOD;
                        t.enabledBillboard = activateBillboard;
                        t.enabledLayerCul = activateLayerCul;
                        t.closeView = closeView;
                        t.normalView = normalView;
                        t.farView = farView;
                        t.backgroundView = backgroundView;
                        t.master = 1;
                    }
                }
            }

            if (currentSelect != null &&
                currentSelect.GetComponent<TerrainObject>() != null &&
                currentSelect.GetComponent<TerrainObject>().material != null) {
                currentSelect.gameObject.layer = LayerType.TERRAIN;
                initMaster = false;
                TerrainObject to = currentSelect.GetComponent<TerrainObject>();
                for (int i = 0, len = layers.Length; i < len; i++) {
                    if (to.material.HasProperty(layers[i].name)) {
                        layers[i].texture = to.material.GetTexture(layers[i].name);
                        layers[i].tile = to.material.GetTextureScale(layers[i].name);
                    } else {
                        layers[i].texture = null;
                    }
                    layers[i].bumpTexture = to.material.HasProperty(layers[i].bumpName)
                        ? to.material.GetTexture(layers[i].bumpName)
                        : null;
                    if (to.material.HasProperty("_ShininessL" + i)) {
                        layers[i].shiness = to.material.GetFloat("_ShininessL" + i);
                    }
                }
                if (to.material.HasProperty("_SpecColor")) {
                    shinessColor = to.material.GetColor("_SpecColor");
                }

                TerrainObject curr = currentSelect.gameObject.GetComponent<TerrainObject>();

                CheckShader();

                activateLOD = curr.enabledLODSystem;
                activateBillboard = curr.enabledBillboard;
                activateLayerCul = curr.enabledLayerCul;

                maxViewDist = curr.maxViewDistance;
                lod2Start = curr.lod2Start;
                lod3Start = curr.lod3Start;
                updateInterval = curr.interval;
                playerCamera = curr.playerCamera;
                billInterval = curr.billboardInterval;
                billDist = curr.billmaxViewDistance;
                backgroundView = curr.backgroundView;
                farView = curr.farView;
                normalView = curr.normalView;
                closeView = curr.closeView;

                lodController = curr.mode == 1 ? LodMode.Mass_Control : LodMode.Independent_Control;

                billboardAxis = curr.axis == 0 ? BillBoardAxis.Y_Axis : BillBoardAxis.All_Axis;
                lodOcclusion = curr.lodBasedOnScript
                    ? OcclusionMode.Max_View_Disance
                    : OcclusionMode.Layer_Cull_Distance;

                billboardOcclusion = curr.billboardBaseOnScript
                    ? OcclusionMode.Max_View_Disance
                    : OcclusionMode.Layer_Cull_Distance;

                billActivate = curr.billboardPosition != null && curr.billboardPosition.Length > 0;
                lodActivate = curr.objPostion != null && curr.objPostion.Length > 0;

                if (playerCamera == null && Camera.main) {
                    playerCamera = Camera.main.transform;
                } else if (playerCamera == null && !Camera.main) {
                    Camera[] cam = Object.FindObjectsOfType<Camera>();

                    foreach (Camera t in cam) {
                        if (t.GetComponent<AudioListener>()) {
                            playerCamera = t.transform;
                        }
                    }
                }


                if (curr.material.HasProperty("_Control2") && curr.material.GetTexture("_Control2")) {
                    maskTexture2 = curr.material.GetTexture("_Control2") as Texture2D;
                } else {
                    maskTexture2 = null;
                }
                if (curr.material.HasProperty("_Control")) {
                    maskTextureUV = curr.material.GetTextureScale("_Control").x;
                    maskTexture = curr.material.GetTexture("_Control") as Texture2D;
                    initialized = true;
                }
            }
            Projector[] projectorObj = Object.FindObjectsOfType<Projector>();
            if (projectorObj.Length > 0) {
                foreach (var p in projectorObj) {
                    if (p.gameObject.name == TerrainCenter.PREVIEWER_NAME) {
                        Object.DestroyImmediate(p.gameObject);
                    }
                }
            }
            terrainDat = null;
            vertexInfo = 0;
            trisInfo = 0;
            partsOfTerrain = 0;
            textures = null;
            terrainSelectId = Selection.activeInstanceID;
        }


        internal void CheckShader() {
            TerrainObject curr = currentSelect.GetComponent<TerrainObject>();
            foreach (var a in TerrainShader.pool) {
                if (a.shader == curr.material.shader) {
                    shaderType = (TerrainShaderType) a.type;
                    if (curr.material.HasProperty("_Tiling")) {
                        upsideTile = curr.material.GetVector("_Tiling");
                        upsideF = curr.material.GetFloat("_UpSide");
                        blendFac = curr.material.GetFloat("_Blend");
                    }
                }
            }
        }


        public void SaveTexture() {
            var path = AssetDatabase.GetAssetPath(maskTexture);
            var bytes = maskTexture.EncodeToPNG();
            File.WriteAllBytes(path, bytes);
        }
    }
}