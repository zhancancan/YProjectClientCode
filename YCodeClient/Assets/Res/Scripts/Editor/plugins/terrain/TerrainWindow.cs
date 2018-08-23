using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    public class TerrainWindow : EditorWindow {
        private static TerrainWindow _editor;

        [MenuItem("EditorTools/Terrain Editor", false, 2101)]
        private static void Initialize() {
            _editor = GetWindow<TerrainWindow>();
            _editor.minSize = new Vector2(400, 600);
            _editor.Show();
        }


        private void OnEnable() {
            TerrainCenter.Start();
        }

        private void OnInspectorUpdate() {
            Repaint();
        }


        private void OnGUI() {
            if (TerrainCenter.SetupGUI()) {
                Rect r = new Rect(position) {x = 0, y = 0};
                GUI.Box(r, "", PGUIStyle.nodeBox);
                TerrainCenter.OnGUI();
            }
        }

        private void OnDestroy() {
            TerrainCenter.Stop();
        }
    }
}