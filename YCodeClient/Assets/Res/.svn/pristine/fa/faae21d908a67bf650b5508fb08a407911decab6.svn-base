using edit.pure.inspector;
using edit.pure.resource;
using edit.pure.treespace.core;
using pure.treeComp.firefx.core;
using UnityEngine;

namespace plugins.tree.canvas.firefx.order {
    [TreeCell(false, "Group/Phase", new[] {typeof (FireFxCanvas)})]
    public class FireFxCell_Phase : FireFxCellBase {
        [Inspector(InspectorType.Int, 0), SettingInt(0, 100), SettingLabelName("阶段")]
        public int phase = 0;

        public override string cellType { get { return FireFxType.PHASE; } }

        public override string defaultLabel { get { return "Phase"; } }

        public override Texture2D iconStyle {
            get {
                float g = 0.2f;
                return PResourceManager.GetTintedTexture("BehaviorIcon/parallel.png", new Color(g, g, g, 1));
            }
        }
    }
}