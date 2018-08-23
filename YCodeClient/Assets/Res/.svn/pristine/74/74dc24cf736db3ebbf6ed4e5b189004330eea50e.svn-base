using edit.pure.meshCut;
using mono.fbx;
using UnityEditor;
using UnityEngine;

namespace inspectors.fbx {
    [CustomEditor(typeof (MeshCut))]
    public class Insp_MeshCut : Editor {
        protected void OnEnable() {
            MeshCut t = target as MeshCut;
            if (t && t.target) {
                Mesh m = t.target.sharedMesh ?? t.target.mesh;
                if (m) MeshCutCenter.Start(t.transform, m);
            }
        }

        public override void OnInspectorGUI() {
            MeshCut t = target as MeshCut;
            if (!t) return;
            EditorGUI.BeginChangeCheck();
            t.target = (MeshFilter) EditorGUILayout.ObjectField("Select", t.target, typeof (MeshFilter), true);
            if (EditorGUI.EndChangeCheck()) {
                Mesh m = t.target ? t.target.sharedMesh ?? t.target.mesh : null;
                if (m) {
                    MeshCutCenter.Start(t.transform, m);
                } else {
                    MeshCutCenter.Stop();
                }
            }
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Cut", GUILayout.MaxWidth(100))) {
                    MeshCutCenter.Cut();
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
        }

        protected void OnSceneGUI() {
            MeshCutCenter.input.Update();
            MeshCutCenter.Update();
        }

        protected void OnDisable() {
            MeshCutCenter.Stop();
        }
    }
}