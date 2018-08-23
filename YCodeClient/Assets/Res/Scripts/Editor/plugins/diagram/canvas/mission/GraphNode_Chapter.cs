using edit.pure.diagram.core;
using edit.pure.inspector;
using UnityEngine;

namespace plugins.diagram.canvas.mission {
    [GraphNode(false, "Mission/Chapter")]
    public class GraphNode_Chapter : GraphNode {
        private static GUIStyle ST_CHAPTER_ON;
        private static GUIStyle ST_CHAPTER;
        private static GUIStyle ST_DISABLED_ON;
        private static GUIStyle ST_DISABLED;

        private static void UpdateStyle() {
            if (ST_CHAPTER_ON == null) {
                ST_CHAPTER_ON = new GUIStyle("flow node hex 4 on") {
                    normal = {textColor = Color.black},
                    fontStyle = FontStyle.Bold
                };
                ST_CHAPTER = new GUIStyle("flow node hex 4") {normal = {textColor = Color.black}};
                ST_DISABLED_ON = new GUIStyle("flow node 0 on") {
                    normal = {textColor = Color.black},
                    fontStyle = FontStyle.Bold
                };
                ST_DISABLED = new GUIStyle("flow node 0") {normal = {textColor = Color.black}};
            }
        }

        [Inspector(InspectorType.Text, 0)]
        public string name = "chapter";

        [Inspector(InspectorType.TextArea, 1000)]
        public string description = "chapter";

        [Inspector(InspectorType.Boolean, 2)]
        public bool enabled = true;

        [Inspector(InspectorType.Texture, 3), SettingIcon("Assets/Res/Arts/Icon")]
        public string icon = "";

        public GraphNode_Chapter() {
            BuildNodes();
            rect = new Rect(0, 0, 150, 40);
        }

        private void BuildNodes() {
            CreateOutput("input chapter", GraphType.CHAPTER, GraphNodeSide.RIGHT, 23);
        }

        public override void DrawNode() {
            UpdateStyle();
            Rect nodeRect = rect;
            nodeRect.position += DiagramCenter.currDiagram.zoomPanAdjust + DiagramCenter.currDiagram.panOffset;
            GUIStyle st;
            if (enabled) {
                st = selected ? ST_CHAPTER_ON : ST_CHAPTER;
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

        public override string nodeType { get { return "chapter"; } }

        public override string filterLabel { get { return "chapter:" + name; } }

        public override string filterContent { get { return "chapter," + name; } }

        protected override void DrawBody() {
        }

        public override GraphNode Clone() {
            return new GraphNode_Chapter {rect = rect};
        }
    }
}