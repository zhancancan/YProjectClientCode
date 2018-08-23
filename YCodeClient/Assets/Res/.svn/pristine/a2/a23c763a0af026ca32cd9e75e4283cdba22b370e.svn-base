using mono.voiceChat;
using pure.voiceChat.core;
using UnityEditor;

namespace inspectors.voiceChat {
    [CustomEditor(typeof (VoiceChatSetting))]
    public class Insp_VoiceSetting : Editor {
        public override void OnInspectorGUI() {  

            VoiceChatSetting s = (VoiceChatSetting) target;
            VoiceChatPreset t= (VoiceChatPreset) EditorGUILayout.EnumPopup("Preset", s.preset);
            if (t != s.preset) {
                s.preset = t; 
            }
            base.OnInspectorGUI();
        }
    }
}