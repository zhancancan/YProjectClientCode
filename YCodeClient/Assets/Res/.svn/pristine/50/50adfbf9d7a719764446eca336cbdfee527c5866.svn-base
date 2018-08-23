using mono.scene;
using pure.scene.mono;
using UnityEditor;
using UnityEngine;

namespace inspectors.place {
    [CustomEditor(typeof (CustomData))]
    public class Insp_CustomData : Insp_PlaceDataCore {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            DrawSnap();
        }
    }
}

namespace inspectors.place {
    [CustomPropertyDrawer(typeof (CustomData_Dll.KeyValue))]
    public class KeyValueEditor : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var k = property.FindPropertyRelative("key");
            var v = property.FindPropertyRelative("value");
            Rect kr = new Rect(position) {width = 200};
            Rect vr = new Rect(position) {x = 205, width = position.width - 205};
            EditorGUIUtility.labelWidth = 50;
            EditorGUI.PropertyField(kr, k);
            EditorGUIUtility.labelWidth = 50;
            EditorGUI.PropertyField(vr, v);
        }
    }
}