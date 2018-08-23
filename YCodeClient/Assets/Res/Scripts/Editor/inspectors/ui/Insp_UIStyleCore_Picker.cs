using System;
using edit.pure.etui.stylePick;
using edit.pure.etui.utils;
using edit.pure.tools.assets;
using pure.ui.core;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace inspectors.ui {
    internal abstract partial class Insp_UIStyleCore {
        protected class PrefabPicker {
            private UIType _type;
            private Rect _pos;
            private UICore _target;

            public PrefabPicker(UIType type) {
                _type = type;
            }

            internal void Draw(UICore core) {
                _target = core;
                string s;
                GUIContent c;
                if (core.prefab) {
                    s = AssetDatabase.GetAssetPath(core.prefab);
                    c = new GUIContent(EditorFileTools.GetFileName(s), s);
                } else {
                    c = new GUIContent("none");
                }
                GUILayout.BeginHorizontal();
                GUILayout.Label("prefab", GUILayout.Width(60));
                if (GUILayout.Button(c, "textfield", GUILayout.MaxWidth(150))) {
                    ShowFilter();
                }
                if (Event.current.type == EventType.Repaint) {
                    _pos = GUILayoutUtility.GetLastRect();
                }
                if (GUILayout.Button("", "IN ObjectField", GUILayout.MinWidth(18), GUILayout.MaxWidth(18f))) {
                    ShowFilter();
                }
                UpdateDragAction();
                GUILayout.EndHorizontal();
            }

            private void UpdateDragAction() {
                Event e = Event.current;
                switch (e.type) {
                    case EventType.DragUpdated:
                    case EventType.DragPerform:
                        if (_pos.Contains(e.mousePosition) == false) {
                            return;
                        }
                        GameObject o = GetDraggngObject();
                        if (o) {
                            DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                            if (e.type == EventType.DragPerform && _target.prefab != o) {
                                _target.prefab = o;
                            }
                        }
                        break;
                }
            }

            private GameObject GetDraggngObject() {
                Object[] os = DragAndDrop.objectReferences;
                foreach (var o in os) {
                    if (o is GameObject && UIPrefabUtility.IsFilterStyle(o as GameObject, _type)) {
                        return o as GameObject;
                    }
                }
                return null;
            }

            private void ShowFilter() {
                Action<GameObject> a = o => {
                    if (o != null && _target.prefab != o) {
                        _target.prefab = o;
                    }
                };
                StylePickCenter.Show(a, _type, _target.prefab);
            }
        }
    }
}