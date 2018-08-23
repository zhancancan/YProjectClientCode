using System.IO;
using System.Linq;
using edit.pure.tools.assetFormat;
using UnityEditor;
using UnityEngine;

namespace plugins.textures {
    internal class TexturePostProcess : AssetPostprocessor {
        internal void OnPreprocessModel() {
            ModelImporter mi = assetImporter as ModelImporter;
            if (mi && mi.importMaterials) {
                mi.importMaterials = false;
                EditorUtility.DisplayDialog("warn", "the FBX import material disabed", "OK");
            }
        }

        //internal void OnPreprocessTexture() {
        //    if (UITextureTools.isReimporting) return;
        //    TextureImporter impl = assetImporter as TextureImporter;
        //    if (impl && impl.textureType == TextureImporterType.Sprite) {
        //        UITextureTools.SetUITexture(impl);
        //    }
        //}

        private static string[] SUPPORT_EXTENTIONS = {".png", ".tga", ".jpg", ".exr"};

        internal void OnPostprocessTexture(Texture2D tex) {
            TextureImporter impl = assetImporter as TextureImporter;
            if (impl) {
                string ext = Path.GetExtension(assetPath);
                if (string.IsNullOrEmpty(ext) || !SUPPORT_EXTENTIONS.Contains(ext)) {
                    AssetDatabase.DeleteAsset(assetPath);
                    EditorUtility.DisplayDialog("error",
                        "the pic @ " + assetPath + " deleted, the game support png and tga only", "OK");
                    return;
                }
                if (TextureFormatUtils.isReimporting) return;
                if (impl && impl.textureType == TextureImporterType.Sprite) {
                    TextureFormatUtils.SetUITexture(impl);
                }
            }
        }
    }
}