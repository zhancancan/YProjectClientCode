using System.Collections;
using edit.pure.resource;
using mono.terrain;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    internal partial class TvPaint {
        private void CreatePreviewer() {
            GameObject projectB = new GameObject(TerrainCenter.PREVIEWER_NAME);
            projectB.AddComponent<Projector>();
            projectB.hideFlags = HideFlags.HideInHierarchy;
            editData.previewer = projectB.GetComponent<Projector>();
            MeshFilter sideOfGeo = currentSelect.GetComponent<MeshFilter>();
            if (sideOfGeo == null) {
                sideOfGeo = currentSelect.GetComponent<TerrainObject>().mesh;
            }
            Vector2 meshSize = new Vector2(sideOfGeo.sharedMesh.bounds.size.x, sideOfGeo.sharedMesh.bounds.size.z);
            editData.previewer.orthographic = true; // set ting type first, otherwise the inputed data will be replace;
            editData.previewer.nearClipPlane = -20f;
            editData.previewer.farClipPlane = 20f;
            editData.previewer.orthographicSize = editData.brushSize*currentSelect.localScale.x*(meshSize.x/100);
            editData.previewer.ignoreLayers = ~TerrainCenter.LAYER_MASK;
            editData.previewer.transform.Rotate(90, -90, 0);
            Material pmat = new Material(Shader.Find("Hidden/Terrain/Previewer"));
            editData.previewer.material = pmat;
            pmat.SetTexture("_MainTex", _textures[editData.selectPaintTexture]);
            pmat.SetTexture("_MaskTex", _brushTex[_selectedBrush]);


            TerrainLayer ly = editData.layers[editData.selectPaintTexture];
            pmat.SetTextureScale("_MainTex", ly.tile);
            pmat.SetVector("_Tiling", 0.1f*new Vector4(ly.tile.x, ly.tile.y, 0, 0));
        }


        private void InitPencil() {
            Material mat = currentSelect.gameObject.GetComponent<TerrainObject>().material;
            for (int i = 0; i < _textures.Length; i++) {
                _textures[i] = null;
            }
            int max = mat.HasProperty("_Splat3")
                ? 4
                : mat.HasProperty("_Splat2")
                    ? 3
                    : mat.HasProperty("_Splat1") ? 2 : 0;

            for (int i = 0; i < max; i++) {
                _textures[i] = mat.GetTexture("_Splat" + i) as Texture2D;
            }
        }

        private void InitBrush() {
            ArrayList burshList = new ArrayList();
            Texture2D tex;
            int index = 0;
            string prefix = "Assets/Res/Scripts/Editor/res/Terrain/Brushes/Brush";
            do {
                tex = AssetDatabase.LoadAssetAtPath(prefix + index + ".png", typeof (Texture2D)) as Texture2D;
                if (tex != null) {
                    burshList.Add(tex);
                }
                index ++;
            } while (tex != null);
            _brushTex = burshList.ToArray(typeof (Texture2D)) as Texture2D[];
        }

        private void ProjectionWorldConfig() {
            if (editData.upsideTile.x.Equals(editData.upsideTile.y) == false && editData.joinTiles ||
                editData.upsideTile.z.Equals(editData.upsideTile.w) == false && editData.joinTiles) {
                editData.joinTiles = false;
            }
            EditorGUILayout.Space();
            GUILayout.Label("Paint is not available for this shader", PGUIStyle.nodeLabelBold);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.Label("World Projection Shaders Option", PGUIStyle.nodeLabelBold);
            EditorGUILayout.Space();
            Slider("up/Sides fighting:", ref editData.upsideF, 0, 10);
            Slider("Blend Factor:", ref editData.blendFac, 0, 20);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            editData.joinTiles = EditorGUILayout.Toggle("Tiling: Join X/Y", editData.joinTiles);
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (editData.joinTiles) {
                Slider("Up Texture Tiling:", ref editData.upsideTile.x, 0.0f, 10, 150);
                editData.upsideTile.y = editData.upsideTile.x;
                Slider("Side Texture Tiling:", ref editData.upsideTile.z, 0.0f, 10, 150);
                editData.upsideTile.w = editData.upsideTile.z;
            } else {
                Slider("Up Texture Tiling X:", ref editData.upsideTile.x, 0.0f, 10, 150);
                Slider("Up Texture Tiling Y:", ref editData.upsideTile.y, 0.0f, 10, 150);
                Slider("Up Texture Tiling Z:", ref editData.upsideTile.z, 0.0f, 10, 150);
                Slider("Up Texture Tiling W:", ref editData.upsideTile.w, 0.0f, 10, 150);
            }

            Material mat = currentSelect.GetComponent<TerrainObject>().material;
            mat.SetVector("_Tiling", editData.upsideTile);
            mat.SetFloat("UpSide", editData.upsideF);
            mat.SetFloat("_Blend", editData.blendFac);
        }
    }
}