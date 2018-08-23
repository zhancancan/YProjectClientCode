﻿using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.core.action {
    [MacActon(false, "Core/FightMode Status", new[] {typeof (AIMachineChart)})]
    public class MacAction_FightModeStatus : Mac_Action {
        public override string actionType { get { return AIActionType.FIGHT_MODE; } }

        public override string renderContent { get { return "Host FightMode Status"; } set { } }
    }
}