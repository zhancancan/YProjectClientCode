using edit.pure.inspector;
using edit.pure.machine.core;
using pure.stateMachine.share;

namespace plugins.machine.canvas.share.actions {
    [MacActon(false, "Share/Lua Action")]
    internal class MacAct_Lua : Mac_Action {
        [Inspector(InspectorType.Asset, 0), SettingAsset("lua")]
        public string lua = string.Empty;

        protected override void Draw() {
        }

        protected override bool canReplaceByLua { get { return false; } }

        public override string actionType { get { return MachineType.ACTION_LUA; } }
        public override string renderContent { get { return "lua action"; } set { } }
    }
}