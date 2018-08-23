using edit.pure.resource;
using edit.pure.treespace;
using edit.pure.treespace.source;
using UnityEditor;
using UnityEngine;

namespace plugins.tree {
    internal class TreeSourceWindow : EditorWindow {
        [MenuItem("EditorTools/Tree/Source", false, 2002)]
        public static void Open() {
            GetWindow<TreeSourceWindow>(false, "Tree Source");
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Tree Source", icon);
            TreeSourceCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        private void OnGUI() {
            TreeSourceCenter.OnGUI(position, this == focusedWindow);
        }

        private void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            TreeSourceCenter.Stop();
        }
    }
}