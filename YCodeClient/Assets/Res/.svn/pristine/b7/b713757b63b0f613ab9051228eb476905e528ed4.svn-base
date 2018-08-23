using edit.pure.etui.utils;
using edit.pure.preview;
using pure.ui.core;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (GameObject))]
    public class Insp_UIPrefab : DecoratorEditor {
        private static UIPreivewUtility previewer;
        private static UIPreivewUtility.Styles styles;

        private static void DestroyPreview() {
            if (previewer != null) previewer.Clear();
        }

        private bool _isUIPrefab;
        private Rect _baseRect;

        public Insp_UIPrefab() : base("GameObjectInspector") {
        }

        public override void OnSceneGUI() {
            // do things cause the gameobjectinspaceor doesn't has the OnSceneGUI Method;
        }

        public void OnEnable() {
            DestroyPreview();
            GameObject p = target as GameObject;
            _isUIPrefab = UIPrefabUtility.IsUIPrefab(p);
            if (_isUIPrefab && p != null) {
                _baseRect = p.GetComponent<RectTransform>().rect;
                if (!previewer) previewer = new UIPreivewUtility();
            }
        }

        public override void OnInspectorGUI() {
            if (_isUIPrefab) {
                GameObject prefab = target as GameObject;
                if (!prefab) return;
                GUILayout.Label("UI Style: " + AssetDatabase.GetAssetPath(prefab));
                RectTransform rt = prefab.transform as RectTransform;
                if (rt != null) {
                    Rect r = rt.rect;
                    GUILayout.Label("Size:  " + r.width + ":" + r.height);
                }
                UIType ut = UIPrefabUtility.GetUIType(prefab);
                EditorGUI.BeginChangeCheck();
                ut = (UIType) EditorGUILayout.EnumPopup("Type:", ut);
                if (EditorGUI.EndChangeCheck()) {
                    UIPrefabUtility.SetUIType(prefab, ut);
                }
                bool enabled = UIPrefabUtility.GetUIModifactionProperty<bool>(prefab, UIUtiltiy.UI_ENABLE);
                EditorGUI.BeginChangeCheck();
                enabled = EditorGUILayout.Toggle("Enable:", enabled);
                if (EditorGUI.EndChangeCheck()) {
                    UIPrefabUtility.SetUIModitionProperty(prefab, UIUtiltiy.UI_ENABLE, enabled);
                }
            }
        }

        public override bool HasPreviewGUI() {
            return _isUIPrefab || buildInEditor.HasPreviewGUI();
        }

        public override void DrawPreview(Rect previewArea) {
            GameObject prefab = target as GameObject;
            if (_isUIPrefab && prefab && previewer != null) {
                if (styles == null) styles = UIPreivewUtility.style;
                previewer.Draw(prefab, new Rect(previewArea) {y = previewArea.y + 32, height = previewArea.height - 32},
                    styles.background);
                Rect area = new Rect(previewArea) {height = 16};
                GUI.Label(area, AssetDatabase.GetAssetPath(prefab), styles.info);
                area.y += 16;
                GUI.Label(area, "Size: " + _baseRect.width + " x " + _baseRect.height, styles.info);
            } else {
                base.DrawPreview(previewArea);
            }
        }

        protected override void OnDestroy() {
            DestroyPreview();
            base.OnDestroy();
        }
    }
}