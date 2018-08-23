using System.IO;
using edit.pure.luabuilder.gui.proto;
using UnityEditor;

namespace plugins.data {
    public class ProtoDecorate : ProtoDecorate_Dll {
        [InitializeOnLoadMethod]
        public static void Init() {
            if (instance == null) {
                ProtoDecorate_Dll asset = AssetDatabase.LoadAssetAtPath<ProtoDecorate_Dll>(ROOT);
                if (!asset) {
                    asset = CreateInstance<ProtoDecorate>();
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