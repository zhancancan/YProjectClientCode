using mono.ui.controls;
using pure.ticker;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (UISceneSpace))]
    internal class Insp_UISceneSpace : Editor {
        private UISceneSpace _space;

        protected void OnEnable() {
            _space = target as UISceneSpace;
        }

        public override void OnInspectorGUI() {
            //_space.prefab =
            //    (GameObject) EditorGUILayout.ObjectField("Model Prefab", _space.prefab, typeof (GameObject), false);
            base.OnInspectorGUI();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (Application.isPlaying && GUILayout.Button("Play", GUILayout.Width(100))) {
                TickerCenter.Init();
                GameTicker.Start();
                _space.Play();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}