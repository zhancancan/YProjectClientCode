using mono.ui.utils;
using UnityEditor;
using UnityEngine;

namespace inspectors.tween {
    [CustomEditor(typeof (UITween))]
    internal class Insp_UITween : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            UITween t = target as UITween;
            if (!t) return;
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (t.isPlaying && GUILayout.Button("Stop", GUILayout.Width(100))) t.Stop();
            if (!t.isPlaying && GUILayout.Button("Play", GUILayout.Width(100))) t.Play();
            if (!t.isPlaying && GUILayout.Button("Restore", GUILayout.Width(100))) t.Restore();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}