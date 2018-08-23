using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.entity.core;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.emitter;
using pure.utils.timeScale;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Bullet/Play", new[] {typeof (FireFxCanvas)})]
    public class BulletFxcell_Play : BulletFxCellBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 30), SettingLabelName("维持时长")]
        public float duration = 0;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("时长Scale影响")]
        public TimeScaleType durationScaleType = 0;

        [Inspector(InspectorType.Enum, 2), SettingEnum(typeof(TimeScaleType), true), SettingLabelName("动画Scale影响")]
        public TimeScaleType animatorScaleType = 0; 

        [Inspector(InspectorType.Enum, 3), SettingEnum(typeof (OrientMode)), SettingLabelName("旋转模式")]
        public OrientMode orientMode = OrientMode.None;

        [Inspector(InspectorType.Enum, 4), SettingEnum(typeof (BulletDirMode)), SettingLabelName("方向选择")]
        public BulletDirMode dirMode = BulletDirMode.Host_Target;

        public override string cellType { get { return FireFxType.BULLET_PLAY; } }

        public override string defaultLabel { get { return "Bullet Play"; } }
    }
}