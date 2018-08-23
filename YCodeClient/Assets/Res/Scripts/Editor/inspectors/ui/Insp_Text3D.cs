﻿using mono.ui.hud;
using mono.ui.so;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (Text3D))]
    public class Insp_Text3D : Editor {
        private SerializedProperty _color;
        private SerializedProperty _size;

        protected void OnEnable() {
            _color = serializedObject.FindProperty("_color");
            _size = serializedObject.FindProperty("_size");
        }

        public override void OnInspectorGUI() {
            Text3D t = target as Text3D;
            if (!t) return;
            serializedObject.Update();
            t.spriteCollection =
                (SpriteCollection)
                    EditorGUILayout.ObjectField("Sprite", t.spriteCollection, typeof (SpriteCollection), false);
            t.text = EditorGUILayout.TextField("Text", t.text);
            EditorGUILayout.PropertyField(_color);
            EditorGUILayout.PropertyField(_size);
            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Play")) {
                t.Play();
            }
        }
    }
}