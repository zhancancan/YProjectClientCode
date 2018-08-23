using edit.pure.diagram.core;
using edit.pure.inspector;
using UnityEngine;

namespace plugins.diagram.canvas.mission.node {
    public abstract class GraphNode_MissionNodeBase : GraphNode {
        private static GUIStyle ST_MAIN_ON;
        private static GUIStyle ST_MAIN;
        private static GUIStyle ST_DISABLED_ON;
        private static GUIStyle ST_DISABLED;


        [Inspector(InspectorType.Text, -10)]
        public string name = "mission node";

        [Inspector(InspectorType.TextArea, 1000), SettingText(100)]
        public string description = "";

        [Inspector(InspectorType.Boolean, -8)]
        public bool enabled = true;

        [Inspector(InspectorType.Int, -7), SettingInt(0, 100)]
        public int nodeIndex = 0;


        private static void UpdateStyle() {
            if (ST_MAIN_ON == null) {
                ST_MAIN_ON = new GUIStyle("flow node 3 on") {
                    normal = {textColor = Color.black},
                    fontStyle = FontStyle.Bold,
                    wordWrap = true
                };
                ST_MAIN = new GUIStyle("flow node 3") {
                    normal = {textColor = Color.black},
                    wordWrap = true
                };

                ST_DISABLED_ON = new GUIStyle("flow node 0 on") {
                    normal = {textColor = Color.black},
                    fontStyle = FontStyle.Bold,
                    wordWrap = true
                };
                ST_DISABLED = new GUIStyle("flow node 0") {
                    normal = {textColor = Color.black},
                    wordWrap = true
                };
            }
        }


        protected GraphNode_MissionNodeBase() {
            InitNode();
        }

        protected void InitNode() {
            rect = new Rect(0, 0, 100, 40);
            CreateInput("Input", GraphType.NODE, GraphNodeSide.LEFT, 20).maxConnections = 1;
        }

        public override void DrawNode() {
            UpdateStyle();
            Rect nodeRect = rect;
            nodeRect.position += DiagramCenter.currDiagram.zoomPanAdjust + DiagramCenter.currDiagram.panOffset;

            GUIStyle st = null;
            if (enabled) {
                st = selected ? ST_MAIN_ON : ST_MAIN;
            } else {
                st = selected ? ST_DISABLED_ON : ST_DISABLED;
            }
            string str = nodeIndex + ": [" + actionType + "] " + name;
            string shortStr = str.Length > 24 ? str.Substring(0, 24) : str;
            GUIContent c = DiagramCenter.settingData.showToolTips
                ? new GUIContent(shortStr, GetToopTips())
                : str.Length > 24
                    ? new GUIContent(shortStr, str)
                    : new GUIContent(str);

            GUI.Label(nodeRect, c, st);
        }

        protected virtual string GetToopTips() {
            PInspectorCore[] ps = GetProperties();
            string t = string.Empty;
            for (int i = 0; i < ps.Length; i++) {
                t += ps[i].propertyName + " : " + ps[i].GetValueString();
                if (i != ps.Length - 1) {
                    t += "\n";
                }
            }
            return t;
        }


        public override string filterContent {
            get { return actionType + "," + name + "," + "node"; }
        }

        public override string filterLabel {
            get { return actionType + ": " + name + "(" + nodeIndex + ")"; }
        }

        protected abstract string actionType { get; }


        protected override void DrawBody() {
        }
    }
}