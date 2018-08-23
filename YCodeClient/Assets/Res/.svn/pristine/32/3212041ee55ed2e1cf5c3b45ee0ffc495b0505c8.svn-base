using mono.terrain;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    [CustomEditor(typeof (TerrainObject))]
    [CanEditMultipleObjects]
    public class TerrainInspector : Editor {
        private int _status = -1;
        private TerrainAction[] _actions = {new TaPaint()};

        private void OnSceneGUI() {
            TerrainCenter.input.Update();
            int _st = -1;
            for (int i = 0; i < _actions.Length; i++) {
                if (_actions[i].Execute()) {
                    _st = i;
                }
            }
            if (_st != _status) {
                KillPreview();
                _status = _st;
            }
            Event e = Event.current;

            if (e.type == EventType.KeyUp && e.control) {
                switch (e.keyCode) {
                    case KeyCode.Z:
                        TerrainCenter.undo.Undo();
                        break;
                    case KeyCode.Y:
                        TerrainCenter.undo.Redo();
                        break;
                }
            }
        }

        private void KillPreview() {
            MeshRenderer[] prev = FindObjectsOfType<MeshRenderer>();
            foreach (var p in prev) {
                if (p.hideFlags == HideFlags.HideInHierarchy || p.name == TerrainCenter.PREVIEWER_NAME) {
                    p.hideFlags = HideFlags.None;
                    DestroyImmediate(p.gameObject);
                }
            }
        }
    }
}