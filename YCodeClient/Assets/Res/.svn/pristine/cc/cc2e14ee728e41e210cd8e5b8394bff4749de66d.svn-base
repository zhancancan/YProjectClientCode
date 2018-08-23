using edit.pure.inspector;
using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Load Version.ver", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_LoadVer : Mac_Action {
        [Inspector(InspectorType.Text, 0), SettingText(true, -1, -1, 30, 80)]
        public string url = string.Empty;

        public override string actionType { get { return LoginCenter.ACT_LOAD_VER; } }
        public override string renderContent { get { return "Load verion.ver"; } set { } }
    }
}