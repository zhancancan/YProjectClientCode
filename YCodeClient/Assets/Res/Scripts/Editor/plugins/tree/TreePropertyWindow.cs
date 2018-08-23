using edit.pure.resource;
using edit.pure.treespace.property;
using UnityEditor;
using UnityEngine;

namespace plugins.tree {
    internal class TreePropertyWindow : EditorWindow {
        [MenuItem("EditorTools/Tree/Property", false, 2003)]
        public static void Open() {
            GetWindow<TreePropertyWindow>(false, "treeproperty");
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Tree Property", icon);
            TreePropertyCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        private void OnGUI() {
            TreePropertyCenter.OnGUI(position);
        }

        private void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            TreePropertyCenter.Stop();
        }
    }
}