using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Connect Socket", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_ReadLocalAssetManager : Mac_Action {
        public override string actionType { get { return LoginCenter.ACT_READ_LOCAL_ASSET_MANAGER; } }
        public override string renderContent { get { return "Connect Socket"; } set { } }
    }
}