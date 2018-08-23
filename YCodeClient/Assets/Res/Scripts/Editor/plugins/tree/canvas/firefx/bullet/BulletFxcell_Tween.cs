using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.entity.core;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;
using pure.utils.tween;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Bullet/Tween", new[] {typeof (FireFxCanvas)})]
    public class BulletFxcell_Tween : BulletFxCellBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 10000), SettingLabelName("时长")]
        public float duration = 1;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("时长受Scale影响")]
        public TimeScaleType durationScaleType = 0;

        [Inspector(InspectorType.Enum, 2), SettingEnum(typeof (OrientMode)), SettingLabelName("旋转模式")]
        public OrientMode orientMode = OrientMode.None;

        [Inspector(InspectorType.Enum, 3), SettingEnum(typeof (EaseType)), SettingLabelName("缓动模式")]
        public EaseType easeType = EaseType.Linear;

        [Inspector(InspectorType.Enum, 4), SettingEnum(typeof (TweenLoopType)), SettingLabelName("循环模式")]
        public TweenLoopType loopType;

        [Inspector(InspectorType.Int, 5), SettingInt(0, 100), SettingLabelName("循环次数")]
        public int loops = 1;

        public override string cellType { get { return FireFxType.BULLET_TWEEN; } }

        public override string defaultLabel { get { return "Bullet Tween"; } }
    }
}