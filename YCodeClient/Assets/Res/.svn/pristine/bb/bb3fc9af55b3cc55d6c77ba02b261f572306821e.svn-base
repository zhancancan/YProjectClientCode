using mono.ui.elements;
using pure.ui.element;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace inspectors.ui {
    [CustomEditor(typeof (ResourceField))]
    internal class Insp_PResource : Editor {
        public override void OnInspectorGUI() {
            ResourceField r = target as ResourceField;
            if (r) {
                bool changed = false;
                EditorGUI.BeginChangeCheck();
                r.resourceType = EditorGUILayout.IntSlider("Type", r.resourceType, 0, r.settingData.Length - 1);
                if (EditorGUI.EndChangeCheck()) {
                    changed = true;
                }
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("settingData"), true);
                if (EditorGUI.EndChangeCheck()) {
                    serializedObject.ApplyModifiedProperties();
                    changed = true;
                }
                if (changed) {
                    if (r.resourceType >= 0 && r.resourceType < r.settingData.Length) {
                        ResourceData rd = r.settingData[r.resourceType];

                        Text t = r.GetComponentInChildren<Text>();
                        if (t) t.color = rd.textColor;

                        Image img = r.GetComponentInChildren<Image>();
                        if (img && rd.icon) {
                            Sprite spr = Sprite.Create(rd.icon, new Rect(0, 0, rd.icon.width, rd.icon.height),
                                Vector2.zero);
                            img.sprite = spr;
                        }
                    }


                    EditorUtility.SetDirty(r);
                }
            }
        }
    }
}