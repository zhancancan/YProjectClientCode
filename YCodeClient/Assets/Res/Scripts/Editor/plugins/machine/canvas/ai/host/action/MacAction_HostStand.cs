﻿using edit.pure.inspector;
using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.host.action {
    [MacActon(false, "Host/Stand", new[] {typeof (AIMachineChart)})]
    public class MacAction_HostStand : Mac_Action {
        [Inspector(InspectorType.Text, 0)]
        public string notifyCommand = string.Empty;

        [Inspector(InspectorType.Float, 1)]
        public float updateInterval = 1;

        [Inspector(InspectorType.Float, 2), SettingFloat(0, 1)]
        public float standModePercent = 0.5f;

        public override string actionType { get { return AIActionType.HOST_STAND; } }

        public override string renderContent { get { return "Host Stand"; } set { } }
    }
}