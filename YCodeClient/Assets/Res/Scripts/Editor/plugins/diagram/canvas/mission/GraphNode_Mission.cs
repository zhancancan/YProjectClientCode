using edit.pure.diagram.core;
using edit.pure.inspector;
using UnityEngine;

namespace plugins.diagram.canvas.mission {
    [GraphNode(false, "Mission/Mission")]
    public class GraphNode_Mission : GraphNode {
        private static GUIStyle ST_MAIN_ON;
        private static GUIStyle ST_MAIN;
        private static GUIStyle ST_BRANCH_ON;
        private static GUIStyle ST_BRANCH;
        private static GUIStyle ST_DISABLED_ON;
        private static GUIStyle ST_DISABLED;

        private static void UpdateStyle() {
            if (ST_MAIN_ON == null) {
                ST_MAIN_ON = new GUIStyle("flow node 5 on") {
                    normal = {textColor = Color.black},
                    fontStyle = FontStyle.Bold
                };
                ST_MAIN = new GUIStyle("flow node 5") {normal = {textColor = Color.black}};
                ST_BRANCH_ON = new GUIStyle("flow node 1 on") {
                    normal = {textColor = Color.black},
                    fontStyle = FontStyle.Bold
                };
                ST_BRANCH = new GUIStyle("flow node 1") {normal = {textColor = Color.black}};
                ST_DISABLED_ON = new GUIStyle("flow node 0 on") {
                    normal = {textColor = Color.black},
                    fontStyle = FontStyle.Bold
                };
                ST_DISABLED = new GUIStyle("flow node 0") {normal = {textColor = Color.black}};
            }
        }

        [Inspector(InspectorType.Text, 0)]
        public string name = string.Empty;

        [Inspector(InspectorType.TextArea, 1000)]
        public string description = string.Empty;

        [Inspector(InspectorType.Boolean, 2)]
        public bool isMain = true;

        [Inspector(InspectorType.Boolean, 3)]
        public bool enabled = true;

        [Inspector(InspectorType.Text, 4)]
        public string rewardId = "none";

        public GraphNode_Mission() {
            BuildNodes();
            rect = new Rect(0, 0, 150, 40);
        }

        private void BuildNodes() {
            CreateInput("input chapter", GraphType.CHAPTER, GraphNodeSide.LEFT, 20).maxConnections = 1;
            CreateInput("Input Top_2", GraphType.MISSIOIN, GraphNodeSide.TOP, 75);
            CreateOutput("ouput", GraphType.MISSIOIN, GraphNodeSide.BOTTOM, 75);
            CreateOutput("ouput sub", GraphType.NODE, GraphNodeSide.RIGHT, 20);
        }

        public override void DrawNode() {
            UpdateStyle();
            Rect nodeRect = rect;
            nodeRect.position += DiagramCenter.currDiagram.zoomPanAdjust + DiagramCenter.currDiagram.panOffset;
            GUIStyle st;
            if (enabled) {
                if (isMain) {
                    st = selected ? ST_MAIN_ON : ST_MAIN;
                } else {
                    st = selected ? ST_BRANCH_ON : ST_BRANCH;
                }
            } else {
                st = selected ? ST_DISABLED_ON : ST_DISABLED;
            }
            string str = name;
            string shortStr = str.Length > 24 ? str.Substring(0, 24) : str;
            GUIContent c;
            if (DiagramCenter.settingData.showToolTips) {
                c = new GUIContent(shortStr, GetToopTips());
            } else {
                c = str.Length > 24 ? new GUIContent(shortStr, str) : new GUIContent(shortStr);
            }
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

        public override string nodeType { get { return "mission"; } }

        public override string filterLabel {
            get { return "mission:" + name + "( " + (isMain ? "main" : "branch") + " )"; }
        }

        public override string filterContent { get { return "mission," + name + (isMain ? "main" : "branch"); } }

        protected override void DrawBody() {
        }

        public override GraphNode Clone() {
            return new GraphNode_Mission {rect = rect};
        }
    }
}