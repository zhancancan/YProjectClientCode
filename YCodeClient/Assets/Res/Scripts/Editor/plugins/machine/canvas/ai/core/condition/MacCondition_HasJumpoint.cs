﻿using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.core.condition {
    [MacCondition(false, "Nav/Has Jump Point", new[] {typeof (AIMachineChart)})]
    public class MacCondition_HasJumpoint : Mac_Condition {
        public override string conditionType { get { return AIConditionType.HAS_JUMP_POINT; } }

        public override string renderContent { get { return "Has Jump Point"; } set { } }

        protected override void Draw() {
        }
    }
}