using pure.ui.element;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomPropertyDrawer(typeof (UIAnimUnit))]
    public class Insp_UIAnimationDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty k = property.FindPropertyRelative("duration");
            SerializedProperty d = property.FindPropertyRelative("mode");
            SerializedProperty v = property.FindPropertyRelative("clip");
            Rect kr = new Rect(position) {x = 60, width = 60};
            Rect dr = new Rect(position) {x = 120, width = 60};
            Rect vr = new Rect(position) {x = 185, width = position.width - 185};
            EditorGUIUtility.labelWidth = 60;
            EditorGUI.LabelField(new Rect(position) {width = 60}, "Unit");
            EditorGUIUtility.labelWidth = 0;
            EditorGUI.PropertyField(kr, k, new GUIContent(""));
            EditorGUI.PropertyField(dr, d, new GUIContent(""));
            EditorGUI.PropertyField(vr, v, new GUIContent(""));
        }
    }
}