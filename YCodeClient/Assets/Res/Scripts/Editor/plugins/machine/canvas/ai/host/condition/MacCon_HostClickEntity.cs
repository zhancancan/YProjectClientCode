﻿using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.host.condition {
    [MacCondition(false, "Host/On Entity Click", new[] {typeof (AIMachineChart)})]
    public class MacCon_HostClickEntity : Mac_Condition {
        protected override void Draw() {
        }

        public override string renderContent { get { return "On Entity Click"; } set { } }

        public override string conditionType { get { return AIConditionType.CLICK_ENTITY; } }
    }
}