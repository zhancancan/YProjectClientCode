using edit.pure.diagram.insp;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace plugins.diagram {
    internal class DiagramPropertyWindow : EditorWindow {
        [MenuItem("EditorTools/Diagram/Property", false, 2003)]
        public static void Open() {
            GetWindow<DiagramPropertyWindow>(false, "Diagram Property");
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");

            titleContent = new GUIContent("Diagram Property", icon);
            DiagramInspCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        private void OnGUI() {
            DiagramInspCenter.OnGUI(position);
        }

        private void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            DiagramInspCenter.Stop();
        }
    }
}