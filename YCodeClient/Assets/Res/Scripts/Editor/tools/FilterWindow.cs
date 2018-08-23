using edit.pure.inspector;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace tools {
    internal static class FilterCommand {
        [InitializeOnLoadMethod]
        internal static void Start() {
            PFilterListCenter.showWindows += OpenWindow;
        }

        private static void OpenWindow(Rect rect, Vector2 windowSize, FilterDisplayMode mode, FilterContent[] contents) {
            if (mode == FilterDisplayMode.WINDOW) {
                FilterWindow.OpenWindow(contents);
            } else {
                FilterPopup.OpenWindow(rect, windowSize, contents);
            }
        }
    }

    internal class FilterWindow : EditorWindow {
        private static FilterWindow _window;

        internal static void OpenWindow(FilterContent[] contents) {
            _window = GetWindow<FilterWindow>(true);
            _window.maxSize = new Vector2(400, 500);
            _window.titleContent = new GUIContent("Search");
            PFilterListCenter.Start(contents);
        }

        private static void CloseWindow() {
            if (_window) {
                _window.Close();
            }
            _window = null;
        }

        internal void OnEnable() {
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
            PFilterListCenter.closeWindows -= CloseWindow;
            PFilterListCenter.closeWindows += CloseWindow;
        }

        protected void OnGUI() {
            PFilterListCenter.OnGUI(position, focusedWindow == this);
        }

        protected void OnDestroy() {
            PFilterListCenter.closeWindows -= CloseWindow;
            PFilterListCenter.Stop();
            PPaintCenter.ClientRepaints -= Repaint;
            _window = null;
        }
    }

    internal class FilterPopup : PopupWindowContent {
        private static FilterPopup _current;
        private static FilterContent[] _contents;
        private static Vector2 _windowSize;

        internal static void OpenWindow(Rect rect, Vector2 size, FilterContent[] contents) { 
            _contents = contents;
            _windowSize = size;
            _current = new FilterPopup();
            PopupWindow.Show(rect, _current);
        }

        private static void CloseWindow() {
            PFilterListCenter.closeWindows -= CloseWindow;
            if (_current != null && _current._window) {
                _current._window.Close();
            }
            _current = null;
        }

        public override Vector2 GetWindowSize() {
            return _windowSize;
        }

        private EditorWindow _window;

        public override void OnOpen() {
            base.OnOpen();
            PFilterListCenter.closeWindows -= CloseWindow;
            PFilterListCenter.closeWindows += CloseWindow;
            PFilterListCenter.Start(_contents);
            _window = editorWindow;
            PPaintCenter.ClientRepaints -= _window.Repaint;
            PPaintCenter.ClientRepaints += _window.Repaint;
        }

        public override void OnGUI(Rect rect) {
            PFilterListCenter.OnGUI(rect, true);
        }

        public override void OnClose() {
            if (_window) PPaintCenter.ClientRepaints -= _window.Repaint;
            PFilterListCenter.closeWindows -= CloseWindow;
            base.OnClose();
            PFilterListCenter.Stop();
            _current = null;
        }
    }
}