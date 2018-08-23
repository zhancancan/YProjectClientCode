using pure.scene.mono;
using pure.utils.dlltools;
using UnityEngine;

namespace mono.scene {
    [ExecuteInEditMode, MonoDll(MonoType.LIGHT_MAP_BAKE)]
    public sealed class LightmapBake : LightmapBake_Dll {
    }
}