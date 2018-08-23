﻿using mono.ui.elements;
using pure.ui.element;
using pure.utils.coroutine;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UIAnimation))]
    public class Insp_UIAnimation : Editor {
        private UIAnimation_Dll.Mode _mode = UIAnimation_Dll.Mode.Show;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            UIAnimation u = target as UIAnimation;
            if (u == null) return;
            _mode = (UIAnimation_Dll.Mode) EditorGUILayout.EnumPopup("Test Mode", _mode);
            if (EditorApplication.isPlaying && GUILayout.Button("Play")) {
                CoroutineManager.longLive.StartCoroutine(u.Play(_mode));
            }
            if (!EditorApplication.isPlaying && GUILayout.Button("Set Animation to Legecy")) {
                for (int i = 0; i < u.units.Length; i++) {
                    AnimationClip a = u.units[i].clip;
                    if (a) {
                        a.legacy = true;
                        a.wrapMode = WrapMode.Clamp;
                    }
                }
            }
        }
    }
}