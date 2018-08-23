using System;
using edit.pure.animation;
using edit.pure.resource;
using edit.pure.tools.reflection;
using UnityEditor;

namespace plugins.effect {
    public class ActionSyncWindow : EditorWindow {
        [MenuItem("EditorTools/Effect/Action Sync", false, 2006)]
        private static void Start() {
            ActionSyncWindow w = GetWindow<ActionSyncWindow>(false);
            Type t = UnityEditorReflectionTool.GetEditorWindow("AnimationWindow");
            w.titleContent = UnityEditorReflectionTool.TextContentWithIcon("ActionSync", t.ToString());
        }

        internal void OnEnable() {
            PResourceManager.SetDefaultResourcePath("Assets/Res/Scripts/Editor/res/");
            ActionSyncCenter.Start();
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
            wantsMouseMove = true; 
        }

        internal void OnGUI() {
            ActionSyncCenter.OnGUI(position,focusedWindow ==this);
        }

        internal void OnDisable() {
            PPaintCenter.ClientRepaints -= Repaint;
            ActionSyncCenter.stop();
        }


        internal void OnDestroy() {
            ActionSyncCenter.stop();
        }
    }
}