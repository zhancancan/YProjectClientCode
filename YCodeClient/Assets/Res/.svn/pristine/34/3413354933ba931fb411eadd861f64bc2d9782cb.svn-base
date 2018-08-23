using edit.pure.inspector;
using edit.pure.resource;
using edit.pure.treespace;
using edit.pure.treespace.core;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.emitter;
using UnityEngine;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Position Calc/Sector", new[] {typeof (FireFxCanvas)})]
    public class BulletPosCell_Sector : BulletFxCellBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 360), SettingLabelName("开始角度")]
        public float startAngle = 0;

        [Inspector(InspectorType.Float, 1), SettingFloat(180, 180), SettingLabelName("扇形角度")]
        public float angle = 0;


        [Inspector(InspectorType.Enum, 2), SettingEnum(typeof (BulletAngleMode)), SettingLabelName("角度模式")]
        public BulletAngleMode angleMode = BulletAngleMode.Random;


        public override string cellType { get { return FireFxType.POS_SECTOR; } }

        public override string defaultLabel { get { return "Calc Sector"; } }

        public override Texture2D iconStyle { get { return PGUIStyle.Icon_LightBlue; } }

        public override bool CanInsertTo(TreeCell obj) {
            return obj != null && obj.IsLeaf == false && IsAncestorOf(obj) == false;
        }
    }
}