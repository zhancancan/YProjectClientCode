﻿using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.host.action {
    [MacActon(false, "Host/Dead", new[] {typeof (AIMachineChart)})]
    public class MacAction_HostDead : Mac_Action {
        public override string actionType { get { return AIActionType.HOST_DEAD; } }

        public override string renderContent { get { return "Dead"; } set { } }
    }
}