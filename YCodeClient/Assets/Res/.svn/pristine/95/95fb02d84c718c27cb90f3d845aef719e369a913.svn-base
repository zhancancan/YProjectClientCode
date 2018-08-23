using edit.pure.diagram.core;
using edit.pure.inspector;

namespace plugins.diagram.canvas.mission.node {
    [GraphNode(false, "Node/Take", new[] {typeof (MissionDiagram)})]
    internal class GraphNode_Take : GraphNode_MissionNodeBase {
        [Inspector(InspectorType.Asset, 0), SettingAsset("place")]
        public string place = "";

        [Inspector(InspectorType.Text, 1)]
        public string npcId = "";

        public override string nodeType { get { return "missionNodeKillTake"; } }

        public override GraphNode Clone() {
            GraphNode_Take c = new GraphNode_Take {rect = rect};
            return c;
        }

        protected override string actionType { get { return "take"; } }
    }
}