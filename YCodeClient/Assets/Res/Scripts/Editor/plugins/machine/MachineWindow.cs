﻿using edit.pure.machine.core;
using edit.pure.resource;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace plugins.machine {
    public class MachineWindow : EditorWindow {
        [MenuItem("EditorTools/Machine/Main", false, 2002)]
        public static void Open() {
            GetWindow<MachineWindow>(false, "Machine");
        }

        [OnOpenAsset(1)]
        private static bool AutotOpenCanvas(int instanceId, int line) {
            string path = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceId));
            string name = Application.dataPath + "/" + path.Replace("Assets/", "");
            if (name.EndsWith(".machine")) {
                MachineCenter.ManuOpen(path);
                Open();
            }
            return false;
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            Texture icon = PResourceManager.LoadTexture(EditorGUIUtility.isProSkin
                ? "Textures/Icon_Dark.png"
                : "Textures/Icon_Light.png");
            titleContent = new GUIContent("Machine", icon);
            minSize = new Vector2(250, 100);
            MachineCenter.notifier -= ShowNotification;
            MachineCenter.notifier += ShowNotification;
            MachineCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        private void OnGUI() {
            MachineCenter.OnGUI(position, this == focusedWindow);
        }

        private void OnDestroy() {
            PPaintCenter.ClientRepaints -= Repaint;
            MachineCenter.notifier -= ShowNotification;
            MachineCenter.Stop();
        }
    }
}