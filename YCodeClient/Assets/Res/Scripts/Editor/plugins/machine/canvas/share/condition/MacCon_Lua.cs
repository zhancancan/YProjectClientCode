using edit.pure.inspector;
using edit.pure.machine.core;
using pure.stateMachine.share;

namespace plugins.machine.canvas.share.condition {
    [MacCondition(false, "Share/Lua")]
    public class MacCon_Lua : Mac_Condition {
        [Inspector(InspectorType.Asset, 0), SettingAsset("lua")]
        public string lua = string.Empty;

        protected override void Draw() {
        }

        protected override bool canReplaceByLua { get { return false; } }

        public override string renderContent { get { return "Lua Condition"; } set { } }
        public override string conditionType { get { return MachineType.CON_LUA; } }
    }
}