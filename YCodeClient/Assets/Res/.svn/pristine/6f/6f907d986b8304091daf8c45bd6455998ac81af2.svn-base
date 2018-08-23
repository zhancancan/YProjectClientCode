using edit.pure.etui.stylePick;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace plugins.ui {
    internal class StylePickerWindow : EditorWindow {
        [InitializeOnLoadMethod]
        private static void StartCommand() {
            StylePickCenter.showWindows += OpenWindow;
        }

        private static StylePickerWindow _window;

        internal static void OpenWindow() {
            _window = GetWindow<StylePickerWindow>(true);
            _window.minSize = new Vector2(200, 300);
            _window.maxSize = new Vector2(400, 800);
            _window.titleContent = new GUIContent("UI Prefab Picker");
            StylePickCenter.Start();
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
            StylePickCenter.closeWindows -= CloseWindow;
            StylePickCenter.closeWindows += CloseWindow;
        }


        private void OnGUI() {
            StylePickCenter.OnGUI(position, focusedWindow == this);
        }

        private void OnDestroy() {
            StylePickCenter.closeWindows -= CloseWindow;
            StylePickCenter.Stop();
            PPaintCenter.ClientRepaints -= Repaint;
            _window = null;
        }
    }
}