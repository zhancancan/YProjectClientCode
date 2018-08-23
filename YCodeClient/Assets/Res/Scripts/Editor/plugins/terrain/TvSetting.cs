using edit.pure.resource;
using mono.terrain;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    internal partial class TvSetting : TerrianEditor {
        private int _menuIndex;
        private string[] _menuLabels = {"Setting", "ATS Mobile Foliage"};

        public override void OnGUI() {
            if (currentSelect.GetComponent<TerrainObject>()) {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                _menuIndex = GUILayout.Toolbar(_menuIndex, _menuLabels);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.Label("Cleaning Scene", PGUIStyle.nodeLabelBold);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Clean Now", GUILayout.Width(200), GUILayout.Height(20))) {
                    MeshRenderer[] _prev = Object.FindObjectsOfType<MeshRenderer>();
                    foreach (var r in _prev) {
                        if (r.hideFlags == HideFlags.HideInHierarchy) {
                            r.hideFlags = HideFlags.None;
                            Object.DestroyImmediate(r.gameObject);
                        }
                    }
                    EditorUtility.DisplayDialog("Scene Cleaned", "", "OK");
                }

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.Space(10);
                switch (_menuIndex) {
                    case 0:
                        DrawSetting();
                        break;
                }
            }
        }
    }
}