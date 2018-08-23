using System.Text.RegularExpressions;
using consoler;

namespace plugins.lua {
    public static partial class LuaConsoler {
        private const string parseErrorPatten = @"file.*=.* (\w+.+\.lua).*message.*:([0-9]+)";

        private static bool OpenParseError(string msg) {
            MatchCollection collection = Regex.Matches(msg, parseErrorPatten);
            for (int i = 0, len = collection.Count; i < len; i++) {
                string u = collection[i].Groups[1].Value;
                int l = int.Parse(collection[i].Groups[2].Value);
                if (string.IsNullOrEmpty(u)) continue;
                ConsolerCommand.ReleaseLock();
                OpenFileAtExternal(u, l);
                return true;
            }
            return false;
        }
    }
}