using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.entity.core;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.emitter;
using pure.utils.timeScale;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Bullet/Fly", new[] {typeof (FireFxCanvas)})]
    public class BulletFxCell_Fly : BulletFxCellBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 10000), SettingLabelName("速度")]
        public float speed = 1;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType), true), SettingLabelName("速度Scale影响")]
        public TimeScaleType speedScaleType = 0;

        [Inspector(InspectorType.Enum, 2), SettingEnum(typeof (OrientMode)), SettingLabelName("旋转模式")]
        public OrientMode orientMode = OrientMode.None;

        [Inspector(InspectorType.Enum, 3), SettingEnum(typeof (BulletFlyDirection)), SettingLabelName("飞行方向")]
        public BulletFlyDirection direction = BulletFlyDirection.Forward;

        [Inspector(InspectorType.Enum, 4), SettingEnum(typeof (BulletFlyDirection)), SettingLabelName("子弹方向")]
        public BulletFlyDirection orientDir = BulletFlyDirection.Forward;

        public override string cellType { get { return FireFxType.BULLET_FLY; } }

        public override string defaultLabel { get { return "Bullet Fly"; } }
    }
}