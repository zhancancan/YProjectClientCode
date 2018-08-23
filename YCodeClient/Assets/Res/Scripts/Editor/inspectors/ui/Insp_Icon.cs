using mono.ui.elements;
using UnityEditor;

namespace inspectors.ui {
    [CustomEditor(typeof (Icon))]
    internal class Insp_Icon : Editor {
        private Icon _icon;

        protected void OnEnable() {
            _icon = target as Icon;
        }

        public override void OnInspectorGUI() {
            _icon.icon = EditorGUILayout.DelayedTextField("Icon", _icon.icon);
        }
    }
}