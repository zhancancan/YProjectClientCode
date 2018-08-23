using System.Collections.Generic;
using edit.pure.inspector;
using edit.pure.machine.core;
using edit.pure.tools.selection;
using pure.ai.aimachine;
using pure.stateMachine.machine.core;
using UnityEditor;
using UnityEngine;

namespace plugins.machine.canvas.ai.core.action {
    [MacActon(false, "Core/Parameter Updater", new[] {typeof (AIMachineChart)})]
    public class MacAction_ParameterSet : Mac_Action, ICustomEditListItem {
        [Inspector(InspectorType.Long, 0, true)]
        public long paramId;

        [Inspector(InspectorType.Float, 1, true)]
        public float targetValue;

        public override string actionType { get { return AIActionType.PARAM_SET; } }

        public override string renderContent { get { return "Parameter Updater"; } set { } }

        public void DrawItem(GUIStyle style) {
            MachineChart chart = MachineCenter.currentChart;
            if (chart == null) return;
            string[] pks;
            int index;
            Mac_Parameter curr;
            UpdateParameter(chart.parameters, paramId, out pks, out index, out curr);
            int ni = EditorGUILayout.Popup(index, pks, GUILayout.MaxWidth(120));
            if (ni != index) {
                index = ni;
                if (index != -1 && index < chart.parameters.Count) {
                    paramId = chart.parameters[index].uuid;
                    UpdateParameter(chart.parameters, paramId, out pks, out index, out curr);
                }
            }
            GUILayout.FlexibleSpace();
            if (curr != null) {
                GUILayoutOption opt = GUILayout.Width(40);
                switch (curr.type) {
                    case CpxParameter.ParameterType.Bool:
                        bool t = EditorGUILayout.Toggle(targetValue.Equals(1), opt);
                        targetValue = t ? 1 : 0;
                        break;
                    case CpxParameter.ParameterType.Float:
                        targetValue = EditorGUILayout.DelayedFloatField(targetValue, opt);
                        break;
                    case CpxParameter.ParameterType.Int:
                        targetValue = EditorGUILayout.DelayedIntField((int) targetValue, opt);
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
    }
}