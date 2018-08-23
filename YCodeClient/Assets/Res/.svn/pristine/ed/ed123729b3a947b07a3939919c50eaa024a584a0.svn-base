using edit.pure.inspector;
using UnityEditor;
using UnityEngine;

namespace tools {
    internal class AlignWindow : PopupWindowContent {
        [InitializeOnLoadMethod]
        private static void Start() {
            PAlignCenter.showWindows += OpenWindow;
        }


        internal static void OpenWindow(Rect rect) {
            PopupWindow.Show(rect, new AlignWindow());
            PAlignCenter.Start();
        }

        private static void CloseWindow() {
            PFilterListCenter.closeWindows -= CloseWindow;
            if (EditorWindow.focusedWindow is PopupWindow) {
                EditorWindow.focusedWindow.Close();
            }
        }

        public override Vector2 GetWindowSize() {
            return new Vector2(105,45);
        }


        public override void OnOpen() {
            base.OnOpen();
            PAlignCenter.closeWindow -= CloseWindow;
            PAlignCenter.closeWindow += CloseWindow;
            PAlignCenter.Start();
        }

        public override void OnGUI(Rect rect) {
            PAlignCenter.OnGUI(rect, true);
        }


        public override void OnClose() {
            PAlignCenter.closeWindow -= CloseWindow;
            base.OnClose();
            PAlignCenter.Stop();
        }
    }
}