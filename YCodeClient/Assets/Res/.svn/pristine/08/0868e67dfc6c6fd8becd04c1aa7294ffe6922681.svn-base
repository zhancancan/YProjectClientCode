using edit.pure.resource;
using mono.terrain;
using UnityEngine;

namespace plugins.terrain {
    internal partial class TvPaint : TerrianEditor {
        private Texture[] _textures = new Texture[4];

        private int _oldBrushSizeInPercent;

        private int _selectProcedural;
        private int _oldSelectedtexture;


        private Texture[] _brushTex = new Texture[10];
        private int _selectedBrush;
        private int _oldSelectedBrush;

        private string[] _materialMenu = {"Painter", "Materials"};
        private int _menuIndex;

        private Vector2 _scrollPos;

        public override void OnGUI() {
            if (currentSelect.GetComponent<TerrainObject>() != null) {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                _menuIndex = GUILayout.Toolbar(_menuIndex, _materialMenu, GUILayout.Width(290), GUILayout.Height(20));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                switch (_menuIndex) {
                    case 0:
                        Material mat = currentSelect.GetComponent<TerrainObject>().material;
                        if (mat != null) {
                            PixelPatiner();
                        } else {
                            GUILayout.Label("no material found");
                        }

                        break;
                    case 1:
                        PickMaterials();
                        break;
                }
            } else {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Please, select the Pure Terrain Object", PGUIStyle.nodeLabelBold);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }
    }
}