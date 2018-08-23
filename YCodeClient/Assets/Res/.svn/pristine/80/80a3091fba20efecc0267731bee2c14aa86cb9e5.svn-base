using System.Text.RegularExpressions;
using consoler;
using UnityEditor;
using UnityEngine;

namespace plugins.lua {
    public static partial class LuaConsoler {
        private const string logPattern = @"\[(\w.+):([0-9]+)\]";

        private static bool OpenLog(string msg) {
            MatchCollection collection = Regex.Matches(msg, logPattern);
            Event e = Event.current;
            if (collection.Count > 0) {
                GenericMenu menu = new GenericMenu();
                for (int i =0; i <collection.Count; i++) {
                    string f = collection[i].Groups[1].Value;
                    int l = int.Parse(collection[i].Groups[2].Value);
                    menu.AddItem(new GUIContent(string.Format("open {0} : {1}", f, l)), false, OpenScriptLog,
                        new ScriptLog {file = f, lineCode = l});
                }
                menu.DropDown(new Rect {position = e.mousePosition, size = new Vector2(5, 5)});
                ConsolerCommand.ReleaseLock();
                return true;
            }
            return false;
        }
    }
}