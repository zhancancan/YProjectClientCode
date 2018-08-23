using edit.pure.inspector;
using edit.pure.resource;
using edit.pure.treespace.core;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;
using UnityEngine;

namespace plugins.tree.canvas.firefx.order {
    [TreeCell(false, "Group/Sequence", new[] {typeof (FireFxCanvas)})]
    public class FireFxCell_Sequence : FireFxCellBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 10), SettingLabelName("起始时间")]
        public float time = 0f;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("起始Scale影响")]
        public TimeScaleType timeScaleType = 0;

        public override string cellType { get { return FireFxType.SEQUENCE; } }

        public override string defaultLabel { get { return "Sequence"; } }

        public override Texture2D iconStyle {
            get {
                float g = 0.2f;
                return PResourceManager.GetTintedTexture("BehaviorIcon/sequence.png", new Color(g, g, g, 1));
            }
        }
    }
}