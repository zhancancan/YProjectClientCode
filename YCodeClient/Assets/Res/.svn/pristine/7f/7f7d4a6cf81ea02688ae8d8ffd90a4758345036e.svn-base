using System.Globalization;
using edit.pure.animation;
using edit.pure.inspector;
using edit.pure.resource;
using edit.pure.treespace.core;
using mono.fbx;
using pure.entity.animation;
using pure.treeComp.firefx.core;
using pure.utils.timeScale;
using UnityEditor;
using UnityEngine;

namespace plugins.tree.canvas.firefx.act {
    [TreeCell(false, "Action/Animation", new[] {typeof (FireFxCanvas)})]
    public class FireFxCell_Animation : FireFxCellActBase {
        [Inspector(InspectorType.Float, 0), SettingFloat(0, 10), SettingLabelName("起始时间")]
        public float time = 0;

        [Inspector(InspectorType.Enum, 1), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("起始Scale影响")]
        public TimeScaleType timeScaleType = 0;

        [Inspector(InspectorType.Float, 2), SettingFloat(0, 100), SettingLabelName("维持时长")]
        public float duration = 1f;

        [Inspector(InspectorType.Enum, 3), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("时长Scale影响")]
        public TimeScaleType durationScaleType = 0;


        [Inspector(InspectorType.Enum, 4), SettingEnum(typeof (AnimatorActionType)), SettingLabelName("动画行为")]
        public AnimatorActionType actionType = AnimatorActionType.Play;

        [Inspector(InspectorType.Text, 5), SettingLabelName("动作")]
        public string anim = string.Empty;

        [Inspector(InspectorType.Text, 6), SettingLabelName("参数名称")]
        public string paramName = string.Empty;

        [Inspector(InspectorType.Enum, 7), SettingEnum(typeof (AnimatorControllerParameterType)),
         SettingLabelName("参数类型")]
        public AnimatorControllerParameterType paramType = AnimatorControllerParameterType.Bool;

        [Inspector(InspectorType.Float, 8), SettingLabelName("参数值")]
        public float paramValue;

        [Inspector(InspectorType.Enum, 9), SettingEnum(typeof (AnimatorTarget)), SettingLabelName("作用对象")]
        public AnimatorTarget animTarget;

        [Inspector(InspectorType.Text, 10), SettingLabelName("骨骼名称")]
        public string bone = string.Empty;

        [Inspector(InspectorType.Boolean, 11), SettingLabelName("是否锁定Block")]
        public bool autoBlock = true;

        [Inspector(InspectorType.Enum, 12), SettingEnum(typeof(TimeScaleType), true), SettingLabelName("动画Scale影响")]
        public TimeScaleType animatorScaleType = 0;

        public override bool OnInspectorDraw(PInspectorCore insp, params object[] arg) {
            FireFxTreeDebuger d = arg.Length > 0 ? arg[0] as FireFxTreeDebuger : null;
            switch (insp.propertyName) {
                case "anim":
                    DrawAnim(d, insp as Insp_TextInput);
                    return true;
                case "paramName":
                    DrawParameter(d, insp as Insp_TextInput);
                    return true;
                case "paramValue":
                    return DrawParamValue(insp as Insp_Float);
                case "paramType":
                    return animTarget == AnimatorTarget.Host;
                case "bone":
                    DrawBone(d, insp as Insp_TextInput);
                    return true;
                case "autoBlock":
                    return actionType != AnimatorActionType.Play;
            }
            return false;
        }

        private void DrawAnim(FireFxTreeDebuger d, Insp_TextInput insp) {
            if (d == null || insp == null || actionType != AnimatorActionType.Play) {
                return;
            }
            if (animTarget != AnimatorTarget.Host) {
                anim = EditorGUILayout.TextField(insp.label, anim);
                return;
            }
            EditorGUI.BeginChangeCheck();
            string st = AnimatorGUI.StateField(d.hero, insp.label, anim);
            if (EditorGUI.EndChangeCheck()) {
                insp.SetValueString(st);
            }
        }

        private void DrawParameter(FireFxTreeDebuger d, Insp_TextInput insp) {
            if (d == null || insp == null || actionType == AnimatorActionType.Play) {
                return;
            }
            if (animTarget != AnimatorTarget.Host) {
                paramName = EditorGUILayout.TextField(insp.label, paramName);
                return;
            }
            EditorGUI.BeginChangeCheck();
            string st = AnimatorGUI.ParameterField(d.hero, insp.label, paramName);
            if (EditorGUI.EndChangeCheck()) {
                insp.SetValueString(st);
                AnimatorControllerParameter p = EditAnimatorUtils.GetAnimParameter(d.hero, st);
                if (p != null) {
                    paramType = p.type;
                }
            }
        }

        private bool DrawParamValue(Insp_Float insp) {
            if (insp == null) return true;
            if (actionType == AnimatorActionType.Play || paramType == AnimatorControllerParameterType.Trigger)
                return true;
            switch (paramType) {
                case AnimatorControllerParameterType.Float:
                    return false;
                case AnimatorControllerParameterType.Bool: {
                    GUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    GUILayout.Label(insp.label, GUILayout.Width(PInspectorCore.labelWidth));
                    bool y = GUILayout.Toggle(paramValue.Equals(1), insp.label);
                    if (EditorGUI.EndChangeCheck()) {
                        insp.SetFloatValue(y ? 1 : 0);
                    }
                    GUILayout.EndHorizontal();
                }
                    break;
                case AnimatorControllerParameterType.Int: {
                    GUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    GUILayout.Label(insp.label, GUILayout.Width(PInspectorCore.labelWidth));
                    int y = EditorGUILayout.DelayedIntField(insp.label, (int) paramValue);
                    if (EditorGUI.EndChangeCheck()) {
                        insp.SetFloatValue(y);
                    }
                    GUILayout.EndHorizontal();
                }
                    break;
            }
            return true;
        }

        private void DrawBone(FireFxTreeDebuger d, Insp_TextInput insp) {
            if (d == null || insp == null || animTarget == AnimatorTarget.Host) {
                return;
            }
            EditorGUI.BeginChangeCheck();
            string st = BoneGUI.BoneField(d.hero, insp.label, bone);
            if (EditorGUI.EndChangeCheck()) {
                insp.SetValueString(st);
            }
        }

        public override string cellType { get { return FireFxType.ACT_ANIMATION; } }

        public override string defaultLabel {
            get {
                string bonePrefix = string.Empty;
                if (animTarget == AnimatorTarget.Bone) {
                    bonePrefix = string.Format("[Bone ={0}]", bone);
                }
                if (actionType == AnimatorActionType.Play) {
                    return bonePrefix + " Animator->Play:" + anim;
                }
                string pv = string.Empty;
                switch (paramType) {
                    case AnimatorControllerParameterType.Bool:
                        pv = " = " + (paramValue.Equals(1) ? "True" : "False");
                        break;
                    case AnimatorControllerParameterType.Float:
                        pv = " = " + paramValue.ToString(CultureInfo.InvariantCulture);
                        break;
                    case AnimatorControllerParameterType.Int:
                        pv = " = " + ((int) paramValue);
                        break;
                    case AnimatorControllerParameterType.Trigger:
                        pv = "(Trigger)";
                        break;
                }
                return string.Format("{0} Animator.Set: {1} {2}", bonePrefix, paramName, pv);
            }
        }

        public override Texture2D iconStyle {
            get { return PResourceManager.GetTintedTexture("BehaviorIcon/action.png", Color.black); }
        }
    }
}