using pure.scene.misc;
using UnityEditor;

namespace projectSet {
    public static class LayerSettingCommand {
        [MenuItem("EditorTools/Project/Layer Set", false, 2200)]
        private static void SetLayer() {
            SerializedObject tagManager =
                new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
 
            SerializedProperty ly = tagManager.FindProperty("layers");
            string[] lns = LayerType.LayerNames;
            for (int i = 0; i < lns.Length; i++) {
                if (lns[i] != "System") {
                    ly.GetArrayElementAtIndex(i).stringValue = lns[i];
                }
            }


            tagManager.ApplyModifiedProperties();
        }
    }
}