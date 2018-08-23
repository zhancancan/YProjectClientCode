using System.IO;
using mono.terrain;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace plugins.terrain {
    internal partial class TvConverter {
        private void Object2Terrain() {
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

            Texture2D _maskTexture = new Texture2D(512, 512, TextureFormat.RGBA32, true);

            Color[] _colorBase = new Color[512*512];
            for (int i = 0, len = _colorBase.Length; i < len; i++) {
                _colorBase[i] = new Color(1, 0, 0, 0);
            }
            _maskTexture.SetPixels(_colorBase);


            string path = dir + "/SplatAlpha0.png";
            byte[] bytes = _maskTexture.EncodeToPNG();
            File.WriteAllBytes(path, bytes);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            TextureImporter temp = AssetImporter.GetAtPath(path) as TextureImporter;
            SetTextureFormat(temp);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            Material tempMat = new Material(Shader.Find(TerrainCenter.DEFAULT_SHADER));


            string _matPath = dir + "/" + exportName + ".mat";
            AssetDatabase.CreateAsset(tempMat, _matPath);
            AssetDatabase.ImportAsset(_matPath, ImportAssetOptions.ForceUpdate);

            Texture _finalMat = (Texture) AssetDatabase.LoadAssetAtPath(path, typeof (Texture));
            currentSelect.gameObject.AddComponent<TerrainObject>();

            int countChild = currentSelect.transform.childCount;
            if (countChild > 0) {
                Renderer[] _objParts = currentSelect.GetComponentsInChildren<Renderer>();
                for (int i = 0, len = _objParts.Length; i < len; i++) {
                    if (_objParts[i].gameObject.GetComponent<Collider>()) {
                        Object.DestroyImmediate(_objParts[i].gameObject.GetComponent<Collider>());
                    }
                    _objParts[i].gameObject.AddComponent<MeshCollider>();
                    _objParts[i].gameObject.isStatic = true;
                    _objParts[i].material = tempMat;
                    _objParts[i].gameObject.AddComponent<TerrainPart>();
                    currentSelect.GetComponent<TerrainObject>().mesh = _objParts[i].GetComponent<MeshFilter>();
                }
            } else {
                if (currentSelect.GetComponent<Collider>()) {
                    Object.DestroyImmediate(currentSelect.GetComponent<Collider>());
                }
                currentSelect.gameObject.AddComponent<MeshCollider>();
                currentSelect.gameObject.GetComponent<Renderer>().material = tempMat;
                currentSelect.gameObject.GetComponent<TerrainObject>().material = tempMat;
                currentSelect.gameObject.GetComponent<TerrainObject>().mesh =
                    currentSelect.gameObject.GetComponent<MeshFilter>();
            }

            currentSelect.gameObject.GetComponent<TerrainObject>().material = tempMat;
            tempMat.SetTexture("_Control", _finalMat);
            currentSelect.gameObject.isStatic = true;
            currentSelect.gameObject.layer = 30;

            if (editData.nbrTerrainObject == 0) {
                TerrainObject t = currentSelect.gameObject.GetComponent<TerrainObject>();
                t.enabledLODSystem = editData.activateLOD;
                t.enabledBillboard = editData.activateBillboard;
                t.enabledLayerCul = editData.activateLayerCul;
                t.closeView = editData.closeView;
                t.normalView = editData.normalView;
                t.farView = editData.farView;
                t.backgroundView = editData.backgroundView;
                t.master = 1;
            }


            if (_createNewPrefab) {
                string _prefabPath = dir + "/" + exportName + ".prefab";
                Object _basePrefab = PrefabUtility.CreateEmptyPrefab(_prefabPath);
                PrefabUtility.ReplacePrefab(currentSelect.gameObject, _basePrefab);
                GameObject _pf = (GameObject) AssetDatabase.LoadAssetAtPath(_prefabPath, typeof (GameObject));
                GameObject forRotate = (GameObject) PrefabUtility.InstantiatePrefab(_pf);
                Object.DestroyImmediate(currentSelect.gameObject);
                Selection.activeTransform = forRotate.transform;
                EditorUtility.SetSelectedRenderState(forRotate.GetComponent<Renderer>(),
                    EditorSelectedRenderState.Highlight);
                forRotate.gameObject.name = exportName;
            } else {
                Selection.activeTransform = currentSelect.transform;
                EditorUtility.SetSelectedRenderState(currentSelect.GetComponent<Renderer>(),
                    EditorSelectedRenderState.Highlight);
                currentSelect.gameObject.name = exportName;
            }

            EditorUtility.DisplayDialog("Pure Terrain Message", "Conversion is Accomplished", "OK");
            TerrainCenter.menuToolBar = 1;
            _terrainName = "";
            AssetDatabase.SaveAssets();
        }
    }
}