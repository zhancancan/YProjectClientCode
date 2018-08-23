using edit.pure.diagram.core;
using edit.pure.inspector;

namespace plugins.diagram.canvas.mission.node {
    [GraphNode(false, "Node/Kill", new[] {typeof (MissionDiagram)})]
    internal class GraphNode_Kill : GraphNode_MissionNodeBase {
        [Inspector(InspectorType.Asset, 0), SettingAsset("place")]
        public string place = "";

        [Inspector(InspectorType.Text, 1)]
        public string npcId = "";

        [Inspector(InspectorType.Int, 2), SettingInt(1, 1000)]
        public int numKill = 1;

        public override string nodeType { get { return "missionNodeKill"; } }

        public override GraphNode Clone() {
            GraphNode_Kill c = new GraphNode_Kill {rect = rect};
            return c;
        }

        protected override string actionType { get { return "kill"; } }
    }
}