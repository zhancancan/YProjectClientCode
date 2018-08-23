using pure.lua;
using pure.ui.element;
using UnityEngine;

namespace mono.ui.elements {
    [LuaExport, AddComponentMenu("UI/Bar", 33), RequireComponent(typeof (RectTransform)), ExecuteInEditMode]
    public sealed class Bar : PBar_Dll {
    }
}