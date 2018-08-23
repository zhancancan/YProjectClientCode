using edit.pure.machine.insp;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace plugins.machine {
    internal class MachineInspector : EditorWindow {
        [MenuItem("EditorTools/Machine/Inspector", false, 2002)]
        public static void Open() {
            GetWindow<MachineInspector>(false, "Machine");
        }

        private Vector2 _scrollView;

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Machine", icon);
            minSize = new Vector2(250, 100);
            MachinePropertyCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        internal void OnGUI() {
            _scrollView = EditorGUILayout.BeginScrollView(_scrollView, false, false);
            MachinePropertyCenter.OnGUI(position, this == focusedWindow);
            EditorGUILayout.EndScrollView();
        }

        internal void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            MachinePropertyCenter.Stop();
        }
    }
}