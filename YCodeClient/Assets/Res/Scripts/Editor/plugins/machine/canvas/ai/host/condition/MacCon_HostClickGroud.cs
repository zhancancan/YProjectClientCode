﻿using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.host.condition {
    [MacCondition(false, "Host/On Ground Click", new[] {typeof (AIMachineChart)})]
    public class MacCon_HostClickGroud : Mac_Condition {
        protected override void Draw() {
        }

        public override string renderContent { get { return "On Ground Click"; } set { } }

        public override string conditionType { get { return AIConditionType.CLICK_GROUND; } }
    }
}