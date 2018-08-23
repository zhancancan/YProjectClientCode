using edit.pure.path;
using edit.pure.resource;
using mono.scene;
using pure.scene.mono;
using UnityEditor;

namespace plugins.path {
    [CustomEditor(typeof (PathWayPoint))]
    public class Insp_PathWay : Editor {
        private bool _playMode;

        private void OnEnable() {
            PathWayCenter.currPath = target as PathWayPoint_Dll;
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
                PathWayCenter.currPath = target as PathWayPoint_Dll;
                PathWayCenter.OnGUI();
            }
        }


        private void OnSceneGUI() {
            PathWayCenter.OnSceneGUI();
        }

        private void OnDestroy() {
            PathWayCenter.currPath = null;
            PPlayModeControl.justEnteredPlayMode -= EnterPlayerMode;
            PPlayModeControl.justLeftPlayMode -= LeftPlayerMode;
            PPaintCenter.ClientRepaints -= Repaint;
        }
    }
}