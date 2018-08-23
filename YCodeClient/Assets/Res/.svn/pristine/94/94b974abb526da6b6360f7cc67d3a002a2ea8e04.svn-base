using mono.ui.elements;
using mono.ui.so;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (MovieClip))]
    internal class Insp_MovieClip : Editor {
        private MovieClip _field;

        protected void OnEnable() {
            _field = target as MovieClip;
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();
            _field.asset =
                EditorGUILayout.ObjectField("Asset", _field.asset, typeof (SpriteCollection), false) as
                    SpriteCollection;
            _field.frameIndex = EditorGUILayout.DelayedIntField("Frame Index", _field.frameIndex);
            _field.maxLoops = EditorGUILayout.DelayedIntField("Max Loop", _field.maxLoops);
            _field.autoPlay = EditorGUILayout.Toggle("Start On Awake", _field.autoPlay);
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(_field);
            }
            if (Application.isPlaying) {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (!_field.playing && GUILayout.Button("Play", GUILayout.Width(80))) {
                    _field.Play();
                }
                if (_field.playing && GUILayout.Button("Stop", GUILayout.Width(80))) {
                    _field.Stop();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }
    }
}