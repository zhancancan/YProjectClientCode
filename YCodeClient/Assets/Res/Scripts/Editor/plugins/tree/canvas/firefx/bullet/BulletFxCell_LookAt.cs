using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.entity.core;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.emitter;
using pure.utils.timeScale;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Bullet/LookAt", new[] {typeof (FireFxCanvas)})]
    public class BulletFxCell_LookAt : BulletFxCellBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 10000), SettingLabelName("维持时长")]
        public float duration = 1;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType), true), SettingLabelName("时长Scale影响")]
        public TimeScaleType durationScaleType = 0;

        [Inspector(InspectorType.Enum, 2), SettingEnum(typeof (OrientMode)), SettingLabelName("旋转模式")]
        public OrientMode orientMode = OrientMode.None;

        [Inspector(InspectorType.Enum, 3), SettingEnum(typeof (BulletDirMode)), SettingLabelName("方向选择")]
        public BulletDirMode dirMode = BulletDirMode.Host_Target;

        [Inspector(InspectorType.Enum, 4), SettingEnum(typeof (BulletLookAtHost)), SettingLabelName("旋转质心")]
        public BulletLookAtHost hostType = BulletLookAtHost.Host;

        [Inspector(InspectorType.Boolean, 5), SettingLabelName("更新位置")]
        public bool updatePosition;
        [Inspector(InspectorType.Boolean, 6), SettingLabelName("使用父节点生命周期")]
        public bool useParentLife;

        public override string cellType { get { return FireFxType.BULLET_LOOKAT; } }

        public override string defaultLabel { get { return "Bullet LookAt"; } }
    }
}