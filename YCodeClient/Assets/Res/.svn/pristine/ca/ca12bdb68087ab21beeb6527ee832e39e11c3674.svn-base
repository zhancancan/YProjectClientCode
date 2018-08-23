using edit.pure.inspector;
using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Init/Lang", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_InitLang : Mac_Action {
        [Inspector(InspectorType.Asset, 0), SettingAsset("lua")]
        public string lua = string.Empty;

        public override string actionType { get { return LoginCenter.ACT_INITLANG; } }
        public override string renderContent { get { return "Init Lang"; } set { } }
    }
}