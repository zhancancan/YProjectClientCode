using edit.pure.resource;
using edit.pure.treespace.main;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace plugins.tree {
    internal class TreeWindow : EditorWindow {
        [MenuItem("EditorTools/Tree/Main", false, 2001)]
        public static void Open() {
            GetWindow<TreeWindow>(false, "Tree");
        }

        [OnOpenAsset(2)]
        internal static bool AutotOpenCanvas(int instanceId, int line) {
            string path = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceId));
            string name = Application.dataPath + "/" + path.Replace("Assets/", "");
            if (name.EndsWith(".tree")) {
                TreeCenter.ManuOpen(path);
                Open();
            }
            return false;
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Tree", icon);
            minSize = new Vector2(250, 100);
            wantsMouseMove = true;
            TreeCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        internal void OnGUI() {
            TreeCenter.OnGUI(position, this == focusedWindow);
            if (Event.current.isKey && Event.current.keyCode == KeyCode.F && Event.current.control) {
                TreeSearchWindow.Open(this);
            }
        }

        internal void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            TreeCenter.Stop();
        }
    }
}