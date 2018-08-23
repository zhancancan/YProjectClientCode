using System.IO;
using edit.pure.tools.meshes;
using mono.terrain;
using pure.scene.misc;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace plugins.terrain {
    internal partial class TvConverter {
        private TerrainData _terrain;


        private void Unity2Terrain() {
            if (_terrainName == "") {
                _terrainName = currentSelect.name;
            }
            Scene sc = SceneManager.GetActiveScene();
            string scenePath = sc.path;
            string dir = Path.GetDirectoryName(scenePath);
            string exportName = GetUniqueDirectory(dir, _terrainName);
            dir = dir + "/" + exportName;
            ValidateDirectory(dir);

            AssetDatabase.Refresh();


            _terrain = currentSelect.GetComponent<Terrain>().terrainData;
            int w = _terrain.heightmapWidth;
            int h = _terrain.heightmapHeight;
            Vector3 _meshScale = _terrain.size;
            _meshScale = new Vector3(_meshScale.x/(h - 1)*_tRes, _meshScale.y, _meshScale.z/(w - 1)*_tRes);
            Vector2 _uvScale = new Vector2((float) (1.0/(w - 1)), (float) (1.0/(h - 1)));
            float[,] tData = _terrain.GetHeights(0, 0, w, h);

            w = (int) ((w - 1)/_tRes + 1);
            h = (int) ((h - 1)/_tRes + 1);

            Vector3[] _vertics = new Vector3[w*h];
            Vector2[] _uvs = new Vector2[w*h];
            int[] _polys = new int[(w - 1)*(h - 1)*6];

            int x, y;
            for (y = 0; y < h; y++) {
                for (x = 0; x < w; x++) {
                    int fx = (int) (x*_tRes);
                    int fy = (int) (y*_tRes);
                    _vertics[y*w + x] = Vector3.Scale(_meshScale, new Vector3(-y, tData[fx, fy], x));
                    _uvs[y*w + x] = Vector2.Scale(new Vector2(y*_tRes, x*_tRes), _uvScale);
                }
            }

            int index = 0;
            for (y = 0; y < h - 1; y++) {
                for (x = 0; x < w - 1; x++) {
                    _polys[index++] = y*w + x;
                    _polys[index++] = (y + 1)*w + x;
                    _polys[index++] = y*w + x + 1;
                    _polys[index++] = (y + 1)*w + x;
                    _polys[index++] = (y + 1)*w + x + 1;
                    _polys[index++] = y*w + x + 1;
                }
            }


            ObjExporter o = new ObjExporter(_vertics, _uvs, _polys, dir + "/" + exportName + ".obj") {
                progressBar = progressBar
            };
            o.Execute();

            string pngName = dir + "/" + exportName + ".png";

            string _assetName = AssetDatabase.GetAssetPath(_terrain);
            Object[] _assets = AssetDatabase.LoadAllAssetsAtPath(_assetName);
            if (_assets != null && _assets.Length > 1 && _keepTexture) {
                for (int i = 0, len = _assets.Length; i < len; i++) {
                    if (_assets[i].name == "SplatAlpha 0") {
                        Texture2D _texture = _assets[i] as Texture2D;
                        if (_texture != null) {
                            byte[] _bytes = _texture.EncodeToPNG();
                            File.WriteAllBytes(pngName, _bytes);
                            AssetDatabase.ImportAsset(pngName, ImportAssetOptions.ForceUpdate);
                        }
                    }
                }
            } else {
                Texture2D _newMaskTexture = new Texture2D(512, 512, TextureFormat.RGBA32, true);
                Color[] _colorBase = new Color[512*512];
                for (int i = 0, len = _colorBase.Length; i < len; i++) {
                    _colorBase[i] = new Color(1, 0, 0, 0);
                }
                _newMaskTexture.SetPixels(_colorBase);
                byte[] _bytes = _newMaskTexture.EncodeToPNG();
                File.WriteAllBytes(pngName, _bytes);
                AssetDatabase.ImportAsset(pngName, ImportAssetOptions.ForceUpdate);
            }

            AssetDatabase.ImportAsset(pngName, ImportAssetOptions.ForceUpdate);
            TextureImporter temp = AssetImporter.GetAtPath(pngName) as TextureImporter;
            SetTextureFormat(temp);


            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset(pngName, ImportAssetOptions.ForceUpdate);

            var tempMat = new Material(Shader.Find(TerrainCenter.DEFAULT_SHADER));
            string _matPath = dir + "/" + exportName + ".mat";
            AssetDatabase.CreateAsset(tempMat, _matPath);
            AssetDatabase.ImportAsset(_matPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();


            if (_keepTexture) {
                SplatPrototype[] texts = _terrain.splatPrototypes;
                for (int i = 0; i < texts.Length; i++) {
                    if (i < 4) {
                        tempMat.SetTexture("_Splat" + i, texts[i].texture);
                        tempMat.SetTextureScale("_Splat" + i, texts[i].tileSize*8.9f); // why 8.9 here?
                    }
                }
            }
            Texture _test = (Texture) AssetDatabase.LoadAssetAtPath(pngName, typeof (Texture));
            tempMat.SetTexture("_Control", _test);


            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(dir + "/" + exportName + ".obj");
            AssetDatabase.Refresh();
            GameObject forReplace = Object.Instantiate(prefab, currentSelect.position, Quaternion.identity);
            Transform childCheck = forReplace.transform.Find("default");
            editData.defaultChild = childCheck.gameObject;
            forReplace.transform.DetachChildren();
            Object.DestroyImmediate(forReplace);

            editData.defaultChild.name = exportName;
            editData.defaultChild.AddComponent<TerrainObject>();

            editData.defaultChild.GetComponent<TerrainObject>().material = tempMat;
            editData.defaultChild.GetComponent<TerrainObject>().convertType = "UT"; 

            editData.vertexInfo = 0;
            editData.partsOfTerrain = 0;
            editData.trisInfo = 0;

            int countChild = editData.defaultChild.transform.childCount;
            if (countChild > 0) {
                Renderer[] _objParts = editData.defaultChild.GetComponentsInChildren<Renderer>();
                for (int i = 0, len = _objParts.Length; i < len; i++) {
                    if (!_objParts[i].gameObject.GetComponent<MeshCollider>()) {
                        _objParts[i].gameObject.AddComponent<MeshCollider>();
                    }
                    _objParts[i].gameObject.isStatic = true;
                    _objParts[i].material = tempMat;
                    _objParts[i].gameObject.layer = 30;
                    _objParts[i].gameObject.AddComponent<TerrainPart>();
                    MeshFilter _meshFilter = _objParts[i].GetComponent<MeshFilter>();
                    editData.defaultChild.GetComponent<TerrainObject>().mesh = _meshFilter;
                    editData.partsOfTerrain += 1;
                    editData.vertexInfo += _meshFilter.sharedMesh.vertexCount;
                    editData.trisInfo += _meshFilter.sharedMesh.triangles.Length/3;
                }
            } else {
                editData.defaultChild.AddComponent<MeshCollider>();
                editData.defaultChild.isStatic = true;
                editData.defaultChild.GetComponent<Renderer>().material = tempMat;
                editData.defaultChild.layer = 30;

                MeshFilter _meshFilter = editData.defaultChild.GetComponent<MeshFilter>();
                editData.vertexInfo += _meshFilter.sharedMesh.vertexCount;
                editData.trisInfo += _meshFilter.sharedMesh.triangles.Length/3;
                editData.partsOfTerrain += 1;
            }


            GameObject _basePrefab = PrefabUtility.CreatePrefab(dir + "/" + exportName + ".prefab",
                editData.defaultChild);
            AssetDatabase.ImportAsset(dir + "/" + exportName + ".prefab", ImportAssetOptions.ForceUpdate);
            forReplace = PrefabUtility.InstantiatePrefab(_basePrefab) as GameObject;
            Object.DestroyImmediate(editData.defaultChild.gameObject);
            editData.defaultChild = forReplace.gameObject;

            currentSelect.GetComponent<Terrain>().enabled = false;

            EditorUtility.SetSelectedRenderState(editData.defaultChild.GetComponent<Renderer>(),
                EditorSelectedRenderState.Highlight);

            editData.unityTerrain = currentSelect.gameObject;

            EditorUtility.ClearProgressBar();
            _terrainName = "";
            AssetDatabase.StartAssetEditing();
            ModelImporter objImporter =
                AssetImporter.GetAtPath(dir + "/" + exportName + ".obj") as ModelImporter;
            if (objImporter != null) {
                objImporter.globalScale = 1;
                objImporter.importTangents = ModelImporterTangents.CalculateLegacyWithSplitTangents;
                objImporter.importNormals = ModelImporterNormals.Calculate;
                objImporter.generateAnimations = ModelImporterGenerateAnimations.None;
                objImporter.meshCompression = ModelImporterMeshCompression.Off;
                objImporter.normalSmoothingAngle = 180f;
            }

            AssetDatabase.ImportAsset(dir + "/" + exportName + ".obj", ImportAssetOptions.ForceSynchronousImport);
            AssetDatabase.StopAssetEditing();

            PrefabUtility.ResetToPrefabState(editData.defaultChild);

            _finalExportName = exportName;
        }
    }
}