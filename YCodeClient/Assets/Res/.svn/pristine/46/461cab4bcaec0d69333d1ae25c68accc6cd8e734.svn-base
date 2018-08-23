using UnityEditor;
using UnityEngine;

public class InspShader_Plane : ShaderGUI {
    private MaterialEditor _currEditor;
    private Material _material;

    private MaterialProperty _offsets;
    private MaterialProperty _pivot;
    private MaterialProperty _rotate;

    private MaterialProperty _dissovleMap;
    private MaterialProperty _dissolveThreshold;
    private MaterialProperty _colorEdge;
    private MaterialProperty _dissolveEdge;
    private MaterialProperty _dissolveColor;
    private MaterialProperty _dissolveEdgeColor;

    private MaterialProperty _sizex;
    private MaterialProperty _sizey;
    private MaterialProperty _seqSpeed;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties) {
        base.OnGUI(materialEditor, properties);
        EditorGUI.BeginChangeCheck();
        _currEditor = materialEditor;
        _material = materialEditor.target as Material;
        FindProperties(properties);
        DrawDissolve();
        DrawUVTransform();
        DrawSequence();
        if (EditorGUI.EndChangeCheck()) {
            EditorUtility.SetDirty(_material);
        }
    }

    private void DrawUVTransform() {
        EditorGUI.BeginChangeCheck();
        bool ison = _material.IsKeywordEnabled("TRANSFER_UV");
        ison = EditorGUILayout.Toggle("Transform UV", ison);
        if (EditorGUI.EndChangeCheck()) {
            if (ison) {
                _material.EnableKeyword("TRANSFER_UV");
            } else {
                _material.DisableKeyword("TRANSFER_UV");
            }
        }

        if (_material.IsKeywordEnabled("TRANSFER_UV")) {
            _currEditor.FloatProperty(_rotate, "Rotate");
            _currEditor.VectorProperty(_offsets, "Offset(xy)/Scale(zw)");
            _currEditor.VectorProperty(_pivot, "Pivot(xy)");
        }
    }

    private void DrawDissolve() {
        EditorGUI.BeginChangeCheck();
        bool ison = _material.IsKeywordEnabled("DISSOLVE_ON");
        ison = EditorGUILayout.Toggle("Enable Dissolve", ison);
        if (EditorGUI.EndChangeCheck()) {
            if (ison) {
                _material.EnableKeyword("DISSOLVE_ON");
            } else {
                _material.DisableKeyword("DISSOLVE_ON");
            }
        }

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

    private void DrawSequence() {
        EditorGUI.BeginChangeCheck();
        bool ison = _material.IsKeywordEnabled("SEQUENCE_ON");
        ison = EditorGUILayout.Toggle("Enable Sequence", ison);
        if (EditorGUI.EndChangeCheck()) {
            if (ison) {
                _material.EnableKeyword("SEQUENCE_ON");
            } else {
                _material.DisableKeyword("SEQUENCE_ON");
            }
        }

        if (_material.IsKeywordEnabled("SEQUENCE_ON")) {
            _currEditor.RangeProperty(_sizex, "size x");
            _currEditor.RangeProperty(_sizey, "size y");
            _currEditor.FloatProperty(_seqSpeed, "speed");
            EditorGUILayout.Space();
        }
    }


    private void FindProperties(MaterialProperty[] props) {
        _offsets = FindProperty("_Offset", props);
        _rotate = FindProperty("_Rotate", props);
        _pivot = FindProperty("_Pivot", props);

        _dissovleMap = FindProperty("_DissolveMap", props);
        _dissolveThreshold = FindProperty("_DissolveThreshold", props);
        _dissolveColor = FindProperty("_DissolveColor", props);
        _dissolveEdgeColor = FindProperty("_DissolveEdgeColor", props);
        _colorEdge = FindProperty("_ColorEdge", props);
        _dissolveEdge = FindProperty("_DissolveEdge", props);

        _sizex = FindProperty("_SizeX", props);
        _sizey = FindProperty("_SizeY", props);
        _seqSpeed = FindProperty("_SequenceSpeed", props);
    }
}