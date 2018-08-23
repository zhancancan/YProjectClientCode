using edit.pure.inspector;
using edit.pure.machine.core;
using pure.ai.aimachine;
using pure.entity.renderer;

namespace plugins.machine.canvas.ai.core.action {
    [MacActon(false, "AI Init/Child Renderer", new[] {typeof (AIMachineChart)})]
    public class MacAction_InitChildRenderer : Mac_Action {
        [Inspector(InspectorType.Text, 0)]
        public string property = string.Empty;

        [Inspector(InspectorType.Text, 1)]
        public string requireBone = string.Empty;

        [Inspector(InspectorType.Enum, 2), SettingEnum(typeof (BoneBundleMode))]
        public BoneBundleMode boneMode = BoneBundleMode.None;

        [Inspector(InspectorType.MultiString, 3)]
        public string[] support = new string[0];

        public override string actionType { get { return AIActionType.INIT_CHILDRENDERER; } }
        public override string renderContent { get { return "Init Child Renderer"; } set { } }
    }
}