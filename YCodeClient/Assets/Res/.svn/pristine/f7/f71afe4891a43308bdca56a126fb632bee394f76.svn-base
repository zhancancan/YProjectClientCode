using edit.pure.inspector;
using edit.pure.treespace;
using edit.pure.treespace.core;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.emitter;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Position Calc/Ring", new[] {typeof (FireFxCanvas)})]
    public class BulletPosCell_Ring : BulletFxCellBase {
        [Inspector(InspectorType.Enum, 0), SettingEnum(typeof (BullectCalcMode),true), SettingLabelName("起始坐标计算模式")]
        public BullectCalcMode calcMode = BullectCalcMode.Calc_Start;


        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (BulletRingRadiusMode)), SettingLabelName("半径计算模式")]
        public BulletRingRadiusMode radiusMode = BulletRingRadiusMode.Random;

        [Inspector(InspectorType.Enum, 2), SettingEnum(typeof (BulletRingAngleMode)), SettingLabelName("角度计算模式")]
        public BulletRingAngleMode angleMode = BulletRingAngleMode.Random;

        [Inspector(InspectorType.Int, 3), SettingInt(0, 500), SettingLabelName("最大半径")]
        public int maxRadius = 8;

        [Inspector(InspectorType.Int, 4), SettingInt(0, 500), SettingLabelName("最小半径")]
        public int minRadius = 0;

        [Inspector(InspectorType.Int, 5), SettingInt(1, 10), SettingLabelName("圈数")]
        public int numRings = 1;

        [Inspector(InspectorType.Int, 6), SettingInt(1, 50), SettingLabelName("每圈的bullet数量")]
        public int numBulletPerRing = 1;

        public override string cellType { get { return FireFxType.POS_RING; } }

        public override string defaultLabel { get { return "Calc Ring"; } }
    }
}