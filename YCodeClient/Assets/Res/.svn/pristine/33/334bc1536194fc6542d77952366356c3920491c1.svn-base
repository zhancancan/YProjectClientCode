using edit.pure.inspector;
using edit.pure.machine.core;
using mono.ui.elements;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.core.action {
    [MacActon(false, "AI Init/Label", new[] {typeof (AIMachineChart)})]
    public class MacAction_InitLabel : Mac_Action {
        [Inspector(InspectorType.Prefab, 0), SettingPrefab(new[] {typeof (Panel)})]
        public string prefab = string.Empty;

        [Inspector(InspectorType.Asset, 1), SettingAsset("lua")]
        public string lua = string.Empty;

        public override string actionType { get { return AIActionType.INIT_LABEL; } }
        public override string renderContent { get { return "Init Label"; } set { } }
    }
}