using edit.pure.diagram.source;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace plugins.diagram {
    internal class DiagramSourceWindow : EditorWindow {
        [MenuItem("EditorTools/Diagram/Source", false, 2002)]
        public static void Open() {
            GetWindow<DiagramSourceWindow>(false, "Diagram Source");
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Diagram Source", icon);
            DiagramSourceCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        private void OnGUI() {
            DiagramSourceCenter.OnGUI(position, this == focusedWindow);
        }

        private void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            DiagramSourceCenter.Stop();
        }
    }
}