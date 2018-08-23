using System.IO;
using edit.pure.place;
using edit.pure.tools.assets;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace plugins.place {
    internal class PlaceEditor : EditorWindow {
        [MenuItem("EditorTools/Place/Create", false, 2050)]
        protected static void CreatePlace() {
            PlaceEditor window = GetWindow<PlaceEditor>(true, "Create Place", true);
            window.minSize = window.maxSize = new Vector2(300, 120);
            Rect p = window.position;
            p.center = new Rect(0f, 0f, Screen.currentResolution.width, Screen.currentResolution.height).center;
            window.position = p;
        }

        [MenuItem("EditorTools/Place/Export To Server", false, 2050)]
        protected static void ExportToServer() {
            new ExportPlaceToBinary().Start(false);
            new ExportPlaceToServer().Execute();
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("info", "输出完成", "OK");
        }

        [OnOpenAsset(1)]
        protected static bool AutotOpenCanvas(int instanceId, int line) {
            string path = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceId));
            string name = Application.dataPath + "/" + path.Replace("Assets/", "");
            if (name.EndsWith(".place")) {
                new OpenPlaceCommand(path, true).Execute();
                return true;
            }
            return false;
        }

        private string _placeName = "";
        private SceneAsset _sceneAsset;

        protected void OnGUI() {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Place:", GUILayout.MaxWidth(60));
            _placeName = GUILayout.TextField(_placeName);
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Scene:", GUILayout.MaxWidth(60));
            _sceneAsset = EditorGUILayout.ObjectField(_sceneAsset, typeof (SceneAsset), false) as SceneAsset;
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("OK", GUILayout.MaxWidth(100))) {
                DoCreate();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void DoCreate() {
            if (_sceneAsset == null) {
                EditorUtility.DisplayDialog("warning", "没有选中场景", "OK");
                return;
            }
            string p = AssetDatabase.GetAssetPath(_sceneAsset);
            if (!p.Contains("_output")) {
                EditorUtility.DisplayDialog("warning", "只有包含_output的场景才是可引用场景", "OK");
                return;
            }
            string sceneName = EditorFileTools.GetFileName(p);
            if (File.Exists(p) == false) {
                EditorUtility.DisplayDialog("warning", sceneName + " not exported yet!", "OK");
                return;
            }
            if (string.IsNullOrEmpty(_placeName)) {
                EditorUtility.DisplayDialog("warning", "place name is needed", "OK");
                return;
            }
            string placeFolder = PlaceCenter.SOURCE + "/" + _placeName;
            if (Directory.Exists(placeFolder)) {
                EditorUtility.DisplayDialog("warning", _placeName + " 已经存在!!!", "OK");
                return;
            }
            string sceneFile = AssetDatabase.GetAssetPath(_sceneAsset);
            string nav = EditorFileTools.GetFolder(sceneFile);
            nav += "/" + "nav.bin";
            if (!File.Exists(nav)) {
                EditorUtility.DisplayDialog("warning", string.Format("{0} 没有NavMesh,联系美术", sceneName), "OK");
            } else {
                new CreatePlaceCommand(sceneFile, _placeName).Execute();
                Close();
            }
        }
    }
}