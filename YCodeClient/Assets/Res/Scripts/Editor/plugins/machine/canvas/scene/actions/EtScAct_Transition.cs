using edit.pure.inspector;
using edit.pure.machine.core;
using game.pure.entity.scene.machine;

namespace plugins.machine.canvas.scene.actions {
    [MacActon(false, "Transition/Show", new[] {typeof (SceneMachineChart)})]
    internal class EtScAct_ShowTransition : Mac_Action {
        [Inspector(InspectorType.Asset, 0), SettingAsset("unity")]
        public string url = string.Empty;

        [Inspector(InspectorType.Asset, 1), SettingAsset("lua")]
        public string luaFile = string.Empty;

        public override string actionType { get { return SceneMachineType.SHOW_TRANSITION; } }
        public override string renderContent { get { return "Show Transition"; } set { } }
    }

    [MacActon(false, "Transition/Hide", new[] {typeof (SceneMachineChart)})]
    internal class EtScAct_HideTransition : Mac_Action {
        public override string actionType { get { return SceneMachineType.HIDE_TRANSITION; } }
        public override string renderContent { get { return "Hide Transition"; } set { } }
    }
}