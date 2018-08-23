using edit.pure.etui.utils;
using edit.pure.ui;
using mono.ui.controls;
using pure.ui.core;
using pure.ui.grid;
using pure.ui.renderer;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UIItemPanel))]
    internal class Insp_UIItemPane : Insp_UIStyleCore {
        private ArrayListDrawer _skinListDrawer;

        public override void OnEnable() {
            base.OnEnable();
            _skinListDrawer = new ArrayListDrawer("_skinList");
        }

        public override void OnInspectorGUI() {
            DrawSeperator();
            UIItemPanel b = style as UIItemPanel;
            if (b != null) {
                EditorGUI.BeginChangeCheck();
                b.skinIndex = EditorGUILayout.DelayedIntField("Skin Index", b.skinIndex);
                _skinListDrawer.Draw(serializedObject);
                if (b.skinCount == 0) {
                    EditorGUILayout.HelpBox("No Skin Picked", MessageType.Error);
                } else {
                    for (int i = 0; i < b.skinCount; i++) {
                        GameObject o = b.GetSkinAt(i);
                        if (!o)
                            EditorGUILayout.HelpBox("skin 丢失", MessageType.Error);
                        else if (!UIPrefabUtility.IsFilterStyle(o, UIType.CellSkin))
                            EditorGUILayout.HelpBox(string.Format("{0}不是cell skill", o.name), MessageType.Error);
                    }
                }
                EditorGUI.BeginChangeCheck();
                int count = EditorGUILayout.DelayedIntField("Test Count:", b.length);
                if (EditorGUI.EndChangeCheck()) {
                    if (count < 0) count = 1;
                    object[] nt = new object[count];
                    for (int i = 0; i < count; i++) {
                        nt[i] = new DataPaneTestData {name = "test_" + i};
                    }
                    b.SetList(nt);
                }
                b.selectable = EditorGUILayout.Toggle("Selectable", b.selectable);
                b.allowMultipleSelecton = EditorGUILayout.Toggle("Multi Selectable", b.allowMultipleSelecton); 
                if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(b);
                if (!b.GetComponent<GridLayoutCore>()) EditorGUILayout.HelpBox("Layout 丢失", MessageType.Error);
                GUILayout.Space(10);
            }
        }
    }
}