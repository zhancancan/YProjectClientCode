using edit.pure.resource;
using mono.terrain;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    internal partial class TvPaint {
        private ProceduralMaterial _procedualAdd;

        private ProceduralMaterial _procedual;
        private Texture _materialAdd;

        private TerrianMaterialType _materialType = TerrianMaterialType.Classic;


        private void PickMaterials() {
            TerrainObject to = currentSelect.GetComponent<TerrainObject>();
            if (to.material != null &&
                to.material.HasProperty("_Control") &&
                to.material.HasProperty("_Splat0") &&
                to.material.HasProperty("_Splat1")) {
                EditorGUILayout.Space();
                InitPencil();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal("box", GUILayout.Width(340));
                GUILayout.FlexibleSpace();
                _selectProcedural = GUILayout.SelectionGrid(_selectProcedural, _textures, 6, "gridlist",
                    GUILayout.Width(340), GUILayout.Height(60));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.Box(PGUIStyle.BorderColor0, GUILayout.ExpandWidth(true), GUILayout.Height(1));
                GUILayout.Label("Add / Replace / Substrances Update", PGUIStyle.nodeLabelBold);
                EditorGUILayout.BeginVertical();

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal(GUILayout.Width(340), GUILayout.Height(20));


                for (int i = 0; i < 4; i++) {
                    if (to.material.HasProperty("_Splat" + i)) {
                        if (GUILayout.Button(PResourceManager.LoadTexture("Terrain/up.png"), GUILayout.Width(53))) {
                            if (_procedualAdd == null && _materialAdd == null && _procedual != null) {
                                _procedualAdd = _procedual;
                            }
                            if (_procedualAdd != null) {
                                to.material.SetTexture("_Splat" + i, _procedualAdd.GetTexture("_MainTex"));
                                if (to.material.HasProperty("_BumpSpat" + i)) {
                                    to.material.SetTexture("_BumpSplat" + i, _procedualAdd.GetTexture("_BumpMap"));
                                }
                            } else if (_materialAdd != null) {
                                to.material.SetTexture("_Splat" + i, _materialAdd);
                            }
                            _selectProcedural = i;
                            _procedualAdd = null;
                            _materialAdd = null;
                            editData.InitSelection();
                        }
                    }
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.Space(5);

                GUILayout.Box(PGUIStyle.BorderColor0, GUILayout.ExpandWidth(true), GUILayout.Height(1));

                GUILayout.Space(5);

                string _assetName =
                    AssetDatabase.GetAssetPath(to.material.GetTexture("_Splat" + _selectProcedural));

                SubstanceImporter importer = AssetImporter.GetAtPath(_assetName) as SubstanceImporter;
                if (importer != null) {
                    ProceduralMaterial[] _promats = importer.GetMaterials();
                    foreach (ProceduralMaterial t in _promats) {
                        if (t.name + "Diffuse" == to.material.GetTexture("_Splat" + _selectProcedural).name) {
                            _procedual = t;
                        }
                    }
                    GUILayout.Space(5);
                    GUILayout.Box(PGUIStyle.BorderColor0, GUILayout.ExpandWidth(true), GUILayout.Height(1));
                } else {
                    _procedual = null;
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Material Type", GUILayout.Width(100));
                _materialType =
                    (TerrianMaterialType)
                        EditorGUILayout.EnumPopup("", _materialType, GUILayout.Width(240));
                GUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                if (_materialType != TerrianMaterialType.Classic) {
                    GUILayout.Label("Substances to add:");
                    _materialAdd = null;
                    _procedualAdd =
                        EditorGUILayout.ObjectField(_procedualAdd, typeof (ProceduralMaterial), true,
                            GUILayout.Width(220)) as ProceduralMaterial;
                } else {
                    GUILayout.Label("Texture to add:");
                    _procedualAdd = null;
                    _materialAdd =
                        EditorGUILayout.ObjectField(_materialAdd, typeof (Texture2D), true, GUILayout.Width(220)) as
                            Texture;
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                if (_procedual != null) {
                    GUILayout.Label("Modify", PGUIStyle.nodeLabelBold);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(350), GUILayout.Height(296));
                    Substance();
                    EditorGUILayout.EndScrollView();
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                } else {
                    ClassicMaterial();
                }
            } else {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Please, select the Pure Terrain Object", PGUIStyle.nodeLabelBold);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }


        private void Substance() {
            var inputs = _procedual.GetProceduralPropertyDescriptions();
            for (int i = 0; i < inputs.Length; i++) {
                var input = inputs[i];
                var type = input.type;
                if (type == ProceduralPropertyType.Boolean) {
                    bool inputbool = _procedual.GetProceduralBoolean(input.name);
                    var oldInputBool = inputbool;
                    inputbool = EditorGUILayout.Toggle(input.name, inputbool, PGUIStyle.ToggleStyle);
                    if (inputbool != oldInputBool) {
                        _procedual.SetProceduralBoolean(input.name, inputbool);
                    }
                } else if (type == ProceduralPropertyType.Float) {
                    if (input.hasRange) {
                        GUILayout.Label(input.name, EditorStyles.boldLabel);

                        var inputFloat = _procedual.GetProceduralFloat(input.name);
                        var oldInputFloat = inputFloat;

                        inputFloat = EditorGUILayout.Slider(inputFloat, input.minimum, input.maximum);
                        if (inputFloat != oldInputFloat)
                            _procedual.SetProceduralFloat(input.name, inputFloat);
                    }
                } else if (type == ProceduralPropertyType.Vector2 ||
                           type == ProceduralPropertyType.Vector3 ||
                           type == ProceduralPropertyType.Vector4) {
                    if (input.hasRange) {
                        GUILayout.Label(input.name, EditorStyles.boldLabel);


                        var vectorComponentAmount = 4;
                        if (type == ProceduralPropertyType.Vector2) vectorComponentAmount = 2;
                        if (type == ProceduralPropertyType.Vector3) vectorComponentAmount = 3;

                        var inputVector = _procedual.GetProceduralVector(input.name);
                        var oldInputVector = inputVector;


                        for (int c = 0; c < vectorComponentAmount; c++)
                            inputVector[c] = EditorGUILayout.Slider(
                                inputVector[c], input.minimum, input.maximum);

                        if (inputVector != oldInputVector)
                            _procedual.SetProceduralVector(input.name, inputVector);
                    }
                } else if (type == ProceduralPropertyType.Color3 || type == ProceduralPropertyType.Color4) {
                    GUILayout.Label(input.name, EditorStyles.boldLabel);


                    var colorInput = _procedual.GetProceduralColor(input.name);
                    var oldColorInput = colorInput;

                    colorInput = EditorGUILayout.ColorField("Shader Color", colorInput);

                    if (colorInput != oldColorInput)
                        _procedual.SetProceduralColor(input.name, colorInput);
                } else if (type == ProceduralPropertyType.Enum) {
                    GUILayout.Label(input.name, EditorStyles.boldLabel);

                    var enumInput = _procedual.GetProceduralEnum(input.name);
                    var oldEnumInput = enumInput;
                    var enumOptions = input.enumOptions;

                    enumInput = GUILayout.SelectionGrid(enumInput, enumOptions, 1);
                    if (enumInput != oldEnumInput)
                        _procedual.SetProceduralEnum(input.name, enumInput);
                }
            }
            _procedual.RebuildTexturesImmediately();
        }


        private void ClassicMaterial() {
            TerrainLayer ly = editData.layers[_selectProcedural];
            if (ly.texture != null) {
                GUILayout.Label("Modify", PGUIStyle.nodeLabelBold);
                GUILayout.BeginHorizontal("Box");
                GUILayout.Label(PResourceManager.LoadTexture("Terrain/TDiff.jpg"));
                ly.texture =
                    EditorGUILayout.ObjectField(ly.texture, typeof (Texture2D), true, GUILayout.Width(75),
                        GUILayout.Height(75)) as Texture;

                TerrainObject tobj = currentSelect.GetComponent<TerrainObject>();
                tobj.material.SetTexture("_Splat" + _selectProcedural, ly.texture);
                if (tobj.material.HasProperty("_BumpSplat" + _selectProcedural)) {
                    GUILayout.Label(PResourceManager.LoadTexture("Terrain/TBump.jpg"));
                    ly.bumpTexture =
                        EditorGUILayout.ObjectField(ly.bumpTexture, typeof (Texture2D), true, GUILayout.Width(75),
                            GUILayout.Height(75)) as Texture;
                    tobj.material.SetTexture("_BumpSplat" + _selectProcedural, ly.bumpTexture);
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }
    }
}