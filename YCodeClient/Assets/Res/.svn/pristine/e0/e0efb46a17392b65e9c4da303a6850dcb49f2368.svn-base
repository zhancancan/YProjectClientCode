using System.IO;
using edit.pure.resource;
using mono.terrain;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace plugins.terrain {
    internal partial class TvConverter : TerrianEditor {
        private class Platform {
            internal string name;
            internal int min;
            internal int max;
        }

        private const string warning = "Since Unity3.5, some convertedd object can be no smooth.\n" +
                                       "Select the New Mesh in the Project window in scene mesh folder:\n" +
                                       "In Inspector window: Decrease \"smoothing angle\",Increate again to 180 and \"Apply\"\n" +
                                       "Now Select your Object on the scene: Uncheck/check the box the \"Mesh Collider\" in Inspector window";


        private TerrainData _terrainDat;
        private float _heightmapWidth;
        private float _heightmapHeight;
        private bool _createNewPrefab;
        private bool _keepTexture;
        private int _resolution = 90;
        private int _resX;
        private int _resY;
        private float _tRes;


        private string _terrainName = "";
        private string _finalExportName;


        private Platform[] _platforms = {
            new Platform {name = "IPhone 3GS", min = 15000, max = 30000},
            new Platform {name = "IPad 1", min = 15000, max = 30000},
            new Platform {name = "IPhone 4", min = 20000, max = 40000},
            new Platform {name = "IPhone 4", min = 20000, max = 30000},
            new Platform {name = "Tegra 2", min = 20000, max = 40000},
            new Platform {name = "IPad 2", min = 25000, max = 45000},
            new Platform {name = "IPhone 4S", min = 25000, max = 45000},
            new Platform {name = "Flash", min = 45000, max = 60000},
            new Platform {name = "Web", min = 100000, max = 200000},
            new Platform {name = "Desktop", min = int.MaxValue, max = int.MaxValue}
        };


        private int[] _options = {0, 1, 2};

        private string[] _optionNames = {
            "Keep Conversion and Destroy Original",
            "Modify Options and Start a New Conversion", "Keep Both and Continue"
        };

        private int _saveOption;

        public override void OnGUI() {
            if (currentSelect == null) {
                return;
            }
            editData.childRenderers = null;
            if (currentSelect != null && currentSelect.GetComponent<TerrainObject>() == null) {
                if (currentSelect.childCount > 0) {
                    editData.childRenderers = currentSelect.GetComponentsInChildren<Renderer>();
                }
            }
            if (editData.childRenderers != null && editData.childRenderers.Length == 0) {
                editData.childRenderers = null;
            }
            Renderer _currRenderer = currentSelect.GetComponent<Renderer>();
            Terrain _unityTerrainComp = currentSelect.GetComponent<Terrain>();


            if (editData.vertexInfo == 0 && editData.trisInfo == 0 && editData.partsOfTerrain == 0) {
                if ((_currRenderer != null || _unityTerrainComp != null || editData.childRenderers != null) &&
                    !currentSelect.GetComponent<TerrainObject>() &&
                    !currentSelect.GetComponent<TerrainPart>()) {
                    DrawConvertGUI();
                } else {
                    _terrainDat = null;
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(
                        currentSelect.GetComponent<TerrainObject>()
                            ? "Already Pure Object"
                            : "Can not convert to pure terrain object", PGUIStyle.nodeLabelBold);
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            } else {
                DrawEndOperation();
            }
        }


        private void DrawConvertGUI() {
            Renderer _currRenderer = currentSelect.GetComponent<Renderer>();
            Terrain _unityTerrainComp = currentSelect.GetComponent<Terrain>();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if ((_currRenderer || editData.childRenderers != null) && !_unityTerrainComp) {
                if (_terrainDat) {
                    _terrainDat = null;
                }
                GUILayout.Label("Convert Object To Pure TerrainObject", PGUIStyle.nodeLabelBold);
            } else {
                if (!_terrainDat && _unityTerrainComp) {
                    GetHeightMap();
                }
                GUILayout.Label("Convert Unity Terrain To Pure TerrainObject", PGUIStyle.nodeLabelBold);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();

            GUILayout.BeginHorizontal("box");
            GUILayout.Label("name", PGUIStyle.nodeLabelBold);
            _terrainName = GUILayout.TextField(_terrainName, 25, GUILayout.Width(155));
            GUILayout.Label("empty = object name");
            GUILayout.EndHorizontal();

            if ((_currRenderer || editData.childRenderers != null) && !_unityTerrainComp) {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Create New Prefab", PGUIStyle.nodeLabelBold, GUILayout.Width(150));
                _createNewPrefab = EditorGUILayout.Toggle(_createNewPrefab, GUILayout.Width(53));
                GUILayout.EndHorizontal();
            } else {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Keep the textures", PGUIStyle.nodeLabelBold, GUILayout.Width(150));
                _keepTexture = EditorGUILayout.Toggle(_keepTexture, GUILayout.Width(53));
                GUILayout.EndHorizontal();
                GUILayout.Label("Can Keep the first 4 splats and first blend", GUILayout.Width(300));
            }

            if (_unityTerrainComp) {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                GUILayout.Label("Quality", PGUIStyle.nodeLabelBold);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                GUILayout.BeginHorizontal();
                GUILayout.Label("<");
                GUILayout.FlexibleSpace();
                _resolution = EditorGUILayout.IntField(_resolution, GUILayout.Width(30));
                GUILayout.Label("x" + _resolution + " : " + _resX*_resY + "Verts");
                GUILayout.FlexibleSpace();
                GUILayout.Label(">");
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                _resolution = (int) GUILayout.HorizontalScrollbar(_resolution, 0, 4, 350, GUILayout.Width(350));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                _tRes = _heightmapWidth/_resolution;
                _resX = (int) ((_heightmapWidth - 1)/_tRes + 1);
                _resY = (int) ((_heightmapHeight - 1)/_tRes + 1);

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                GUILayout.Label("Vertex Performances (Approximate Indications)", PGUIStyle.nodeLabelBold);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                float _t = _resX*_resY;
                GUILayout.BeginHorizontal();
                GUILayout.Space(50);
                GUILayout.BeginVertical();
                for (int i = 0, len = _platforms.Length; i < len; i++) {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(_platforms[i].name);
                    GUILayout.FlexibleSpace();
                    if (_t <= _platforms[i].min) {
                        GUILayout.Label(PResourceManager.LoadTexture("Terrain/ok.png"));
                    } else if (_t > _platforms[i].min && _t <= _platforms[i].max) {
                        GUILayout.Label(
                            PResourceManager.LoadTexture("Terrain/avoid.png"));
                    } else {
                        GUILayout.Label(PResourceManager.LoadTexture("Terrain/ko.png"));
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
                GUILayout.Space(50);
                GUILayout.EndHorizontal();
            }
            GUILayout.Box(PGUIStyle.BorderColor0, GUILayout.ExpandWidth(true), GUILayout.Height(1.0f));
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.Label("Might Take Some Time", PGUIStyle.nodeLabelBold);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (!_unityTerrainComp && (_currRenderer || editData.childRenderers != null)) {
                if (GUILayout.Button("Convert Object To Pure", GUILayout.Width(200), GUILayout.Height(30))) {
                    Object2Terrain();
                }
            } else {
                if (GUILayout.Button("Convert Unity Terrain To Pure", GUILayout.Width(200), GUILayout.Height(30))) {
                    Unity2Terrain();
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }


        private void DrawEndOperation() {
            GUILayout.Label("Pure Final Resolution : ", PGUIStyle.nodeLabelBold);
            if (editData.partsOfTerrain > 1) {
                GUILayout.Label("Vertex: ~" + editData.vertexInfo + " in " + editData.partsOfTerrain + " Parts");
            } else {
                GUILayout.Label("Vertex: " + editData.vertexInfo + " in " + editData.partsOfTerrain + " Part");
            }
            GUILayout.Label("Triangles : " + editData.trisInfo);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.TextArea(warning);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("save option:", GUILayout.Width(80));
            _saveOption = EditorGUILayout.IntPopup("", _saveOption, _optionNames, _options, GUILayout.Height(20));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();


            if (GUILayout.Button("Continue", GUILayout.Width(150), GUILayout.Height(30))) {
                string dir = SceneManager.GetActiveScene().path;
                dir = Path.GetDirectoryName(dir);

                switch (_saveOption) {
                    case 0:
                        Object.DestroyImmediate(currentSelect.gameObject);
                        Selection.activeTransform = editData.defaultChild.transform;
                        editData.trisInfo = 0;
                        editData.trisInfo = 0;
                        editData.partsOfTerrain = 0;
                        TerrainCenter.menuToolBar = 1;
                        UpdateTerrainComponent();
                        DeleteDirectory(dir + "/Materials");
                        AssetDatabase.Refresh();
                        break;

                    case 1:
                        Object.DestroyImmediate(editData.defaultChild);
                        editData.defaultChild = null;

                        DeleteDirectory(dir + "/" + _finalExportName);

                        editData.unityTerrain.SetActive(true);
                        editData.vertexInfo = 0;
                        editData.trisInfo = 0;
                        editData.partsOfTerrain = 0;
                        editData.unityTerrain = null;
                        _terrainDat = null;
                        AssetDatabase.Refresh();
                        if (editData.unityTerrain != null) editData.unityTerrain.SetActive(true);
                        break;

                    case 2:
                        if (editData.unityTerrain != null) editData.unityTerrain.SetActive(false);
                        editData.unityTerrain = null;
                        Selection.activeTransform = editData.defaultChild.transform;
                        editData.vertexInfo = 0;
                        editData.trisInfo = 0;
                        editData.partsOfTerrain = 0;
                        TerrainCenter.menuToolBar = 1;
                        UpdateTerrainComponent();

                        break;
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }


        private void UpdateTerrainComponent() {
            if (editData.nbrTerrainObject == 0) {
                TerrainObject _child = editData.defaultChild.gameObject.GetComponent<TerrainObject>();

                _child.enabledLODSystem = editData.activateLOD;
                _child.enabledBillboard = editData.activateBillboard;
                _child.enabledLayerCul = editData.activateLayerCul;
                _child.closeView = editData.closeView;
                _child.normalView = editData.normalView;
                _child.farView = editData.farView;
                _child.backgroundView = editData.backgroundView;
                _child.master = 1;
            }
        }


        private void GetHeightMap() {
            _terrainDat = currentSelect.GetComponent<Terrain>().terrainData;
            _heightmapWidth = _terrainDat.heightmapWidth;
            _heightmapHeight = _terrainDat.heightmapHeight;
        }


        private static void ValidateDirectory(string dir) {
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
        }


        private static void SetTextureFormat(TextureImporter importer) {
            TextureImporterSettings _settings = new TextureImporterSettings {
                textureType = TextureImporterType.Default,
                sRGBTexture = true,
                readable = true,
                aniso = 9,
                mipmapEnabled = false,
                alphaSource = TextureImporterAlphaSource.FromInput,
                alphaIsTransparency = false,
                wrapMode = TextureWrapMode.Clamp,
                textureShape = TextureImporterShape.Texture2D
            };
            importer.SetTextureSettings(_settings);
            importer.textureCompression = TextureImporterCompression.Uncompressed;
        }
    }
}