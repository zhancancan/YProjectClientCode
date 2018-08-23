﻿using edit.pure.animation;
using edit.pure.inspector;
using edit.pure.resource;
using edit.pure.treespace.core;
using edit.pure.treespace.main;
using edit.pure.ui;
using mono.fbx;
using plugins.tree.canvas.firefx;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace plugins.effect {
    [CustomEditor(typeof (FireFxTreeDebuger))]
    public class Insp_FireFxDebuger : Editor {
        private FireFxTreeDebuger _debuger;
        private ArrayListDrawer _listDrawer;

        internal void OnEnable() {
            _debuger = target as FireFxTreeDebuger;
            _listDrawer = new ArrayListDrawer("targets");
            UpdateHost();
            TreeCenter.selection.selectionChanged -= OnSelectChanged;
            TreeCenter.selection.selectionChanged += OnSelectChanged;
        }

        private void OnSelectChanged() {
            Repaint();
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();
            _debuger.hero = (GameObject) EditorGUILayout.ObjectField("Host", _debuger.hero, typeof (GameObject), true);
            if (EditorGUI.EndChangeCheck()) {
                UpdateHost();
            }
            EditorGUI.BeginChangeCheck();
            _listDrawer.Draw(serializedObject);
            FireFxCanvas canvas = TreeCenter.currCanvas as FireFxCanvas;
            if (canvas != null) {
                TreeCell main = TreeCenter.selection.main;
                if (main != null) {
                    EditStyles.DrawHorizontalSeperator();
                    GUILayout.Label("Current Cell : " + main.cellType, EditStyles.boldLabel);
                    PInspectorCore[] ps = main.GetProperties();
                    PInspectorCore.BeginLabelWidth(ps);
                    EditorGUI.BeginChangeCheck();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Name", GUILayout.Width(PInspectorCore.labelWidth));
                    main.editName = EditorGUILayout.DelayedTextField(main.editName);
                    GUILayout.EndHorizontal();
                    if (EditorGUI.EndChangeCheck()) {
                        PPaintCenter.RepaintClients();
                    }
                    PInspectorCore.useLabelString = TreeCenter.useChn;
                    for (int i = 0; i < ps.Length; i++) {
                        if (!main.OnInspectorDraw(ps[i], _debuger) && !ps[i].hideInInspector) {
                            ps[i].Draw();
                        }
                        main.AfterInspectorDraw(ps[i]);
                    }
                    PInspectorCore.useLabelString = false;
                    PInspectorCore.EndLabelWidth();
                }
                if (Application.isPlaying) {
                    if (main != null) {
                        EditStyles.DrawHorizontalSeperator();
                    }
                    GUILayout.Space(10);
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Play", GUILayout.Width(100))) {
                        _debuger.Play(canvas);
                    }
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            }
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(_debuger);
            }
        }

        private void UpdateHost() {
            AnimatorState[] statelist;
            string[] stateNameKeys;
            if (!EditAnimatorUtils.GetSupportStates(_debuger.hero, out statelist, out stateNameKeys)) {
            }
            string[] bs;
            EditAnimatorUtils.GetSupportBoneNames(_debuger.hero, out bs);
        }

        internal void OnDisable() {
            TreeCenter.selection.selectionChanged -= OnSelectChanged;
        }

        internal void OnDestroy() {
            TreeCenter.selection.selectionChanged -= OnSelectChanged;
        }
    }
}