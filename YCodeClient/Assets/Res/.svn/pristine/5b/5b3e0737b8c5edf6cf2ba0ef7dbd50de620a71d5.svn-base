using edit.pure.inspector;
using edit.pure.machine.core;
using pure.stateMachine.share;

namespace plugins.machine.canvas.share.actions {
    [MacActon(false, "Share/Set Inited")]
    internal class MacAct_SetInited : Mac_Action {
        [Inspector(InspectorType.Boolean, 0)]
        public bool value;

        protected override void Draw() {
        }

        public override string actionType { get { return MachineType.ACTION_SET_INITED; } }

        public override string renderContent { get { return "Set Cpx Inited"; } set { } }
    }
}