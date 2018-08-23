using edit.pure.inspector;
using edit.pure.resource;
using pure.utils.timeScale;
using UnityEngine;

namespace plugins.tree.canvas.firefx.emitter {
    public abstract class FireFxEmitCellBase : FireFxCellBase {
        [Inspector(InspectorType.Float, -7), SettingFloat(0, 30), SettingLabelName("起始时间")]
        public float time = 0;

        [Inspector(InspectorType.Enum, -6), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("起始Scale影响")]
        public TimeScaleType timeScaleType = 0;

        [Inspector(InspectorType.Float, -5), SettingFloat(0, 30), SettingLabelName("维持时长")]
        public float duration;

        [Inspector(InspectorType.Enum, -4), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("时长Scale影响")]
        public TimeScaleType durationScaleType;

        [Inspector(InspectorType.Float, -3), SettingFloat(0, 30), SettingLabelName("延迟时长")]
        public float delay;

        [Inspector(InspectorType.Enum, -2), SettingEnum(typeof (TimeScaleType),true), SettingLabelName("延迟Scale影响")]
        public TimeScaleType delayScaleType;

        [Inspector(InspectorType.Int, -1), SettingInt(1, 50), SettingLabelName("发射数量")]
        public int numParticle = 1;

        public override Texture2D iconStyle {
            get {
                float g = 0.2f;
                return PResourceManager.GetTintedTexture("Icons/Icon_Dimension.png", new Color(g, g, g, 1f), 0.5f);
            }
        }
    }
}