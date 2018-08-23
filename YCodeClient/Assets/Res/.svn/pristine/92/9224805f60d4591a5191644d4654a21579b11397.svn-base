using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;
using pure.utils.tween;

namespace plugins.tree.canvas.firefx.act {
    [TreeCell(false, "Action/Suddent Move", new[] {typeof (FireFxCanvas)})]
    public class FireFxCell_Move : FireFxCellActBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 10), SettingLabelName("起始时间")]
        public float time = 0f;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("起始Scale影响")]
        public TimeScaleType timeScaleType = 0;

        [Inspector(InspectorType.Float, 2), SettingFloat(0, 100), SettingLabelName("时长")]
        public float duration = 0f;

        [Inspector(InspectorType.Enum, 3), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("时长Scale影响")]
        public TimeScaleType durationScaleType = 0;

        [Inspector(InspectorType.Enum, 4), SettingEnum(typeof (EaseType)), SettingLabelName("缓动模式")]
        public EaseType ease = EaseType.Linear;

        public override string cellType { get { return FireFxType.ACT_MOVE; } }

        public override string defaultLabel { get { return "Suddent Move"; } }
    }
}