using mono.test;
using UnityEditor;
using UnityEngine;

namespace inspectors.demo {
    [CustomEditor(typeof (EtParticleAndAnimator))]
    public class Insp_ParticleAndAnimator : Editor {
        private EtParticleAndAnimator _pa;

        private void OnEnable() {
            _pa = target as EtParticleAndAnimator;
        }

        public override void OnInspectorGUI() {
            if (GUILayout.Button("Play Loop")) {
                _pa.PlayLoop();
            }
            if (GUILayout.Button("Play Once")) {
                _pa.PlayOnce();
            }
        }
    }
}