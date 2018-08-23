using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Init/Data", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_InitData : Mac_Action {
        public override string actionType { get { return LoginCenter.ACT_INIT_DATA; } }
        public override string renderContent { get { return "Init Data"; } set { } }
    }
}