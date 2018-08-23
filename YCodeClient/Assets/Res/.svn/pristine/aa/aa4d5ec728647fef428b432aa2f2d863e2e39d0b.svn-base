using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Bullet/Follow", new[] {typeof (FireFxCanvas)})]
    public class BulletFxcell_Follow : BulletFxCellBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 10), SettingLabelName("维持时长")]
        public float duration = 0;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType), true), SettingLabelName("时长Scale影响")]
        public TimeScaleType durationScaleType = 0;

        public override string cellType { get { return FireFxType.BULLET_FOLLOW; } }

        public override string defaultLabel { get { return "Bullet Follow"; } }
    }
}