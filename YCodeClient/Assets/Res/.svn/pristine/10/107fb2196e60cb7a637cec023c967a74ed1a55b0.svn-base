using edit.pure.inspector;
using edit.pure.treespace.core;
using mono.fbx;
using pure.treeComp.firefx.core;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Bullet/Create", new[] {typeof (FireFxCanvas)})]
    public class BulletFxcell_Create : BulletFxCellBase {
        [Inspector(InspectorType.Prefab, 0), SettingPrefab(new[] {typeof (Fx_BulletPrefab)})]
        public string asset = string.Empty;

        public override string cellType { get { return FireFxType.BULLET_CREATE; } }

        public override string defaultLabel { get { return "Bullet Create"; } }
    }
}