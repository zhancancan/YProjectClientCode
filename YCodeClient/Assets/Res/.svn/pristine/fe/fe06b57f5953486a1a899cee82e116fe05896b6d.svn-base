using edit.pure.inspector;
using edit.pure.machine.core;
using game.entity.game.machine;
using mono.ui.elements;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Enter Logo/Show", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_ShowEnterLogo : Mac_Action {
        [Inspector(InspectorType.Prefab, 0), SettingPrefab(new[] {typeof (Panel)})]
        public string panel;

        [Inspector(InspectorType.Float, 1)]
        public float duration = 1;

        public override string actionType { get { return LoginCenter.ACT_SHOW_ENTER_LOGO; } }
        public override string renderContent { get { return "Show Enter Logo"; } set { } }
    }

    [MacActon(false, "Enter Logo/Hide", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_HideEnterLogo : Mac_Action {
        public override string actionType { get { return LoginCenter.ACT_HIDE_ENTER_LOGO; } }
        public override string renderContent { get { return "Hide Enter Logo"; } set { } }
    }
}