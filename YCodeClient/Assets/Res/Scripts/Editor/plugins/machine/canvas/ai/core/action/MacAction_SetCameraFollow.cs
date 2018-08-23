using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.core.action {
    [MacActon(false, "AI Init/Camera Follow", new[] {typeof (AIMachineChart)})]
    public class MacAction_SetCameraFollow : Mac_Action {
        public override string actionType { get { return AIActionType.INIT_CAMERA_FOLLOW; } }
        public override string renderContent { get { return "Init Camera follow"; } set { } }
    }
}