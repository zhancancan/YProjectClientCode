using edit.pure.inspector;
using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.actions {
    [MacActon(false, "Bundle/Update Files", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_LoadBundle : Mac_Action {
        [Inspector(InspectorType.Asset, 0), SettingAsset(".lua")]
        public string luaFile = string.Empty;

        public override string actionType { get { return LoginCenter.ACT_LOAD_BUNDLE_FILES; } }
        public override string renderContent { get { return "Update Bundle Files"; } set { } }
    }

    [MacActon(false, "Bundle/Copy To Persist", new[] {typeof (LoginMachineChart)})]
    internal class LogAct_CopyBundle : Mac_Action {
        [Inspector(InspectorType.Asset, 0), SettingAsset(".lua")]
        public string luaFile = string.Empty;

        public override string actionType { get { return LoginCenter.ACT_COPY_BUNDLE_PERSIT; } }
        public override string renderContent { get { return "Copy Bundle To Persist"; } set { } }
    }
}