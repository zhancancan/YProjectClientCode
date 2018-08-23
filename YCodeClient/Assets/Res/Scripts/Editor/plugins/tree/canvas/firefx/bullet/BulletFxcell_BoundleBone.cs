using edit.pure.animation;
using edit.pure.inspector;
using edit.pure.treespace.core;
using mono.fbx;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;
using UnityEditor;
using UnityEngine;

namespace plugins.tree.canvas.firefx.bullet {
    [TreeCell(false, "Bullet/Boundle Bone", new[] {typeof (FireFxCanvas)})]
    public class BulletFxcell_BoundleBone : BulletFxCellBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 30), SettingLabelName("起始时间")]
        public float time = 1;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType), true), SettingLabelName("起始Scale影响")]
        public TimeScaleType timeScaleType = 0;

        [Inspector(InspectorType.Float, 2), SettingFloat(0, 30), SettingLabelName("维持时长")]
        public float duration = 0;

        [Inspector(InspectorType.Enum, 3), SettingEnum(typeof (TimeScaleType), true), SettingLabelName("时长Scale影响")]
        public TimeScaleType durationScaleType = 0;

        [Inspector(InspectorType.Text, 4), SettingLabelName("骨骼名称")]
        public string boneName = string.Empty;

        [Inspector(InspectorType.Vector, 5)]
        public Vector3 position = Vector3.zero;

        [Inspector(InspectorType.Vector, 6)]
        public Vector3 rotate = Vector3.zero;

        [Inspector(InspectorType.Vector, 7)]
        public Vector3 scale = Vector3.one;

        public override string cellType { get { return FireFxType.BULLET_BONE; } }

        public override string defaultLabel { get { return "Bullet Boundle Bone"; } }

        public override bool OnInspectorDraw(PInspectorCore insp, params object[] arg) {
            FireFxTreeDebuger d = arg.Length > 0 ? arg[0] as FireFxTreeDebuger : null;
            if (insp.propertyName == "boneName") {
                DrawBone(d, insp as Insp_TextInput);
                return true;
            }
            return false;
        }

        private void DrawBone(FireFxTreeDebuger d, Insp_TextInput insp) {
            if (d == null || insp == null) {
                return;
            }
            EditorGUI.BeginChangeCheck();
            string st = BoneGUI.BoneField(d.hero, insp.label, boneName);
            if (EditorGUI.EndChangeCheck()) {
                insp.SetValueString(st);
            }
        }
    }
}