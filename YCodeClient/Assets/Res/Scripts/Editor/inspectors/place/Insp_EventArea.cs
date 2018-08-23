using mono.scene;
using pure.scene.misc;
using pure.scene.mono;
using pure.utils.coroutine;
using UnityEditor;
using UnityEngine;

namespace inspectors.place {
    [CustomEditor(typeof (EventArea)), CanEditMultipleObjects]
    internal class Insp_EventArea : Insp_PlaceDataCore {
        private EventArea _area;
        private SerializedObject[] _sobs;

        protected void OnEnable() {
            _area = target as EventArea;
            _sobs = new SerializedObject[targets.Length];
            for (int i = 0; i < targets.Length; i++) {
                _sobs[i] = new SerializedObject(targets[i]);
            }
        }

        public override void OnInspectorGUI() {
            foreach (var o in targets) {
                Undo.RecordObject(o, "EventArea" + o.name);
            }
            EditorGUI.BeginChangeCheck();
            _area.shape = (EventArea_Dll.Shape) EditorGUILayout.EnumPopup("Shape", _area.shape);
            if (EditorGUI.EndChangeCheck()) {
                foreach (Object t in targets) {
                    EventArea a = t as EventArea;
                    if (a) {
                        a.invalidator.Invalidate(InvalidateType.Style);
                        a.shape = _area.shape;
                        EditorUtility.SetDirty(a);
                    }
                }
            }
            switch (_area.shape) {
                case EventArea_Dll.Shape.Circle:
                    EditorGUI.BeginChangeCheck();
                    _area.radius = EditorGUILayout.FloatField("Radius", _area.radius);
                    if (EditorGUI.EndChangeCheck()) {
                        foreach (Object t in targets) {
                            EventArea a = t as EventArea;
                            if (a) {
                                a.radius = _area.radius;
                                _area.invalidator.Invalidate(InvalidateType.Size);
                                EditorUtility.SetDirty(_area);
                            }
                        }
                    }
                    break;
                case EventArea_Dll.Shape.Rect:
                    EditorGUI.BeginChangeCheck();
                    _area.width = EditorGUILayout.FloatField("Width", _area.width);
                    _area.depth = EditorGUILayout.FloatField("Depth", _area.depth);
                    if (EditorGUI.EndChangeCheck()) {
                        foreach (Object t in targets) {
                            EventArea a = t as EventArea;
                            if (a) {
                                a.width = _area.width;
                                a.depth = _area.depth;
                                a.invalidator.Invalidate(InvalidateType.Size);
                                EditorUtility.SetDirty(a);
                            }
                        }
                    }
                    break;
            }
            EditorGUI.BeginChangeCheck();
            _area.color = EditorGUILayout.ColorField("Color", _area.color);
            if (EditorGUI.EndChangeCheck()) {
                foreach (Object t in targets) {
                    EventArea a = t as EventArea;
                    a.color = _area.color;
                    a.Invalid(InvalidateType.Data);
                }
                EditorUtility.SetDirty(_area);
            }
            EditorGUI.BeginChangeCheck();
            _area.ignoreMask = EditorGUILayout.MaskField(new GUIContent("Igore Layers"), _area.ignoreMask,
                LayerType.LayerNames);
            if (EditorGUI.EndChangeCheck()) {
                foreach (Object t in targets) {
                    EventArea a = t as EventArea;
                    a.ignoreMask = _area.ignoreMask;
                    a.Invalid(InvalidateType.Data);
                }
                EditorUtility.SetDirty(_area);
            }
            Event e = Event.current;
            if ((e.type == EventType.ValidateCommand || e.type == EventType.ExecuteCommand) &&
                e.commandName == "UndoRedoPerformed") {
                foreach (SerializedObject o in _sobs) {
                    EventArea a = o.targetObject as EventArea;
                    if (a) {
                        a.invalidator.Invalidate();
                    }
                }
            }
            DrawSnap();
        }

        protected void OnDisable() {
            _area = null;
            _sobs = null;
        }
    }
}