using mono.fbx;
using pure.utils.coroutine;
using UnityEditor;
using UnityEngine;

namespace inspectors.fbx {
    [CustomEditor(typeof (Fbx_Bone))]
    public class Insp_FbxBone : Editor {
        private Fbx_Bone _bone;

        internal void OnEnable() {
            _bone = target as Fbx_Bone;
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();
            _bone.prefab = (GameObject) EditorGUILayout.ObjectField("Prefab", _bone.prefab, typeof (GameObject), false);
            _bone.position = EditorGUILayout.Vector3Field("Position:", _bone.position);
            _bone.rotate = EditorGUILayout.Vector3Field("Rotation:", _bone.rotate);
            _bone.scale = EditorGUILayout.Vector3Field("Scale:", _bone.scale);
            if (EditorGUI.EndChangeCheck()) {
                _bone.EditInvalidate(InvalidateType.All);
                EditorUtility.SetDirty(_bone);
            }
        }
    }
}