using mono.scene;
using pure.scene.misc;
using pure.scene.mono;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace plugins.nav {
    internal class NavEditorMenu {
        [MenuItem("GameObject/Nav/NavMesh", false, 1002)]
        private static void Create() {
            Scene sc = SceneManager.GetActiveScene();
            GameObject root = sc.GetRootGameObjects()[0];

            Navigator_Dll[] prev = root.GetComponentsInChildren<Navigator_Dll>();
            if (prev.Length > 0) {
                EditorUtility.DisplayDialog("warning", "has Nav mesh already", "OK");
            }
            root = Selection.activeGameObject;
            GameObject o = new GameObject("Nav Mesh");
            o.AddComponent<MeshCollider>();
            o.AddComponent<Navigator>();
            o.transform.position = Vector3.zero;
            o.layer = LayerType.COLLIDE;

            if (root != null) {
                o.transform.SetParent(root.transform);
            }
        }
    }
}