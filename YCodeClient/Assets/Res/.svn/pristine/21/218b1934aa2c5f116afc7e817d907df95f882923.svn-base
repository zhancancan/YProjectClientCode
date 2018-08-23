using edit.pure.preview;
using edit.pure.resource;
using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    internal abstract partial class Insp_UIStyleCore : Editor {
        private static UIPreivewUtility previewer;

        private bool _soloView;

        private static void DestroyPreview() {
            if (previewer != null) previewer.Clear();
        }

        protected UICore style { get; private set; }

        public virtual void OnEnable() {
            style = target as UICore;
            DestroyPreview();
            if (!previewer) previewer = new UIPreivewUtility();
            _soloView = Selection.objects.Length != 0 && Selection.objects[0] == style;
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background) {
            previewer.Draw(style.prefab, r, background);
        }

        protected void DrawSolovView() {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (!_soloView) {
                if (GUILayout.Button("Solo View", GUILayout.Width(80), GUILayout.Height(24))) {
                    Selection.objects = new Object[] {style};
                    _soloView = true;
                    Repaint();
                }
            } else {
                if (GUILayout.Button("Back To Object", GUILayout.Width(110), GUILayout.Height(24))) {
                    Selection.objects = new Object[] {style.gameObject};
                    _soloView = false;
                    Repaint();
                }
            }
            GUILayout.Space(20);
            GUILayout.EndHorizontal();
        }

        public override bool HasPreviewGUI() {
            return true;
        }

        protected void DrawSeperator() {
            GUILayout.Space(5);
            GUILayout.Box("", EditStyles.Border1, GUILayout.Height(1));
            GUILayout.Space(2);
        }

        public void OnDestroy() {
            DestroyPreview();
        }
    }
}