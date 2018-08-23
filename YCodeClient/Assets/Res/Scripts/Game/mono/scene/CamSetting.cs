using pure.scene.mono;
using pure.utils.dlltools;
using UnityEngine;

namespace mono.scene {
    [MonoDll(MonoType.CAMERA_SETTING), RequireComponent(typeof (Camera))]
    public class CamSetting : CamSetting_Dll {
    }
}