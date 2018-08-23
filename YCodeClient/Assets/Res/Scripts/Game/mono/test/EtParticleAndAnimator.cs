using System.Collections.Generic;
using UnityEngine;

namespace mono.test {
    public class EtParticleAndAnimator : MonoBehaviour {
        private Dictionary<Animator, int> _defaultClips;
#if UNITY_EDITOR
        public void Start() {
            Animator[] anis = GetComponentsInChildren<Animator>(true);
            _defaultClips = new Dictionary<Animator, int>();
            foreach (Animator an in anis) {
                AnimatorStateInfo? infos = an.GetCurrentAnimatorStateInfo(0);
                _defaultClips.Add(an, infos.Value.shortNameHash);
            }
        }

        [ContextMenu("Play Loop")]
        public void PlayLoop() {
            ParticleSystem[] pss = GetComponentsInChildren<ParticleSystem>(true);
            foreach (ParticleSystem ps in pss) {
                //ParticleSystem.MainModule m = ps.main;
                //m.loop = true;
                ps.Play();
            }
            Animator[] anis = GetComponentsInChildren<Animator>(true);
            foreach (Animator an in anis) {
                //AnimationClip[] clips = an.runtimeAnimatorController.animationClips;
                //foreach (var c in clips) {
                //    c.wrapMode = WrapMode.Loop;
                //}
                int nameHash;
                if (_defaultClips.TryGetValue(an, out nameHash)) {
                    an.Play(nameHash, 0, 0);
                }
            }
            Animation[] animations = GetComponentsInChildren<Animation>(true);
            foreach (var an in animations) {
                //an.wrapMode = WrapMode.Loop;
                an.Play();
            }
        }

        [ContextMenu("Play Once")]
        public void PlayOnce() {
            ParticleSystem[] pss = GetComponentsInChildren<ParticleSystem>(true);
            foreach (ParticleSystem ps in pss) {
                //ParticleSystem.MainModule m = ps.main;
                //m.loop = false;
                ps.Play();
            }
            Animator[] anis = GetComponentsInChildren<Animator>(true);
            foreach (Animator an in anis) {
                //AnimationClip[] clips = an.runtimeAnimatorController.animationClips;
                //foreach (var c in clips) {
                //    c.wrapMode = WrapMode.Once;
                //}
                int nameHash;
                if (_defaultClips.TryGetValue(an, out nameHash)) {
                    an.Play(nameHash, 0, 0);
                }
            }
            Animation[] animations = GetComponentsInChildren<Animation>(true);
            foreach (var an in animations) {
                //an.wrapMode = WrapMode.Once;
                an.Play();
            }
        }
#endif
    }
}