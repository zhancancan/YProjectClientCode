using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.entity.core;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;
using UnityEngine;

namespace plugins.tree.canvas.firefx.act {
    [TreeCell(false, "Action/Hit", new[] {typeof (FireFxCanvas)})]
    public class FireFxCell_Hit : FireFxCellActBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 10), SettingLabelName("起始时间")]
        public float time = 0f;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType), true), SettingLabelName("起始Scale影响")]
        public TimeScaleType timeScaleType = 0;

        [Inspector(InspectorType.Asset, 2), SettingAsset("prefab"), SettingLabelName("Prefab")]
        public string asset = string.Empty;

        [Inspector(InspectorType.Float, 3), SettingFloat(-180, 180), SettingLabelName("角度偏移")]
        public float angleOffset = 0f;

        [Inspector(InspectorType.Vector, 4), SettingLabelName("位置偏移")]
        public Vector3 offset = Vector3.zero;

        [Inspector(InspectorType.Enum, 5), SettingEnum(typeof (OrientMode)), SettingLabelName("旋转模式")]
        public OrientMode orient = OrientMode.None;

        public override string cellType { get { return FireFxType.ACT_HIT; } }

        public override string defaultLabel { get { return "Hit"; } }
    }
}