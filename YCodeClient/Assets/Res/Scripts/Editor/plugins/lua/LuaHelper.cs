﻿using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace plugins.lua {
    public static class LuaHelper {
        private const string EDITOR_PATH_KEY = "luaeditor";

        internal static bool OpenFromProject(int instanceId) {
            string path = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceId));
            string name = Application.dataPath + "/" + path.Replace("Assets/", "");
            return name.EndsWith(".lua") && OpenFileAtExternal(name, 0);
        }

        private static bool OpenFileAtExternal(string fileName, int line) {
            OpenFileWith(fileName, line);
            return true;
        }

        private static void OpenFileWith(string fileName, int line) {
            string editor = EditorUserSettings.GetConfigValue(EDITOR_PATH_KEY);
            Process proc = new Process {
                StartInfo = {
                    FileName = editor,
                    Arguments = fileName + ":" + line
                }
            };
            proc.Start();
        }

        [MenuItem("Wrapper/Lua/Set External Editor Path")]
        internal static void SetExternalEditorPath() {
            string path = EditorUserSettings.GetConfigValue(EDITOR_PATH_KEY);
            path = EditorUtility.OpenFilePanel("SetExternalEditorPath", path, "exe");
            if (path != "") {
                EditorUserSettings.SetConfigValue(EDITOR_PATH_KEY, path);
                Debug.Log("Set Editor Path: " + path);
            }
        }
    }
}