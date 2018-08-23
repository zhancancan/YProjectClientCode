using edit.pure.resource;
using mono.ui.elements;
using pure.ui.element;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (Panel))]
    internal class Insp_Panel : Editor {
        private Panel _panel;

        internal void OnEnable() {
            _panel = (Panel) target;
        }


        public override void OnInspectorGUI() {
            _panel.process = (ProductProcss) EditorGUILayout.EnumPopup("Process", _panel.process);
            EditStyles.DrawHorizontalSeperator();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Collect Data", GUILayout.Width(150), GUILayout.Height(24))) {
                //    new PickSelectPanel(_panel.gameObject).Execute();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}