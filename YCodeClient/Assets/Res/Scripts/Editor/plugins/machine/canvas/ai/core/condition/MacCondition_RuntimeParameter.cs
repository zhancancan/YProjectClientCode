using edit.pure.inspector;
using edit.pure.machine.core;
using pure.ai.aimachine;
using pure.utils.enumeration;

namespace plugins.machine.canvas.ai.core.condition {
    [MacCondition(false, "Core/Runtime Parameter", new[] {typeof (AIMachineChart)})]
    public class MacCondition_RuntimeParameter : Mac_Condition {
        [Inspector(InspectorType.Int, 0)]
        public int paramType = 0;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (ValueCompareType))]
        public ValueCompareType compareMode = ValueCompareType.Less;

        [Inspector(InspectorType.Enum, 2), SettingEnum(typeof (ValueMethod))]
        public ValueMethod valueType = ValueMethod.Fix;

        [Inspector(InspectorType.Int, 3)]
        public int denominatorType = 0;

        [Inspector(InspectorType.Float, 4)]
        public float targetValue;

        protected override void Draw() {
        }

        public override string conditionType { get { return AIConditionType.RUNTIME_PARAMETER; } }

        public override string renderContent { get { return "Runtime Parameter"; } }
    }
}