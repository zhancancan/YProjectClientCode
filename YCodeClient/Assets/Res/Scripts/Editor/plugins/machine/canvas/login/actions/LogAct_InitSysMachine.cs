using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Init/Machine", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_InitSysMachine : Mac_Action {
        public override string actionType { get { return LoginCenter.ACT_INIT_MACHINE; } }
        public override string renderContent { get { return "Init Machine"; } set { } }
    }
}