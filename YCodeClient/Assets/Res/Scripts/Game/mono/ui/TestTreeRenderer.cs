using pure.ui.controls;
using UnityEngine;
#if UNITY_EDITOR
using System.Collections.Generic;
using pure.ui.container;
using pure.ui.data;
using pure.ui.interfaces;
using pure.ui.renderer;
using UnityEditor;
using UnityEngine.UI;

#endif

namespace mono.ui {
    [ExecuteInEditMode]
    public class TestTreeRenderer : MonoBehaviour {
#if UNITY_EDITOR
        private static Dictionary<string, GameObject> _prefabPool = new Dictionary<string, GameObject>();

        public static GameObject LoadPrefab(string path) {
            GameObject p;
            if (!_prefabPool.TryGetValue(path, out p)) {
                p = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                _prefabPool.Add(path, p);
            }
            return p;
        }

        private TreePane_Dll _pane;

        public void OnEnable() {
            _pane = GetComponent<TreePane_Dll>();
            if (_pane) {
                _pane.tree = new Root();
            }
        }

        private class Root : UITreeRoot {
            public Root() {
                for (int i = 0; i < 5; i++) {
                    AddChild(new Branch());
                }
            }
        }

        private class Branch : UITreeNode {
            internal Branch() {
                for (int i = 0; i < 10; i++) {
                    AddChild(new Leaf());
                }
            }

            public override ITreeItem GetRenderer() {
                return new BrachRenderer();
            }

            public override Rect CalcContentSize(int index) {
                return Rect.zero;
            }
        }

        private class Leaf : UITreeNode {
            public override ITreeItem GetRenderer() {
                return new LeafRenderer();
            }
        }

        private class BrachRenderer : TreeCellBase {
            internal BrachRenderer()
                : base(LoadPrefab("Assets/Res/Arts/UI/ItemSkin/Demo_Style_TreeBranch.prefab")) {
            }

            private Image _ticker;

            protected override void OnRenderInit() {
                base.OnRenderInit();
                _ticker = GetComponentInChildren<Image>("Background/Checkmark");
                clickTrigger = GetComponentInChildren<UIButton_Dll>("Background");
                UpdateTreeStatus();
            }

            public override void UpdateTreeStatus() {
                if (_ticker) {
                    _ticker.enabled = node.opened;
                }
            }
        }

        private class LeafRenderer : TreeCellBase {
            internal LeafRenderer()
                : base(LoadPrefab("Assets/Res/Arts/UI/ItemSkin/Demo_Style_TreeLeaf.prefab")) {
            }
        }
#endif
    }
}