using mono.ui.controls;
using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UITreePane))]
    internal class Insp_UITreePane : Insp_UIStyleCore {
        private PrefabPicker _picker;
        private Drawer_ScrollView _scrollDrawer;

        public override void OnEnable() {
            base.OnEnable();
            _picker = new PrefabPicker(UIType.DataPane);
            _scrollDrawer = new Drawer_ScrollView();
        }

        public override void OnInspectorGUI() {
            _picker.Draw(style);
            DrawSeperator();
            UITreePane b = style as UITreePane;
            if (b != null) {
                _scrollDrawer.Draw(b.scrollSetting, b);
                DrawSeperator();
                EditorGUI.BeginChangeCheck();


                EditorGUI.BeginChangeCheck();
                b.rowHeight = EditorGUILayout.DelayedFloatField("Row Height", b.rowHeight);
                b.margin = EditorGUILayout.Vector2Field("Margin (Left, Top,)", b.margin);
                b.itemInterval = EditorGUILayout.Vector2Field("Item Interval", b.itemInterval);

                EditorGUILayout.LabelField("Border (Left, Top, Right, Bottom)");
                b.border = EditorGUILayout.Vector4Field("", b.border);


                DrawSeperator();

                b.selectable = EditorGUILayout.Toggle("Selectable", b.selectable);
                b.branchSelectable = EditorGUILayout.Toggle("Branch Selectable", b.branchSelectable);
                b.SingleOpenNode = EditorGUILayout.Toggle("Single Node Open", b.SingleOpenNode);


                if (EditorGUI.EndChangeCheck()) {
                    EditorUtility.SetDirty(b);
                }
                EditorGUI.indentLevel--;
            }
            DrawSeperator();
            DrawSolovView();
            GUILayout.Space(10);
        }
    }
}