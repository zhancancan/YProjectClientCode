using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Http/Get config", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_HttpConfig : Mac_Action {
        public override string actionType { get { return LoginCenter.ACT_GET_CONFIG; } }
        public override string renderContent { get { return "Http get config"; } set { } }
    }
}