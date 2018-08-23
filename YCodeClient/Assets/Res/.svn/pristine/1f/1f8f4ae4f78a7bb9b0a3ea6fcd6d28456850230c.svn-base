using edit.pure.resource;
using mono.terrain;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    internal partial class TvSetting {
        private void DrawSetting() {
            GUILayout.Label("Shader Model", PGUIStyle.nodeLabelBold);


            GUILayout.Space(10);


            GUILayout.BeginHorizontal();
            GUILayout.Label("Shader:", GUILayout.Width(80));

            editData.shaderType =
                (TerrainShaderType) EditorGUILayout.EnumPopup("", editData.shaderType, GUILayout.Width(300));


            GUILayout.EndHorizontal();
            GUILayout.Space(20);

            //if (editData.shaderMode != ShaderMode.CustomShader) {
            //    GUILayout.Label("Shader Compatibiltiy", PGUIStyle.nodeLabelBold);

            //    GUILayout.BeginHorizontal();
            //    GUILayout.Space(20);
            //    GUILayout.BeginVertical();
            //    DrawShaderCompability("GLES 1.1", IsOkForGLES1());
            //    DrawShaderCompability("GLES 2.0", IsOkForGLES2());
            //    DrawShaderCompability("Desktop", IsOkForDesktop());
            //    DrawShaderCompability("UnityWebPlayer", IsOkForWebPlayer());

            //    DrawShaderCompability("Flash", IsOkForFlash());
            //    DrawShaderCompability("NaCI", IsOkForFlash());
            //    GUILayout.EndVertical();
            //    GUILayout.Space(20);
            //    GUILayout.EndHorizontal();
            //}


            GUILayout.Space(20);

            editData.isMaster = GUILayout.Toggle(editData.isMaster, "Master Pure Terrain Object", PGUIStyle.ToggleStyle);


            if (editData.isMaster) {
                GUILayout.BeginVertical("Box");

                GUILayout.BeginHorizontal();
                GUILayout.Label("Scene Camera", PGUIStyle.nodeLabelBold, GUILayout.Width(190));
                editData.playerCamera =
                    EditorGUILayout.ObjectField(editData.playerCamera, typeof (Transform), true) as Transform;
                GUILayout.EndHorizontal();

                TerrainObject to = currentSelect.GetComponent<TerrainObject>();


                GUILayout.BeginHorizontal();
                editData.activateLOD = GUILayout.Toggle(editData.activateLOD, "Activate LOD System",
                    PGUIStyle.ToggleStyle);
                GUILayout.FlexibleSpace();
                to.lodPreview =
                    GUILayout.Toggle(to.lodPreview, "Preview",
                        PGUIStyle.ToggleStyle);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                editData.activateBillboard = GUILayout.Toggle(editData.activateBillboard, "Activate Billboard System",
                    PGUIStyle.ToggleStyle);
                GUILayout.FlexibleSpace();
                to.billboardPreview =
                    GUILayout.Toggle(to.billboardPreview, "Preview",
                        PGUIStyle.ToggleStyle);
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                editData.activateLayerCul = GUILayout.Toggle(editData.activateLayerCul, "Activate Layer Cull",
                    PGUIStyle.ToggleStyle);
                GUILayout.FlexibleSpace();
                to.layerCullPreview =
                    GUILayout.Toggle(to.layerCullPreview, "Preview",
                        PGUIStyle.ToggleStyle);
                GUILayout.EndHorizontal();


                EditorGUILayout.Space();

                if (editData.activateLayerCul) {
                    GUILayout.BeginVertical("box");
                    GUILayout.Label("Maxinum distance of view", PGUIStyle.nodeLabelBold, GUILayout.Width(220));


                    Slider("Close Distance", ref editData.closeView, 0, 500);
                    Slider("Normal Distance", ref editData.normalView, 0, 500);
                    Slider("Far Distance", ref editData.farView, 0, 500);
                    Slider("Background Distance", ref editData.backgroundView, 0, 10000);
                    GUILayout.EndVertical();
                }
                if (editData.backgroundView < editData.farView) {
                    editData.backgroundView = editData.farView;
                } else if (editData.farView < editData.normalView) {
                    editData.farView = editData.normalView;
                } else if (editData.normalView < editData.closeView) {
                    editData.normalView = editData.closeView;
                }
                GUILayout.EndVertical();
            }
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Update", GUILayout.Width(100), GUILayout.Height(25))) {
                ChangeTerrainSetting();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
        }


        private static void DrawShaderCompability(string label, bool compatible) {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.Label(compatible
                ? PResourceManager.LoadTexture("Terrain/ok.png")
                : PResourceManager.LoadTexture("Terrain/ko.png"));
            GUILayout.EndHorizontal();
        }


        private void ChangeTerrainSetting() {
            Shader shader = TerrainShader.GetShader((int) editData.shaderType);

            TerrainObject to = currentSelect.gameObject.GetComponent<TerrainObject>();
            if (shader != null) {
                to.material.shader = shader;
                //if (to.material.HasProperty("_ToonShader")) {
                //    Cubemap _toon =
                //        AssetDatabase.LoadAssetAtPath<Cubemap>(TerrainCenter.SHADER_PATH + "/Source/toony lighting.psd");
                //    to.material.SetTexture("_ToonShade", _toon);
                //}
            }
            foreach (var layer in editData.layers) {
                if (to.material.HasProperty(layer.name)) {
                    to.material.SetTexture(layer.name, layer.texture);
                }
                if (to.material.HasProperty(layer.bumpName)) {
                    to.material.SetTexture(layer.bumpName, layer.bumpTexture);
                }
            }

            if (editData.isMaster) {
                to.enabledLODSystem = editData.activateLOD;
                to.enabledBillboard = editData.activateBillboard;
                to.enabledLayerCul = editData.activateLayerCul;
                to.closeView = editData.closeView;
                to.normalView = editData.normalView;
                to.farView = editData.farView;
                to.backgroundView = editData.backgroundView;
                to.master = 1;
                to.playerCamera = editData.playerCamera;
                PrefabUtility.RecordPrefabInstancePropertyModifications(to);
            } else {
                to.enabledLODSystem = false;
                to.enabledBillboard = false;
                to.enabledLayerCul = false;
                to.master = 0;
                TerrainLod[] lods = Object.FindObjectsOfType<TerrainLod>();
                foreach (var o in lods) {
                    o.LOD2.enabled = o.LOD3.enabled = false;
                    o.LOD1.enabled = true;
                    switch (editData.lodController) {
                        case LodMode.Mass_Control:
                            o.mode = 0;
                            break;
                        case LodMode.Independent_Control:
                            o.mode = 1;
                            break;
                    }
                    PrefabUtility.RecordPrefabInstancePropertyModifications(o);
                }

                TerrainBillboard[] bbs = Object.FindObjectsOfType<TerrainBillboard>();
                foreach (var b in bbs) {
                    b.render.enabled = true;
                    b.matrix.LookAt(Vector3.one, Vector3.up);
                }

                to.billboardPosition = new Vector3[0];
                to.billboardStatus = new int[0];
                to.billboardScript = new TerrainBillboard[0];

                to.objLodScript = new TerrainLod[0];
                to.objPostion = new Vector3[0];
                to.objLodStatus = new int[0];
                to.mode = 0;
                PrefabUtility.RecordPrefabInstancePropertyModifications(to);
            }
            editData.textures = null;
            editData.InitSelection();
            EditorUtility.DisplayDialog("Pure Terrain", "Update Accomplish", "OK");
        }
    }
}