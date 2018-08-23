using edit.pure.inspector;
using edit.pure.machine.core;
using edit.pure.tools.selection;
using pure.stateMachine.share;
using UnityEditor;
using UnityEngine;

namespace plugins.machine.canvas.share.condition {
    [MacCondition(false, "Share/Is Inited")]
    public class MacCon_CpxInited : Mac_Condition, ICustomEditListItem {
        [Inspector(InspectorType.Boolean, 0, true)]
        public bool compare;

        protected override void Draw() {
        }

        public override string renderContent { get { return "Is Cpx Inited"; } set { } }

        public void DrawItem(GUIStyle style) {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Is Cpx Inited", style);
            compare = EditorGUILayout.Toggle("", compare);
            GUILayout.EndHorizontal();
        }

        public override string conditionType { get { return MachineType.CON_INITED; } }
    }
}