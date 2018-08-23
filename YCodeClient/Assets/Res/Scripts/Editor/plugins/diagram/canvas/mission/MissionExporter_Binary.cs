using edit.pure.diagram.serializer;

namespace plugins.diagram.canvas.mission {
    internal class MissionExporter_Binary : IDiagramExporter {
        public void Export() {
        }

        public string rootPath { get; set; }
        public string output { get; set; }
        public string diagramType { get; set; }
    }
}