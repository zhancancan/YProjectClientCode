using edit.pure.etui.styleList;
using edit.pure.resource;
using UnityEditor;
using UnityEngine;

namespace plugins.ui {
    internal class StyleListWindow : EditorWindow {
        [MenuItem("EditorTools/UI/StyleList", false, 2003)]
        internal static void OpenWindow() {
            StyleListWindow _window = GetWindow<StyleListWindow>(false);
            _window.titleContent = new GUIContent("UI Style List");
            StyleListCenter.Start();
        }


        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            StyleListCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }


        private void OnGUI() {
            StyleListCenter.OnGUI(position, focusedWindow == this);
        }

        private void OnDestroy() {
            StyleListCenter.Stop();
            PPaintCenter.ClientRepaints -= Repaint;
        }
    }
}