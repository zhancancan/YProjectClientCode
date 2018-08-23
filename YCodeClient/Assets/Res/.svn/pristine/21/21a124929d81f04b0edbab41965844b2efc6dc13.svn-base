using edit.pure.inspector;
using edit.pure.machine.core;
using pure.ai.aimachine;
using pure.entity.renderer;

namespace plugins.machine.canvas.ai.core.action {
    [MacActon(false, "AI Init/Renderer", new[] {typeof (AIMachineChart)})]
    public class MacAction_InitRenderer : Mac_Action {
        [Inspector(InspectorType.Text, 0)]
        public string property = string.Empty;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (RendererType))]
        public RendererType type = RendererType.Simple;

        public override string actionType { get { return AIActionType.INIT_RENDERER; } }
        public override string renderContent { get { return "Init Renderer"; } set { } }
    }
}