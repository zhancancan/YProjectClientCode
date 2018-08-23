using edit.pure.inspector;
using edit.pure.resource;
using edit.pure.treespace.core;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;
using UnityEngine;

namespace plugins.tree.canvas.firefx.act {
    [TreeCell(false, "Time Scale/Animator", new[] {typeof (FireFxCanvas)})]
    public class FireFxCell_TimeScale : FireFxCellBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 100), SettingLabelName("起始时间")]
        public float time;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType), true), SettingLabelName("起始Scale影响")]
        public TimeScaleType timeScaleType = 0;

        [Inspector(InspectorType.Enum, 2), SettingEnum(typeof (TimeScaleType), true), SettingLabelName("Scale类型")]
        public TimeScaleType scaleType = TimeScaleType.TimeScale0;

        [Inspector(InspectorType.Float, 3), SettingFloat(0, 100), SettingLabelName("时间Scale")]
        public float scale = 1;

        [Inspector(InspectorType.Int, 4), SettingFloat(0, 50), SettingLabelName("阶段组(Phase)")]
        public int phase = 0;

        public override string cellType { get { return FireFxType.ACT_TIMESCALE; } }

        public override string defaultLabel { get { return "Time Scale"; } }

        public override Texture2D iconStyle {
            get { return PResourceManager.GetTintedTexture("BehaviorIcon/action.png", Color.black); }
        }
        public override bool IsLeaf { get { return true; } }
    }
}