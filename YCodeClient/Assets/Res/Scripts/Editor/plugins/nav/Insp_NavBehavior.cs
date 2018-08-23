using edit.pure.nav;
using edit.pure.resource;
using mono.scene;
using pure.scene.mono;
using UnityEditor;
using UnityEngine;

namespace plugins.nav {
    [CustomEditor(typeof (Navigator))]
    internal class Insp_Navigator : Editor {
        private static string[] barName = {"EDIT", "PAINT", "LAYER", "JUMP", "TEST"};
        private bool _playMode;

        private void OnEnable() {
            EtNavCenter.currNav = target as Navigator_Dll;
            PPlayModeControl.justEnteredPlayMode -= EnterPlayerMode;
            PPlayModeControl.justEnteredPlayMode += EnterPlayerMode;

            PPlayModeControl.justLeftPlayMode -= LeftPlayerMode;
            PPlayModeControl.justLeftPlayMode += LeftPlayerMode;
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        private void EnterPlayerMode() {
            _playMode = true;
        }

        private void LeftPlayerMode() {
            Repaint();
            _playMode = false;
        }

        public override void OnInspectorGUI() {
            if (!_playMode && !EditorApplication.isPlayingOrWillChangePlaymode) {
                base.OnInspectorGUI();
                int mode = (int) EtNavCenter.mode;
                mode = GUILayout.Toolbar(mode, barName);
                EtNavCenter.mode = (EtNavMode) mode;
                EtNavCenter.currNav = target as Navigator_Dll;
                EtNavCenter.OnGUI();
            }
        }


        private void OnSceneGUI() {
            EtNavCenter.OnSceneGUI();
        }

        private void OnDestroy() {
            EtNavCenter.currNav = null;
            PPlayModeControl.justEnteredPlayMode -= EnterPlayerMode;
            PPlayModeControl.justLeftPlayMode -= LeftPlayerMode;
            PPaintCenter.ClientRepaints -= Repaint;
        }
    }
}