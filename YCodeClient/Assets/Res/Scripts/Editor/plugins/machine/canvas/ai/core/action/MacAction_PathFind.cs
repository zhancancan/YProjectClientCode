﻿using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.core.action {
    [MacActon(false, "Nav/Path Find", new[] {typeof (AIMachineChart)})]
    public class MacAction_PathFind : Mac_Action {
        public override string actionType { get { return AIActionType.PATH_FIND; } }

        public override string renderContent { get { return "Path Find"; } set { } }
        protected override bool canReplaceByLua { get { return false; } }
    }
}