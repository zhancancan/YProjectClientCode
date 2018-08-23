using edit.pure.inspector;
using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.host.action {
    [MacActon(false, "Host/Path Move", new[] {typeof (AIMachineChart)})]
    public class MacAction_PathMove : Mac_Action {
        [Inspector(InspectorType.Int, 0)]
        public int notifyCommand = -1;

        [Inspector(InspectorType.Float, 1), SettingFloat(0.1f, 10f)]
        public float jumpIdleTime = 0.1f;

        public override string actionType { get { return AIActionType.HOST_PATH_MOVE; } }

        public override string renderContent { get { return "Host Path Move"; } set { } }
    }
}