using System.IO;
using edit.pure.pack;
using UnityEditor;

namespace plugins.pack {
    public class PackSetting : PackSetting_Dll {
        [InitializeOnLoadMethod]
        public static void Init() {
            if (instance == null) {
                PackSetting_Dll asset = AssetDatabase.LoadAssetAtPath<PackSetting_Dll>(ROOT);
                if (!asset) {
                    asset = CreateInstance<PackSetting>();
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