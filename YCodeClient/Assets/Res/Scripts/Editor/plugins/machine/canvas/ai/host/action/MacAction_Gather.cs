using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.host.action {
    [MacActon(false, "Host/Gather", new[] {typeof (AIMachineChart)})]
    public class MacAction_Gather : Mac_Action {
        public override string actionType { get { return AIActionType.HOST_GATHER; } }

        public override string renderContent { get { return "Gather"; } set { } }
    }
}