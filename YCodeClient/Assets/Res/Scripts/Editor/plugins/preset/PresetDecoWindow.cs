using edit.pure.luabuilder.gui.preset;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace plugins.preset {
    internal class PresetDecoWindow : EditorWindow {
        [MenuItem("Wrapper/Lua Script/Preset Decorate", false, 2002)]
        public static void Open() {
            GetWindow<PresetDecoWindow>(false, "Preset");
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Preset", icon);
            minSize = new Vector2(250, 100);
            wantsMouseMove = true;
            PresetDecoCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        internal void OnGUI() {
            PresetDecoCenter.OnGUI(position, this == focusedWindow);
        }

        internal void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            PresetDecoCenter.Stop();
        }
    }
}