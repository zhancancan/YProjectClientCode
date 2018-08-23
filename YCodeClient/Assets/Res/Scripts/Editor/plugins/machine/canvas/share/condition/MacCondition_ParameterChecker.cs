using System.Collections.Generic;
using edit.pure.inspector;
using edit.pure.machine.core;
using edit.pure.tools.selection;
using pure.stateMachine.machine.core;
using pure.stateMachine.share;
using UnityEditor;
using UnityEngine;

namespace plugins.machine.canvas.share.condition {
    [MacCondition(false, "Share/Machine Parameter Checker")]
    public class MacCondition_ParameterChecker : Mac_Condition, ICustomEditListItem {
        [Inspector(InspectorType.Long, 0, true)]
        public long paramId;

        [Inspector(InspectorType.Float, 1, true)]
        public float threshold;

        [Inspector(InspectorType.Enum, 2, true), SettingEnum(typeof (CpxParameter.ConditionMode))]
        public CpxParameter.ConditionMode mode = CpxParameter.ConditionMode.Equals;

        public override string conditionType { get { return MachineType.CON_MACHINE_PARAMETER; } }

        public override string renderContent { get { return "Machine Parameter Checker"; } set { } }

        public void DrawItem(GUIStyle style) {
            MachineChart chart = MachineCenter.currentChart;
            if (chart == null) return;
            string[] pks;
            int index;
            Mac_Parameter curr;
            UpdateParameter(chart.parameters, paramId, out pks, out index, out curr);
            int ni = EditorGUILayout.Popup(index, pks, GUILayout.MaxWidth(150));
            if (ni != index) {
                index = ni;
                if (index != -1 && index < chart.parameters.Count) {
                    paramId = chart.parameters[index].uuid;
                    UpdateParameter(chart.parameters, paramId, out pks, out index, out curr);
                }
            }
            if (curr != null) {
                GUILayoutOption opt = GUILayout.Width(60);
                switch (curr.type) {
                    case CpxParameter.ParameterType.Bool:
                        bool b0 = threshold.Equals(1);
                        bool b = EditorGUILayout.Toggle(b0, opt);
                        if (b != b0) threshold = b ? 1 : 0;
                        break;
                    case CpxParameter.ParameterType.Float:
                        mode = (CpxParameter.ConditionMode) EditorGUILayout.EnumPopup(mode, opt);
                        threshold = EditorGUILayout.DelayedFloatField(threshold, opt);
                        break;
                    case CpxParameter.ParameterType.Int:
                        mode = (CpxParameter.ConditionMode) EditorGUILayout.EnumPopup(mode, opt);
                        threshold = (int) EditorGUILayout.DelayedFloatField(threshold, opt);
                        break;
                }
            }
            GUILayout.Space(5);
        }

        private static void UpdateParameter(List<Mac_Parameter> ps, long pid, out string[] pks, out int index,
            out Mac_Parameter curr) {
            pks = new string[ps.Count];
            index = -1;
            for (int i = 0, len = ps.Count; i < len; i++) {
                pks[i] = ps[i].name;
                if (ps[i].uuid == pid) {
                    index = i;
                }
            }
            curr = index != -1 && index < ps.Count ? ps[index] : null;
        }

        protected override void Draw() {
        }
    }
}