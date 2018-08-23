#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#endif

namespace mono.materials {
    public static class MaterialTools {
#if UNITY_EDITOR
        public static void ClearUselessProperty(Material mat) {
            if (mat != null) {
                SerializedObject psSource = new SerializedObject(mat);
                SerializedProperty emissionProperty = psSource.FindProperty("m_SavedProperties");
                SerializedProperty texEnvs = emissionProperty.FindPropertyRelative("m_TexEnvs");
                SerializedProperty floats = emissionProperty.FindPropertyRelative("m_Floats");
                SerializedProperty colos = emissionProperty.FindPropertyRelative("m_Colors");

                CleanMaterialSerializedProperty(texEnvs, mat);
                CleanMaterialSerializedProperty(floats, mat);
                CleanMaterialSerializedProperty(colos, mat);
                psSource.ApplyModifiedProperties();

                EditorUtility.SetDirty(mat);
            }
        }

        private static void CleanMaterialSerializedProperty(SerializedProperty property, Material mat) {
            for (int j = property.arraySize - 1; j >= 0; j--) {
                string propertyName =
                    property.GetArrayElementAtIndex(j)
                        .FindPropertyRelative("first")
                        .FindPropertyRelative("name")
                        .stringValue;
                //   Debug.Log("Find property in serialized object : " + propertyName);
                if (!mat.HasProperty(propertyName)) {
                    property.DeleteArrayElementAtIndex(j);
                    Debug.Log("Delete legacy property in serialized object : " + propertyName);
                }
            }
        }
#endif
    }
}