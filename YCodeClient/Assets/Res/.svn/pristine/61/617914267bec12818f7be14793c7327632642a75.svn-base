using mono.voiceChat;
using pure.voiceChat.mono;
using UnityEditor;
using UnityEngine;

namespace inspectors.voiceChat {
    [CustomEditor(typeof (VoiceTest))]
    internal class Insp_VoiceTest : Editor {
        public override void OnInspectorGUI() {
            if (Application.isPlaying) {
                ChatRecorder_Dll r = ChatRecorder_Dll.GetInstance();
                if (!r.recording && GUILayout.Button("Record")) {
                    r.StartRecoding();
                }
                if (r.recording && GUILayout.Button("Stop Record")) {
                    r.StopRecording();
                }
            }
        }
    }
}