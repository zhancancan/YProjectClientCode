using edit.pure.flexSkeleton;
using edit.pure.resource;
using mono.fbx;
using UnityEditor;

namespace inspectors.fbx {
    [CustomEditor(typeof (FlexSkeleton))]
    public class Insp_FlexSkeleton : Editor {
        private FlexSkeleton _skeleton;

        internal void OnEnable() {
            _skeleton = target as FlexSkeleton;
            FlexSkeletonCenter.Start(_skeleton);
            PPaintCenter.ClientRepaints -= Repaint;
            PPaintCenter.ClientRepaints += Repaint;
        }

        public override void OnInspectorGUI() {
            FlexSkeletonCenter.OnGUI(serializedObject);
        }

        internal void OnDisable() {
            PPaintCenter.ClientRepaints -= Repaint;
            FlexSkeletonCenter.Stop();
        }
    }
}