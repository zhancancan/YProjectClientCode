using edit.pure.machine.hierachy;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace plugins.machine {
    internal class MachineHierachy : EditorWindow {
        [MenuItem("EditorTools/Machine/Hierarchy", false, 2002)]
        public static void Open() {
            GetWindow<MachineHierachy>(false, "Machine");
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Machine", icon);
            minSize = new Vector2(150, 100);
            MachineHierachyCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        internal void OnGUI() {
            MachineHierachyCenter.OnGUI(position, this == focusedWindow);
        }

        internal void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            MachineHierachyCenter.Stop();
        }
    }
}