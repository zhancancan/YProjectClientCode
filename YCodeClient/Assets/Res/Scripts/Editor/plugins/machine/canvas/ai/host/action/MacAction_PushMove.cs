using edit.pure.inspector;
using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.host.action {
    [MacActon(false, "Host/Push Move", new[] {typeof (AIMachineChart)})]
    public class MacAction_PushMove : Mac_Action {
        [Inspector(InspectorType.Asset, 0), SettingAsset("lua")]
        public string notifier = string.Empty;

        [Inspector(InspectorType.Int, 1)]
        public int notifyInterval = 100;

        [Inspector(InspectorType.Int, 2)]
        public int forcast = 600;

        public override string actionType { get { return AIActionType.HOST_PUSH_MOVE; } }

        public override string renderContent { get { return "Host Push Move"; } set { } }
        protected override bool canReplaceByLua { get { return false; } }
    }
}