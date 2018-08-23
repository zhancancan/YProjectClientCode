using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.emitter;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Position Calc/Divid Space", new[] {typeof (FireFxCanvas)})]
    public class BulletPosCell_DividSpace : BulletFxCellBase {
        [Inspector(InspectorType.Enum, 0), SettingEnum(typeof (BullectCalcMode), true), SettingLabelName("计算外置模式")]
        public BullectCalcMode calcMode = BullectCalcMode.Calc_Start;

        public override string cellType { get { return FireFxType.POS_DIVID_SPACE; } }

        public override string defaultLabel { get { return "Calc DIvid Space"; } }
    }
}