//using edit.pure.atlas;
//using edit.pure.preview;
//using edit.pure.resource;
//using mono.ui.elements;
//using pure.ui.core;
//using pure.ui.element;
//using UnityEditor;
//using UnityEngine;
//using GenericMenu = UnityEditor.GenericMenu;

//namespace plugins.sprites {
//    [CustomEditor(typeof (SpriteAtlas))]
//    internal class Insp_SpriteAtlas : Editor {
//        private class Styles {
//            internal GUIStyle preivew;
//            internal GUIStyle richText;

//            internal Styles() {
//                richText = new GUIStyle(GUI.skin.label) {richText = true};
//                preivew = new GUIStyle(GUI.skin.label) {normal = {textColor = Color.white}, richText = true};
//            }
//        }

//        private static Styles _styes;


//        private bool _isDirty;
//        private SpriteAtlas _asset;


//        private Rect _menuPos;
//        private Rect _scrollRect;
//        private Vector2 _scrollPos = Vector2.zero;

//        private SpriteCollection_Dll.SpriteInfo _selected;


//        internal void OnEnable() {
//            _asset = target as SpriteAtlas;
//        }


//        public override void OnInspectorGUI() {
//            if (_styes == null) _styes = new Styles();
//            EditStyles.Init();
//            _isDirty = false;
//            GUILayout.BeginHorizontal("", "Toolbar");
//            DrawSeperator();
//            if (GUILayout.Button("Menu", "IN DropDown", GUILayout.MaxWidth(60))) {
//                ShowMenu();
//            }

//            if (Event.current.type == EventType.Repaint) {
//                _menuPos = GUILayoutUtility.GetLastRect();
//                _menuPos.x -= 5;
//            }
//            DrawSeperator();
//            _asset.showInfo = GUILayout.Toggle(_asset.showInfo, "Show Info", GUILayout.MaxWidth(80));
//            DrawSeperator();
//            _asset.showGroup = GUILayout.Toggle(_asset.showGroup, "Show Group", GUILayout.MaxWidth(100));
//            GUILayout.FlexibleSpace();
//            GUILayout.EndHorizontal();
//            EditorGUI.indentLevel ++;
//            _scrollPos = GUILayout.BeginScrollView(_scrollPos, false, true);
//            EditorGUI.BeginChangeCheck();
//            foreach (var g in _asset.groups) {
//                if (_asset.showGroup) {
//                    EditorGUILayout.BeginHorizontal();
//                    g.open = EditorGUILayout.Foldout(g.open, g.tag + "   (" + g.sprites.Count + ")", true);

//                    GUILayout.Label("Frame Rate", GUILayout.Width(80));
//                    g.frameRate = EditorGUILayout.DelayedIntField("", g.frameRate, GUILayout.MaxWidth(80));

//                    GUILayout.Label("ID", GUILayout.MaxWidth(20));
//                    g.id = EditorGUILayout.DelayedIntField("", g.id, GUILayout.MaxWidth(80));
//                    EditorGUILayout.Space();
//                    EditorGUILayout.EndHorizontal();
//                }

//                if (g.open || !_asset.showGroup) {
//                    DrawGroup(g);
//                }
//            }
//            GUILayout.EndScrollView();
//            if (Event.current.type == EventType.Repaint) {
//                _scrollRect = GUILayoutUtility.GetLastRect();
//            }


//            EditorGUI.indentLevel--;
//            if (EditorGUI.EndChangeCheck()) {
//                _isDirty = true;
//            }
//            if (_isDirty) {
//                EditorUtility.SetDirty(_asset);
//            }
//            Event e = Event.current;
//            if (e.type == EventType.KeyUp && e.keyCode == KeyCode.Return) {
//                PFocusManager.ClearFocus();
//                Repaint();
//            }
//        }


//        private void DrawGroup(SpriteGroup group) {
//            for (int i = 0; i < group.sprites.Count; i++) {
//                SpriteCollection_Dll.SpriteInfo info = group.sprites[i];
//                if (info == _selected) {
//                    GUILayout.BeginHorizontal(EditStyles.Selected0, GUILayout.Width(_scrollRect.width - 20));
//                } else {
//                    GUILayout.BeginHorizontal(GUILayout.Width(_scrollRect.width - 20));
//                }
//                GUIStyle label = info == _selected ? EditStyles.SelectLabel : EditStyles.Label;
//                GUILayout.Space(20);
//                GUILayout.Label("ID", label, GUILayout.Width(20), GUILayout.ExpandWidth(false));
//                info.id = EditorGUILayout.DelayedIntField("", info.id, GUILayout.Width(60));
//                if (!_asset.showGroup) DrawSeperator();
//                GUILayout.Label("Name", label, GUILayout.Width(40));
//                info.name = EditorGUILayout.DelayedTextField("", info.name, GUILayout.MinWidth(50),
//                    GUILayout.MaxWidth(300));
//                if (!_asset.showGroup) DrawSeperator();
//                GUILayout.Label("Tag", label, GUILayout.Width(40));
//                info.tag = EditorGUILayout.DelayedTextField("", info.tag, GUILayout.MinWidth(50),
//                    GUILayout.MaxWidth(300));
//                if (!_asset.showGroup) DrawSeperator();
//                GUILayout.FlexibleSpace();
//                GUILayout.EndHorizontal();
//                Rect r = GUILayoutUtility.GetLastRect();
//                if (Event.current.type == EventType.MouseUp && Event.current.button == 0) {
//                    if (r.Contains(Event.current.mousePosition)) {
//                        if (_selected != info) {
//                            _selected = info;
//                            Repaint();
//                        }
//                    }
//                }
//                if (_asset.showInfo) {
//                    GUILayout.BeginHorizontal(GUILayout.Width(_scrollRect.width - 20));
//                    GUILayout.Space(20);
//                    GUILayout.Label(GetInfo(info), _styes.richText);
//                    GUILayout.EndHorizontal();
//                }
//            }
//        }


//        private void DrawSeperator() {
//            GUILayout.Space(5);
//            GUILayout.Box("", EditStyles.Border2, GUILayout.Width(1), GUILayout.Height(18));
//            GUILayout.Space(5);
//        }

//        private void ShowMenu() {
//            GenericMenu menu = new GenericMenu();
//            menu.AddItem(new GUIContent("Regroup By Tag"), false, RegroudById);
//            menu.AddItem(new GUIContent("Resort By ID"), false, ResortById);
//            menu.AddItem(new GUIContent("Auto ID By Hierarchy"), false, AutoIdByHirarchy);
//            menu.AddSeparator("");
//            menu.AddItem(new GUIContent("Collaps All"), false, CollpaseAll);
//            menu.AddItem(new GUIContent("Open All"), false, OpenAll);
//            menu.AddSeparator("");
//            menu.AddItem(new GUIContent("Save"), false, Save);
//            menu.DropDown(_menuPos);
//        }

//        private void RegroudById() {
//            SpriteAtlasCommands.RegrounpByTag(_asset);
//            Save();
//        }

//        private void ResortById() {
//            SpriteAtlasCommands.SortById(_asset);
//            Save();
//        }

//        private void AutoIdByHirarchy() {
//            SpriteAtlasCommands.AutoIDByHierarchy(_asset);
//            Save();
//        }

//        private void Save() {
//            EditorUtility.SetDirty(_asset);
//            AssetDatabase.SaveAssets();
//            _isDirty = false;
//        }

//        private void CollpaseAll() {
//            if (_asset) {
//                foreach (var g in _asset.groups) {
//                    g.open = false;
//                }
//            }
//        }

//        private void OpenAll() {
//            if (_asset) {
//                foreach (var g in _asset.groups) {
//                    g.open = true;
//                }
//            }
//        }


//        public override bool HasPreviewGUI() {
//            return true;
//        }


//        public override void OnPreviewGUI(Rect r, GUIStyle background) {
//            if (_selected == null) {
//                EditorGUI.DrawPreviewTexture(r, _asset.texture, null, ScaleMode.ScaleToFit);
//            } else {
//                SpriteDrawUtility.DrawSprite(_selected.sprite, r, Color.white);
//                if (_styes == null) _styes = new Styles();
//                Rect a = new Rect(r) {x = r.x + 5, y = r.y + 5, width = r.width - 10, height = r.height - 10};
//                GUI.TextArea(a, GetInfo(_selected, "\n"), _styes.preivew);
//            }
//        }


//        private static string GetInfo(SpriteCollection_Dll.SpriteInfo info, string breaker = " ") {
//            string t = string.Empty;
//            if (info.sprite && info.sprite.texture) {
//                t += "Texture:" + info.sprite.texture.name + breaker;
//            } else {
//                t += "<color='#ff0000ff'>Texture: LOST</color>" + breaker;
//            }
//            t += "Rect: (x" + info.rect.x + " y:" + info.rect.y + " w:" + info.rect.width + " h:" +
//                 info.rect.height + ")" + breaker;
//            t += "Pivot:" + info.pivot;
//            return t;
//        }


//        internal void OnDestroy() {
//            _selected = null;
//        }


//        public override bool UseDefaultMargins() {
//            return false;
//        }
//    }
//}