using edit.pure.inspector;
using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Init/Style", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_InitStyle : Mac_Action {
        [Inspector(InspectorType.Text, 0)]
        public string file = string.Empty;

        public override string actionType { get { return LoginCenter.ACT_INIT_STYLE; } }
        public override string renderContent { get { return "Init Style"; } set { } }
    }
}