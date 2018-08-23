using edit.pure.panel;
using UnityEditor;

namespace plugins.panel {
    public class PanelMenuCommand {
        [MenuItem("Assets/Panel/Collect Data", false, 2001)]
        public static void Open() {
            PanelCenter.PickFromSelected();
        }
    }
}