using edit.pure.luabuilder.gui.proto;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace plugins.data {
    internal class ProtoDecoWindow : EditorWindow {
        [MenuItem("Wrapper/Lua Script/Proto Decorate", false, 2001)]
        public static void Open() {
            GetWindow<ProtoDecoWindow>(false, "Proto");
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Proto", icon);
            minSize = new Vector2(250, 100);
            wantsMouseMove = true;
            ProtoDecoCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        internal void OnGUI() {
            ProtoDecoCenter.OnGUI(position, this == focusedWindow);
        }

        internal void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            ProtoDecoCenter.Stop();
        }
    }
}