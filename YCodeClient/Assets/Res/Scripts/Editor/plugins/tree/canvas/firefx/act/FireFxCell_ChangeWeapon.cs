using edit.pure.animation;
using edit.pure.inspector;
using edit.pure.resource;
using edit.pure.treespace.core;
using mono.fbx;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;
using UnityEditor;
using UnityEngine;

namespace plugins.tree.canvas.firefx.act {
    [TreeCell(false, "Action/Change Weapon", new[] {typeof (FireFxCanvas)})]
    public class FireFxCell_ChangeWeapon : FireFxCellActBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 10), SettingLabelName("起始时间")]
        public float time = 0;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType), true), SettingLabelName("起始Scale影响")]
        public TimeScaleType timeScaleType = 0;

        [Inspector(InspectorType.Text, 2), SettingLabelName("骨骼名称")]
        public string bone = string.Empty;

        [Inspector(InspectorType.Prefab, 3), SettingPrefab]
        public string prefab = string.Empty;

        public override bool OnInspectorDraw(PInspectorCore insp, params object[] arg) {
            FireFxTreeDebuger d = arg.Length > 0 ? arg[0] as FireFxTreeDebuger : null;
            switch (insp.propertyName) {
                case "bone":
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
            string st = BoneGUI.BoneField(d.hero, insp.label, bone);
            if (EditorGUI.EndChangeCheck()) {
                insp.SetValueString(st);
            }
        }

        public override string cellType { get { return FireFxType.ACT_CHANGE_WEAPON; } }

        public override string defaultLabel { get { return "Change Weapon"; } }

        public override Texture2D iconStyle {
            get { return PResourceManager.GetTintedTexture("BehaviorIcon/action.png", Color.black); }
        }
    }
}