using edit.pure.etui.utils;
using edit.pure.ui;
using mono.ui.controls;
using pure.ui.core;
using pure.ui.grid;
using pure.ui.interfaces;
using pure.ui.renderer;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UIDataPane))]
    internal class Insp_UIDataPane : Insp_UIStyleCore {
        private PrefabPicker _picker;
        private Drawer_ScrollView _scrollDrawer;
        private ArrayListDrawer _listDrawer;

        public override void OnEnable() {
            base.OnEnable();
            _picker = new PrefabPicker(UIType.DataPane);
            _scrollDrawer = new Drawer_ScrollView();
            _listDrawer = new ArrayListDrawer("_skinList");
        }

        public override void OnInspectorGUI() {
            _picker.Draw(style);
            DrawSeperator();
            UIDataPane b = style as UIDataPane;
            if (b != null) {
                EditorGUI.BeginChangeCheck();
                _scrollDrawer.Draw(b.scrollSetting, b);
                DrawSeperator();
                b.skinIndex = EditorGUILayout.DelayedIntField("Skin Index", b.skinIndex);
                _listDrawer.Draw(serializedObject);
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
                DrawSeperator();
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
                EditorGUILayout.LabelField("Border (Left, Top, Right, Bottom)");
                b.border = EditorGUILayout.Vector4Field("", b.border);
                GUILayout.Space(10);
                b.controlObj = (UICore) EditorGUILayout.ObjectField("Pager Host", b.controlObj, typeof (UICore), true);
                if (b.controlObj && !(b.controlObj is ISelectableContainer)) {
                    EditorGUILayout.HelpBox(
                        "control host must be typeof (ISelectableUIContainer) include:\r\n\tTabNavigaor\r\n\tDataPane\r\r\tItemPane",
                        MessageType.Warning);
                }
                if (!b.GetComponent<GridLayoutCore>()) {
                    EditorGUILayout.HelpBox("Layout 丢失", MessageType.Error);
                }
                GUILayout.Space(10);
            }
        }
    }
}