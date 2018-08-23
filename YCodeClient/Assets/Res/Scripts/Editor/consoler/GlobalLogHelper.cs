using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace consoler {
    internal static class GlobalLogHelper {
        private static string reg = @"\(at(.*):(.*)\)";

        internal static bool Open(string log) {
            if (!log.StartsWith("[GlobalLog]")) return false;
            if (log.Contains("lua parse error")) return false;
            MatchCollection ms = Regex.Matches(log, reg, RegexOptions.None);
            for (int i = 0; i < ms.Count; i++) {
                Match m = ms[i];
                if (m.Groups.Count != 3) continue;
                string src = m.Groups[1].Value.Trim();
                if (string.IsNullOrEmpty(src)) continue;
                int line;
                if (!int.TryParse(m.Groups[2].Value, out line)) continue;
                Object t = AssetDatabase.LoadAssetAtPath<Object>(src);
                if (t == null) continue;
                AssetDatabase.OpenAsset(t.GetInstanceID(), line);
                return true;
            }
            return false;
        }
    }
}