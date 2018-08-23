using UnityEditor;
using UnityEngine;

public class InspShader_MultiPlane : ShaderGUI {
    private MaterialEditor _currEditor;
    private Material _material;

    private MaterialProperty[] _textures = new MaterialProperty[0];
    private MaterialProperty[] _offsets = new MaterialProperty[0];
    private MaterialProperty[] _pivots = new MaterialProperty[0];
    private MaterialProperty[] _rotate = new MaterialProperty[0];

    private MaterialProperty _dissovleMap;
    private MaterialProperty _dissolveThreshold;
    private MaterialProperty _colorEdge;
    private MaterialProperty _dissolveEdge;
    private MaterialProperty _dissolveColor;
    private MaterialProperty _dissolveEdgeColor;


    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties) {
        base.OnGUI(materialEditor, properties);
        EditorGUI.BeginChangeCheck();
        _currEditor = materialEditor;
        _material = materialEditor.target as Material;
        FindProperties(properties);
        DrawDissolve();
        DrawTexture();
        if (EditorGUI.EndChangeCheck()) {
            EditorUtility.SetDirty(_material);
        }
    }

    private void DrawDissolve() {
        if (_material.IsKeywordEnabled("DISSOLVE_ON")) {
            _currEditor.TexturePropertySingleLine(new GUIContent("Dissolve Map"), _dissovleMap);
            _currEditor.ColorProperty(_dissolveColor, "Dissolve Color");
            _currEditor.RangeProperty(_colorEdge, "Color Edge");
            _currEditor.ColorProperty(_dissolveEdgeColor, "Dissolve Edge Color");
            _currEditor.RangeProperty(_dissolveEdge, "Dissolve edge");
            _currEditor.RangeProperty(_dissolveThreshold, "Dissolve Threshold");
            EditorGUILayout.Space();
        }
    }

    private void DrawTexture() {
        for (int i = 0; i < _textures.Length; i++) {
            _currEditor.TexturePropertySingleLine(new GUIContent("Tex_" + i), _textures[i]);
            _currEditor.FloatProperty(_rotate[i], "Rotate");
            _currEditor.VectorProperty(_offsets[i], "offset(x,y)/scale(z,w)");
            _currEditor.VectorProperty(_pivots[i], "pivot(x,y)");
        }
    }


    private void FindProperties(MaterialProperty[] props) {
        int numSkins = 2;
        if (_material.IsKeywordEnabled("_TEX_L2")) {
            numSkins = 2;
        } else if (_material.IsKeywordEnabled("_TEX_L3")) {
            numSkins = 3;
        } else if (_material.IsKeywordEnabled("_TEX_L4")) {
            numSkins = 4;
        }
        _textures = new MaterialProperty[numSkins];
        _pivots = new MaterialProperty[numSkins];
        _offsets = new MaterialProperty[numSkins];
        _rotate = new MaterialProperty[numSkins];
        for (int i = 0; i < numSkins; i++) {
            _textures[i] = FindProperty("_Tex_" + i, props);
            _rotate[i] = FindProperty("_Rotate_" + i, props);
            _offsets[i] = FindProperty("_Offset_" + i, props);
            _pivots[i] = FindProperty("_Pivot_" + i, props);
        }


        _dissovleMap = FindProperty("_DissolveMap", props);
        _dissolveThreshold = FindProperty("_DissolveThreshold", props);
        _dissolveColor = FindProperty("_DissolveColor", props);
        _dissolveEdgeColor = FindProperty("_DissolveEdgeColor", props);
        _colorEdge = FindProperty("_ColorEdge", props);
        _dissolveEdge = FindProperty("_DissolveEdge", props);
    }
}