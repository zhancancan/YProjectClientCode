using pure.utils.color;
using UnityEditor;
using UnityEngine;

public class InspShader_MultiSkin : ShaderGUI {
    private MaterialEditor _currEditor;
    private Material _material;

    private MaterialProperty _mainTex;
    private MaterialProperty _controlTex;
    private MaterialProperty _bumpTex;

    private MaterialProperty _color;
    private MaterialProperty _specColor;
    private MaterialProperty _shinness;
    private MaterialProperty[] _emissions;


    private MaterialProperty _dissoveMap;
    private MaterialProperty _dissolveColor;
    private MaterialProperty _colorEdge;
    private MaterialProperty _dissolveEdgeColor;
    private MaterialProperty _dissolveEdge;
    private MaterialProperty _dissolveThreshhold;

    private MaterialProperty _rimColor;
    private MaterialProperty _rimPower;

    private MaterialProperty _reflection;
    private MaterialProperty _cube;
    private MaterialProperty _alpha;


    private int _layerSelected;


    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties) {
        EditorGUI.BeginChangeCheck();
        _currEditor = materialEditor;
        _material = materialEditor.target as Material;
        FindProperties(properties);
        DrawTexture();
        DrawReflection();
        DrawColor();
        DrawRimLight();
        DrawDissolve();
        DrawSkins();
        if (EditorGUI.EndChangeCheck()) {
            EditorUtility.SetDirty(_material);
        }
        base.OnGUI(materialEditor, properties);
    }

    private void DrawTexture() {
        _currEditor.TexturePropertySingleLine(new GUIContent("Albedo"), _mainTex);
        _currEditor.TexturePropertySingleLine(new GUIContent("Control"), _controlTex);
        if (_bumpTex != null) {
            _currEditor.TexturePropertySingleLine(new GUIContent("Bump"), _bumpTex);
        }
    }


    private void DrawColor() {
        EditorGUILayout.Space();
        _currEditor.ColorProperty(_color, "Color: ");
        _currEditor.ColorProperty(_specColor, "Specular: ");
        _currEditor.RangeProperty(_shinness, "Shininess: ");
    }

    private void DrawSkins() {
        if (_emissions.Length == 0) return;
        EditorGUILayout.Space();

        _layerSelected = Mathf.Max(0, Mathf.Min(_layerSelected, _emissions.Length - 1));

        if (_emissions.Length > 1) {
            GUIContent[] bars = new GUIContent[_emissions.Length];
            for (int i = 0; i < _emissions.Length; i++) {
                bars[i] = new GUIContent("Layer " + i);
            }
            _layerSelected = Mathf.Min(_layerSelected, _emissions.Length);
            _layerSelected = GUILayout.Toolbar(_layerSelected, bars);
        } else {
            _layerSelected = 0;
        }
        _currEditor.RangeProperty(_alpha, "Alpha");


        bool changed = false;

        EditorGUI.BeginChangeCheck();
        _currEditor.RangeProperty(_emissions[_layerSelected], "emission: ");
        if (EditorGUI.EndChangeCheck()) {
            changed = true;
        }

        EditorGUI.BeginChangeCheck();
        Vector4 v = _material.GetVector("_ColorT_" + _layerSelected);
        v.x = EditorGUILayout.Slider("Hue", v.x, -1, 1);
        v.y = EditorGUILayout.Slider("Saturation", v.y, -1, 1);
        v.z = EditorGUILayout.Slider("Brightness", v.z, -1, 1);
        v.w = EditorGUILayout.Slider("Contrast", v.w, -1, 1);
        if (EditorGUI.EndChangeCheck()) {
            _material.SetVector("_ColorT_" + _layerSelected, v);
            ApplyColorMatrix();
            changed = true;
        }
        if (changed) EditorUtility.SetDirty(_material);
        if (IsSelectInScene) {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Apply", GUILayout.Width(150))) {
                ApplyColorMatrix();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(20);
    }


    private void ApplyColorMatrix() {
        ColorMatrix c = new ColorMatrix();
        for (int i = 0; i < 5; i++) {
            Vector4 v = _material.GetVector("_ColorT_" + i);
            c.hue = v.x;
            c.saturation = v.y;
            c.brightness = v.z;
            c.contrast = v.w;
            _material.SetMatrix("_ColorMatrix" + i, c.GetMatrix());
            _material.SetVector("_ColorOffset" + i, c.GetOffset());
        }
    }


    private void DrawRimLight() {
        EditorGUILayout.Space();
        bool inOn = _material.IsKeywordEnabled("RIM_ON");
        EditorGUI.BeginChangeCheck();
        inOn = EditorGUILayout.Toggle("Rimlight ON:", inOn);
        if (EditorGUI.EndChangeCheck()) {
            if (inOn) {
                _material.EnableKeyword("RIM_ON");
            } else {
                _material.DisableKeyword("RIM_ON");
            }
        }
        if (inOn) {
            _currEditor.ColorProperty(_rimColor, "Rimlight Color");
            _currEditor.RangeProperty(_rimPower, "RimLight Power");
        }
    }

    private void DrawReflection() {
        EditorGUILayout.Space();
        bool inOn = _material.IsKeywordEnabled("REFLECT_ON");
        EditorGUI.BeginChangeCheck();
        inOn = EditorGUILayout.Toggle("Reflect ON:", inOn);
        if (EditorGUI.EndChangeCheck()) {
            if (inOn) {
                _material.EnableKeyword("REFLECT_ON");
            } else {
                _material.DisableKeyword("REFLECT_ON");
            }
        }
        if (inOn) {
            _currEditor.TexturePropertySingleLine(new GUIContent("Cubte"), _cube);
            _currEditor.TexturePropertySingleLine(new GUIContent("Reflection"), _reflection);
        }
    }


    private void DrawDissolve() {
        EditorGUILayout.Space();
        bool inOn = _material.IsKeywordEnabled("DISSOLVE_ON");
        EditorGUI.BeginChangeCheck();
        inOn = EditorGUILayout.Toggle("Dissolve ON:", inOn);
        if (EditorGUI.EndChangeCheck()) {
            if (inOn) {
                _material.EnableKeyword("DISSOLVE_ON");
            } else {
                _material.DisableKeyword("DISSOLVE_ON");
            }
        }
        if (inOn) {
            _currEditor.TexturePropertySingleLine(new GUIContent("DissolveMap"), _dissoveMap);
            _currEditor.ColorProperty(_dissolveColor, "Color");
            _currEditor.RangeProperty(_colorEdge, "Color Factor");
            _currEditor.ColorProperty(_dissolveEdgeColor, "Edge Color");
            _currEditor.RangeProperty(_dissolveEdge, "Edge Color Factor");
            _currEditor.RangeProperty(_dissolveThreshhold, "Dissolve Threshold");


            if (_material.HasProperty("_DissolveUV2")) {
                bool uv2 = _material.IsKeywordEnabled("UV2_ON");
                EditorGUI.BeginChangeCheck();
                inOn = EditorGUILayout.Toggle("Dissolve Use Uv2:", uv2);
                if (EditorGUI.EndChangeCheck()) {
                    if (inOn) {
                        _material.EnableKeyword("UV2_ON");
                    } else {
                        _material.DisableKeyword("UV2_ON");
                    }
                }
            }
        }
    }
     

    private void FindProperties(MaterialProperty[] props) {
        _mainTex = FindProperty("_MainTex", props);
        _controlTex = FindProperty("_Control", props);
        _bumpTex = _material.HasProperty("_BumpTex") ? FindProperty("_BumpTex", props) : null;


        _color = FindProperty("_Color", props);
        _specColor = FindProperty("_SpecColor", props);
        _shinness = FindProperty("_Shininess", props);

        int sk = (int) Mathf.Max(1, _material.GetFloat("_SKIN"));

        _emissions = new MaterialProperty[sk];
        //   _colorMatrics = new MaterialProperty[sk];
        for (int i = 0; i < sk; i++) {
            _emissions[i] = FindProperty("_Emission" + i, props);
            //     _colorMatrics[i] = FindProperty("_Color_T_" + i, props);
        }

        _rimColor = FindProperty("_RimColor", props);
        _rimPower = FindProperty("_RimPower", props);
        _alpha = FindProperty("_Alpha", props);


        _dissoveMap = FindProperty("_DissolveMap", props);
        _dissolveColor = FindProperty("_DissolveColor", props);
        _colorEdge = FindProperty("_ColorEdge", props);
        _dissolveEdgeColor = FindProperty("_DissolveEdgeColor", props);
        _dissolveEdge = FindProperty("_DissolveEdge", props);
        _dissolveThreshhold = FindProperty("_DissolveThreshold", props); 
       
        _reflection = FindProperty("_Reflection", props);
        _cube = FindProperty("_Cube", props);
    }


    private bool IsSelectInScene {
        get {
            if (Selection.activeGameObject == null) return false;
            GameObject o = Selection.activeGameObject;
            if (o.GetComponent<Renderer>() == null) return false;
            return o.GetComponent<Renderer>().sharedMaterial == _material;
        }
    }
}