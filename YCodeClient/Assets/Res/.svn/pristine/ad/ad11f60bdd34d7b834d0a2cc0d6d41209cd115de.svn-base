using pure.scene.misc;
using UnityEditor;
using UnityEngine;

namespace inspectors.place {
    public abstract class Insp_PlaceDataCore : Editor {
        protected void DrawSnap() {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Snap To Group", GUILayout.Width(100))) {
                SnapToGroup();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void SnapToGroup() {
            foreach (Object o in targets) {
                MonoBehaviour a = o as MonoBehaviour;
                if (a) {
                    Vector3 p = a.transform.position;
                    Ray ray = new Ray(new Vector3(p.x, 1000000, p.z), Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerType.COLLIDE)) {
                        a.transform.position = hit.point;
                        EditorUtility.SetDirty(a);
                    } else {
                        Debug.Log("hit nothing");
                    }
                }
            }
        }
    }
}