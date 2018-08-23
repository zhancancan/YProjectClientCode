﻿using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.core.action {
    [MacActon(false, "Core/Jump", new[] {typeof (AIMachineChart)})]
    public class MacAction_Jump : Mac_Action {
        public override string actionType { get { return AIActionType.JUMP; } }

        public override string renderContent { get { return "Jump"; } set { } } 
        
    }
}