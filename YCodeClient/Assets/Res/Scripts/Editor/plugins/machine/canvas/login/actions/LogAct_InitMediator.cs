﻿using edit.pure.inspector;
using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Init/Mediator", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_InitPanel : Mac_Action {
        [Inspector(InspectorType.Asset, 0), SettingAsset("bytes")]
        public string file = string.Empty;

        public override string actionType { get { return LoginCenter.ACT_INIT_MEDIATOR; } }
        public override string renderContent { get { return "Init Mediator"; } set { } }
    }
}