using edit.pure.resource;
using mono.terrain;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    internal class TvOptmaizer : TerrianEditor {
        private int _optimizeLevel;
        private string[] _optimizeWords = {"Good", "Very Good", "Impressive", "Amazing"};

        private string[] _optimizeFuncType = {
            "Optimize Mesh",
            "Optimize Mesh + Low Compression",
            "Optimize Mesh + Medium Compression",
            "Optimize Mesh + High Compression"
        };

        private string[] _optimizeImpact = {
            "Nothing",
            "Low Degradation",
            "Medium Degradation",
            "High Degradation"
        };


        public override void OnGUI() {
            if (currentSelect.GetComponent<TerrainObject>() != null) {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Optimization of load time", PGUIStyle.nodeLabelBold);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Level:", PGUIStyle.nodeLabelBold);
                _optimizeLevel = (int) EditorGUILayout.Slider(_optimizeLevel, 0, 3);
                GUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.Space();


                GUILayout.Label("Load Time", PGUIStyle.nodeLabelBold);
                GUILayout.Label(_optimizeWords[_optimizeLevel]);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                GUILayout.Label("Type", PGUIStyle.nodeLabelBold);
                GUILayout.Label(_optimizeFuncType[_optimizeLevel]);

                GUILayout.Label("Mesh Impact", PGUIStyle.nodeLabelBold);
                GUILayout.Label(_optimizeImpact[_optimizeLevel]);

                EditorGUILayout.Space();
                EditorGUILayout.Space();


                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Might Take Some Time!", PGUIStyle.nodeLabelBold);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Process", GUILayout.Width(100), GUILayout.Height(30))) {
                    DoOptimize();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            } else {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Please Select the Pure Terrain object", PGUIStyle.nodeLabelBold);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }


        private void DoOptimize() {
            string _assetName = "";
            int numchildren = currentSelect.transform.childCount;
            if (numchildren > 0) {
                MeshFilter[] _parts = currentSelect.GetComponentsInChildren<MeshFilter>();
                _assetName = AssetDatabase.GetAssetPath(_parts[0].sharedMesh);
            } else {
                MeshFilter _mesh = currentSelect.GetComponent<MeshFilter>();
                _assetName = AssetDatabase.GetAssetPath(_mesh.sharedMesh);
            }
            ModelImporter _porter = AssetImporter.GetAtPath(_assetName) as ModelImporter;
            if (_porter != null) {
                switch (_optimizeLevel) {
                    case 0:
                        _porter.optimizeMesh = true;
                        break;
                    case 1:
                        _porter.optimizeMesh = true;
                        _porter.meshCompression = ModelImporterMeshCompression.Low;
                        break;
                    case 2:
                        _porter.optimizeMesh = true;
                        _porter.meshCompression = ModelImporterMeshCompression.Medium;
                        break;
                    case 3:
                        _porter.optimizeMesh = true;
                        _porter.meshCompression = ModelImporterMeshCompression.High;
                        break;
                }
                AssetDatabase.ImportAsset(_assetName, ImportAssetOptions.ForceUpdate);
                PrefabUtility.RevertPrefabInstance(currentSelect.gameObject);

                EditorUtility.DisplayDialog("Optizier", "Complete", "OK");
            } else {
                Debug.LogError("no asset found at" + _assetName);
            }
        }
    }
}