﻿using main;
using UnityEditor;
using UnityEngine;

namespace inspectors.game {
    [CustomEditor(typeof (GameStarter))]
    public class Insp_GameStart : Editor {
        private GameStarter _data;

        protected void OnEnable() {
            _data = target as GameStarter;
        }

        public override void OnInspectorGUI() {
            Object asset = AssetDatabase.LoadAssetAtPath<Object>(_data.loginMachine);
            EditorGUI.BeginChangeCheck();
            asset = EditorGUILayout.ObjectField("Login Machine", asset, typeof (TextAsset), false);
            if (EditorGUI.EndChangeCheck()) {
                string p = asset ? AssetDatabase.GetAssetPath(asset) : string.Empty;
                if (p.EndsWith(".bytes")) {
                    _data.loginMachine = p;
                    EditorUtility.SetDirty(_data);
                }
            }
            if (asset == null) {
                EditorGUILayout.HelpBox("No Login machine found", MessageType.Error);
            } else if (AssetDatabase.GetAssetPath(asset).EndsWith(".bytes") == false) {
                EditorGUILayout.HelpBox("the machine's extension should be .bytes", MessageType.Error);
            }


            _data.configHttp = EditorGUILayout.DelayedTextField("configHttp:", _data.configHttp);
        }
    }
}