using System;
using System.Collections.Generic;
using edit.pure.etui.utils;
using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace plugins.ui {
    internal class UIPrefabProcessor : AssetPostprocessor {
        internal static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths) {
            foreach (string path in importedAssets) {
                if (path.EndsWith(".prefab")) {
                    GameObject o = AssetDatabase.LoadMainAssetAtPath(path) as GameObject;
                    if (IsUI(o)) {
                        HandlerPrefabAdd(path, o);
                    }
                }
            }
        }
        private static void HandlerPrefabAdd(string path, GameObject obj) {
            if (UIPrefabUtility.GetUIType(obj) == UIType.None) {
                UITypeEditor window = EditorWindow.GetWindow<UITypeEditor>(true);
                window.maxSize = window.minSize = new Vector2(220, 110);
                var postion = window.position;
                postion.center =
                    new Rect(0f, 0f, Screen.currentResolution.width, Screen.currentResolution.height).center;
                window.position = postion;
                window.prefab = obj;
                window.path = path;
            }
        }

        private static bool IsUI(GameObject obj) {
            return obj && obj.transform is RectTransform;
        }
    }

    internal class UITypeEditor : EditorWindow {
        private static readonly List<string> errMsg = new List<string>();
        internal GameObject prefab;
        internal string path;

        internal void OnGUI() {
            UIType ut = UIPrefabUtility.GetUIType(prefab);
            GUILayout.Space(5);
            GUILayout.TextArea("this is Very important to pick the type of the UI Style for further usage",
                GUILayout.Height(50));
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Type:", GUILayout.MaxWidth(60));
            EditorGUI.BeginChangeCheck();
            ut = (UIType) EditorGUILayout.EnumPopup("", ut, GUILayout.MaxWidth(160));
            if (EditorGUI.EndChangeCheck()) {
                errMsg.Clear();
                if (UIPrefabUtility.CheckUIPrefabValidation(prefab, ut, errMsg)) {
                    string msg = Enum.GetName(typeof (UIType), ut) + " need Component below:\n" +
                                 string.Join(",", errMsg.ToArray());
                    EditorUtility.DisplayDialog("Invalidate Type", msg, "OK");
                    ut = UIType.None;
                    UIPrefabUtility.SetUIModitionProperty(prefab, UIUtiltiy.UI_TYPE, ut);
                } else {
                    UIPrefabUtility.SetUIModitionProperty(prefab, UIUtiltiy.UI_TYPE, ut);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Apply", GUILayout.MaxWidth(100), GUILayout.Height(20))) {
                if (ut == UIType.None) {
                    EditorUtility.DisplayDialog("warning", "ui type cannot be none", "ok");
                } else {
                    UIPrefabUtility.SetUIModitionProperty(prefab, UIUtiltiy.UI_ENABLE, true);
                    AssetDatabase.Refresh();
                    Close();
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }

        internal void OnDestroy() {
            if (prefab) {
                if (UIPrefabUtility.GetUIType(prefab) == UIType.None) {
                    EditorUtility.DisplayDialog("Miss UIType", "The UI Type can not be NONE, The Prefab will be delete!",
                        "OK");
                    AssetDatabase.DeleteAsset(path);
                }
            }
            prefab = null;
        }
    }
}