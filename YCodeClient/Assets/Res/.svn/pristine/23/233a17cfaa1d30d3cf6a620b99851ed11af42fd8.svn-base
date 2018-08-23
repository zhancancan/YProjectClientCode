using mono.fbx;
using UnityEditor;
using UnityEngine;

namespace inspectors.fbx {
    [CustomEditor(typeof (Fx_BulletPrefab))]
    public class Insp_FbxBulletPrefab : Editor {
        public override void OnInspectorGUI() {
            if (Application.isPlaying) {
                if (GUILayout.Button("Play")) {
                    ((Fx_BulletPrefab) target).Play();
                }
            }
        }
    }
}