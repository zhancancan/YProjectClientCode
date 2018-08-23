using System.IO;
using edit.pure.undo;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain.undoOpt {
    public class TextureUndoOperation : IUndoOperation {
        private Texture2D _texture;
        private Color[] _undoCache;
        private Color[] _redoCache;

        public TextureUndoOperation(Texture2D tex) {
            UndoUtils.tryBlock("Texture Changed");
            _texture = tex;
            _undoCache = Save();
        }

        private Color[] Save() {
            return _texture.GetPixels(0, 0, _texture.width, _texture.height, 0);
        }

        private void Restore(Color[] colors) {
            _texture.SetPixels(0, 0, _texture.width, _texture.height, colors);
            _texture.Apply();
            SaveTexture();
        }

        public void SaveTexture() {
            var path = AssetDatabase.GetAssetPath(_texture);
            var bytes = _texture.EncodeToPNG();
            File.WriteAllBytes(path, bytes);
        }


        public void Undo() {
            if (_redoCache == null) {
                _redoCache = Save();
            }
            Restore(_undoCache);
        }

        public void Redo() {
            Restore(_redoCache);
        }
    }
}