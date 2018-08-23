using System.IO;
using edit.pure.ui;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    internal abstract class TerrianEditor {
        public abstract void OnGUI();

        protected Transform currentSelect {
            get { return TerrainCenter.editData.currentSelect; }
        }

        protected EditProgressBar progressBar {
            get { return TerrainCenter.progressBar; }
        }

        protected TerrainEditData editData {
            get { return TerrainCenter.editData; }
        }


        protected string GetUniqueDirectory(string dir, string filename) {
            bool _exportNameSuccess = false;

            int num = 1;
            string _next;
            string finalName = null;
            do {
                _next = filename + num;
                if (!Directory.Exists(dir + "/" + filename)) {
                    finalName = filename;
                    _exportNameSuccess = true;
                } else if (!Directory.Exists(dir + "/" + _next)) {
                    finalName = _next;
                    _exportNameSuccess = true;
                }
                num++;
            } while (!_exportNameSuccess);


            return finalName;
        }


        protected void DeleteDirectory(string targetDir) {
            AssetDatabase.DeleteAsset(targetDir);
        }

        protected static void Slider(string label, ref float figutre, float min, float max, float labelWidth = 130) {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            figutre = EditorGUILayout.Slider(figutre, min, max);
            GUILayout.EndHorizontal();
        }


        protected static void Slider(string label, ref int figutre, float min, float max, float labelWidth = 130) {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            figutre = (int) EditorGUILayout.Slider(figutre, min, max);
            GUILayout.EndHorizontal();
        }
    }
}