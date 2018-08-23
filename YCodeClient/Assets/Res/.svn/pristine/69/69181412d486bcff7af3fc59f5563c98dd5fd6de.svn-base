using System.Text.RegularExpressions;
using consoler;
using edit.pure.tools.assets;
using UnityEditor;
using UnityEngine;

namespace plugins.lua {
    public static partial class LuaConsoler {
        private const string errorPattern = @"(\w+):([0-9]+): in function .*[^\r\n]";

        private class ScriptLog {
            internal string file;
            internal int lineCode;
        }

        private static bool OpenException(string msg) {
            MatchCollection collection = Regex.Matches(msg, errorPattern);
            Event e = Event.current;
            if (collection.Count > 0) {
                GenericMenu menu = new GenericMenu();
                for (int i = 0; i < collection.Count; i++) {
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

        private static void OpenScriptLog(object o) {
            ScriptLog s = o as ScriptLog;
            if (s == null) return;
            string u;
            AssetCollection ac = AssetCollection.GetCollection(".lua");
            if (ac.GetFile(s.file, out u)) {
                ConsolerCommand.ReleaseLock();
                OpenFileAtExternal(u, s.lineCode);
            }
            ac = AssetCollection.GetCollection(".lua", "Test/LuaDll");
            if (ac.GetFile(s.file, out u)) {
                ConsolerCommand.ReleaseLock();
                OpenFileAtExternal(u, s.lineCode);
            }
        }
    }
}