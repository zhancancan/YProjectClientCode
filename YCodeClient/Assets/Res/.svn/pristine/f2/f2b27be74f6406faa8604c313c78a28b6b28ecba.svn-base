using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.entity.core;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;
using UnityEngine;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Bullet/Seek", new[] {typeof (FireFxCanvas)})]
    public class BulletFxcell_Seek : BulletFxCellBase {
        [Inspector(InspectorType.Vector, 0), SettingLabelName("初速度偏移")]
        public Vector3 velocityOffset = Vector3.back;

        [Inspector(InspectorType.Vector, 1), SettingLabelName("最大初速度")]
        public Vector3 velocityMax = Vector3.zero;

        [Inspector(InspectorType.Vector, 2), SettingLabelName("最小初速度")]
        public Vector3 velocityMin = Vector3.zero;

        [Inspector(InspectorType.Float, 3), SettingFloat(0, 1000), SettingLabelName("速度")]
        public float speed = 10;

        [Inspector(InspectorType.Float, 4), SettingFloat(0, 1000), SettingLabelName("最大速度")]
        public float maxSpeed = 30;

        [Inspector(InspectorType.Float, 5), SettingFloat(0, 1000), SettingLabelName("最大加力")]
        public float maxForce = 40;

        [Inspector(InspectorType.Float, 6), SettingFloat(0, 10), SettingLabelName("到达阀值")]
        public float arriveThreshhold = 0.1f;

        [Inspector(InspectorType.Float, 7), SettingFloat(0, 10), SettingLabelName("质量")]
        public float mass = 1f;

        [Inspector(InspectorType.Float, 8), SettingFloat(0, 10), SettingLabelName("最大时长")]
        public float maxDuration = 1f;

        [Inspector(InspectorType.Enum, 9), SettingEnum(typeof (OrientMode)), SettingLabelName("旋转模式")]
        public OrientMode orientMode = OrientMode.None;

        [Inspector(InspectorType.Enum, 10), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("速度Scale影响")]
        public TimeScaleType speedScaleType = 0;

        public override string cellType { get { return FireFxType.BULLET_SEEK; } }

        public override string defaultLabel { get { return "Bullet Seek"; } }
    }
}