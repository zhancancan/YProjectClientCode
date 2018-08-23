using edit.pure.resource;
using mono.terrain;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    internal partial class TvPaint {
        private GUIStyle _gridStyle;


        private void PixelPatiner() {
            if (_gridStyle == null) {
                GUIStyle st = "GroupBox";
                _gridStyle = new GUIStyle("gridlist") {normal = st.normal};
            }
            TerrainObject to = currentSelect.GetComponent<TerrainObject>();
            if (to != null) {
                if (to.material != null &&
                    to.material.HasProperty("_Splat0") &&
                    to.material.HasProperty("_Splat1") &&
                    to.material.HasProperty("_Control")) {
                    InitBrush();
                    InitPencil();

                    if (editData.previewer == null) {
                        CreatePreviewer();
                    }
                    if (editData.initialized) {
                        GUILayout.Label("Brushes", PGUIStyle.nodeLabelBold);
                        GUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        _selectedBrush = GUILayout.SelectionGrid(_selectedBrush, _brushTex, 9, _gridStyle,
                            GUILayout.Width(290), GUILayout.Height(70));
                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                        GUILayout.Box(PGUIStyle.BorderColor0, GUILayout.ExpandWidth(true), GUILayout.Height(1));
                        EditorGUILayout.Space();
                        GUILayout.Label("Textures", PGUIStyle.nodeLabelBold);
                        GUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        editData.selectPaintTexture = GUILayout.SelectionGrid(editData.selectPaintTexture, _textures, 4,
                            "gridList",
                            GUILayout.Width(290), GUILayout.Height(86));
                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();

                        EditorGUILayout.Space();

                        GUILayout.Box(PGUIStyle.BorderColor0, GUILayout.ExpandWidth(true), GUILayout.Height(1));
                        EditorGUILayout.Space();

                        GUILayout.BeginHorizontal("box");
                        GUILayout.Space(20);
                        GUILayout.BeginVertical();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Preview Type:", GUILayout.Width(100));

                        editData.paintPreview =
                            (PaintHandler) EditorGUILayout.EnumPopup(editData.paintPreview, GUILayout.Width(160));
                        GUILayout.EndHorizontal();

                        Slider("Brush Size:", ref editData.brushSize, 1, 36, 120f);
                        Slider("Brush Stronger", ref editData.brushStronger, 0.05f, 1f, 120);
                        GUILayout.EndVertical();
                        GUILayout.Space(20);
                        GUILayout.EndHorizontal();


                        if (to.material != null && to.material.HasProperty("_SpecColor")) {
                            EditorGUILayout.Space();
                            GUILayout.BeginHorizontal();
                            GUILayout.Space(20);
                            GUILayout.BeginVertical();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Shinness Color");
                            editData.shinessColor = EditorGUILayout.ColorField("", editData.shinessColor,
                                GUILayout.MaxWidth(100));
                            GUILayout.FlexibleSpace();
                            GUILayout.EndHorizontal();
                            EditorGUILayout.Space();
                            for (int i = 0; i < 4; i++) {
                                if (to.material.HasProperty("_ShininessL" + i)) {
                                    Slider("Shiness Layer " + i, ref editData.layers[i].shiness, 0.00f, 1.0f);
                                    to.material.SetFloat("_ShininessL" + i, editData.layers[i].shiness);
                                }
                            }

                            GUILayout.EndVertical();
                            GUILayout.Space(20);
                            GUILayout.EndHorizontal();
                        }
                        EditorGUILayout.Space();

                        GUILayout.BeginHorizontal();
                        GUILayout.Space(20);
                        if (to.material != null && to.material.HasProperty("_SepcColor")) {
                            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(350),
                                GUILayout.Height(140));
                            GUILayout.BeginVertical();
                        } else {
                            GUILayout.BeginVertical(GUILayout.Width(320));
                            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(340),
                                GUILayout.Height(240));
                        }


                        editData.joinTiles = GUILayout.Toggle(editData.joinTiles, "Tiling : Join X/Y",
                            PGUIStyle.ToggleStyle);
                        EditorGUILayout.Space();
                        if (editData.joinTiles) {
                            for (int i = 0; i < 4; i++) {
                                if (to.material != null && to.material.HasProperty("_Splat" + i)) {
                                    Slider("Layer Tiling" + i, ref editData.layers[i].tile.x, 1,
                                        500*editData.maskTextureUV);
                                    editData.layers[i].tile.y = editData.layers[i].tile.x;
                                    to.material.SetTextureScale("_Splat" + i,
                                        new Vector2(editData.layers[i].tile.x, editData.layers[i].tile.x));

                                    if (i > 2) {
                                        Vector4 v = new Vector4 {
                                            x = editData.layers[i].tile.x,
                                            y = editData.layers[i].tile.x
                                        };
                                        to.material.SetVector("_Tiling" + i, v);
                                    }
                                }
                            }
                        } else {
                            for (int i = 0; i < 4; i++) {
                                if (to.material != null && to.material.HasProperty("_Splat" + i)) {
                                    Slider("Layer TilingX" + i, ref editData.layers[i].tile.x, 1,
                                        500*editData.maskTextureUV);
                                    Slider("Layer TilingZ" + i, ref editData.layers[i].tile.y, 1,
                                        500*editData.maskTextureUV);

                                    to.material.SetTextureScale("_Splat" + i,
                                        new Vector2(editData.layers[i].tile.x, editData.layers[i].tile.y));

                                    if (i > 2) {
                                        Vector4 v = new Vector4 {
                                            x = editData.layers[i].tile.x,
                                            y = editData.layers[i].tile.y
                                        };
                                        to.material.SetVector("_Tiling" + i, v);
                                    }
                                }
                            }
                        }
                        EditorGUILayout.EndScrollView();
                        GUILayout.EndVertical();
                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();
                    }

                    if (_brushTex.Length > 0 && editData.previewer != null) {
                        editData.previewer.material.SetTexture("_MaskTex", _brushTex[_selectedBrush]);
                        MeshFilter temp = currentSelect.GetComponent<MeshFilter>();
                        if (temp == null) {
                            temp = to.mesh;
                        }
                        editData.previewer.orthographicSize = editData.brushSize*currentSelect.localScale.x*
                                                              (temp.sharedMesh.bounds.size.x/200);
                        editData.previewer.orthographic = true;
                        float test = editData.brushStronger*200/100;
                        editData.previewer.material.SetFloat("_Transp", Mathf.Clamp(test, 0.4f, 1.0f));
                        editData.brushSizeInPercenter =
                            (int) Mathf.Floor(editData.brushSize*editData.maskTexture.width/100f);

                        TerrainLayer ly = editData.layers[editData.selectPaintTexture];
                        editData.previewer.material.SetTextureScale("_MainTex", ly.tile);
                        editData.previewer.material.SetVector("_Tiling", 0.1f*new Vector4 {x = ly.tile.x, y = ly.tile.y});


                        if (_selectedBrush != _oldSelectedBrush ||
                            editData.brushSizeInPercenter != _oldBrushSizeInPercent ||
                            editData.brushAlpha == null || editData.selectPaintTexture != _oldSelectedtexture) {
                            editData.previewer.material.SetTexture("_MainTex",
                                to.material.GetTexture("_Splat" + editData.selectPaintTexture));

                            switch (editData.selectPaintTexture) {
                                case 0:
                                    editData.targetColor = new Color(1, 0, 0, 0);
                                    break;
                                case 1:
                                    editData.targetColor = new Color(0, 1, 0, 0);
                                    break;
                                case 2:
                                    editData.targetColor = new Color(0, 0, 1, 0);
                                    break;
                                case 3:
                                    editData.targetColor = new Color(0, 0, 0, 1);
                                    break;
                            }


                            Texture2D _tbrush = _brushTex[_selectedBrush] as Texture2D;
                            editData.brushAlpha = new float[editData.brushSizeInPercenter*editData.brushSizeInPercenter];
                            for (int i = 0; i < editData.brushSizeInPercenter; i++) {
                                for (int j = 0; j < editData.brushSizeInPercenter; j++) {
                                    editData.brushAlpha[j*editData.brushSizeInPercenter + i] =
                                        _tbrush.GetPixelBilinear(
                                            (float) i/editData.brushSizeInPercenter,
                                            (float) j/editData.brushSizeInPercenter
                                            ).a;
                                }
                            }
                            _oldSelectedBrush = _selectedBrush;
                            _oldSelectedtexture = editData.selectPaintTexture;
                            _oldBrushSizeInPercent = editData.brushSizeInPercenter;
                        }
                    }
                }
            } else {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Please select the Pure Terrain Oject first", PGUIStyle.nodeLabelBold);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }
    }
}