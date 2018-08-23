using edit.pure.inspector;
using edit.pure.treespace;
using edit.pure.treespace.core;
using plugins.tree.canvas.firefx.emitter;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.emitter;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Position Calc/Line", new[] {typeof (FireFxCanvas)})]
    public class BulletPosCell_Line : BulletFxCellBase {
        [Inspector(InspectorType.Enum, 0), SettingEnum(typeof (BulletSpaceMode)),SettingLabelName("间隔模式")]
        public BulletSpaceMode spaceMode { get; set; }

        public override string cellType {
            get { return FireFxType.POS_LINE; }
        }

        public override string defaultLabel {
            get { return "Calc Line"; }
        }

        public override bool CanInsertTo(TreeCell obj) {
            return base.CanInsertTo(obj) && obj is FireFxEmitCell_Line;
        }
    }
}