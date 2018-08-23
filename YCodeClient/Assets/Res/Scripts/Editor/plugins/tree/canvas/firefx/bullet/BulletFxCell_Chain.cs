using edit.pure.inspector;
using edit.pure.treespace.core;
using mono.fbx;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.emitter;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Bullet/Chain", new[] {typeof (FireFxCanvas)})]
    public class BulletFxcell_Chain : BulletFxCellBase {
        [Inspector(InspectorType.Prefab, 0), SettingPrefab(new[] {typeof (Fx_BulletPrefab)})]
        public string asset = string.Empty;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (BulletChainMode)), SettingLabelName("延展模式")]
        public BulletChainMode mode = BulletChainMode.Tile;

        [Inspector(InspectorType.Float, 2), SettingFloat(0.1f, 1000, 0.1f, 1)]
        public float size = 1;

        public override string cellType { get { return FireFxType.BULLET_CHAIN; } }

        public override string defaultLabel { get { return "Bullet Chain"; } }
    }
}