using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.entity.core;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;
using UnityEngine;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Bullet/Parabola", new[] {typeof (FireFxCanvas)})]
    public class BulletFxcell_Parabola : BulletFxCellBase {
        [Inspector(InspectorType.Vector, 0), SettingLabelName("起始速度")]
        public Vector3 velocity = new Vector3(0, 0, 1);

        [Inspector(InspectorType.Vector, 1), SettingFloat(0, 10000), SettingLabelName("加速度")]
        public Vector3 gravity = new Vector3(0, 0, 1);

        [Inspector(InspectorType.Enum, 2), SettingEnum(typeof (OrientMode)), SettingLabelName("旋转模式")]
        public OrientMode orientMode;

        [Inspector(InspectorType.Enum, 3), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("速度Scale影响")]
        public TimeScaleType speedScaleType = TimeScaleType.TimeScale1;

        public override string cellType { get { return FireFxType.BULLET_PARABOLA; } }

        public override string defaultLabel { get { return "Bullet Parabola"; } }
    }
}