using mono.scene;
using UnityEditor;
using UnityEngine;

namespace inspectors.materials {
    [CustomEditor(typeof (LightmapBake))]
    internal class Insp_LightmapBake : Editor {
        private static bool foldOut;

        public override void OnInspectorGUI() {
            LightmapBake bake = target as LightmapBake;
            if (!bake) return;
            GUILayout.Label("Number Light Maps:" + bake.numLightmaps);
            foldOut = EditorGUILayout.Foldout(foldOut, string.Format("total renderers : {0}", bake.lightInfos.Length));
            if (foldOut) {
                for (int i = 0; i < bake.lightInfos.Length; i++) {
                    GUILayout.Label(string.Format("renderer:\t{0}", bake.lightInfos[i].renderer.name));
                }
            }
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Collect", GUILayout.Width(100), GUILayout.Height(30))) {
                bake.CollectLightMapTextures();
                EditorUtility.SetDirty(bake);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}