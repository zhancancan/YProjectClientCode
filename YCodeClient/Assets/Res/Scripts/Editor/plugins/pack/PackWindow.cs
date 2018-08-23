using edit.pure.pack;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace plugins.pack {
    public class PackWindow : EditorWindow {
        [MenuItem("EditorTools/Pack/Window", false, 2001)]
        public static void Open() {
            GetWindow<PackWindow>(false, "Packer");
        }

        [MenuItem("EditorTools/Pack/UnLock Script", false, 2001)]
        public static void UnlockScript() {
            EditorApplication.UnlockReloadAssemblies();
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Packer", icon);
            minSize = new Vector2(250, 100);
            wantsMouseMove = true;
            PackCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        internal void OnGUI() {
            PackCenter.DrawGUI(position, this == focusedWindow);
        }

        internal void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            PackCenter.Stop();
        }
    }
}