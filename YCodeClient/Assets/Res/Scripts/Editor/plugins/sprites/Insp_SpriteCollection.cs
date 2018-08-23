﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using edit.pure.resource;
using edit.pure.tools.assets;
using edit.pure.tools.selection;
using mono.ui.so;
using pure.ui.so;
using UnityEditor;
using UnityEngine;

namespace plugins.sprites {
    [CustomEditor(typeof (SpriteCollection))]
    internal class Insp_SpriteCollection : Editor {
        private class Renderer : ICustomEditListItem {
            internal SpriteInfo info;
            public string renderContent { get { return info.sprite.name; } set { } }
            public string renderTips {
                get {
                    return string.Format("Texture:\t{0}\r\nName:\t{1}\r\nSize:\t{2}",
                        AssetDatabase.GetAssetPath(info.sprite),
                        info.sprite.name,
                        info.rect);
                }
            }
            public bool itemLabelEditing { get; set; }

            public void DrawItem(GUIStyle style) {
                GUILayout.Label(new GUIContent(renderContent, renderTips), style);
                info.key = GUILayout.TextField(info.key, GUILayout.MinWidth(30), GUILayout.MaxWidth(80));
            }
        }

        private SpriteCollection _collection;

        private List<Renderer> _items;
        private ListView<Renderer> _listGUI;

        protected void OnEnable() {
            _collection = target as SpriteCollection;
            if (_listGUI == null) {
                _listGUI = new ListView<Renderer> {
                    selection = new EditSelection<Renderer>(),
                    multiselectable = true
                };
                _listGUI.afterStructChange += OnStructChanged;
            }
            CollectionToList();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        private void OnStructChanged() {
            ListToCollection();
        }

        public override void OnInspectorGUI() {
            GUILayout.Space(5);
            _collection.frameRate = EditorGUILayout.IntField("frameRate", _collection.frameRate);
            GUILayout.Space(5);
            float h = _items.Count == 0 ? 42 : _items.Count*_listGUI.rowHeight + 30;
            GUILayout.BeginVertical("RL Background", GUILayout.Height(h));
            GUILayout.Label("Sprite List");
            GUILayout.Box("", EditStyles.Border2, GUILayout.Height(1));
            if (_items.Count > 0) {
                GUILayout.Space(5);
                _listGUI.UpdateItems(_items, true);
            } else {
                GUILayout.Label("List Is Empty");
            }
            GUILayout.EndVertical();
            if (Event.current.type == EventType.Repaint) {
                _listGUI.dropArea = GUILayoutUtility.GetLastRect();
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+ File", GUILayout.Width(60))) PickFromFile();
            GUILayout.Space(10);
            if (GUILayout.Button("+ Folder", GUILayout.Width(60))) PickFromFolder();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void PickFromFile() {
            string dir = AssetDatabase.GetAssetPath(_collection);
            dir = Directory.GetParent(dir).FullName;
            string file = EditorUtility.OpenFilePanelWithFilters("Pick File", dir, new[] {"Image Files", "png,jpg,tga"});
            if (string.IsNullOrEmpty(file)) return;
            file = EditorFileTools.GetPath(file);
            List<SpriteInfo> sprites = new List<SpriteInfo>();
            sprites.AddRange(_collection.sprites);
            HandlerFile(file, sprites);
            _collection.sprites = sprites.ToArray();
            CollectionToList();
            EditorUtility.SetDirty(_collection);
        }

        private void PickFromFolder() {
            string dir = AssetDatabase.GetAssetPath(_collection);
            dir = Directory.GetParent(dir).FullName;
            dir = EditorUtility.OpenFolderPanel("open", dir, "");
            if (string.IsNullOrEmpty(dir)) return;
            dir = EditorFileTools.GetPath(dir);
            if (!Directory.Exists(dir)) return;
            List<SpriteInfo> sprites = new List<SpriteInfo>();
            sprites.AddRange(_collection.sprites);
            string[] files = Directory.GetFiles(dir).Where(a => a.EndsWith(".jpg") || a.EndsWith(".png")).ToArray();
            for (int i = 0; i < files.Length; i++) {
                string f = files[i];
                HandlerFile(f, sprites);
            }
            _collection.sprites = sprites.ToArray();
            CollectionToList();
            EditorUtility.SetDirty(_collection);
        }

        private void HandlerFile(string file, List<SpriteInfo> to) {
            Object[] objs = AssetDatabase.LoadAllAssetsAtPath(file);
            for (int i = 0; i < objs.Length; i++) {
                Sprite spr = objs[i] as Sprite;
                if (spr == null) continue;
                SpriteInfo info = new SpriteInfo {sprite = spr, rect = spr.rect};
                if (!to.Contains(info)) to.Add(info);
            }
        }

        private void CollectionToList() {
            if (_collection != null) {
                _items = new List<Renderer>();
                foreach (var s in _collection.sprites) {
                    if (s.sprite != null) {
                        _items.Add(new Renderer {info = s});
                        s.rect = s.sprite.rect;
                    }
                }
            }
        }

        private void ListToCollection() {
            if (_collection != null && _items != null) {
                SpriteInfo[] datas = new SpriteInfo[_items.Count];
                for (int i = 0; i < _items.Count; i++) {
                    datas[i] = _items[i].info;
                }
                _collection.sprites = datas;
                EditorUtility.SetDirty(_collection);
            }
        }

        private bool GetInfo(Sprite spr, out SpriteInfo info) {
            foreach (var i in _collection.sprites) {
                if (i.sprite == spr) {
                    info = i;
                    return true;
                }
            }
            info = null;
            return false;
        }

        protected void OnDisable() {
            _listGUI.Clear();
            PPaintCenter.ClientRepaints -= Repaint;
        }
    }
}