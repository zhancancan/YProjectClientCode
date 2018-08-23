using edit.pure.reference;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace reference {
    public class RefWindow : EditorWindow {
        public static void Open() {
            GetWindow<RefWindow>(false, "Reference");
        }


        [MenuItem("Assets/Check Using Reference", false, 2000)]
        private static void CheckRef() {
            Open();
        }


        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Reference", icon);

            minSize = new Vector2(250, 100);
            wantsMouseMove = true;
            ReferenceCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        internal void OnGUI() {
            ReferenceCenter.OnGUI(position, this == focusedWindow);
        }

        internal void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            ReferenceCenter.Stop();
        }
    }
}