using edit.pure.inspector;
using edit.pure.machine.core;
using game.entity.game.machine;
using mono.ui.elements;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Preloader/Show Pre-loader", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_PreloaderShow : Mac_Action {
        [Inspector(InspectorType.Prefab, 0), SettingPrefab(new[] {typeof (Panel)})]
        public string panel { get; set; }

        public override string actionType { get { return LoginCenter.ACT_SHOW_PRELOAD; } }
        public override string renderContent { get { return "Show Pre-loader"; } set { } }
    }
}