using edit.pure.resource;
using edit.pure.treespace.finder;
using UnityEditor;
using UnityEngine;

namespace plugins.tree {
    internal class TreeSearchWindow : EditorWindow {
        internal static void Open() {
            GetWindow<TreeSearchWindow>(false, "Search");
        }

        internal static void Open(TreeWindow main) {
            TreeSearchWindow w = GetWindow<TreeSearchWindow>(false, "Search");
            Rect r = w.position;
            Rect m = main.position;
            r.center = m.center;
            w.position = r;
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Search", icon);
            minSize = new Vector2(300, 100);
            wantsMouseMove = true;
            TreeSearchCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
            TreeSearchCenter.windowCloseHandler -= ShouldClose;
            TreeSearchCenter.windowCloseHandler += ShouldClose;
        }

        private void ShouldClose() {
            Close();
        }

        internal void OnGUI() {
            TreeSearchCenter.OnGUI(position, this == focusedWindow);
        }

        internal void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            TreeSearchCenter.windowCloseHandler -= ShouldClose;
            TreeSearchCenter.Stop();
        }
    }
}