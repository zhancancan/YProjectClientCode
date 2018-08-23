using System.IO;
using edit.pure.luabuilder.gui.preset;
using UnityEditor;

namespace plugins.preset {
    public class PresetDecorate : PresetDecorate_Dll {
        [InitializeOnLoadMethod]
        public static void Init() {
            ReadData();
            Read += ReadData;
        }

        private static void ReadData() {
            if (instance == null) {
                PresetDecorate_Dll asset = AssetDatabase.LoadAssetAtPath<PresetDecorate_Dll>(ROOT);
                if (!asset) {
                    asset = CreateInstance<PresetDecorate>();
                    string dir = Path.GetDirectoryName(ROOT);
                    if (!string.IsNullOrEmpty(dir)) {
                        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                        AssetDatabase.CreateAsset(asset, ROOT);
                    }
                }
                instance = asset;
            }
        }
    }
}