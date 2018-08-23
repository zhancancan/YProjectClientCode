using plugins.lua;
using UnityEditor.Callbacks;

namespace consoler {
    internal static class ConsolerCommand {
        internal static void ReleaseLock() {
            prevInstanceId = -1;
        }

        private static int prevInstanceId = -1;

        [OnOpenAsset(-1)]
        internal static bool OnOpenAsset2(int instanceId, int a) {
            if (instanceId == prevInstanceId) {
                prevInstanceId = -1;
                return false;
            }
            prevInstanceId = instanceId;
            return ConsolerHelper.IsActiveFromConsole() ? OpenFromConsoler() : OpenFromProject(instanceId);
        }

        private static bool OpenFromConsoler() {
            string logText = ConsolerHelper.GetLog();
            if (string.IsNullOrEmpty(logText)) {
                return false;
            }
            if (GlobalLogHelper.Open(logText)) return true;
            if (LuaConsoler.OpenFromConsoler(logText)) return true;
            return false;
        }

        private static bool OpenFromProject(int instanceId) {
            return LuaConsoler.OpenFromProject(instanceId);
        }
    }
}