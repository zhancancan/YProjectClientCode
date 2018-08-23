using edit.pure.diagram.core;
using edit.pure.resource;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace plugins.diagram {
    public class DiagramWindow : EditorWindow {
        [MenuItem("EditorTools/Diagram/Main", false, 2002)]
        public static void Open() {
            GetWindow<DiagramWindow>(false, "Diagram");
        }

        [MenuItem("EditorTools/Data/Refresh DataBase", false, 2003)]
        public static void RefreshDataBase() {
            EditorUtility.DisplayDialog("message", "Database refresh complete", "OK");
        }

        [OnOpenAsset(1)]
        private static bool AutotOpenCanvas(int instanceId, int line) {
            string path = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceId));
            string name = Application.dataPath + "/" + path.Replace("Assets/", "");
            if (name.EndsWith(".diagram")) {
                DiagramCenter.ManuOpen(path);
                Open();
            }
            return false;
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Diagram", icon);
            minSize = new Vector2(250, 100);
            //   wantsMouseMove = true;
            DiagramCenter.notifier -= ShowNotification;
            DiagramCenter.notifier += ShowNotification;
            DiagramCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        private void OnGUI() {
            DiagramCenter.OnGUI(position, this == focusedWindow);
        }

        private void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            DiagramCenter.notifier -= ShowNotification;
            DiagramCenter.Stop();
        }
    }
}