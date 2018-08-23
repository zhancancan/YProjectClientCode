using System;
using System.Collections;
using System.IO;
using edit.pure.resource;
using edit.pure.system;
using edit.pure.tools.meshes;
using mono.terrain;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace plugins.terrain {
    internal class TvCollapse : TerrianEditor, IEditRunnable {
        private int _mod;
        private float _hightError;
        private bool _keepBorder = true;
        private Mesh _collapseMesh;
        private TerrainModifier _modifier;
        private Mesh _source;
        private int _collapseTris;
        private int _collpaseVerts;
        private MeshFilter _meshFilter;

        private GameObject _currEditing;

        public override void OnGUI() {
            if (_currEditing != currentSelect.gameObject) {
                _source = null;
                _meshFilter = null;
                _collapseMesh = null;
                _collapseTris = _collpaseVerts = 0;
                _currEditing = currentSelect != null ? currentSelect.gameObject : null;
            }
            if (currentSelect == null) {
                GUILayout.Label("No Object Selected");
                _source = null;
            } else if (currentSelect.GetComponent<MeshFilter>() == null) {
                GUILayout.Label("No MeshFilter On Selected Object");
                _source = null;
            } else if (currentSelect.GetComponent<TerrainObject>() == null) {
                GUILayout.Label("No TerrainObject On Selected Object");
                _source = null;
            } else if (currentSelect.GetComponent<TerrainObject>().convertType != "UT") {
                GUILayout.Label("Terrain object is not from Unity Terrain System");
                _source = null;
            } else {
                if (_source == null) {
                    _meshFilter = currentSelect.GetComponent<TerrainObject>().mesh;
                    _source = _meshFilter.sharedMesh;
                }


                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                GUILayout.BeginVertical();

                GUILayout.Space(10);
                GUILayout.Label("Source    tris:" + _source.triangles.Length/3 + "  verts: " + _source.vertexCount);

                GUILayout.Space(10);

                Slider("Mod: ", ref _mod, 0, 8);
                Slider("Hight Error: ", ref _hightError, 0, 10);
                _keepBorder = GUILayout.Toggle(_keepBorder, "Keep Border", PGUIStyle.ToggleStyle);

                GUILayout.Space(5);
                GUILayout.Box(PGUIStyle.BorderColor0, GUILayout.ExpandWidth(true), GUILayout.Height(1));
                GUILayout.Space(5);

                if (_collapseMesh == null) {
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (_modifier == null) {
                        if (GUILayout.Button("Collaps", GUILayout.Width(100))) {
                            DoCollapse();
                        }
                    } else {
                        GUILayout.Label("Collapsing...");
                    }
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                } else {
                    try {
                        GUILayout.Label("Collapsed mesh    tris:" + _collapseTris + "  verts:" + _collpaseVerts);
                        GUILayout.Space(5);
                        GUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("Yes", GUILayout.Width(80))) {
                            Confirm();
                        }
                        GUILayout.Space(20);
                        if (GUILayout.Button("No", GUILayout.Width(80))) {
                            Revert();
                        }
                        GUILayout.Space(20);
                        if (GUILayout.Button("retry", GUILayout.Width(80))) {
                            DoCollapse();
                        }
                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();
                    } catch (Exception) {
                    }
                }

                GUILayout.EndVertical();
                GUILayout.Space(20);
                GUILayout.EndHorizontal();
            }
        }


        private void DoCollapse() {
            if (_modifier == null) {
                aborted = false;
                _modifier = new TerrainModifier(_source) {
                    mod = _mod,
                    keepBorder = _keepBorder,
                    heightError = _hightError,
                    progressBar = TerrainCenter.progressBar
                };
                TerrainCenter.coroutine.StartCoroutine(RunCollapse(), this);
            }
        }

        private IEnumerator RunCollapse() {
            IEnumerator e = _modifier.Collaps();
            while (e.MoveNext()) {
                yield return e;
            }
            _collapseMesh = _modifier.GetMesh();
            _modifier = null;
            if (_meshFilter != null) {
                _meshFilter.sharedMesh = _collapseMesh;
                _collapseTris = _collapseMesh.triangles.Length/3;
                _collpaseVerts = _collapseMesh.vertexCount;
            }
        }


        private void Revert() {
            if (_meshFilter != null) {
                _meshFilter.sharedMesh = _source;
                _collapseMesh = null;
                _currEditing = null;
            }
        }

        private void Confirm() {
            _source.Clear();
            _source.vertices = _collapseMesh.vertices;
            _source.uv = _collapseMesh.uv;
            _source.triangles = _collapseMesh.triangles;

            _meshFilter = _currEditing.GetComponent<MeshFilter>();
            _meshFilter.sharedMesh = _source;


            Scene sc = SceneManager.GetActiveScene();
            string scenePath = sc.path;
            string dir = Path.GetDirectoryName(scenePath);
            string exportName = _meshFilter.name;
            dir = dir + "/" + exportName;


            ObjExporter o = new ObjExporter(_collapseMesh, dir + "/" + exportName + ".obj") {progressBar = progressBar};
            o.Execute();

            AssetDatabase.Refresh();
            PrefabUtility.ResetToPrefabState(_currEditing);

            Object.DestroyImmediate(_collapseMesh);


            _collapseMesh = null;
            _source = null;
            _meshFilter = null;
            _currEditing = null;
        }

        public bool aborted { get; private set; }

        public void Abort() {
            if (_modifier != null) {
                _modifier.Abort();
                _modifier = null;
            }
            aborted = true;
        }
    }
}