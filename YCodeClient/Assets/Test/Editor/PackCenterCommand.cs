using edit.pure.pack;
using UnityEditor;

namespace Test.Editor.hosAction
{
    internal static class PackCenterCommand
    {
        [InitializeOnLoadMethod]
        internal static void Start()
        {
            PackCenter.allowPack = true;
        }
    }
}