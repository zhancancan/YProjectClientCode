using edit.pure.inspector;
using edit.pure.machine.core;
using game.entity.game.machine;
using mono.ui.elements;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Force Re-Instance", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_ForceReinstall : Mac_Action {
        [Inspector(InspectorType.Prefab, 0), SettingPrefab(new[] {typeof (Panel)})]
        public string panel = string.Empty;

        public override string actionType { get { return LoginCenter.ACT_FORCE_REINSTALL; } }
        public override string renderContent { get { return "Force Re-Instance"; } set { } }
    }
}