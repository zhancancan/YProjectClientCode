using edit.pure.inspector;
using edit.pure.machine.core;
using pure.ai.aimachine;

namespace plugins.machine.canvas.ai.core.action {
    [MacActon(false, "Core/Animator Bool Setter", new[] {typeof (AIMachineChart)})]
    public class MacAction_AnimatorBoolSet : Mac_Action {
        [Inspector(InspectorType.Text, 0, false)]
        public string animKey = string.Empty;

        [Inspector(InspectorType.Boolean, 1, false)]
        public bool threshhold;

        public override string actionType { get { return AIActionType.ANIMATOR_BOOL_SET; } }

        public override string renderContent { get { return "Animator Bool Setter"; } set { } }
    }
}