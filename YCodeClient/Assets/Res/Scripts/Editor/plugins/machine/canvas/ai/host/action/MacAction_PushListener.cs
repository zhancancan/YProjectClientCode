using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.host.action {
    [MacActon(false, "Host/Push Listener", new[] {typeof (AIMachineChart)})]
    public class MacAction_PushListener : Mac_Action {
        public override string actionType { get { return AIActionType.HOST_PUSH_LISTENER; } }

        public override string renderContent { get { return "Host Push Listener"; } set { } }
    }
}