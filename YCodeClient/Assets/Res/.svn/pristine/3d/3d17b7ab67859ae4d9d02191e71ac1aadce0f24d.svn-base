using edit.pure.inspector;
using edit.pure.treespace.core;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.emitter;

namespace plugins.tree.canvas.firefx.emitter {
    [TreeCell(false, "Emitter/Line", new[] {typeof (FireFxCanvas)})]
    public class FireFxEmitCell_Line : FireFxEmitCellBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 30), SettingLabelName("间隔时长")]
        public float interval = 0f;

        [Inspector(InspectorType.Boolean, 1), SettingLabelName("启用随机延迟")]
        public bool useRandomDelay;

        [Inspector(InspectorType.Float, 2), SettingFloat(0, 10000), SettingLabelName("最大距离")]
        public float maxDistance = 10f;

        [Inspector(InspectorType.Float, 3), SettingFloat(0, 10000), SettingLabelName("最小距离")]
        public float minDistance = 0f;

        [Inspector(InspectorType.Enum, 4), SettingEnum(typeof (BulletPositionMode))]
        public BulletPositionMode startMode = BulletPositionMode.Host;

        [Inspector(InspectorType.Enum, 5), SettingEnum(typeof (BulletPositionMode))]
        public BulletPositionMode destinyMode = BulletPositionMode.Target;

        [Inspector(InspectorType.Enum, 6), SettingEnum(typeof (BulletLineMode))]
        public BulletLineMode lineMode = BulletLineMode.Number_By_Distance;

        [Inspector(InspectorType.Float, 7), SettingFloat(0, 10000)]
        public float space = 1f;

        public override string cellType { get { return FireFxType.EMITTER_LINE; } }

        public override string defaultLabel { get { return "Emitter Line"; } }
    }
}