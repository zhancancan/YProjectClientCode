using pure.lua;
using pure.ui.element;
using UnityEngine;

namespace mono.ui.elements {
    [LuaExport, AddComponentMenu("UI/TouchableRect", 34), RequireComponent(typeof (RectTransform))]
    public sealed class TouchableRect : PTouchableRect_Dll {
    }
}