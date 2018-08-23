using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.emitter;
using UnityEngine;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Position Calc/Area", new[] {typeof (FireFxCanvas)})]
    public class BulletPosCell_Area : BulletFxCellBase {
        [Inspector(InspectorType.Enum, 0), SettingEnum(typeof (BullectCalcMode), true), SettingLabelName("计算模式")]
        public BullectCalcMode calcMode { get; set; }

        [Inspector(InspectorType.Vector, 1), SettingLabelName("区域最小坐标")]
        public Vector3 minArea { get; set; }

        [Inspector(InspectorType.Vector, 2), SettingLabelName("区域最大坐标")]
        public Vector3 maxArea { get; set; }

        [Inspector(InspectorType.Vector, 3), SettingLabelName("偏移")]
        public Vector3 offset { get; set; }

        public override string cellType { get { return FireFxType.POS_AREA; } }

        public override string defaultLabel { get { return "Calc Area"; } }
    }
}