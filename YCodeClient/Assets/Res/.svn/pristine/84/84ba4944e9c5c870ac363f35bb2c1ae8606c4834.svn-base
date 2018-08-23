using UnityEngine;

namespace mono.fbx {
    public class CharacterMouseRotater : MonoBehaviour {
        private float _startx;
        private bool _press;
        private float _startrotate;

        internal void Update() {
            if (Input.GetMouseButtonDown(0)) {
                _startx = Input.mousePosition.x;
                _press = true;
                _startrotate = transform.localEulerAngles.y;
            }
            if (Input.GetMouseButtonUp(0)) {
                _press = false;
            }

            if (_press) {
                float r = Input.mousePosition.x - _startx;
                r %= 360 + 180;
                transform.localEulerAngles = new Vector3(0, r + _startrotate, 0);
            }
        }
    }
}