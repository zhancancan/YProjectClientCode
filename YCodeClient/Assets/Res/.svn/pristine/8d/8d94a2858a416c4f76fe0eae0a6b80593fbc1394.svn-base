using pure.ticker;
using UnityEngine;

namespace mono.fbx {
    public class TickerCenterTrigger : MonoBehaviour {
#if UNITY_EDITOR
        internal void Update() {
            SystemTime.Update();
            TickerCenter.GetInstance().OnUpdate();
        }
#endif
    }
}