using edit.pure.diagram.core;
using edit.pure.inspector;

namespace plugins.diagram.canvas.mission.node {
    [GraphNode(false, "Node/Submit", new[] {typeof (MissionDiagram)})]
    internal class GraphNode_Submit : GraphNode_MissionNodeBase {
        [Inspector(InspectorType.Asset, 0), SettingAsset("place")]
        public string place = "";

        [Inspector(InspectorType.Text, 1)]
        public string npcId = "";

        public override string nodeType { get { return "missionNodeSubmit"; } }

        public override GraphNode Clone() {
            GraphNode_Submit c = new GraphNode_Submit {rect = rect};
            return c;
        }

        protected override string actionType { get { return "submit"; } }
    }
}